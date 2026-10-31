// Title: Count Unique Barcodes and Display Their Positions after Recognition
// Description: Demonstrates generating sample barcodes, recognizing them with Aspose.BarCode, counting unique barcode texts, and printing each barcode's location within the image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for detecting them. Developers often need to extract barcode data from images, identify distinct values, and locate barcodes for further processing such as inventory management, document automation, or quality control. The example highlights key API classes like BarcodeGenerator, BarCodeReader, BarCodeResult, and the FoundBarCodes collection, providing a practical reference for common scanning scenarios.
/// Prompt: Access FoundBarCodes collection after recognition to count unique barcodes and display their positions.
/// Tags: barcode, generation, recognition, unique, position, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides an example that generates several barcodes, reads them back,
/// counts unique values and prints their positions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that executes the barcode generation, recognition, and reporting logic.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (type, text, file name)
        var samples = new (BaseEncodeType Encode, string Text, string FileName)[]
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM-001", "datamatrix.png")
        };

        var generatedFiles = new List<string>();

        // Generate barcode images and store their file paths
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.FileName);
            using (var generator = new BarcodeGenerator(sample.Encode, sample.Text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // HashSet to keep track of unique barcode texts
        var uniqueCodes = new HashSet<string>();

        // Recognize barcodes in each generated image and access FoundBarCodes
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Perform barcode detection
                reader.ReadBarCodes();

                Console.WriteLine($"Processing file: {Path.GetFileName(file)}");
                Console.WriteLine($"FoundCount: {reader.FoundCount}");

                // Iterate through all detected barcodes
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    // Add barcode text to the set of unique codes
                    uniqueCodes.Add(result.CodeText ?? string.Empty);

                    // Retrieve the bounding rectangle of the barcode region
                    var rect = result.Region.Rectangle;

                    // Output barcode details and position
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"Position - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                    Console.WriteLine();
                }
            }
        }

        // Display the total count of unique barcodes found
        Console.WriteLine($"Unique barcodes count: {uniqueCodes.Count}");

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}