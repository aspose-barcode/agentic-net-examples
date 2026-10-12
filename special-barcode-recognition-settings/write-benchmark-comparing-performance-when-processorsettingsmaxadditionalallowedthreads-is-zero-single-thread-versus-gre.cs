// Title: Benchmark BarCodeReader single‑thread vs multi‑thread performance
// Description: Demonstrates measuring recognition time of PDF417 barcodes using Aspose.BarCode with different processor thread settings.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning category, showcasing how to configure BarCodeReader.ProcessorSettings (UseAllCores, UseOnlyThisCoresCount, MaxAdditionalAllowedThreads) for single‑thread and multi‑thread scenarios. Developers often need to benchmark barcode recognition to choose optimal threading settings for high‑throughput applications.
// Prompt: Write a benchmark comparing performance when ProcessorSettings.MaxAdditionalAllowedThreads is zero (single‑thread) versus greater than zero.
// Tags: pdf417, barcode recognition, performance benchmark, multithreading, aspose.barcode, processor settings

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a benchmark for Aspose.BarCode barcode recognition using different threading configurations.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, runs single‑thread and multi‑thread benchmarks, and outputs timing results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a set of sample barcode images
        int sampleCount = 5;
        string[] files = GenerateSampleBarcodes(tempFolder, sampleCount);

        // Benchmark single‑thread recognition (MaxAdditionalAllowedThreads = 0)
        long singleThreadMs = BenchmarkRead(files, true);
        Console.WriteLine($"Single‑thread recognition time: {singleThreadMs} ms");

        // Benchmark multi‑thread recognition (MaxAdditionalAllowedThreads > 0)
        long multiThreadMs = BenchmarkRead(files, false);
        Console.WriteLine($"Multi‑thread recognition time: {multiThreadMs} ms");

        // Clean up temporary files and folder
        foreach (string f in files)
        {
            try { File.Delete(f); } catch { }
        }
        try { Directory.Delete(tempFolder, true); } catch { }
    }

    /// <summary>
    /// Generates a specified number of PDF417 barcode images and saves them as PNG files.
    /// </summary>
    /// <param name="folder">The folder where images will be saved.</param>
    /// <param name="count">Number of barcode images to generate.</param>
    /// <returns>Array of file paths to the generated images.</returns>
    static string[] GenerateSampleBarcodes(string folder, int count)
    {
        string[] paths = new string[count];
        for (int i = 0; i < count; i++)
        {
            string filePath = Path.Combine(folder, $"sample_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, $"Sample{i}"))
            {
                // Save the barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            paths[i] = filePath;
        }
        return paths;
    }

    /// <summary>
    /// Measures the time required to read a collection of barcode images using either single‑thread or multi‑thread settings.
    /// </summary>
    /// <param name="files">Array of barcode image file paths.</param>
    /// <param name="singleThread">If true, configures processor for single‑thread execution; otherwise multi‑thread.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    static long BenchmarkRead(string[] files, bool singleThread)
    {
        // Configure processor settings based on the desired threading mode
        if (singleThread)
        {
            BarCodeReader.ProcessorSettings.UseAllCores = false;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;
        }
        else
        {
            BarCodeReader.ProcessorSettings.UseAllCores = true;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Math.Max(2, Environment.ProcessorCount);
        }

        Stopwatch sw = Stopwatch.StartNew();

        // Process each barcode image
        foreach (string file in files)
        {
            using (var reader = new BarCodeReader(file, DecodeType.Pdf417))
            {
                // Perform recognition
                reader.ReadBarCodes();

                // Output results to prevent compiler optimizations from removing the call
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}