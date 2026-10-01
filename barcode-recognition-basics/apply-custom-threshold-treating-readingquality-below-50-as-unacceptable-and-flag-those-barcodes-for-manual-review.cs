// Title: Custom ReadingQuality Threshold for Barcode Validation
// Description: Demonstrates generating Code128 barcodes, reading them, and flagging any with a ReadingQuality below 50 for manual review.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator, BarCodeReader, and BarCodeResult to create barcodes, decode them, and evaluate the ReadingQuality metric. Developers often need to assess scan quality and automatically identify low‑quality reads for further inspection.
// Prompt: Apply a custom threshold treating ReadingQuality below 50 as unacceptable and flag those barcodes for manual review.
// Tags: barcode symbology, generation, recognition, readingquality, manual review, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates sample Code128 barcodes, reads them back, and flags low‑quality scans.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary barcode images, evaluates their ReadingQuality, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold generated file paths
        List<string> barcodeFiles = new List<string>();

        // Sample data for barcode generation
        var samples = new[]
        {
            new { Text = "1234567890", Symbology = EncodeTypes.Code128 },
            new { Text = "ABCDEFGHIJ", Symbology = EncodeTypes.Code128 }
        };

        // Generate barcode images and store their file paths
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{sample.Symbology}_{sample.Text}.png");
            using (var generator = new BarcodeGenerator(sample.Symbology, sample.Text))
            {
                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Read each barcode and evaluate ReadingQuality
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // ReadingQuality is a double value (0-100). Lower values indicate poorer quality.
                    double quality = result.ReadingQuality;
                    Console.WriteLine($"File: {Path.GetFileName(file)} | CodeText: {result.CodeText} | ReadingQuality: {quality}");

                    // Flag barcodes with quality below the custom threshold
                    if (quality < 50.0)
                    {
                        Console.WriteLine("=> Flagged for manual review (ReadingQuality below threshold).");
                    }
                }
            }
        }

        // Clean up temporary files (optional)
        try
        {
            foreach (string file in barcodeFiles)
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup warning: {ex.Message}");
        }
    }
}