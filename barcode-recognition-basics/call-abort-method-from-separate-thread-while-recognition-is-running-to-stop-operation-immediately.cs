// Title: Abort Barcode Recognition from a Separate Thread
// Description: Demonstrates how to abort an ongoing barcode recognition operation using the Abort method from another thread.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of BarCodeReader to decode barcodes and the Abort method to stop processing on demand. Typical scenarios include long‑running scans that need to be cancelled based on user input or timeout conditions. Developers working with barcode scanning, multithreading, or responsive UI designs often need to interrupt recognition promptly.
// Prompt: Call Abort method from a separate thread while recognition is running to stop the operation immediately.
// Tags: barcode recognition, abort, multithreading, aspose.barcode, qr, csharp

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates aborting barcode recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a QR code, starts recognition on a background thread,
    /// and aborts the operation from another thread after a short delay.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AbortDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a simple QR barcode image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize the barcode reader (shared between threads)
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Thread that performs barcode recognition
            Thread readThread = new Thread(() =>
            {
                try
                {
                    // Start the recognition process; this call blocks until completed or aborted
                    reader.ReadBarCodes();
                    Console.WriteLine("Recognition completed.");

                    // Output each detected barcode
                    foreach (BarCodeResult result in reader.FoundBarCodes)
                    {
                        Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                    }
                }
                catch (RecognitionAbortedException ex)
                {
                    // Expected when Abort is called from another thread
                    Console.WriteLine($"Recognition aborted: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Handle any unexpected errors
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            });

            // Thread that aborts the recognition after a short, non‑blocking delay
            Thread abortThread = new Thread(() =>
            {
                // Wait 100 ms before invoking Abort
                Task.Delay(100).Wait();
                Console.WriteLine("Calling Abort...");
                reader.Abort();
            });

            // Start both threads concurrently
            readThread.Start();
            abortThread.Start();

            // Wait for both threads to finish before disposing the reader
            readThread.Join();
            abortThread.Join();
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect the demo outcome
        }
    }
}