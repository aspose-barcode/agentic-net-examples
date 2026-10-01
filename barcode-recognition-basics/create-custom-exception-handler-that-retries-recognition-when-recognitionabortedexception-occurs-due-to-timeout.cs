// Title: Barcode Recognition with Retry on Timeout
// Description: Demonstrates generating a QR barcode, then reading it with a short timeout and retrying when a RecognitionAbortedException occurs.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them, highlighting typical scenarios such as handling timeouts and implementing retry logic. Developers working with barcode imaging often need to generate barcodes, read them from files or streams, and gracefully handle recognition failures using key API classes like BarcodeGenerator, BarCodeReader, and related result types.
// Prompt: Create a custom exception handler that retries recognition when RecognitionAbortedException occurs due to timeout.
// Tags: qr,barcode,generation,recognition,retry,timeout,exception handling,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a QR barcode, attempts to read it with a forced timeout,
/// and retries the recognition when a <see cref="RecognitionAbortedException"/> is thrown.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the barcode generation, reading with retry logic,
    /// and cleanup of temporary resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR barcode and save it to the file
        BaseEncodeType encodeType = EncodeTypes.QR;
        using (var generator = new BarcodeGenerator(encodeType, "RetryDemo"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Set up reader with a short timeout to force a timeout scenario
        BaseDecodeType decodeType = DecodeType.QR;
        int maxAttempts = 3;
        int timeoutMs = 100; // 0.1 second timeout for demonstration

        // Retry loop: attempt to read the barcode up to maxAttempts times
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            Console.WriteLine($"Attempt {attempt} of {maxAttempts}...");

            using (var reader = new BarCodeReader(barcodePath, decodeType))
            {
                reader.Timeout = timeoutMs; // Apply the short timeout

                try
                {
                    // Attempt to read barcodes from the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results != null && results.Length > 0)
                    {
                        // Successful read – output details and exit the retry loop
                        foreach (var result in results)
                        {
                            Console.WriteLine($"CodeText: {result.CodeText}");
                            Console.WriteLine($"CodeType: {result.CodeTypeName}");
                            Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                        }
                        break;
                    }
                    else
                    {
                        // No barcode detected – no point in further retries
                        Console.WriteLine("No barcode detected.");
                        break;
                    }
                }
                catch (RecognitionAbortedException ex)
                {
                    // Handle timeout-specific exception and decide whether to retry
                    Console.WriteLine($"Recognition aborted due to timeout after {ex.ExecutionTime} ms.");
                    if (attempt == maxAttempts)
                    {
                        Console.WriteLine("Maximum retry attempts reached. Giving up.");
                    }
                    else
                    {
                        Console.WriteLine("Retrying...");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any unexpected errors and abort further retries
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                    break;
                }
            }
        }

        // Clean up temporary files and directories
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program exit
        }

        Console.WriteLine("Program completed.");
    }
}