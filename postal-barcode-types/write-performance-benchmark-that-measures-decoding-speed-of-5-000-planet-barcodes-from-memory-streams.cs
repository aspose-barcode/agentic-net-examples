// Title: Performance benchmark for decoding Planet barcodes from memory streams
// Description: Demonstrates measuring the decoding speed of multiple Planet barcodes stored in memory streams. Useful for evaluating throughput of Aspose.BarCode's decoder.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to generate barcodes with BarcodeGenerator and decode them with BarCodeReader. Developers often need to benchmark decoding performance for high‑volume scenarios such as bulk scanning or real‑time processing, and this snippet provides a template for measuring throughput using EncodeTypes, DecodeType, and memory streams. It serves as a reference for performance testing across various barcode symbologies.
// Prompt: Write a performance benchmark that measures decoding speed of 5,000 Planet barcodes from memory streams.
// Tags: planet, barcode, decoding, performance, benchmark, memory-stream, aspose.barcode, generation, recognition

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates a simple performance benchmark that decodes a set of Planet barcodes
/// generated in memory, measuring total and average decoding time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark. Generates a collection of Planet barcodes,
    /// performs a warm‑up decode, then measures the time required to decode all barcodes.
    /// </summary>
    static void Main()
    {
        // Number of barcodes to process (kept small for safe execution)
        const int barcodeCount = 10;

        // Prepare a list to hold generated barcode images in memory
        var barcodeStreams = new List<MemoryStream>(barcodeCount);

        // Generate Planet barcodes and store them in memory streams
        for (int i = 0; i < barcodeCount; i++)
        {
            // Each barcode will contain a unique text value
            string codeText = $"Planet{i:D4}";

            using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
            {
                // Save the barcode image to a memory stream in PNG format
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                // Reset position so it can be read later
                ms.Position = 0;
                barcodeStreams.Add(ms);
            }
        }

        // Warm‑up: decode a single barcode to avoid one‑time overhead affecting the benchmark
        using (var warmReader = new BarCodeReader(barcodeStreams[0], DecodeType.Planet))
        {
            warmReader.ReadBarCodes();
        }

        // Start measuring decoding performance
        var stopwatch = Stopwatch.StartNew();

        foreach (var stream in barcodeStreams)
        {
            // Ensure the stream is positioned at the beginning before each read
            stream.Position = 0;

            using (var reader = new BarCodeReader(stream, DecodeType.Planet))
            {
                // Perform the decoding; result is not used further in this benchmark
                reader.ReadBarCodes();
            }
        }

        stopwatch.Stop();

        // Output benchmark results
        Console.WriteLine($"Decoded {barcodeCount} Planet barcodes in {stopwatch.Elapsed.TotalMilliseconds:F2} ms.");
        Console.WriteLine($"Average time per barcode: {stopwatch.Elapsed.TotalMilliseconds / barcodeCount:F2} ms.");

        // Clean up memory streams
        foreach (var stream in barcodeStreams)
        {
            stream.Dispose();
        }
    }
}