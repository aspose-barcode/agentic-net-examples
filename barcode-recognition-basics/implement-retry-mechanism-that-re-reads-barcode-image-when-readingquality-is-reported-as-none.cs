// Title: Retry barcode reading when quality is None
// Description: Demonstrates how to retry reading a QR barcode image until a non‑None reading quality is obtained, using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode reading category, illustrating the use of BarCodeReader, DecodeType, and BarCodeResult to detect and evaluate barcode quality. Typical use cases include validating scan reliability in automated workflows where low‑quality reads must be retried. Developers often need to implement retry loops and inspect ReadingQuality to ensure accurate data extraction.
// Prompt: Implement a retry mechanism that re‑reads a barcode image when ReadingQuality is reported as None.
// Tags: qr, barcode, reading, retry, readingquality, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates retrying barcode reading when the reading quality is reported as None.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample QR barcode, then attempts to read it up to three times,
    /// retrying when the reading quality is None.
    /// </summary>
    static void Main()
    {
        // Create a temporary file path for the barcode image
        string tempImagePath = Path.Combine(Path.GetTempPath(), "barcode_" + Guid.NewGuid().ToString("N") + ".png");
        GenerateSampleBarcode(tempImagePath);

        // Verify that the image was created successfully
        if (!File.Exists(tempImagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        const int maxAttempts = 3; // Maximum number of read attempts
        bool success = false;      // Flag indicating a successful read with acceptable quality

        // Retry loop: attempt to read the barcode up to maxAttempts times
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            // Initialize the reader for the generated image, specifying QR decoding
            using (var reader = new BarCodeReader(tempImagePath, DecodeType.QR))
            {
                // Read all barcodes found in the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // If no barcodes were detected, log and continue to next attempt
                if (results.Length == 0)
                {
                    Console.WriteLine($"Attempt {attempt}: No barcode detected.");
                    continue;
                }

                // Evaluate each detected barcode result
                foreach (var result in results)
                {
                    Console.WriteLine($"Attempt {attempt}: CodeType={result.CodeTypeName}, CodeText={result.CodeText}, ReadingQuality={result.ReadingQuality}");

                    // ReadingQuality.None is represented by 0; any other value indicates acceptable quality
                    if (result.ReadingQuality != 0)
                    {
                        Console.WriteLine("Barcode read with acceptable quality.");
                        success = true;
                        break;
                    }
                }

                // If a successful read occurred, exit the retry loop
                if (success) break;

                // Otherwise, indicate that a retry will occur
                Console.WriteLine($"Attempt {attempt}: ReadingQuality was None, retrying...");
            }
        }

        // Report final outcome if all attempts failed to achieve acceptable quality
        if (!success)
        {
            Console.WriteLine("Failed to read barcode with sufficient quality after retries.");
        }

        // Clean up the temporary barcode image file
        try
        {
            File.Delete(tempImagePath);
        }
        catch
        {
            // Suppress any exceptions during cleanup
        }
    }

    /// <summary>
    /// Generates a QR barcode image with the specified text and saves it to the given path.
    /// </summary>
    /// <param name="path">The file system path where the barcode image will be saved.</param>
    private static void GenerateSampleBarcode(string path)
    {
        // Initialize the barcode generator with QR symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose.BarCode.Sample"))
        {
            // Optional: configure barcode appearance
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG image
            generator.Save(path, BarCodeImageFormat.Png);
        }
    }
}