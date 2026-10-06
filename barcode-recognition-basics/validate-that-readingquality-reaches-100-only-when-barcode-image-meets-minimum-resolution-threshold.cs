// Title: Validate ReadingQuality based on barcode image resolution
// Description: Demonstrates how the ReadingQuality property reaches 100 only when the barcode image meets a minimum resolution of 300 DPI.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes at specific resolutions and BarCodeReader to decode them, focusing on the ReadingQuality metric. Developers often need to ensure barcode readability under varying image qualities, making resolution a key factor in quality assessment.
// Prompt: Validate that ReadingQuality reaches 100 only when the barcode image meets a minimum resolution threshold.
// Tags: qr, readingquality, resolution, barcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates validation that ReadingQuality reaches 100 only when the barcode image meets a minimum resolution threshold.
/// </summary>
class Program
{
    /// <summary>
    /// Generates QR barcodes at various resolutions, reads them, and validates the ReadingQuality metric.
    /// </summary>
    static void Main()
    {
        // Minimum resolution required for ReadingQuality 100
        const float minResolution = 300f;

        // Create a temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeQualityDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Resolutions to test
        float[] resolutions = new float[] { 96f, 150f, 300f };

        // Iterate over each resolution, generate barcode, read it, and validate quality
        foreach (float res in resolutions)
        {
            // Build file path for the current resolution image
            string filePath = Path.Combine(tempFolder, $"qr_{res}dpi.png");

            // Generate barcode image at the specified resolution
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose"))
            {
                generator.Parameters.Resolution = res;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Verify that the image file was created successfully
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Failed to create image at {res} DPI.");
                continue;
            }

            // Read the barcode and obtain the ReadingQuality value
            double readingQuality = -1;
            using (var reader = new BarCodeReader(filePath, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    readingQuality = result.ReadingQuality;
                    Console.WriteLine($"Resolution: {res} DPI, CodeText: {result.CodeText}, ReadingQuality: {readingQuality}");
                    break; // Only need the first result
                }
            }

            // Perform validation based on resolution and ReadingQuality
            bool qualityIsFull = Math.Abs(readingQuality - 100.0) < 0.0001;
            bool meetsThreshold = res >= minResolution;

            if (qualityIsFull && meetsThreshold)
            {
                Console.WriteLine("Validation passed: Quality 100 achieved at sufficient resolution.");
            }
            else if (qualityIsFull && !meetsThreshold)
            {
                Console.WriteLine("Validation failed: Quality 100 reached below minimum resolution.");
            }
            else if (!qualityIsFull && meetsThreshold)
            {
                Console.WriteLine("Validation warning: Expected Quality 100 at or above minimum resolution, but got lower.");
            }
            else
            {
                Console.WriteLine("Validation OK: Quality below 100 as expected for lower resolution.");
            }

            Console.WriteLine();
        }

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