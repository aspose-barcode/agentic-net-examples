// Title: Code128 Barcode Generation with XDimension=2 Pixels and Multi-Resolution Recognition
// Description: Demonstrates generating Code128 barcodes with an XDimension of 2 pixels at various image resolutions and recognizing them using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, configuring XDimension and image resolution, and BarCodeReader for decoding. Typical use cases include testing barcode readability across different DPI settings and ensuring consistent XDimension handling. Developers often need to adjust XDimension, resolution, and decoding settings when integrating barcode workflows into imaging pipelines.
// Prompt: Test recognition of Code128 barcodes with XDimension set to 2 pixels across multiple image resolutions.
// Tags: code128, xdimension, barcode generation, barcode recognition, resolution, png, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates Code128 barcodes with a specific XDimension and tests their recognition at multiple resolutions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcodes, reads them back, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code128XDimTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define resolutions to test (in DPI)
        float[] resolutions = new float[] { 72f, 150f, 300f };
        string codeText = "AsposeTest";

        // Store generated file paths for later recognition
        List<string> barcodeFiles = new List<string>();

        // Generate Code128 barcodes with XDimension = 2 pixels at each resolution
        foreach (float res in resolutions)
        {
            string filePath = Path.Combine(tempFolder, $"code128_{res}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set XDimension to 2 pixels
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                // Set image resolution (DPI)
                generator.Parameters.Resolution = res;
                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Recognize each generated barcode using default XDimension mode (Normal)
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Optionally, set recognition XDimension mode if needed
                // reader.QualitySettings.XDimension = XDimensionMode.Normal;

                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"Reading '{Path.GetFileName(file)}' (Resolution: {reader.QualitySettings.XDimension}) - Barcodes found: {results.Length}");
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}