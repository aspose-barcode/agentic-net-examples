// Title: Barcode recognition threading performance benchmark
// Description: Demonstrates how to measure the impact of Aspose.BarCode's threading settings on barcode recognition speed using single‑thread and multi‑thread configurations.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning category, showcasing the use of BarCodeReader.ProcessorSettings (UseAllCores, UseOnlyThisCoresCount, MaxAdditionalAllowedThreads) to control threading. Typical use cases include optimizing large‑scale barcode processing pipelines where developers need to balance CPU utilization and latency.
// Prompt: Write a benchmark comparing performance when ProcessorSettings.MaxAdditionalAllowedThreads is zero (single‑thread) versus greater than zero.
// Tags: barcode, code128, pdf417, performance, threading, benchmark, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a simple benchmark that compares single‑thread and multi‑thread barcode recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample Code128 barcodes, then runs recognition benchmarks with different threading settings.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBench_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a set of sample barcode PNG files
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string codeText = "Sample" + i;
            string filePath = Path.Combine(tempFolder, $"code{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Run benchmark using a single thread (MaxAdditionalAllowedThreads = 0)
        Benchmark("Single‑Thread", barcodeFiles, maxAdditionalThreads: 0, useAllCores: false, onlyCoresCount: 1);

        // Run benchmark using multiple threads (MaxAdditionalAllowedThreads > 0)
        int additionalThreads = Environment.ProcessorCount * 2;
        Benchmark("Multi‑Thread", barcodeFiles, maxAdditionalThreads: additionalThreads, useAllCores: true, onlyCoresCount: 0);

        // Clean up temporary files and folder
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
    /// Executes a recognition benchmark for a list of barcode image files using specified processor settings.
    /// </summary>
    /// <param name="description">Label displayed in console output.</param>
    /// <param name="files">Paths to barcode image files.</param>
    /// <param name="maxAdditionalThreads">Value for ProcessorSettings.MaxAdditionalAllowedThreads.</param>
    /// <param name="useAllCores">Value for ProcessorSettings.UseAllCores.</param>
    /// <param name="onlyCoresCount">Value for ProcessorSettings.UseOnlyThisCoresCount.</param>
    static void Benchmark(string description, List<string> files, int maxAdditionalThreads, bool useAllCores, int onlyCoresCount)
    {
        // Apply threading configuration to the BarCodeReader processor
        BarCodeReader.ProcessorSettings.UseAllCores = useAllCores;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = onlyCoresCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = maxAdditionalThreads;

        Stopwatch totalWatch = Stopwatch.StartNew();

        int totalFound = 0;
        foreach (string file in files)
        {
            // Recognize PDF417 barcodes in the current image file
            using (var reader = new BarCodeReader(file, DecodeType.Pdf417))
            {
                Stopwatch watch = Stopwatch.StartNew();
                reader.ReadBarCodes();
                watch.Stop();

                totalFound += reader.FoundCount;
                Console.WriteLine($"{description}: File '{Path.GetFileName(file)}' read {reader.FoundCount} barcodes in {watch.ElapsedMilliseconds} ms");

                // Output each detected barcode's type and text
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    Console.WriteLine($"  {result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        totalWatch.Stop();
        Console.WriteLine($"{description} total: {totalFound} barcodes read in {totalWatch.ElapsedMilliseconds} ms");
        Console.WriteLine();
    }
}