using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode image, reading it asynchronously,
/// aborting the read operation after a short delay, and cleaning up resources.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare a temporary file path for the barcode image.
        // --------------------------------------------------------------------
        string tempDir = Path.GetTempPath();
        string imagePath = Path.Combine(tempDir, "sample_barcode.png");

        // --------------------------------------------------------------------
        // 2. Generate a simple Code128 barcode and save it as PNG.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // 3. Verify that the image file was created successfully.
        // --------------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // 4. Create a barcode reader that supports all barcode types.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            var results = new List<string>();

            // ----------------------------------------------------------------
            // 5. Start barcode recognition on a separate thread.
            // ----------------------------------------------------------------
            var recognitionThread = new Thread(() =>
            {
                try
                {
                    // Iterate through all detected barcodes.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Protect shared list with a lock.
                        lock (results)
                        {
                            results.Add(result.CodeText);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Expected exception when Abort is called; log the message.
                    Console.WriteLine("Recognition stopped: " + ex.Message);
                }
            });

            recognitionThread.Start();

            // ----------------------------------------------------------------
            // 6. Abort the recognition after a short delay (100 ms).
            // ----------------------------------------------------------------
            Task.Delay(100).ContinueWith(_ => reader.Abort()).Wait();

            // ----------------------------------------------------------------
            // 7. Wait for the recognition thread to finish.
            // ----------------------------------------------------------------
            recognitionThread.Join();

            // ----------------------------------------------------------------
            // 8. Output the number of detected barcodes and their values.
            // ----------------------------------------------------------------
            Console.WriteLine("Recognition completed. Detected barcodes: " + results.Count);
            foreach (var txt in results)
            {
                Console.WriteLine("Detected: " + txt);
            }
        }

        // --------------------------------------------------------------------
        // 9. Clean up the temporary image file.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Suppress any errors that occur during cleanup.
        }
    }
}