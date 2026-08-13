// Title: Benchmark DotCode barcode generation speed across encoding modes
// Description: Demonstrates measuring the time required to generate DotCode barcodes using various encoding modes, useful for performance tuning.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category, showcasing how to use BarcodeGenerator with DotCode symbology, configure encoding modes, and run parallel benchmarks. Developers often need to compare generation speed for different settings to optimize high‑throughput applications.
// Prompt: Benchmark generation speed of DotCode barcodes using different encoding modes in parallel.
// Tags: dotcode, barcode, performance, benchmark, parallel, aspose.barcode, generation

using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates benchmarking the generation speed of DotCode barcodes using different encoding modes in parallel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes parallel benchmarks for each DotCode encoding mode and prints elapsed times.
    /// </summary>
    static void Main()
    {
        // Define the encoding modes to benchmark
        var modes = new DotCodeEncodeMode[]
        {
            DotCodeEncodeMode.Auto,
            DotCodeEncodeMode.Binary,
            DotCodeEncodeMode.ECI,
            DotCodeEncodeMode.Extended
        };

        // Thread‑safe collection to store benchmark results
        var results = new ConcurrentDictionary<DotCodeEncodeMode, long>();

        // Run benchmarks for each mode concurrently
        Parallel.ForEach(modes, mode =>
        {
            long elapsedMs = BenchmarkMode(mode);
            results[mode] = elapsedMs;
        });

        // Output the measured generation times
        foreach (var kvp in results)
        {
            Console.WriteLine($"Mode {kvp.Key}: {kvp.Value} ms");
        }
    }

    // Benchmarks a single DotCode encoding mode
    static long BenchmarkMode(DotCodeEncodeMode mode)
    {
        const string codeText = "Sample123";

        // Initialize generator for DotCode symbology with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Apply the specific encode mode to the generator
            generator.Parameters.Barcode.DotCode.EncodeMode = mode;

            // For ECI mode, specify the character encoding (UTF‑8 in this example)
            if (mode == DotCodeEncodeMode.ECI)
            {
                generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.UTF8;
            }

            // Use a memory stream to avoid file I/O overhead during timing
            using (var ms = new MemoryStream())
            {
                var sw = Stopwatch.StartNew();
                generator.Save(ms, BarCodeImageFormat.Png);
                sw.Stop();
                return sw.ElapsedMilliseconds;
            }
        }
    }
}