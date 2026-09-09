// Title: Barcode Generation, Recognition, and Quality Threshold Evaluation
// Description: This example generates barcode images, reads them back, and flags any barcode with a reading quality below 50 for manual review.
// Category-Description: Demonstrates core Aspose.BarCode operations including barcode generation (BarcodeGenerator) and recognition (BarCodeReader, BarCodeResult). Typical for quality‑control workflows where developers need to assess scan reliability and isolate low‑quality reads for further inspection. Useful for batch processing, automated verification, and integration into inventory or document management systems.
// Prompt: Apply a custom threshold treating ReadingQuality below 50 as unacceptable and flag those barcodes for manual review.
// Tags: barcode, generation, recognition, quality, readingquality, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating barcode images, recognizing them, and flagging low‑quality reads.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, reads them, and evaluates reading quality.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (type, text, output file name)
        var samples = new List<(BaseEncodeType Encode, string Text, string FileName)>
        {
            (EncodeTypes.Code128, "1234567890", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png")
        };

        // Generate barcode images and save them to the temporary folder
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.FileName);
            using (var generator = new BarcodeGenerator(sample.Encode, sample.Text))
            {
                // Set visual parameters (optional)
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Read barcodes from the generated files and evaluate their ReadingQuality
        Console.WriteLine("Barcode recognition results:");
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.FileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (var reader = new BarCodeReader(filePath))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {sample.FileName}");
                    Console.WriteLine($"  Code Type: {result.CodeTypeName}");
                    Console.WriteLine($"  Code Text: {result.CodeText}");
                    Console.WriteLine($"  Reading Quality: {result.ReadingQuality}");

                    // Flag barcodes with quality below the custom threshold (50)
                    if (result.ReadingQuality < 50)
                    {
                        Console.WriteLine("  --> Flagged for manual review (quality below threshold)");
                    }
                }
            }
        }

        // Clean up temporary files (optional)
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}