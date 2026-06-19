using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a barcode, storing it in memory, and reading it back with retry logic.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode, saves it to a memory stream,
    /// and attempts to read it back up to three times, adjusting quality settings on each attempt.
    /// </summary>
    static void Main()
    {
        // Create an in‑memory stream to hold the generated barcode image.
        using (var imageStream = new MemoryStream())
        {
            // Generate a Code128 barcode with the text "12345".
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                // Save the barcode as a PNG image into the memory stream.
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Define the maximum number of read attempts.
            const int maxAttempts = 3;
            bool success = false; // Tracks whether a satisfactory read has occurred.

            // Retry loop: attempt to read the barcode up to maxAttempts times.
            for (int attempt = 1; attempt <= maxAttempts && !success; attempt++)
            {
                // Reset the stream position to the beginning before each read.
                imageStream.Position = 0;

                // Initialize a barcode reader that supports all barcode types.
                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                {
                    // Apply a high‑quality preset to improve detection on subsequent attempts.
                    reader.QualitySettings = QualitySettings.HighQuality;

                    // Perform the barcode detection.
                    var results = reader.ReadBarCodes();

                    // If no barcodes were detected, log and continue to the next attempt.
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"Attempt {attempt}: No barcode detected.");
                        continue;
                    }

                    // Process each detected barcode.
                    foreach (var result in results)
                    {
                        // ReadingQuality: 0 = None, higher values indicate better quality.
                        double quality = result.ReadingQuality;
                        Console.WriteLine($"Attempt {attempt}: Detected barcode type {result.CodeTypeName}, text '{result.CodeText}', ReadingQuality = {quality}");

                        // Accept the result if the quality is greater than zero.
                        if (quality > 0.0)
                        {
                            success = true; // Mark as successful and exit loops.
                            break;
                        }
                        else
                        {
                            // Quality is insufficient; will retry.
                            Console.WriteLine($"Attempt {attempt}: ReadingQuality is None, will retry.");
                        }
                    }
                }
            }

            // If all attempts failed to produce a satisfactory read, inform the user.
            if (!success)
            {
                Console.WriteLine("Failed to read barcode with sufficient quality after retries.");
            }
        }
    }
}