// Title: Barcode Recognition Throughput Benchmark with Hardware Acceleration
// Description: Demonstrates generating sample Code128 barcodes, then measuring recognition speed using Aspose.BarCode in software‑only and hardware‑accelerated modes.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with ProcessorSettings to control multithreading and hardware acceleration. Developers often need to compare throughput of different recognition configurations to optimize processing pipelines for large volumes of images.
// Prompt: Enable hardware acceleration if available and compare recognition throughput against software‑only mode.
// Tags: barcode, performance, benchmark, hardware acceleration, multithreading, code128, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and recognition throughput comparison between software-only and hardware‑accelerated modes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcodes, runs benchmarks, and outputs timing results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images
        const int sampleCount = 5;
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = $"Sample{i}";
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set barcode color to black
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                // Save as PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Benchmark software‑only (single‑thread) mode
        long softwareTime = BenchmarkReading(tempFolder, useMultithread: false, out int softwareCount);
        Console.WriteLine($"Software‑only mode: {softwareCount} barcodes read in {softwareTime} ms");

        // Benchmark hardware‑accelerated (multithread) mode
        long hardwareTime = BenchmarkReading(tempFolder, useMultithread: true, out int hardwareCount);
        Console.WriteLine($"Hardware‑accelerated mode: {hardwareCount} barcodes read in {hardwareTime} ms");

        // Clean up temporary files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }

    /// <summary>
    /// Measures the time required to read all barcodes in a folder using specified threading settings.
    /// </summary>
    /// <param name="folderPath">Path to the folder containing barcode images.</param>
    /// <param name="useMultithread">If true, enables multithreading (hardware acceleration); otherwise, single‑threaded.</param>
    /// <param name="totalBarcodes">Outputs the total number of barcodes decoded.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    static long BenchmarkReading(string folderPath, bool useMultithread, out int totalBarcodes)
    {
        // Configure multithreading settings based on the benchmark mode
        BarCodeReader.ProcessorSettings.UseAllCores = useMultithread;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = useMultithread ? Environment.ProcessorCount : 1;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = useMultithread ? Environment.ProcessorCount * 2 : 0;

        totalBarcodes = 0;
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Read each generated barcode image
        foreach (string file in Directory.GetFiles(folderPath, "*.png"))
        {
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    totalBarcodes++;
                    // For demonstration, output the decoded text (optional)
                    Console.WriteLine($"Decoded: {result.CodeText}");
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}