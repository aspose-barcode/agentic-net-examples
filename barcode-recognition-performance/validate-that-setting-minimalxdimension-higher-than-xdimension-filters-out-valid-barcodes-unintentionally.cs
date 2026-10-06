// Title: Validate MinimalXDimension Filtering on Code128 Barcode
// Description: Demonstrates how setting MinimalXDimension higher than the generated XDimension can unintentionally filter out valid Code128 barcodes.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create a barcode, and BarCodeReader with QualitySettings (XDimensionMode, MinimalXDimension) to control barcode detection. Developers often need to fine‑tune XDimension parameters when scanning barcodes in varied printing or imaging conditions; this snippet illustrates typical API usage and common pitfalls.
// Prompt: Validate that setting MinimalXDimension higher than XDimension filters out valid barcodes unintentionally.
// Tags: barcode symbology, generation, recognition, minimalxdimension, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, then reads it using different MinimalXDimension settings to illustrate filtering behavior.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it with various MinimalXDimension values, and outputs the read counts.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the output file path and the text to encode
        string barcodePath = Path.Combine(tempFolder, "code128.png");
        string codeText = "Aspose123";

        // Generate a Code128 barcode with XDimension = 2 points
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Local function to read a barcode with an optional MinimalXDimension constraint
        int ReadBarcode(string path, float? minimalXDimension = null)
        {
            using (var reader = new BarCodeReader(path, DecodeType.Code128))
            {
                // Configure the reader to respect MinimalXDimension when scanning
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

                if (minimalXDimension.HasValue)
                {
                    reader.QualitySettings.MinimalXDimension = minimalXDimension.Value;
                }

                BarCodeResult[] results = reader.ReadBarCodes();
                return results?.Length ?? 0;
            }
        }

        // Read with default MinimalXDimension (should succeed)
        int countDefault = ReadBarcode(barcodePath);
        Console.WriteLine($"Default MinimalXDimension read count: {countDefault}");

        // Read with MinimalXDimension lower than generator's XDimension (should succeed)
        int countLow = ReadBarcode(barcodePath, minimalXDimension: 1f);
        Console.WriteLine($"MinimalXDimension = 1 (lower) read count: {countLow}");

        // Read with MinimalXDimension higher than generator's XDimension (expected to filter out)
        int countHigh = ReadBarcode(barcodePath, minimalXDimension: 5f);
        Console.WriteLine($"MinimalXDimension = 5 (higher) read count: {countHigh}");

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect validation
        }
    }
}