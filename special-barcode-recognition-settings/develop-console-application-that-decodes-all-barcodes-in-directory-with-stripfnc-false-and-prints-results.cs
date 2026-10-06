// Title: Decode multiple barcodes from generated images with StripFNC disabled
// Description: Demonstrates generating several Code128 barcodes, saving them to a temporary folder, and decoding each image while keeping Function characters (FNC) intact.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeGenerator for image creation and BarCodeReader for batch decoding. It highlights configuring BarcodeSettings.StripFNC, handling multiple files, and typical cleanup steps—common tasks for developers implementing bulk barcode processing in .NET applications.
// Prompt: Develop a console application that decodes all barcodes in a directory with StripFNC false and prints results.
// Tags: barcode, code128, batch decoding, stripfnc, console, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating barcode images, decoding them with StripFNC disabled, and outputting results to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application. Generates sample barcodes, decodes them, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample texts to encode into barcodes
        var sampleTexts = new List<string> { "ABC123", "XYZ789", "HelloWorld" };
        var barcodeFiles = new List<string>();

        // Generate barcode images and store their file paths
        for (int i = 0; i < sampleTexts.Count; i++)
        {
            string text = sampleTexts[i];
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Set X-dimension for better image quality
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            barcodeFiles.Add(filePath);
        }

        // Decode each generated barcode image with StripFNC set to false
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Preserve Function characters during decoding
                    reader.BarcodeSettings.StripFNC = false;
                    BarCodeResult[] results = reader.ReadBarCodes();

                    foreach (var result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle files that cannot be processed by the reader
                Console.WriteLine($"Skipping file {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // Optional cleanup of the temporary folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any errors during cleanup
        }
    }
}