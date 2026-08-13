// Title: Generate and Read Barcodes with Confidence Scores
// Description: This example generates barcode images for several symbologies and then reads them using BarCodeReader to display confidence scores and reading quality.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows. It showcases the BarcodeGenerator for creating PNG images and the BarCodeReader for extracting barcode data, confidence, and quality metrics. Developers working with barcode automation, batch processing, or quality assessment will find this pattern useful when integrating Aspose.BarCode into .NET Core applications.
// Prompt: Create a PowerShell script that invokes BarCodeReader via .NET Core to process barcode images and output confidence scores.
// Tags: barcode symbology, generation, recognition, confidence, readingquality, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate barcode images and then read them back,
/// outputting confidence scores and reading quality information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, saves them as PNG files,
    /// and reads each file to display detection details.
    /// </summary>
    static void Main()
    {
        // Define the directory where barcode images will be stored.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Define a set of sample barcodes to generate.
        var samples = new (BaseEncodeType EncodeType, string CodeText, string FileName)[]
        {
            (EncodeTypes.Code128, "Sample12345", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM1234567890", "datamatrix.png")
        };

        // -----------------------------------------------------------------
        // Generate barcode images and save them as PNG files.
        // -----------------------------------------------------------------
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(outputDir, sample.FileName);
            using (BarcodeGenerator generator = new BarcodeGenerator(sample.EncodeType, sample.CodeText))
            {
                // Save the generated barcode image in PNG format.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // -----------------------------------------------------------------
        // Read each generated image and output barcode details.
        // -----------------------------------------------------------------
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(outputDir, sample.FileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Initialize the reader for all supported barcode types.
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Apply normal quality settings for balanced performance.
                reader.QualitySettings = QualitySettings.NormalQuality;

                // Iterate through all detected barcodes in the image.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {sample.FileName}");
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                    Console.WriteLine($"  Confidence: {result.Confidence}");
                    Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                }
            }
        }
    }
}