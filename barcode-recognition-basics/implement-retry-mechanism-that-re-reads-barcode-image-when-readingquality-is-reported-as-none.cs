// Title: Retry barcode read on low quality
// Description: Demonstrates generating a barcode image and reading it with a retry mechanism that re‑reads when the reading quality is reported as None.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a barcode, BarCodeReader with DecodeType.AllSupportedTypes to detect barcodes, and QualitySettings to improve detection on retries. Developers often need to handle low‑confidence reads by adjusting quality settings and retrying until acceptable confidence is achieved.
// Prompt: Implement a retry mechanism that re‑reads a barcode image when ReadingQuality is reported as None.
// Tags: barcode symbology, generation, recognition, retry, qualitysettings, code128, png, barcodereader, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode image (if missing) and attempts to read it,
/// retrying with higher quality settings when the reading quality is reported as None.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Implements the retry logic for barcode reading.
    /// </summary>
    static void Main()
    {
        const string imagePath = "sample_barcode.png";
        const string codeText = "1234567890";
        const int maxRetries = 3;

        // Ensure the barcode image exists; generate it if missing.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Simple generation settings: set X-dimension and save as PNG.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Save(imagePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode image: {imagePath}");
            }
        }

        int attempt = 0;
        bool success = false;

        // Retry loop: attempt to read the barcode up to maxRetries times.
        while (attempt < maxRetries && !success)
        {
            attempt++;
            Console.WriteLine($"Attempt {attempt} to read barcode...");

            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // On retries, switch to a higher quality preset to improve detection.
                if (attempt > 1)
                {
                    reader.QualitySettings = QualitySettings.HighQuality;
                }

                // Perform the read operation.
                var results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                    continue; // Proceed to next retry attempt.
                }

                // Process each detected barcode.
                foreach (var result in results)
                {
                    // ReadingQuality is a double; 0 indicates BarCodeConfidence.None.
                    if (result.ReadingQuality == 0.0)
                    {
                        Console.WriteLine("ReadingQuality is None (0). Will retry if attempts remain.");
                        // Do not set success; loop will retry if attempts remain.
                    }
                    else
                    {
                        // Successful read with acceptable quality; output details.
                        Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                        Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                        Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                        success = true;
                    }
                }
            }
        }

        // Final status message after all attempts.
        if (!success)
        {
            Console.WriteLine("Failed to read barcode with sufficient quality after retries.");
        }
    }
}