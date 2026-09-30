// Title: Benchmark Code39 barcode generation with and without checksum
// Description: Demonstrates measuring the time required to generate Code 39 barcodes using Aspose.BarCode, comparing performance when the checksum is enabled versus disabled.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category, illustrating how to use the BarcodeGenerator class together with EncodeTypes.Code39 and the IsChecksumEnabled property. Developers often need to evaluate generation speed for different barcode settings, such as enabling checksums, to make informed decisions for high‑throughput applications. The snippet shows typical use of Stopwatch for timing, MemoryStream for in‑memory image creation, and common output formats like PNG.
// Prompt: Write a performance benchmark measuring barcode generation time with checksum enabled versus disabled for Code 39.
// Tags: code39, checksum, performance, benchmark, barcode, aspose.barcode, generation, png

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides a simple performance benchmark for generating Code 39 barcodes
/// with checksum enabled and disabled using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// Measures and prints the elapsed time for barcode generation under two settings.
    /// </summary>
    static void Main()
    {
        // Barcode data to encode
        const string codeText = "123ABC";

        // Number of iterations for each benchmark run
        const int iterations = 10;

        // Benchmark with checksum enabled
        long enabledTicks = Benchmark(codeText, EnableChecksum.Yes, iterations);

        // Benchmark with checksum disabled
        long disabledTicks = Benchmark(codeText, EnableChecksum.No, iterations);

        // Convert elapsed ticks to milliseconds for readability
        double enabledMs = enabledTicks * 1000.0 / Stopwatch.Frequency;
        double disabledMs = disabledTicks * 1000.0 / Stopwatch.Frequency;

        // Output the results
        Console.WriteLine($"Code39 (checksum enabled)  : {enabledMs:F2} ms over {iterations} runs");
        Console.WriteLine($"Code39 (checksum disabled) : {disabledMs:F2} ms over {iterations} runs");
    }

    /// <summary>
    /// Executes the barcode generation repeatedly and returns the elapsed tick count.
    /// </summary>
    /// <param name="text">The text to encode in the barcode.</param>
    /// <param name="checksumSetting">Whether to enable checksum calculation.</param>
    /// <param name="repeatCount">Number of times to generate the barcode.</param>
    /// <returns>Total elapsed ticks for the repeated generation.</returns>
    private static long Benchmark(string text, EnableChecksum checksumSetting, int repeatCount)
    {
        // Warm‑up call to avoid one‑time initialization overhead affecting timing
        GenerateBarcode(text, checksumSetting);

        // Start timing
        var sw = Stopwatch.StartNew();

        // Generate the barcode repeatedly
        for (int i = 0; i < repeatCount; i++)
        {
            GenerateBarcode(text, checksumSetting);
        }

        // Stop timing and return elapsed ticks
        sw.Stop();
        return sw.ElapsedTicks;
    }

    /// <summary>
    /// Generates a Code 39 barcode image in memory using the specified checksum setting.
    /// </summary>
    /// <param name="text">The text to encode.</param>
    /// <param name="checksumSetting">Checksum enable flag.</param>
    private static void GenerateBarcode(string text, EnableChecksum checksumSetting)
    {
        // Create a barcode generator for Code 39
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, text))
        {
            // Apply the checksum setting
            generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;

            // Save the generated barcode to a memory stream as PNG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }
        }
    }
}