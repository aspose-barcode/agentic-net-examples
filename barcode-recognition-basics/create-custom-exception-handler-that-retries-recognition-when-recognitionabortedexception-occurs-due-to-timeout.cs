// Title: Barcode recognition with retry on timeout
// Description: Demonstrates generating a QR code, then recognizing it with custom retry logic that handles RecognitionAbortedException caused by a short timeout.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, including handling time‑outs via the RecognitionAbortedException. Developers often need to implement retry mechanisms when reading barcodes from low‑quality images or constrained environments.
// Prompt: Create a custom exception handler that retries recognition when RecognitionAbortedException occurs due to timeout.
// Tags: qr, barcode generation, barcode recognition, timeout, retry, aspose.barcode, recognitionabortedexception

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and recognition with retry logic for timeout exceptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, attempts recognition with retries, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR barcode image
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Attempt to recognize the barcode with retry logic
        const int maxAttempts = 3;
        const int timeoutMs = 1; // Intentionally low to trigger timeout
        RecognizeWithRetry(barcodePath, maxAttempts, timeoutMs);

        // Clean up temporary files and folder
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

    /// <summary>
    /// Reads a barcode image with a configurable number of retry attempts.
    /// Retries when a RecognitionAbortedException occurs due to a timeout.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="maxAttempts">Maximum number of recognition attempts.</param>
    /// <param name="timeoutMs">Timeout in milliseconds for each attempt.</param>
    static void RecognizeWithRetry(string imagePath, int maxAttempts, int timeoutMs)
    {
        // Verify that the image file exists before attempting recognition
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Loop through the allowed number of attempts
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            Console.WriteLine($"Attempt {attempt} of {maxAttempts}...");

            // Initialize the barcode reader for QR codes
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
            {
                // Apply the timeout setting for this attempt
                reader.Timeout = timeoutMs;

                try
                {
                    // Perform the recognition
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // If any barcodes were detected, output their details
                    if (results != null && results.Length > 0)
                    {
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"Code Type: {result.CodeTypeName}");
                            Console.WriteLine($"Code Text: {result.CodeText}");
                            Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                        }
                        return; // Success, exit method
                    }
                    else
                    {
                        Console.WriteLine("No barcodes detected.");
                        return;
                    }
                }
                catch (RecognitionAbortedException ex)
                {
                    // Handle timeout-specific exception and continue to next attempt
                    Console.WriteLine($"Recognition aborted due to timeout after {ex.ExecutionTime} ms.");
                }
                catch (Exception ex)
                {
                    // Handle any other unexpected errors and abort further attempts
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                    return;
                }
            }
        }

        // All attempts exhausted without success
        Console.WriteLine("All attempts failed due to timeout.");
    }
}