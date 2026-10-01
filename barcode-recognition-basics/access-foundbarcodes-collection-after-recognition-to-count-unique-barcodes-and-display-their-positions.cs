// Title: Count Unique Barcodes and Display Their Positions
// Description: Demonstrates generating barcode images, recognizing them, accessing the FoundBarCodes collection, counting unique barcodes, and printing each barcode's location.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, BarCodeReader to detect them, and BarCodeResult to retrieve details such as code text and region. Typical scenarios include batch processing of scanned documents, inventory tracking, and quality‑control checks where developers need to aggregate distinct barcodes and know where they appear in images. The code illustrates common patterns for temporary file handling, dictionary aggregation, and cleanup.
// Prompt: Access FoundBarCodes collection after recognition to count unique barcodes and display their positions.
// Tags: barcode, symbology, generation, recognition, count, positions, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates sample barcodes, reads them back, aggregates unique codes, and prints their positions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, recognition, aggregation, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample data: code text and the symbology to use
        var samples = new List<(string CodeText, BaseEncodeType EncodeType)>
        {
            ("ABC123", EncodeTypes.Code128),
            ("ABC123", EncodeTypes.Code128),
            ("XYZ789", EncodeTypes.QR)
        };

        // Generate barcode images and collect their file paths
        var imagePaths = new List<string>();
        foreach (var (codeText, encodeType) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{codeText}_{Guid.NewGuid().ToString("N")}.png");
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save directly to PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(filePath);
        }

        // Dictionary to hold unique code texts and their positions (case‑insensitive)
        var barcodePositions = new Dictionary<string, List<RectangleF>>(StringComparer.OrdinalIgnoreCase);

        // Read each generated image and collect recognition results
        foreach (string imagePath in imagePaths)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Perform recognition
                reader.ReadBarCodes();

                // Access the FoundBarCodes collection after recognition
                BarCodeResult[] found = reader.FoundBarCodes;
                if (found == null || found.Length == 0)
                {
                    Console.WriteLine($"No barcodes detected in {Path.GetFileName(imagePath)}");
                    continue;
                }

                // Aggregate each detected barcode's text and region
                foreach (BarCodeResult result in found)
                {
                    string code = result.CodeText ?? string.Empty;
                    RectangleF rect = result.Region.Rectangle; // Position of the barcode

                    if (!barcodePositions.ContainsKey(code))
                    {
                        barcodePositions[code] = new List<RectangleF>();
                    }
                    barcodePositions[code].Add(rect);
                }
            }
        }

        // Output the count of unique barcodes and their positions
        Console.WriteLine($"Unique barcodes detected: {barcodePositions.Count}");
        foreach (var kvp in barcodePositions)
        {
            Console.WriteLine($"CodeText: {kvp.Key}");
            int index = 1;
            foreach (RectangleF pos in kvp.Value)
            {
                Console.WriteLine($"  Position {index}: X={pos.X}, Y={pos.Y}, Width={pos.Width}, Height={pos.Height}");
                index++;
            }
        }

        // Cleanup temporary files and folder
        try
        {
            foreach (string file in imagePaths)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}