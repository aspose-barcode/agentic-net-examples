// Title: Barcode Recognition Throughput Comparison with Hardware Acceleration
// Description: Generates a set of Code128 barcodes, then measures the time required to recognize them using Aspose.BarCode's hardware‑accelerated multi‑core mode versus a single‑core software‑only mode.
// Category-Description: This example belongs to the Aspose.BarCode performance and optimization category. It demonstrates how to use BarcodeGenerator for barcode creation and BarCodeReader with ProcessorSettings to control threading and hardware acceleration. Typical scenarios include high‑volume scanning, benchmarking, and tuning recognition speed for enterprise applications.
// Prompt: Enable hardware acceleration if available and compare recognition throughput against software‑only mode.
// Tags: barcode, code128, performance, hardware-acceleration, recognition, generation, aspose.barcode, .net

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates hardware‑accelerated versus software‑only barcode recognition throughput.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, runs two benchmarks, and prints the results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodePerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a set of sample Code128 barcodes
        int sampleCount = 5;
        for (int i = 0; i < sampleCount; i++)
        {
            string text = $"CODE{i}";
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Benchmark using hardware acceleration (all available cores)
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;
        var hwResult = BenchmarkReading(tempFolder, "Hardware-accelerated (all cores)");

        // Benchmark using software‑only mode (single core)
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 1;
        var swResult = BenchmarkReading(tempFolder, "Software-only (single core)");

        // Output the comparison results
        Console.WriteLine();
        Console.WriteLine("=== Throughput Comparison ===");
        Console.WriteLine($"{hwResult.Label}: {hwResult.TotalMilliseconds} ms total, {hwResult.AverageMs:F2} ms per image");
        Console.WriteLine($"{swResult.Label}: {swResult.TotalMilliseconds} ms total, {swResult.AverageMs:F2} ms per image");

        // Clean up the temporary folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }

    /// <summary>
    /// Measures the time required to read all barcodes in the specified folder.
    /// </summary>
    /// <param name="folderPath">Path to the folder containing barcode images.</param>
    /// <param name="label">Label describing the benchmark scenario.</param>
    /// <returns>A tuple containing the label, total elapsed milliseconds, and average milliseconds per image.</returns>
    private static (string Label, long TotalMilliseconds, double AverageMs) BenchmarkReading(string folderPath, string label)
    {
        // Retrieve all PNG files in the folder
        string[] files = Directory.GetFiles(folderPath, "*.png");
        Stopwatch sw = new Stopwatch();
        int totalBarcodes = 0;

        // Start timing
        sw.Start();
        foreach (string file in files)
        {
            try
            {
                // Read barcodes from the current image
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    if (results != null)
                    {
                        totalBarcodes += results.Length;
                    }
                }
            }
            catch (ArgumentException)
            {
                // Skip files that cannot be loaded as images
                continue;
            }
        }
        // Stop timing
        sw.Stop();

        // Calculate average time per image
        double avg = files.Length > 0 ? (double)sw.ElapsedMilliseconds / files.Length : 0;
        return (label, sw.ElapsedMilliseconds, avg);
    }
}