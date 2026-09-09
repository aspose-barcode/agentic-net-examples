// Title: Code128 Barcode Generation and Recognition with XDimension = 2 Pixels at Multiple Resolutions
// Description: Demonstrates generating Code128 barcodes with an XDimension of 2 pixels, saving them at various DPI settings, and then recognizing them using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and related parameter classes to create and read barcodes. Developers often need to adjust XDimension and image resolution for optimal scanning across devices, making this pattern useful for testing and quality assurance.
// Prompt: Test recognition of Code128 barcodes with XDimension set to 2 pixels across multiple image resolutions.
// Tags: code128, barcode generation, barcode recognition, xdimension, resolution, aspose.barcode, png, c#

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating Code128 barcodes with a specific XDimension and recognizing them across multiple image resolutions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates temporary barcodes, saves them, reads them back, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code128XDimTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define test parameters
        string codeText = "Test123";
        float[] resolutions = new float[] { 72f, 150f, 300f };
        List<string> generatedFiles = new List<string>();

        try
        {
            // Generate barcodes at different resolutions with XDimension = 2 pixels
            foreach (float dpi in resolutions)
            {
                string filePath = Path.Combine(tempFolder, $"code128_{dpi}.png");
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Set XDimension to 2 pixels (point unit)
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    // Set image resolution (DPI)
                    generator.Parameters.Resolution = dpi;
                    // Save barcode image as PNG
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                // Verify file creation and record the path
                if (File.Exists(filePath))
                {
                    generatedFiles.Add(filePath);
                    Console.WriteLine($"Generated barcode at {dpi} DPI: {filePath}");
                }
                else
                {
                    Console.WriteLine($"Failed to generate barcode at {dpi} DPI.");
                }
            }

            // Recognize each generated barcode
            foreach (string file in generatedFiles)
            {
                using (var reader = new BarCodeReader(file, DecodeType.Code128))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    Console.WriteLine($"Reading '{Path.GetFileName(file)}': {results.Length} barcode(s) detected.");
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }
        finally
        {
            // Clean up temporary files and folder
            foreach (string file in generatedFiles)
            {
                try { File.Delete(file); } catch { }
            }
            try { Directory.Delete(tempFolder, true); } catch { }
        }
    }
}