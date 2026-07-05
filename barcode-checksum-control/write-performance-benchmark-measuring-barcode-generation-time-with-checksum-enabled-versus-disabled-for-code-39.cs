// Title: Code39 Barcode Generation Performance Benchmark
// Description: Measures the time required to generate Code 39 barcodes with checksum enabled versus disabled.
// Prompt: Write a performance benchmark measuring barcode generation time with checksum enabled versus disabled for Code 39.
// Tags: code39, checksum, performance, benchmark, barcode, aspose.barcode, generation, csharp

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates a simple performance benchmark for Code 39 barcode generation
/// with and without checksum using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// </summary>
    static void Main()
    {
        // Define the barcode text and number of iterations for the benchmark.
        const string codeText = "ABC123";
        const int iterations = 5;

        // Measure generation time with checksum enabled.
        long timeWithChecksum = MeasureGenerationTime(codeText, iterations, EnableChecksum.Yes);
        // Measure generation time with checksum disabled.
        long timeWithoutChecksum = MeasureGenerationTime(codeText, iterations, EnableChecksum.No);

        // Output the measured times to the console.
        Console.WriteLine($"Code39 generation time with checksum enabled: {timeWithChecksum} ms");
        Console.WriteLine($"Code39 generation time with checksum disabled: {timeWithoutChecksum} ms");
    }

    /// <summary>
    /// Measures the time taken to generate a barcode a specified number of times.
    /// </summary>
    /// <param name="text">The text to encode in the barcode.</param>
    /// <param name="count">Number of barcode generations to perform.</param>
    /// <param name="checksumSetting">Whether to enable checksum for the barcode.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    static long MeasureGenerationTime(string text, int count, EnableChecksum checksumSetting)
    {
        // Initialize a stopwatch to capture elapsed time.
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Generate the barcode repeatedly according to the count.
        for (int i = 0; i < count; i++)
        {
            // Create a new barcode generator for Code39FullASCII symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, text))
            {
                // Apply the checksum setting.
                generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;

                // Save the generated barcode to a memory stream (PNG format) to force rendering.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }
        }

        // Stop the stopwatch and return the elapsed milliseconds.
        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}