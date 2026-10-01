// Title: Asynchronous vs Synchronous Barcode Scanning with Latency Measurement
// Description: Demonstrates generating sample Code128 barcodes, scanning them synchronously and asynchronously, and measuring the time difference to illustrate latency reduction.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical use cases include batch processing of images, performance benchmarking, and parallel scanning in high‑throughput applications. Developers often need to compare synchronous and asynchronous patterns to optimize latency and resource utilization.
// Prompt: Implement asynchronous barcode scanning and measure latency reduction compared to synchronous calls.
// Tags: barcode symbology, scanning, async, performance, aspose.barcode, generation, recognition, code128, png

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates asynchronous barcode scanning using Aspose.BarCode and compares its latency to synchronous scanning.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates sample barcodes, scans them synchronously and asynchronously,
    /// and reports the elapsed time and latency reduction.
    /// </summary>
    static async Task Main(string[] args)
    {
        // Create a unique temporary folder for the generated barcode images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeAsyncDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a set of sample barcode PNG files.
        List<string> barcodeFiles = GenerateSampleBarcodes(tempFolder, 5);

        // -------------------------------------------------
        // Synchronous scanning
        // -------------------------------------------------
        Stopwatch swSync = Stopwatch.StartNew();
        List<string> syncResults = new List<string>();
        foreach (string file in barcodeFiles)
        {
            // Scan each image one by one.
            string text = ScanBarcodeSync(file);
            syncResults.Add(text ?? "<null>");
        }
        swSync.Stop();

        // -------------------------------------------------
        // Asynchronous scanning (parallel execution)
        // -------------------------------------------------
        Stopwatch swAsync = Stopwatch.StartNew();
        // Start a scan task for each file.
        Task<string>[] tasks = barcodeFiles.Select(f => ScanBarcodeAsync(f)).ToArray();
        // Await all tasks to complete.
        string[] asyncResults = await Task.WhenAll(tasks);
        swAsync.Stop();

        // -------------------------------------------------
        // Output timing results and latency reduction
        // -------------------------------------------------
        Console.WriteLine("Synchronous scan time: {0} ms", swSync.ElapsedMilliseconds);
        Console.WriteLine("Asynchronous scan time: {0} ms", swAsync.ElapsedMilliseconds);
        if (swSync.ElapsedMilliseconds > 0)
        {
            double reduction = 100.0 * (swSync.ElapsedMilliseconds - swAsync.ElapsedMilliseconds) / swSync.ElapsedMilliseconds;
            Console.WriteLine("Latency reduction: {0:F2} %", reduction);
        }

        // -------------------------------------------------
        // Display decoded values for verification
        // -------------------------------------------------
        Console.WriteLine("\nDecoded values (sync):");
        foreach (var txt in syncResults) Console.WriteLine(txt);

        Console.WriteLine("\nDecoded values (async):");
        foreach (var txt in asyncResults) Console.WriteLine(txt ?? "<null>");

        // -------------------------------------------------
        // Clean up temporary files and folder
        // -------------------------------------------------
        foreach (string file in barcodeFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder); } catch { }
    }

    /// <summary>
    /// Generates a set of barcode PNG files using Code128 symbology and returns their file paths.
    /// </summary>
    /// <param name="folder">The folder where barcode images will be saved.</param>
    /// <param name="count">Number of barcode images to generate.</param>
    /// <returns>List of file paths for the generated barcode images.</returns>
    static List<string> GenerateSampleBarcodes(string folder, int count)
    {
        var files = new List<string>();
        for (int i = 0; i < count; i++)
        {
            string codeText = $"Sample{i + 1}";
            string filePath = Path.Combine(folder, $"barcode_{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            files.Add(filePath);
        }
        return files;
    }

    /// <summary>
    /// Scans a barcode image synchronously and returns the decoded text.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <returns>Decoded barcode text, or null if not found.</returns>
    static string ScanBarcodeSync(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return null;
        }

        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Read the first detected barcode.
            var result = reader.ReadBarCodes().FirstOrDefault();
            return result?.CodeText;
        }
    }

    /// <summary>
    /// Scans a barcode image asynchronously by wrapping the synchronous operation in <c>Task.Run</c>.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <returns>A task that resolves to the decoded barcode text, or null if not found.</returns>
    static Task<string> ScanBarcodeAsync(string imagePath)
    {
        return Task.Run(() =>
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return null;
            }

            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                var result = reader.ReadBarCodes().FirstOrDefault();
                return result?.CodeText;
            }
        });
    }
}