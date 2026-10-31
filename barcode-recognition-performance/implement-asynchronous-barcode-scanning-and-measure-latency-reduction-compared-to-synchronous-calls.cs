// Title: Asynchronous barcode scanning with latency comparison
// Description: Demonstrates generating Code128 barcodes, scanning them synchronously and asynchronously, and measuring the time difference.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator for image creation and BarCodeReader for decoding. It illustrates typical use cases such as batch processing of barcode images and performance benchmarking with async Task parallelism, helping developers evaluate throughput improvements in high‑volume scanning scenarios.
// Prompt: Implement asynchronous barcode scanning and measure latency reduction compared to synchronous calls.
// Tags: barcode, code128, generation, recognition, async, performance, task, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates sample Code128 barcodes, scans them synchronously and asynchronously,
/// and reports the elapsed time for each approach.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, scanning, timing, and cleanup.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for storing generated barcode images.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Generate a set of sample barcode PNG files.
        // --------------------------------------------------------------------
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string codeText = "Sample" + i;
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Perform synchronous scanning and measure elapsed time.
        // --------------------------------------------------------------------
        Stopwatch syncWatch = Stopwatch.StartNew();
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
                continue;

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                reader.ReadBarCodes();
                // Optionally process results:
                // foreach (var result in reader.FoundBarCodes) { /* ... */ }
            }
        }
        syncWatch.Stop();
        Console.WriteLine($"Synchronous scan time: {syncWatch.ElapsedMilliseconds} ms");

        // --------------------------------------------------------------------
        // Perform asynchronous scanning using Task.Run for each file and measure elapsed time.
        // --------------------------------------------------------------------
        Stopwatch asyncWatch = Stopwatch.StartNew();
        List<Task> tasks = new List<Task>();
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
                continue;

            tasks.Add(Task.Run(() =>
            {
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    reader.ReadBarCodes();
                }
            }));
        }
        Task.WaitAll(tasks.ToArray());
        asyncWatch.Stop();
        Console.WriteLine($"Asynchronous scan time: {asyncWatch.ElapsedMilliseconds} ms");

        // --------------------------------------------------------------------
        // Cleanup generated files and temporary directory.
        // --------------------------------------------------------------------
        try
        {
            foreach (string file in barcodeFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}