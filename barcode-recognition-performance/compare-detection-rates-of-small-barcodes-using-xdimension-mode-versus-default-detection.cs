// Title: Compare XDimension detection modes for small barcodes
// Description: Demonstrates generating small Code128 barcodes and measuring recognition counts using default, Small, and UseMinimalXDimension detection modes.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to configure XDimension detection settings via the QualitySettings.XDimension property. It shows typical use cases such as optimizing recognition of compact barcodes by switching between Normal, Small, and minimal XDimension modes. Developers working with barcode scanning and image preprocessing can use these patterns to improve detection rates for small-sized symbols.
// Prompt: Compare detection rates of small barcodes using XDimension mode versus default detection.
// Tags: barcode, code128, xdimension, detection, recognition, aspose.barcode, generation, qualitysettings

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Program that generates small Code128 barcodes and compares recognition counts using different XDimension detection modes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, reads them with various XDimension settings, outputs counts, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "XDimCompare_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a set of small barcodes and collect their file paths
        List<string> barcodeFiles = GenerateBarcodes(tempFolder);

        // Read barcodes using the default (Normal) XDimension mode
        int normalCount = ReadBarcodes(barcodeFiles, XDimensionMode.Normal);

        // Read barcodes using the Small XDimension mode
        int smallCount = ReadBarcodes(barcodeFiles, XDimensionMode.Small);

        // Read barcodes using the UseMinimalXDimension mode with a custom minimal value
        int minimalCount = ReadBarcodesWithMinimal(barcodeFiles, 1f);

        // Output the recognition results for each mode
        Console.WriteLine($"Default (Normal) detection: {normalCount} barcodes recognized.");
        Console.WriteLine($"Small XDimension detection: {smallCount} barcodes recognized.");
        Console.WriteLine($"UseMinimalXDimension detection: {minimalCount} barcodes recognized.");

        // Cleanup generated files and temporary folder
        foreach (string file in barcodeFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder, true); } catch { }
    }

    /// <summary>
    /// Generates small Code128 barcode images in the specified folder.
    /// </summary>
    /// <param name="folder">The folder where barcode images will be saved.</param>
    /// <returns>A list of file paths for the generated barcode images.</returns>
    static List<string> GenerateBarcodes(string folder)
    {
        var files = new List<string>();
        string[] codes = { "A1", "B2", "C3", "D4", "E5" };

        foreach (string text in codes)
        {
            string filePath = Path.Combine(folder, $"code_{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Set a small XDimension to keep the barcode compact
                generator.Parameters.Barcode.XDimension.Point = 1f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            files.Add(filePath);
        }

        return files;
    }

    /// <summary>
    /// Reads barcodes from the provided files using the specified XDimension detection mode.
    /// </summary>
    /// <param name="files">List of barcode image file paths.</param>
    /// <param name="mode">The XDimension detection mode to apply.</param>
    /// <returns>The total number of barcodes successfully recognized.</returns>
    static int ReadBarcodes(List<string> files, XDimensionMode mode)
    {
        int total = 0;

        foreach (string file in files)
        {
            if (!File.Exists(file))
                continue;

            BaseDecodeType decode = DecodeType.Code128;
            using (var reader = new BarCodeReader(file, decode))
            {
                // Apply the chosen XDimension mode for recognition
                reader.QualitySettings.XDimension = mode;
                BarCodeResult[] results = reader.ReadBarCodes();
                total += results.Length;
            }
        }

        return total;
    }

    /// <summary>
    /// Reads barcodes using the UseMinimalXDimension mode with a custom minimal XDimension value.
    /// </summary>
    /// <param name="files">List of barcode image file paths.</param>
    /// <param name="minimalXDimension">The minimal XDimension value to enforce.</param>
    /// <returns>The total number of barcodes successfully recognized.</returns>
    static int ReadBarcodesWithMinimal(List<string> files, float minimalXDimension)
    {
        int total = 0;

        foreach (string file in files)
        {
            if (!File.Exists(file))
                continue;

            BaseDecodeType decode = DecodeType.Code128;
            using (var reader = new BarCodeReader(file, decode))
            {
                // Enable minimal XDimension mode and set the custom value
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = minimalXDimension;
                BarCodeResult[] results = reader.ReadBarCodes();
                total += results.Length;
            }
        }

        return total;
    }
}