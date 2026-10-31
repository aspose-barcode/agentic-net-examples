// Title: Abort barcode recognition from a separate thread
// Description: Demonstrates how to abort an ongoing barcode recognition operation using the Abort method while it runs on a background thread.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of BarCodeReader to decode barcodes and the Abort method to stop processing on demand. Typical scenarios include long‑running scans where a user or system may need to cancel the operation promptly. Developers working with barcode scanning, multithreading, or responsive UI designs often need to control recognition lifecycle using these APIs.
// Prompt: Call Abort method from a separate thread while recognition is running to stop the operation immediately.
// Tags: barcode, abort, recognition, multithreading, aspose.barcode, csharp

using System;
using System.IO;
using System.Threading;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates aborting a barcode recognition operation from another thread using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, starts recognition on a background thread,
    /// aborts the operation, and cleans up temporary resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeAbortDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "sample.png");

        // Generate a simple Code128 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize the barcode reader for all supported symbologies
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            Exception threadException = null;

            // Start recognition on a separate thread; ReadBarCodes blocks until finished or aborted
            Thread readThread = new Thread(() =>
            {
                try
                {
                    var results = reader.ReadBarCodes();
                    Console.WriteLine($"Read completed, found {results.Length} barcode(s).");
                    foreach (var result in results)
                    {
                        Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                    }
                }
                catch (RecognitionAbortedException ex)
                {
                    // Expected when Abort is called
                    Console.WriteLine($"Recognition aborted: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Capture any unexpected errors to report after the thread joins
                    threadException = ex;
                }
            });

            readThread.Start();

            // Briefly yield to allow the reading operation to commence
            for (int i = 0; i < 10; i++)
            {
                Thread.Yield();
            }

            // Abort the recognition from the main thread
            reader.Abort();

            // Wait for the background thread to finish processing
            readThread.Join();

            // Report any exception that occurred inside the reading thread
            if (threadException != null)
            {
                Console.WriteLine($"Thread error: {threadException}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failure is non‑critical for the demo
        }
    }
}