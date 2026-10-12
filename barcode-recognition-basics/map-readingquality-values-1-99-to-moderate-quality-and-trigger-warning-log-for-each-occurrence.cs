// Title: Barcode Generation, Reading, and Quality Evaluation Example
// Description: Demonstrates generating a Code128 barcode, reading it back, and evaluating the ReadingQuality metric to identify moderate-quality scans.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the ReadingQuality property of BarCodeResult to assess scan quality. Developers often need to generate barcodes, read them, and handle quality warnings in automated workflows.
// Prompt: Map ReadingQuality values 1‑99 to moderate quality and trigger a warning log for each occurrence.
// Tags: barcode, code128, generation, recognition, readingquality, warning, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, reading, and quality assessment using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, reads it, and logs warnings for moderate reading quality.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for storing the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as a PNG image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was created before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Error: Generated barcode image not found.");
            return;
        }

        // Read the barcode from the image and evaluate its ReadingQuality
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");

                // Map ReadingQuality values 1‑99 to moderate quality and log a warning
                if (result.ReadingQuality >= 1 && result.ReadingQuality <= 99)
                {
                    Console.WriteLine($"Warning: Moderate quality detected (ReadingQuality={result.ReadingQuality})");
                }
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}