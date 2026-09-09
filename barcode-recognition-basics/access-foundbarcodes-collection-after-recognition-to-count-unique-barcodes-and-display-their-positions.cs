// Title: Count Unique Barcodes and Show Their Positions after Recognition
// Description: Demonstrates how to generate sample barcodes, recognize them using Aspose.BarCode, count distinct barcode texts, and output each barcode's location within the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the FoundBarCodes collection to retrieve detailed results such as code type, text, and region coordinates. Developers often need to process multiple images, identify unique barcodes, and obtain their positions for inventory, tracking, or quality‑control applications.
// Prompt: Access FoundBarCodes collection after recognition to count unique barcodes and display their positions.
// Tags: barcode symbology, recognition, uniqueness, position, aspose.barcode, csharp, generation, decoding

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates barcode images, reads them back,
/// counts unique barcode texts, and prints each barcode's position.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcode data (including duplicates to test uniqueness)
        var samples = new (string Text, BaseEncodeType Type)[]
        {
            ("ABC123", EncodeTypes.Code128),
            ("XYZ789", EncodeTypes.QR),
            ("ABC123", EncodeTypes.DataMatrix)
        };

        var imageFiles = new List<string>();

        // Generate barcode images and store their file paths
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{sample.Text}_{Guid.NewGuid().ToString("N")}.png");
            using (var generator = new BarcodeGenerator(sample.Type, sample.Text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        var uniqueBarcodes = new HashSet<string>();

        // Recognize barcodes in each image and display their positions
        foreach (string file in imageFiles)
        {
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Perform recognition; results are stored in FoundBarCodes
                reader.ReadBarCodes();

                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");

                    // Extract rectangle region and angle information
                    var rect = result.Region.Rectangle;
                    Console.WriteLine($"Position: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}, Angle={result.Region.Angle}");
                    Console.WriteLine();

                    // Add barcode text to the set of unique values
                    uniqueBarcodes.Add(result.CodeText);
                }
            }
        }

        // Output the total count of distinct barcode texts found
        Console.WriteLine($"Unique barcodes count: {uniqueBarcodes.Count}");
    }
}