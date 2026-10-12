// Title: Barcode recognition with retry on timeout
// Description: Demonstrates generating a QR code image, then reading it with a timeout and automatically retrying when a RecognitionAbortedException occurs.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them, handling timeouts via the RecognitionAbortedException. Developers often need to implement retry logic for robust barcode scanning in environments with variable performance.
// Prompt: Create a custom exception handler that retries recognition when RecognitionAbortedException occurs due to timeout.
// Tags: barcode, qr, recognition, retry, timeout, aspose.barcode, generation, reading

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, recognition with timeout, and retry logic on RecognitionAbortedException.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, attempts to read it with a timeout, and retries on failure.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeRetryDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR barcode image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        const int maxAttempts = 3;      // Maximum number of retry attempts
        const int timeoutMs = 200;      // Timeout for each recognition attempt (in milliseconds)

        // Attempt to read the barcode, retrying on timeout up to the maximum attempts
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
            {
                reader.Timeout = timeoutMs; // Apply the timeout setting

                try
                {
                    // Perform barcode recognition
                    BarCodeResult[] results = reader.ReadBarCodes();

                    Console.WriteLine($"Attempt {attempt}: Successfully read {results.Length} barcode(s).");
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"CodeType: {result.CodeTypeName}, CodeText: {result.CodeText}");
                    }

                    // Successful read; exit the retry loop
                    break;
                }
                catch (RecognitionAbortedException ex)
                {
                    // Handle timeout-induced aborts
                    Console.WriteLine($"Attempt {attempt}: Recognition aborted after {ex.ExecutionTime} ms (timeout).");
                    if (attempt == maxAttempts)
                    {
                        Console.WriteLine("Maximum retry attempts reached. Giving up.");
                    }
                    else
                    {
                        Console.WriteLine("Retrying...");
                    }
                }
            }
        }

        // Clean up temporary files and directories
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}