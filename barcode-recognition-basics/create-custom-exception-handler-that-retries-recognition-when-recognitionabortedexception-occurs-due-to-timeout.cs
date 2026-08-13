// Title: Barcode Recognition with Retry on Timeout
// Description: Demonstrates generating a Code128 barcode image and recognizing it with a custom retry handler for timeout exceptions.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating how to use BarcodeGenerator and BarCodeReader classes to create and read barcodes. It shows typical use cases such as handling RecognitionAbortedException, setting timeouts, and implementing retry logic—common tasks for developers integrating barcode scanning into applications.
// Prompt: Create a custom exception handler that retries recognition when RecognitionAbortedException occurs due to timeout.
// Tags: code128, barcode, recognition, timeout, retry, aspose.barcode, generation, reading

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode image (if missing) and attempts to recognize it,
/// retrying when a timeout causes a <see cref="RecognitionAbortedException"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Handles barcode generation, recognition, and retry logic.
    /// </summary>
    static void Main()
    {
        // File path for the barcode image
        const string imagePath = "sample.png";

        // Text to encode in the barcode
        const string codeText = "1234567890";

        // Maximum number of recognition attempts
        const int maxRetries = 3;

        // Timeout in milliseconds (intentionally low to provoke a timeout)
        const int timeoutMs = 100;

        // ------------------------------------------------------------
        // Generate a barcode image if it does not already exist
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(imagePath);
                Console.WriteLine($"Barcode image generated at '{imagePath}'.");
            }
        }
        else
        {
            Console.WriteLine($"Using existing barcode image at '{imagePath}'.");
        }

        int attempt = 0;
        bool success = false;

        // ------------------------------------------------------------
        // Attempt recognition with retry logic
        // ------------------------------------------------------------
        while (attempt < maxRetries && !success)
        {
            attempt++;
            try
            {
                using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
                {
                    // Apply a short timeout to simulate a timeout scenario
                    reader.Timeout = timeoutMs;

                    // Perform recognition; iterate over all detected barcodes
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Attempt {attempt}: Detected barcode type: {result.CodeTypeName}");
                        Console.WriteLine($"Attempt {attempt}: Detected barcode text: {result.CodeText}");
                    }

                    // If we reach this point, recognition succeeded
                    success = true;
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle timeout-specific exception and retry
                Console.WriteLine($"Attempt {attempt}: Recognition aborted due to timeout. Retrying... ({ex.Message})");
                // Loop continues for next attempt
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors and abort further attempts
                Console.WriteLine($"Attempt {attempt}: Unexpected error: {ex.Message}");
                break;
            }
        }

        // ------------------------------------------------------------
        // Final outcome reporting
        // ------------------------------------------------------------
        if (!success)
        {
            Console.WriteLine($"Failed to recognize barcode after {maxRetries} attempts.");
        }
    }
}