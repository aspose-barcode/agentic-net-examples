// Title: Benchmark ProcessorSettings.MaxAdditionalAllowedThreads performance
// Description: Demonstrates measuring barcode recognition speed using single‑thread vs multi‑thread settings.
// Category-Description: This example belongs to Aspose.BarCode performance tuning, showing how to configure BarCodeReader.ProcessorSettings for threading. It uses BarcodeGenerator to create sample Code128 barcodes and BarCodeReader to decode them, a common scenario when developers need to optimize bulk barcode processing in server or batch jobs.
// Prompt: Write a benchmark comparing performance when ProcessorSettings.MaxAdditionalAllowedThreads is zero (single‑thread) versus greater than zero.
// Tags: barcode, code128, performance, multithreading, aspnet, aspose.barcode, generation, recognition

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates benchmarking barcode recognition with different thread settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, runs single‑ and multi‑thread benchmarks, and outputs elapsed times.
    /// </summary>
    static void Main()
    {
        const int sampleCount = 5;
        var barcodeStreams = new List<MemoryStream>();

        // Generate sample barcode images in memory
        for (int i = 0; i < sampleCount; i++)
        {
            var codeText = $"Sample{i}";
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                barcodeStreams.Add(ms);
            }
        }

        // Benchmark with single‑thread (MaxAdditionalAllowedThreads = 0)
        long singleThreadTicks = RunBenchmark(barcodeStreams, 0);
        Console.WriteLine($"Single‑thread elapsed: {TimeSpan.FromTicks(singleThreadTicks).TotalMilliseconds} ms");

        // Benchmark with multi‑thread (MaxAdditionalAllowedThreads > 0)
        int additionalThreads = Math.Max(1, Environment.ProcessorCount - 1);
        long multiThreadTicks = RunBenchmark(barcodeStreams, additionalThreads);
        Console.WriteLine($"Multi‑thread (MaxAdditionalAllowedThreads={additionalThreads}) elapsed: {TimeSpan.FromTicks(multiThreadTicks).TotalMilliseconds} ms");

        // Clean up streams
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }
    }

    /// <summary>
    /// Runs the recognition benchmark on the provided streams using the specified thread count.
    /// </summary>
    /// <param name="streams">Memory streams containing barcode images.</param>
    /// <param name="maxAdditionalThreads">Maximum additional threads allowed for processing.</param>
    /// <returns>Elapsed ticks for the benchmark.</returns>
    static long RunBenchmark(List<MemoryStream> streams, int maxAdditionalThreads)
    {
        // Configure processor settings
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = maxAdditionalThreads;

        var stopwatch = Stopwatch.StartNew();

        foreach (var stream in streams)
        {
            // Ensure the stream is positioned at the beginning for each read
            stream.Position = 0;
            using (var reader = new BarCodeReader(stream, DecodeType.Code128))
            {
                // Perform recognition
                foreach (var result in reader.ReadBarCodes())
                {
                    // Access result to ensure processing (no output needed)
                    var _ = result.CodeText;
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedTicks;
    }
}