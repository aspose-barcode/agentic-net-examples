using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates benchmarking of barcode recognition using Aspose.BarCode.
/// Generates sample barcodes, measures recognition time in single‑ and multi‑threaded modes,
/// and outputs the results to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// Generates a set of barcode images, runs recognition benchmarks, and disposes resources.
    /// </summary>
    static void Main()
    {
        // Create a small collection of barcode images held in memory.
        var barcodeStreams = GenerateSampleBarcodes(5);

        // Benchmark single‑threaded processing (no additional threads allowed).
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;
        long singleThreadMs = MeasureRecognitionTime(barcodeStreams);
        Console.WriteLine($"Single‑threaded recognition time: {singleThreadMs} ms");

        // Benchmark multi‑threaded processing (allow as many extra threads as there are processors).
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount;
        long multiThreadMs = MeasureRecognitionTime(barcodeStreams);
        Console.WriteLine($"Multi‑threaded recognition time: {multiThreadMs} ms");

        // Release all memory streams.
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }
    }

    // Generates the specified number of Code128 barcode images and returns them as MemoryStreams.
    private static List<MemoryStream> GenerateSampleBarcodes(int count)
    {
        var streams = new List<MemoryStream>();

        for (int i = 0; i < count; i++)
        {
            var ms = new MemoryStream();

            // Use a unique code text for each image (e.g., CODE0001, CODE0002, ...).
            string codeText = $"CODE{i + 1:D4}";

            // Create a barcode generator for Code128 and save the image directly into the stream.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position so it can be read from the beginning.
            ms.Position = 0;
            streams.Add(ms);
        }

        return streams;
    }

    // Measures the total time (in milliseconds) required to recognize all barcodes in the provided streams.
    private static long MeasureRecognitionTime(List<MemoryStream> streams)
    {
        var stopwatch = Stopwatch.StartNew();

        foreach (var ms in streams)
        {
            // Create a new reader for each stream; it is disposed automatically after use.
            using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
            {
                // ReadBarCodes returns an array of detection results.
                var results = reader.ReadBarCodes();

                // Output each detected barcode to the console (prevents compiler optimizations from removing the call).
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                }
            }

            // Reset the stream position for the next iteration/read.
            ms.Position = 0;
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}