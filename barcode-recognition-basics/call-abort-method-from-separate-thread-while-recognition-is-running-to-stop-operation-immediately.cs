// Title: Abort Barcode Recognition from Another Thread
// Description: Demonstrates how to abort an ongoing barcode recognition operation using the Abort method from a separate thread.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of BarCodeReader, DecodeType, and the Abort method to stop processing. Typical scenarios include long-running scans where a user or system needs to cancel the operation promptly. Developers often need to manage recognition lifecycles in multithreaded environments, making this pattern essential for responsive applications.
// Prompt: Call Abort method from a separate thread while recognition is running to stop the operation immediately.
// Tags: barcode, recognition, abort, multithreading, qr, aspose.barcode, barcodereader

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates aborting a barcode recognition operation from a separate thread using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a QR code, starts recognition on a background thread,
    /// aborts it shortly after, and cleans up temporary resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AbortDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "barcode.png");

        // Generate a simple QR barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Initialize the reader for QR codes and start recognition on a separate thread
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            Thread readThread = new Thread(() =>
            {
                try
                {
                    // Perform the recognition; this call blocks until completed or aborted
                    reader.ReadBarCodes();

                    // Output results if recognition finishes normally
                    Console.WriteLine($"Reading completed. Found count: {reader.FoundCount}");
                    foreach (var result in reader.FoundBarCodes)
                    {
                        Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                    }
                }
                catch (RecognitionAbortedException ex)
                {
                    // Handle the expected abort scenario
                    Console.WriteLine($"Recognition aborted: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Handle any unexpected errors
                    Console.WriteLine($"Error during reading: {ex.Message}");
                }
            });

            // Start the background recognition thread
            readThread.Start();

            // Short delay before aborting to ensure the read operation has started
            Task.Delay(100).Wait();

            // Abort the recognition process from the main thread
            reader.Abort();

            // Wait for the background thread to finish handling the abort
            readThread.Join();
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored: cleanup failures are non‑critical for the demo
        }

        Console.WriteLine("Demo finished.");
    }
}