// Title: MaxiCode barcode generation and retry decoding example
// Description: Demonstrates generating a MaxiCode barcode, saving it as PNG, and attempting to decode it up to three times, handling failures gracefully.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating a MaxiCode symbology and BarCodeReader for decoding. Typical use cases include batch processing of shipping labels or inventory tags where decoding may need retries due to image quality issues. Developers often need to implement retry logic around the ReadBarCodes method to improve robustness.
// Prompt: Implement a retry mechanism that attempts to decode a MaxiCode barcode up to three times on failure.
// Tags: maxicode, barcode generation, barcode recognition, retry, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a MaxiCode barcode image and decoding it with a retry mechanism.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a MaxiCode barcode, saves it, and attempts to decode it up to three times.
    /// </summary>
    static void Main()
    {
        // ----------------------------------------------------------------------
        // Prepare a temporary folder and file path for the generated barcode image.
        // ----------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "maxicode.png");

        // --------------------------------------------------------------
        // Generate a simple MaxiCode barcode and save it as a PNG file.
        // --------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "1234567890"))
        {
            // Set the X-dimension (module size) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 15f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        const int maxAttempts = 3; // Maximum number of decode attempts.
        bool decoded = false;      // Flag indicating successful decoding.

        // --------------------------------------------------------------
        // Attempt to decode the barcode up to the defined number of tries.
        // --------------------------------------------------------------
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                // Verify that the image file exists before trying to read it.
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"File not found: {imagePath}");
                    break;
                }

                // Create a reader configured for MaxiCode symbology.
                using (var reader = new BarCodeReader(imagePath, DecodeType.MaxiCode))
                {
                    // Iterate through all detected barcodes in the image.
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // If a non‑empty code text is found, report success.
                        if (!string.IsNullOrEmpty(result.CodeText))
                        {
                            Console.WriteLine($"Decoded on attempt {attempt}: {result.CodeText}");
                            decoded = true;
                            break;
                        }
                    }
                }

                // Exit the retry loop if decoding succeeded.
                if (decoded)
                    break;

                // Inform that the current attempt did not locate a barcode.
                Console.WriteLine($"Attempt {attempt} did not find a barcode.");
            }
            catch (Exception ex)
            {
                // Log any exception that occurs during the decode attempt.
                Console.WriteLine($"Attempt {attempt} exception: {ex.Message}");
            }
        }

        // --------------------------------------------------------------
        // Report final outcome if decoding was not successful after retries.
        // --------------------------------------------------------------
        if (!decoded)
        {
            Console.WriteLine("Failed to decode the MaxiCode barcode after maximum attempts.");
        }

        // --------------------------------------------------------------
        // Clean up temporary files and directories.
        // --------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome.
        }
    }
}