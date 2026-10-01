// Title: Abort barcode recognition from a separate thread
// Description: Demonstrates how to stop an ongoing barcode recognition operation instantly by calling the Abort method from another thread.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the BarCodeReader class and its Abort capability. It is useful for scenarios where long‑running recognition must be cancelled, such as responsive UI applications or timeout handling. Developers working with barcode scanning, multithreading, or real‑time image processing often need to abort recognition to free resources or enforce time limits.
// Prompt: Call Abort method from a separate thread while recognition is running to stop the operation immediately.
// Tags: barcode, abort, recognition, multithreading, aspose.barcode, qr, csharp

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates aborting a barcode recognition operation from a separate thread using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a QR code, starts recognition on a background task,
    /// and aborts the operation from another task after a short delay.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for the demo files
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AbortDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Define the path for the sample barcode image
        // --------------------------------------------------------------------
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // --------------------------------------------------------------------
        // Generate a QR barcode and save it as a PNG file
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "HelloWorld"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was created successfully
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Initialize the reader (shared between threads)
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // ----------------------------------------------------------------
            // Task that performs the recognition
            // ----------------------------------------------------------------
            Task readTask = Task.Run(() =>
            {
                try
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        Console.WriteLine($"CodeText: {result.CodeText}");
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    }
                }
                catch (RecognitionAbortedException ex)
                {
                    Console.WriteLine($"Recognition aborted after {ex.ExecutionTime} ms");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            });

            // ----------------------------------------------------------------
            // Task that aborts the recognition after a short delay
            // ----------------------------------------------------------------
            Task abortTask = Task.Run(async () =>
            {
                await Task.Delay(100); // short pause before aborting
                reader.Abort();
                Console.WriteLine("Abort method called from separate thread.");
            });

            // ----------------------------------------------------------------
            // Wait for both tasks to complete
            // ----------------------------------------------------------------
            readTask.Wait();
            abortTask.Wait();
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and folder
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures are non‑critical for the demo
        }
    }
}