// Title: Barcode generation benchmark for Code 39 with checksum toggle
// Description: Demonstrates measuring the time required to generate Code 39 barcodes with checksum enabled versus disabled.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category, showcasing how to use BarcodeGenerator, BarcodeParameters, and Stopwatch to evaluate generation speed. Developers often need to compare configuration impacts such as checksum, symbology settings, or image formats when optimizing barcode creation in high‑throughput applications.
// Prompt: Write a performance benchmark measuring barcode generation time with checksum enabled versus disabled for Code 39.
// Tags: barcode, code39, checksum, performance, benchmark, generation, aspose.barcode, stopwatch

using System;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates a simple performance benchmark for generating Code 39 barcodes with and without checksum enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Runs a generation benchmark for the specified parameters.
    /// </summary>
    /// <param name="count">Number of barcode images to generate.</param>
    /// <param name="text">Text to encode in the barcode.</param>
    /// <param name="enableChecksum">Whether to enable checksum calculation.</param>
    static void Benchmark(int count, string text, bool enableChecksum)
    {
        // Create a generator for Code 39 Full ASCII symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, text))
        {
            // Enable or disable checksum based on the parameter.
            generator.Parameters.Barcode.IsChecksumEnabled = enableChecksum ? EnableChecksum.Yes : EnableChecksum.No;

            // Start measuring elapsed time.
            var stopwatch = Stopwatch.StartNew();

            // Generate the specified number of barcode images.
            for (int i = 0; i < count; i++)
            {
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Image generated; no further action needed.
                }
            }

            // Stop timing.
            stopwatch.Stop();

            // Output the benchmark result.
            Console.WriteLine($"Checksum {(enableChecksum ? "Enabled" : "Disabled")}: {stopwatch.ElapsedMilliseconds} ms for {count} iterations.");
        }
    }

    /// <summary>
    /// Entry point that runs the benchmark for a sample text and iteration count.
    /// </summary>
    static void Main()
    {
        int sampleCount = 5;
        string sampleText = "CODE39TEST";

        // Benchmark without checksum.
        Benchmark(sampleCount, sampleText, false);
        // Benchmark with checksum.
        Benchmark(sampleCount, sampleText, true);
    }
}