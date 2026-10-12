// Title: Demonstrates setting XDimension and using MinimalXDimension during barcode generation and recognition
// Description: Shows how to generate Code128 barcodes with specific XDimension values, save them as PNG images, and then recognize them using the MinimalXDimension setting, logging the dimensions used.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It illustrates the use of BarcodeGenerator for setting XDimension, and BarCodeReader with QualitySettings to control MinimalXDimension during decoding. Typical use cases include fine‑tuning barcode size for printing and ensuring reliable scanning by configuring minimal module width. Developers often need to log these parameters to verify consistency between generation and reading.
// Prompt: Implement a feature that logs the exact XDimension and MinimalXDimension values used for each image.
// Tags: barcode, code128, xdimension, minimalxdimension, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with custom XDimension and recognition using MinimalXDimension,
/// logging the exact dimension values for each processed image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, reads them back, and logs dimension settings.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXDimDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare sample data: list of XDimension values (in pixels) and corresponding code texts
        var samples = new List<(float xDim, string codeText)>
        {
            (2f, "Sample01"),
            (3f, "Sample02"),
            (4f, "Sample03")
        };

        // Store generated file paths together with the XDimension used for each
        var generatedFiles = new List<(string filePath, float xDim)>();

        try
        {
            // -------------------- Barcode Generation --------------------
            foreach (var (xDim, codeText) in samples)
            {
                // Build the output file path
                string filePath = Path.Combine(tempFolder, $"barcode_{codeText}.png");

                // Create a generator for Code128 with the specified text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Set the XDimension (module width) in pixels
                    generator.Parameters.Barcode.XDimension.Pixels = xDim;

                    // Save the barcode image as PNG
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                // Record the generated file and its XDimension
                generatedFiles.Add((filePath, xDim));

                // Log generation details
                Console.WriteLine($"Generated '{filePath}' with XDimension = {xDim} pixels");
            }

            // -------------------- Barcode Recognition --------------------
            foreach (var (filePath, genXDim) in generatedFiles)
            {
                // Verify that the file exists before attempting to read it
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // Define the expected barcode type for decoding
                BaseDecodeType decodeType = DecodeType.Code128;

                // Initialize the reader for the generated image
                using (var reader = new BarCodeReader(filePath, decodeType))
                {
                    // Configure the reader to use minimal XDimension mode
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

                    // Example minimal XDimension value (in pixels) to be used during recognition
                    float minimalXDim = 1f;
                    reader.QualitySettings.MinimalXDimension = minimalXDim;

                    // Perform the barcode reading operation
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Log recognition details, including both generator and reader dimension settings
                    Console.WriteLine($"Reading '{Path.GetFileName(filePath)}':");
                    Console.WriteLine($"  Generator XDimension   = {genXDim} pixels");
                    Console.WriteLine($"  Reader MinimalXDimension = {minimalXDim} pixels");
                    Console.WriteLine($"  Barcodes found: {results.Length}");
                    foreach (var result in results)
                    {
                        Console.WriteLine($"    Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }
        finally
        {
            // -------------------- Cleanup --------------------
            try
            {
                // Delete each generated file
                foreach (var (filePath, _) in generatedFiles)
                {
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                }

                // Remove the temporary folder
                if (Directory.Exists(tempFolder))
                    Directory.Delete(tempFolder, true);
            }
            catch
            {
                // Suppress any exceptions that occur during cleanup
            }
        }
    }
}