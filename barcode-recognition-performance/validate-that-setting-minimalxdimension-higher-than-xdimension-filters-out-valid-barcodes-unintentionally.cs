// Title: Effect of MinimalXDimension on Code128 Barcode Recognition
// Description: Demonstrates how setting MinimalXDimension higher than the barcode's XDimension can cause valid Code128 barcodes to be missed during recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to create barcodes with a specific XDimension using BarcodeGenerator, and how to read them with BarCodeReader while configuring QualitySettings (XDimensionMode and MinimalXDimension). Developers often need to fine‑tune these settings to balance scan reliability and tolerance for printing variations, especially when working with high‑density symbologies like Code128.
/// Prompt: Validate that setting MinimalXDimension higher than XDimension filters out valid barcodes unintentionally.
// Tags: code128, minimalxdimension, barcode-generation, barcode-recognition, qualitysettings, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates the impact of MinimalXDimension settings on barcode recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a set of Code128 barcodes with a small XDimension, then reads them using different
    /// XDimensionMode and MinimalXDimension configurations to show how recognition results vary.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Generate sample Code128 barcodes with a small XDimension (module size)
        List<string> barcodeFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(tempDir, $"code{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"Test{i}"))
            {
                generator.Parameters.Barcode.XDimension.Point = 2f; // small module size
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Read with normal XDimension mode (baseline)
        int countNormal = ReadBarcodes(barcodeFiles, XDimensionMode.Normal, null);
        Console.WriteLine($"Baseline (Normal mode) barcodes read: {countNormal}");

        // Read with UseMinimalXDimension and a low MinimalXDimension (should read all)
        int countLowMinimal = ReadBarcodes(barcodeFiles, XDimensionMode.UseMinimalXDimension, 1f);
        Console.WriteLine($"UseMinimalXDimension with MinimalXDimension=1 read: {countLowMinimal}");

        // Read with UseMinimalXDimension and a high MinimalXDimension (may miss some)
        int countHighMinimal = ReadBarcodes(barcodeFiles, XDimensionMode.UseMinimalXDimension, 5f);
        Console.WriteLine($"UseMinimalXDimension with MinimalXDimension=5 read: {countHighMinimal}");

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors (e.g., files in use)
        }
    }

    /// <summary>
    /// Reads barcodes from the specified files using the given XDimensionMode and optional MinimalXDimension.
    /// </summary>
    /// <param name="files">Collection of image file paths containing barcodes.</param>
    /// <param name="mode">The XDimensionMode to apply during recognition.</param>
    /// <param name="minimalXDimension">Optional minimal XDimension value; null to leave unset.</param>
    /// <returns>Total number of barcodes successfully read.</returns>
    static int ReadBarcodes(IEnumerable<string> files, XDimensionMode mode, float? minimalXDimension)
    {
        int total = 0;
        foreach (string file in files)
        {
            if (!File.Exists(file))
                continue;

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Apply the selected XDimension mode
                reader.QualitySettings.XDimension = mode;

                // Optionally set a minimal XDimension threshold
                if (minimalXDimension.HasValue)
                {
                    reader.QualitySettings.MinimalXDimension = minimalXDimension.Value;
                }

                // Perform barcode reading and accumulate results
                BarCodeResult[] results = reader.ReadBarCodes();
                total += results.Length;
            }
        }
        return total;
    }
}