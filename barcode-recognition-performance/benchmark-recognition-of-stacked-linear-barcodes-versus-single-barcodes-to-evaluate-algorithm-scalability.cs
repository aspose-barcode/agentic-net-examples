// Title: Benchmark Stacked vs Single Linear Barcode Recognition
// Description: Demonstrates how to measure recognition performance of stacked linear barcodes compared to regular single‑line barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category, illustrating the use of BarcodeGenerator, BarCodeReader, and related classes to generate PNG images and evaluate decoding speed. Developers often need to assess scalability of barcode recognition algorithms across different symbologies such as Databar stacked types and common linear codes like Code128, EAN13, and UPCA.
// Prompt: Benchmark recognition of stacked linear barcodes versus single barcodes to evaluate algorithm scalability.
// Tags: barcode, benchmark, recognition, stacked, linear, databar, code128, ean13, upca, aspose.barcode, performance

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a console application that benchmarks the recognition speed of stacked linear barcodes
/// against standard single‑line barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, measures decoding time, and prints total and average results.
    /// </summary>
    static void Main()
    {
        // Define barcode types for stacked linear barcodes
        var stackedBarcodes = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.DatabarStacked, "(01)12345678901231"),
            (EncodeTypes.DatabarStackedOmniDirectional, "(01)12345678901231"),
            (EncodeTypes.DatabarExpandedStacked, "(01)12345678901231")
        };

        // Define barcode types for single linear barcodes
        var singleBarcodes = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code128, "ABC123XYZ"),
            (EncodeTypes.EAN13, "1234567890128"),
            (EncodeTypes.UPCA, "012345678905")
        };

        // Number of samples to generate per barcode type
        const int samplesPerType = 5;

        // Benchmark stacked barcodes
        Console.WriteLine("Benchmarking stacked linear barcodes...");
        var stackedResult = BenchmarkBarcodes(stackedBarcodes, samplesPerType);
        Console.WriteLine($"Total read time (stacked): {stackedResult.TotalMilliseconds} ms for {stackedResult.TotalCount} reads");
        Console.WriteLine($"Average read time (stacked): {stackedResult.AverageMilliseconds:F2} ms");
        Console.WriteLine();

        // Benchmark single barcodes
        Console.WriteLine("Benchmarking single linear barcodes...");
        var singleResult = BenchmarkBarcodes(singleBarcodes, samplesPerType);
        Console.WriteLine($"Total read time (single): {singleResult.TotalMilliseconds} ms for {singleResult.TotalCount} reads");
        Console.WriteLine($"Average read time (single): {singleResult.AverageMilliseconds:F2} ms");
    }

    // Holds aggregated benchmark data
    private struct BenchmarkResult
    {
        public long TotalMilliseconds;
        public int TotalCount;
        public double AverageMilliseconds => TotalCount == 0 ? 0 : (double)TotalMilliseconds / TotalCount;
    }

    // Generates barcodes, reads them, and measures recognition time
    private static BenchmarkResult BenchmarkBarcodes(List<(BaseEncodeType type, string text)> definitions, int samplesPerType)
    {
        var result = new BenchmarkResult();

        foreach (var (encodeType, codeText) in definitions)
        {
            for (int i = 0; i < samplesPerType; i++)
            {
                // Generate barcode image in memory
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0;

                        // Measure time taken to decode the barcode
                        var sw = Stopwatch.StartNew();
                        using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                        {
                            // Read all barcodes in the image (expected to be one)
                            foreach (var _ in reader.ReadBarCodes())
                            {
                                // No additional processing required
                            }
                        }
                        sw.Stop();

                        result.TotalMilliseconds += sw.ElapsedMilliseconds;
                        result.TotalCount++;
                    }
                }
            }
        }

        return result;
    }
}