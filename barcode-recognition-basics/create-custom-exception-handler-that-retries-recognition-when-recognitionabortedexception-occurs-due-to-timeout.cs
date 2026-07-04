// Title: Barcode generation and recognition with retry on timeout
// Description: Demonstrates creating a Code128 barcode, saving it, and recognizing it with a custom retry handler for RecognitionAbortedException caused by timeout.
// Prompt: Create a custom exception handler that retries recognition when RecognitionAbortedException occurs due to timeout.
// Tags: barcode, code128, recognition, retry, timeout, aspose.barcodes, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode image and attempts to recognize it,
/// retrying when a timeout aborts the recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, saves it, then reads it with retry logic for timeout exceptions.
    /// </summary>
    static void Main()
    {
        // Path for the temporary barcode image
        string imagePath = "sample.png";

        // Create a simple Code128 barcode and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image was not created at '{imagePath}'.");
            return;
        }

        const int maxRetries = 3; // Maximum number of recognition attempts
        int attempt = 0;          // Current attempt counter
        bool success = false;    // Flag indicating successful recognition

        // Retry loop for barcode recognition
        while (attempt < maxRetries && !success)
        {
            attempt++;
            try
            {
                // Initialize the reader without an image
                using (var reader = new BarCodeReader())
                {
                    // Set a short timeout to trigger RecognitionAbortedException on delay
                    reader.Timeout = 500; // milliseconds

                    // Load the barcode image into the reader
                    using (var bitmap = new Bitmap(imagePath))
                    {
                        reader.SetBarCodeImage(bitmap);

                        // Perform recognition and output each detected result
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Attempt {attempt}: Detected barcode type '{result.CodeTypeName}' with text '{result.CodeText}'.");
                        }
                    }
                }

                // If we reach this point, recognition succeeded
                success = true;
            }
            catch (RecognitionAbortedException ex)
            {
                // Recognition timed out – log and retry
                Console.WriteLine($"Attempt {attempt}: Recognition aborted due to timeout. Retrying... ({ex.Message})");
            }
            catch (Exception ex)
            {
                // Any other unexpected exception stops the retry loop
                Console.WriteLine($"Attempt {attempt}: Unexpected error: {ex.Message}");
                break;
            }
        }

        // Final status message if all attempts failed
        if (!success)
        {
            Console.WriteLine("Failed to recognize the barcode after multiple attempts.");
        }
    }
}