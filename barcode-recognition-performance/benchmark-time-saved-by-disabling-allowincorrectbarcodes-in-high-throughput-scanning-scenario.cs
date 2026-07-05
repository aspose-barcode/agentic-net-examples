// Title: Benchmarking AllowIncorrectBarcodes Impact on Scan Speed
// Description: Generates a set of Code128 barcodes, reads them with and without AllowIncorrectBarcodes, and measures the time difference to illustrate performance gains in high‑throughput scenarios.
// Prompt: Benchmark the time saved by disabling AllowIncorrectBarcodes in a high‑throughput scanning scenario.
// Tags: barcode, code128, performance, benchmark, allowincorrectbarcodes, Aspose.BarCode

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how disabling <c>AllowIncorrectBarcodes</c> can improve scanning performance
/// by benchmarking read operations on a collection of generated Code128 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, runs two benchmarks
    /// (with and without <c>AllowIncorrectBarcodes</c>), and prints the timing results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Generate a small set of barcode images in memory.
        // --------------------------------------------------------------------
        var barcodeStreams = new List<MemoryStream>();
        for (int i = 0; i < 5; i++)
        {
            // Create a barcode generator for Code128 with unique text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Test{i}12345"))
            {
                // Set a simple size parameter (X-dimension) for readability.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Render the barcode to a bitmap.
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the bitmap to a memory stream in PNG format.
                    var ms = new MemoryStream();
                    bitmap.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // Reset position for later reading.
                    barcodeStreams.Add(ms);
                }
            }
        }

        // --------------------------------------------------------------------
        // 2. Benchmark reading with AllowIncorrectBarcodes disabled (default).
        // --------------------------------------------------------------------
        long timeWithout = BenchmarkReading(barcodeStreams, allowIncorrect: false);

        // --------------------------------------------------------------------
        // 3. Benchmark reading with AllowIncorrectBarcodes enabled.
        // --------------------------------------------------------------------
        long timeWith = BenchmarkReading(barcodeStreams, allowIncorrect: true);

        // --------------------------------------------------------------------
        // 4. Output the timing comparison.
        // --------------------------------------------------------------------
        Console.WriteLine($"Reading time without AllowIncorrectBarcodes: {timeWithout} ms");
        Console.WriteLine($"Reading time with    AllowIncorrectBarcodes: {timeWith} ms");
        Console.WriteLine($"Time saved (approx.): {timeWithout - timeWith} ms");
    }

    /// <summary>
    /// Measures the time required to read a collection of barcode streams using the specified
    /// <c>AllowIncorrectBarcodes</c> setting.
    /// </summary>
    /// <param name="streams">The barcode image streams to be read.</param>
    /// <param name="allowIncorrect">If true, the reader will tolerate incorrect barcodes.</param>
    /// <returns>The elapsed time in milliseconds.</returns>
    static long BenchmarkReading(List<MemoryStream> streams, bool allowIncorrect)
    {
        var stopwatch = Stopwatch.StartNew();

        foreach (var stream in streams)
        {
            // Ensure the stream is positioned at the beginning before each read.
            stream.Position = 0;

            // Initialize a barcode reader for Code128.
            using (var reader = new BarCodeReader(stream, DecodeType.Code128))
            {
                // Apply the quality setting based on the benchmark parameter.
                reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;

                // Perform the read operation and iterate over all detected barcodes.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Access the result to simulate processing (e.g., logging).
                    Console.WriteLine($"Detected: {result.CodeText}");
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}