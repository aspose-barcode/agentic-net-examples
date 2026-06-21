using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code128 barcodes, scanning them synchronously and asynchronously,
/// and comparing the execution times.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcode images, scans them synchronously and asynchronously,
    /// and prints timing results and decoded texts.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Generate sample barcode images (Code128) and store as byte arrays
        // ------------------------------------------------------------
        var barcodeImages = new List<byte[]>();
        for (int i = 0; i < 5; i++)
        {
            // Create a unique code text for each barcode (e.g., CODE000, CODE001, ...)
            string codeText = $"CODE{i:D3}";
            using (var ms = new MemoryStream())
            {
                // Generate the barcode and save it as PNG into the memory stream
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
                // Store the generated image bytes for later scanning
                barcodeImages.Add(ms.ToArray());
            }
        }

        // ------------------------------------------------------------
        // Synchronous scanning of the generated barcode images
        // ------------------------------------------------------------
        var syncStopwatch = Stopwatch.StartNew(); // Start timing
        var syncResults = new List<string>();
        foreach (var imgData in barcodeImages)
        {
            using (var ms = new MemoryStream(imgData))
            {
                // Initialize a barcode reader for Code128 type
                using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    // Read all barcodes found in the image and collect their text
                    foreach (var result in reader.ReadBarCodes())
                    {
                        syncResults.Add(result.CodeText);
                    }
                }
            }
        }
        syncStopwatch.Stop(); // Stop timing

        // ------------------------------------------------------------
        // Asynchronous scanning (wrapped in Task.Run for parallel execution)
        // ------------------------------------------------------------
        var asyncStopwatch = Stopwatch.StartNew(); // Start timing
        var asyncTasks = new List<Task<List<string>>>();
        foreach (var imgData in barcodeImages)
        {
            // Queue a task that scans a single image and returns the decoded texts
            asyncTasks.Add(Task.Run(() =>
            {
                var texts = new List<string>();
                using (var ms = new MemoryStream(imgData))
                {
                    using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            texts.Add(result.CodeText);
                        }
                    }
                }
                return texts;
            }));
        }

        // Wait for all scanning tasks to complete
        Task.WaitAll(asyncTasks.ToArray());

        // Aggregate results from all tasks
        var asyncResults = new List<string>();
        foreach (var task in asyncTasks)
        {
            asyncResults.AddRange(task.Result);
        }
        asyncStopwatch.Stop(); // Stop timing

        // ------------------------------------------------------------
        // Output timing comparison and decoded results
        // ------------------------------------------------------------
        Console.WriteLine($"Synchronous scan time: {syncStopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Asynchronous scan time: {asyncStopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine("Synchronous results:");
        foreach (var txt in syncResults)
        {
            Console.WriteLine(txt);
        }

        Console.WriteLine("Asynchronous results:");
        foreach (var txt in asyncResults)
        {
            Console.WriteLine(txt);
        }
    }
}