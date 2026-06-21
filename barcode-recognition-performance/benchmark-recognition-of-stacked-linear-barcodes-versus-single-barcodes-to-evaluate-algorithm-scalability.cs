using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and recognition benchmarking using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes benchmark tests for selected barcode symbologies.
    /// </summary>
    static void Main()
    {
        const int sampleCount = 5; // Number of iterations per benchmark

        // Benchmark a single linear barcode (Code128)
        BenchmarkSymbology(
            "Code128 (single)",
            EncodeTypes.Code128,
            "ABC1234567",
            DecodeType.Code128,
            sampleCount);

        // Benchmark stacked linear barcodes (DataBar stacked variants)
        BenchmarkSymbology(
            "DataBar Stacked",
            EncodeTypes.DatabarStacked,
            "(01)12345678901231",
            DecodeType.DatabarStacked,
            sampleCount);

        BenchmarkSymbology(
            "DataBar Stacked OmniDirectional",
            EncodeTypes.DatabarStackedOmniDirectional,
            "(01)12345678901231",
            DecodeType.DatabarStackedOmniDirectional,
            sampleCount);
    }

    /// <summary>
    /// Generates a barcode, reads it back, and measures the average recognition time.
    /// </summary>
    /// <param name="label">Descriptive label for the benchmark output.</param>
    /// <param name="encodeType">The barcode symbology to generate.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="decodeType">The barcode symbology to use for recognition.</param>
    /// <param name="iterations">Number of times to repeat the test.</param>
    private static void BenchmarkSymbology(string label, BaseEncodeType encodeType, string codeText, BaseDecodeType decodeType, int iterations)
    {
        double totalMs = 0; // Accumulator for total elapsed time

        for (int i = 0; i < iterations; i++)
        {
            // Generate barcode image into a memory stream
            using (var ms = new MemoryStream())
            {
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Save the generated barcode as PNG into the stream
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Reset stream position to the beginning for reading
                ms.Position = 0;

                // Load the image from the stream as a Bitmap
                using (var bitmap = new Bitmap(ms))
                {
                    // Create a reader configured for the expected decode type
                    using (var reader = new BarCodeReader(bitmap, decodeType))
                    {
                        // Start timing the recognition process
                        var sw = Stopwatch.StartNew();

                        // Perform barcode detection
                        var results = reader.ReadBarCodes();

                        // Stop timing
                        sw.Stop();

                        // Accumulate elapsed milliseconds
                        totalMs += sw.Elapsed.TotalMilliseconds;

                        // Simple validation to ensure detection succeeded
                        if (results.Length == 0)
                        {
                            Console.WriteLine($"{label}: No barcode detected in iteration {i + 1}");
                        }
                    }
                }
            }
        }

        // Compute and display the average recognition time
        double avgMs = totalMs / iterations;
        Console.WriteLine($"{label}: Average recognition time over {iterations} runs = {avgMs:F2} ms");
    }
}