// Title: Asynchronous Barcode Scanning Benchmark
// Description: Demonstrates generating barcode images, then reading them synchronously and asynchronously to compare latency.
// Category-Description: This example belongs to the Aspose.BarCode scanning category, showcasing how to use BarCodeReader for decoding images. It illustrates typical use cases such as batch processing of barcode files, measuring performance of synchronous versus asynchronous reads, and helps developers understand when to apply parallel tasks for faster throughput. The example uses BarcodeGenerator, BarCodeReader, and common .NET timing utilities, useful for performance testing and optimization.
// Prompt: Implement asynchronous barcode scanning and measure latency reduction compared to synchronous calls.
// Tags: barcode symbology, scanning, async, performance, benchmark, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates asynchronous barcode scanning and latency comparison with synchronous scanning using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, measures synchronous and asynchronous read times, and reports latency reduction.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Create a temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images
        List<string> barcodeFiles = GenerateSampleBarcodes(tempFolder, 5);

        // Synchronous reading benchmark
        TimeSpan syncTime = MeasureSyncRead(barcodeFiles);
        Console.WriteLine($"Synchronous read time: {syncTime.TotalMilliseconds} ms");

        // Asynchronous reading benchmark
        TimeSpan asyncTime = await MeasureAsyncRead(barcodeFiles);
        Console.WriteLine($"Asynchronous read time: {asyncTime.TotalMilliseconds} ms");

        // Calculate latency reduction percentage
        double reduction = (syncTime.TotalMilliseconds - asyncTime.TotalMilliseconds) / syncTime.TotalMilliseconds * 100;
        Console.WriteLine($"Latency reduction: {reduction:F2}%");

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Generates a set of barcode image files using Code128 symbology.
    /// </summary>
    /// <param name="folder">Folder where barcode images will be saved.</param>
    /// <param name="count">Number of barcode images to generate.</param>
    /// <returns>List of file paths for the generated barcode images.</returns>
    static List<string> GenerateSampleBarcodes(string folder, int count)
    {
        var files = new List<string>();
        for (int i = 0; i < count; i++)
        {
            string codeText = $"CODE{i + 1:D3}";
            string filePath = Path.Combine(folder, $"barcode_{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            files.Add(filePath);
        }
        return files;
    }

    /// <summary>
    /// Measures the time required to read all barcode files synchronously.
    /// </summary>
    /// <param name="files">List of barcode image file paths.</param>
    /// <returns>Elapsed time for the synchronous read operation.</returns>
    static TimeSpan MeasureSyncRead(List<string> files)
    {
        var stopwatch = Stopwatch.StartNew();
        foreach (string file in files)
        {
            if (!File.Exists(file))
                continue;

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                reader.ReadBarCodes();
                // Process results if needed (omitted for benchmark)
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    // No output needed for benchmark
                }
            }
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    /// <summary>
    /// Measures the time required to read all barcode files asynchronously using parallel tasks.
    /// </summary>
    /// <param name="files">List of barcode image file paths.</param>
    /// <returns>Elapsed time for the asynchronous read operation.</returns>
    static async Task<TimeSpan> MeasureAsyncRead(List<string> files)
    {
        var stopwatch = Stopwatch.StartNew();
        var tasks = new List<Task>();
        foreach (string file in files)
        {
            if (!File.Exists(file))
                continue;

            // Run each read operation on a thread‑pool thread
            tasks.Add(Task.Run(() =>
            {
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    reader.ReadBarCodes();
                    foreach (BarCodeResult result in reader.FoundBarCodes)
                    {
                        // No output needed for benchmark
                    }
                }
            }));
        }
        await Task.WhenAll(tasks);
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }
}