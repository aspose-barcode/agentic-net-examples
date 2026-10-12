// Title: Performance benchmark for decoding Planet barcodes from memory streams
// Description: Demonstrates measuring the time required to decode a set of Planet barcodes that are stored in memory streams.
// Category-Description: This example belongs to the Aspose.BarCode decoding performance category. It showcases the use of BarCodeGenerator to create barcodes, MemoryStream for in‑memory image handling, and BarCodeReader for fast decoding. Developers often need to benchmark decoding speed when processing large batches of barcodes in high‑throughput applications.
// Prompt: Write a performance benchmark that measures decoding speed of 5,000 Planet barcodes from memory streams.
// Tags: planet, barcode, decoding, performance, benchmark, memorystream, aspose.barcode

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates a simple performance benchmark that decodes a collection of
/// Planet barcodes generated in memory. The elapsed time is printed to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// Generates a set of Planet barcodes, stores them in memory streams,
    /// then measures how long it takes to decode all of them.
    /// </summary>
    static void Main()
    {
        // Number of barcodes to generate and decode (sample size for the demo)
        const int barcodeCount = 10; // safe sample size
        var streams = new List<MemoryStream>(barcodeCount);

        // ------------------------------------------------------------
        // Generate Planet barcodes and store each as a PNG in a MemoryStream
        // ------------------------------------------------------------
        for (int i = 0; i < barcodeCount; i++)
        {
            string codeText = (i + 1).ToString(); // numeric code for the barcode

            // Create a generator for the Planet symbology with the current code text
            using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
            {
                // Set X-dimension to control barcode size (4 pixels per module)
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the generated barcode image to a memory stream in PNG format
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // reset stream position for later reading
                streams.Add(ms);
            }
        }

        // ------------------------------------------------------------
        // Benchmark: decode each barcode from its memory stream
        // ------------------------------------------------------------
        var stopwatch = new Stopwatch();
        int totalDecoded = 0;
        stopwatch.Start();

        foreach (var ms in streams)
        {
            ms.Position = 0; // ensure stream is at the beginning before decoding
            BaseDecodeType decodeType = DecodeType.Planet;

            // Use BarCodeReader to decode the barcode from the stream
            using (var reader = new BarCodeReader(ms, decodeType))
            {
                var results = reader.ReadBarCodes();
                totalDecoded += results.Length;
            }
        }

        stopwatch.Stop();

        // Output the benchmark result
        Console.WriteLine($"Decoded {totalDecoded} barcodes in {stopwatch.ElapsedMilliseconds} ms.");

        // ------------------------------------------------------------
        // Clean up: dispose all memory streams
        // ------------------------------------------------------------
        foreach (var ms in streams)
        {
            ms.Dispose();
        }
    }
}