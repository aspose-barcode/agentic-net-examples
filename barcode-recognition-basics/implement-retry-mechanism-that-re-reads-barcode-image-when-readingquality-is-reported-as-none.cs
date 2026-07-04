// Title: Barcode Reading with Retry on Low Quality
// Description: Demonstrates generating a barcode image and reading it with a retry mechanism that re‑reads when the reading quality is reported as None.
// Prompt: Implement a retry mechanism that re‑reads a barcode image when ReadingQuality is reported as None.
// Tags: barcode, code128, retry, readingquality, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, saves it as a PNG,
/// and attempts to read it with a retry mechanism when the reading quality is insufficient.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode if needed and reads it,
    /// retrying up to a maximum number of attempts if the reading quality is reported as None.
    /// </summary>
    static void Main()
    {
        // Define barcode content and output image file name
        const string codeText = "1234567890";
        const string imagePath = "sample_barcode.png";

        // ------------------------------------------------------------
        // Generate a barcode image if it does not already exist
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // ------------------------------------------------------------
        // Verify the image file exists before attempting to read it
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image file '{imagePath}' not found.");
            return;
        }

        // ------------------------------------------------------------
        // Set up retry parameters
        // ------------------------------------------------------------
        const int maxAttempts = 3;
        int attempt = 0;
        bool success = false;

        // ------------------------------------------------------------
        // Attempt to read the barcode, retrying when quality is None
        // ------------------------------------------------------------
        while (attempt < maxAttempts && !success)
        {
            attempt++;

            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Use higher quality settings on subsequent attempts for better detection
                if (attempt > 1)
                {
                    reader.QualitySettings = QualitySettings.HighQuality;
                }

                // Iterate over all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // ReadingQuality == 0 indicates 'None' (no confidence)
                    if (result.ReadingQuality == 0.0)
                    {
                        Console.WriteLine($"Attempt {attempt}: ReadingQuality is None. Retrying...");
                        // Continue to next attempt
                        continue;
                    }

                    // Successful read with sufficient quality
                    Console.WriteLine($"Attempt {attempt}: Barcode detected.");
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                    Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                    success = true;
                    break;
                }

                // If not successful and more attempts remain, loop will continue
            }
        }

        // ------------------------------------------------------------
        // Report final outcome
        // ------------------------------------------------------------
        if (!success)
        {
            Console.WriteLine("Failed to read barcode with sufficient quality after retries.");
        }
    }
}