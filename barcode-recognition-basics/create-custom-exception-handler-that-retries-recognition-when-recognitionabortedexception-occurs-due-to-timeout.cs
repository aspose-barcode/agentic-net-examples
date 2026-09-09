// Title: Barcode Recognition with Retry on Timeout
// Description: Demonstrates generating a QR code and recognizing it with a custom retry mechanism that handles RecognitionAbortedException caused by timeout.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them, handling common scenarios such as timeouts. Developers often need to implement retry logic when reading barcodes from images that may be processed slowly or under constrained resources. The key API classes demonstrated are BarcodeGenerator, BarCodeReader, BarCodeResult, and RecognitionAbortedException.
// Prompt: Create a custom exception handler that retries recognition when RecognitionAbortedException occurs due to timeout.
// Tags: qr, barcode, recognition, timeout, retry, exception handling, aspose.barcode, generation, reading

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
    /// Entry point of the example. Generates a QR code, attempts to read it with retry handling for timeouts, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for storing the generated barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the sample barcode image.
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple QR barcode and save it as a PNG file.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        const int maxAttempts = 3;
        bool success = false;

        // Attempt to read the barcode, retrying on timeout up to the maximum number of attempts.
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            // Use a short timeout on the first attempt to intentionally trigger a timeout, then increase it.
            int timeout = attempt == 1 ? 1 : 2000;

            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                reader.Timeout = timeout;
                try
                {
                    // Perform barcode recognition.
                    BarCodeResult[] results = reader.ReadBarCodes();
                    Console.WriteLine($"Attempt {attempt}: Read {results?.Length ?? 0} barcode(s).");

                    // Output details of each recognized barcode.
                    if (results != null)
                    {
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }

                    // Mark success and exit the retry loop.
                    success = true;
                    break;
                }
                catch (RecognitionAbortedException ex)
                {
                    // Handle timeout-specific exception and decide whether to retry.
                    Console.WriteLine($"Attempt {attempt} aborted due to timeout: {ex.Message}");
                    if (attempt == maxAttempts)
                    {
                        Console.WriteLine("All retry attempts failed.");
                    }
                }
            }
        }

        // Clean up temporary files and directories.
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit.
        }

        // Report final outcome of the recognition process.
        if (success)
        {
            Console.WriteLine("Barcode recognition succeeded.");
        }
        else
        {
            Console.WriteLine("Barcode recognition did not succeed.");
        }
    }
}