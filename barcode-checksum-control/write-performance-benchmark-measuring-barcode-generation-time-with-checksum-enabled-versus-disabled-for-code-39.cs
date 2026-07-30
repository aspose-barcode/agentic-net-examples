// Title: Benchmark Code 39 barcode generation with and without checksum
// Description: Measures the time required to generate Code 39 barcodes using Aspose.BarCode, comparing checksum enabled versus disabled.
// Category-Description: Demonstrates performance testing of Aspose.BarCode generation APIs, focusing on the BarcodeGenerator class, EncodeTypes, and checksum settings. Useful for developers evaluating encoding speed for Code 39 in high‑throughput scenarios, such as batch processing or real‑time scanning applications.
// Prompt: Write a performance benchmark measuring barcode generation time with checksum enabled versus disabled for Code 39.
// Tags: code39, checksum, performance, benchmark, aspnet, aspose.barcode, generation, png

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Contains the entry point and benchmark logic for measuring barcode generation performance.
/// </summary>
class Program
{
    /// <summary>
    /// Executes the performance benchmark for Code 39 barcode generation with checksum enabled and disabled.
    /// </summary>
    static void Main()
    {
        const int iterations = 5;               // Number of times each test is run
        string codeText = "CODE39";             // Text to encode in the barcode

        // Benchmark with checksum enabled
        long enabledTicks = Benchmark(iterations, codeText, EnableChecksum.Yes);
        // Benchmark with checksum disabled
        long disabledTicks = Benchmark(iterations, codeText, EnableChecksum.No);

        // Convert elapsed ticks to milliseconds for reporting
        double enabledMs = enabledTicks * 1000.0 / Stopwatch.Frequency;
        double disabledMs = disabledTicks * 1000.0 / Stopwatch.Frequency;

        // Output total and average times for each scenario
        Console.WriteLine($"Checksum Enabled:  Total {enabledMs:F2} ms for {iterations} runs (avg {enabledMs / iterations:F2} ms)");
        Console.WriteLine($"Checksum Disabled: Total {disabledMs:F2} ms for {iterations} runs (avg {disabledMs / iterations:F2} ms)");
    }

    /// <summary>
    /// Runs the barcode generation loop <paramref name="count"/> times using the specified checksum setting.
    /// </summary>
    /// <param name="count">Number of barcode generations to perform.</param>
    /// <param name="text">The text to encode in the barcode.</param>
    /// <param name="checksumSetting">Whether to enable checksum calculation.</param>
    /// <returns>Total elapsed ticks for the benchmark run.</returns>
    static long Benchmark(int count, string text, EnableChecksum checksumSetting)
    {
        // Choose the Code 39 full ASCII symbology
        BaseEncodeType encodeType = EncodeTypes.Code39FullASCII;

        // Warm‑up the generator to mitigate JIT compilation overhead
        using (var warmGen = new BarcodeGenerator(encodeType, text))
        {
            warmGen.Parameters.Barcode.IsChecksumEnabled = checksumSetting;
            using (var warmMs = new MemoryStream())
            {
                warmGen.Save(warmMs, BarCodeImageFormat.Png);
            }
        }

        // Start timing the actual benchmark
        Stopwatch sw = Stopwatch.StartNew();

        for (int i = 0; i < count; i++)
        {
            // Create a new generator for each iteration to simulate typical usage
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;
                using (var ms = new MemoryStream())
                {
                    // Generate the barcode image in PNG format
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }
        }

        // Stop timing and return the elapsed ticks
        sw.Stop();
        return sw.ElapsedTicks;
    }
}