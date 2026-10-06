// Title: Benchmark Code 39 barcode generation with and without checksum
// Description: Demonstrates measuring the time required to generate Code 39 barcodes when the checksum is enabled versus disabled.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category, showcasing how to use BarcodeGenerator, EncodeTypes, and generation parameters to evaluate runtime. Typical use cases include assessing the impact of checksum settings on processing speed for bulk barcode creation. Developers often need such benchmarks to choose optimal settings for high‑throughput applications.
// Prompt: Write a performance benchmark measuring barcode generation time with checksum enabled versus disabled for Code 39.
// Tags: code39, checksum, performance, benchmark, png, aspose.barcode, generation

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple performance benchmark for generating Code 39 barcodes with checksum enabled and disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes the benchmark and outputs elapsed times for both checksum settings.
    /// </summary>
    static void Main()
    {
        // Sample texts to encode as Code 39 barcodes
        string[] sampleTexts = { "CODE39A", "CODE39B", "CODE39C", "CODE39D", "CODE39E" };

        // Measure time with checksum enabled
        TimeSpan enabledDuration = Benchmark(sampleTexts, true);
        // Measure time with checksum disabled
        TimeSpan disabledDuration = Benchmark(sampleTexts, false);

        // Output the benchmark results
        Console.WriteLine($"Checksum Enabled total time: {enabledDuration.TotalMilliseconds} ms");
        Console.WriteLine($"Checksum Disabled total time: {disabledDuration.TotalMilliseconds} ms");
    }

    /// <summary>
    /// Runs a benchmark that generates barcodes for the provided texts using the specified checksum setting.
    /// </summary>
    /// <param name="texts">Array of strings to encode.</param>
    /// <param name="enableChecksum">True to enable checksum; false to disable.</param>
    /// <returns>The elapsed time for the entire operation.</returns>
    static TimeSpan Benchmark(string[] texts, bool enableChecksum)
    {
        // Start timing
        Stopwatch sw = Stopwatch.StartNew();

        foreach (string txt in texts)
        {
            // Create a barcode generator for Code 39 Full ASCII
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, txt))
            {
                // Set checksum option based on the benchmark parameter
                generator.Parameters.Barcode.IsChecksumEnabled = enableChecksum ? EnableChecksum.Yes : EnableChecksum.No;

                // Save the generated barcode to a memory stream in PNG format
                using (MemoryStream ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }
        }

        // Stop timing and return the elapsed duration
        sw.Stop();
        return sw.Elapsed;
    }
}