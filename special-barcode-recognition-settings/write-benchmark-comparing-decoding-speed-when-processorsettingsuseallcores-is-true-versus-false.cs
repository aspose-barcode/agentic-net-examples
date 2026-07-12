// Title: Benchmark decoding speed with and without multi‑core processing
// Description: Demonstrates measuring the time required to decode a set of Code128 barcodes using Aspose.BarCode, comparing ProcessorSettings.UseAllCores true vs false.
// Category-Description: This example belongs to the Aspose.BarCode decoding performance category. It shows how to generate barcodes, configure the BarCodeReader processor settings, and benchmark decoding using BarCodeReader and DecodeType. Developers often need to evaluate multi‑core decoding impact for bulk barcode processing scenarios.
// Prompt: Write a benchmark comparing decoding speed when ProcessorSettings.UseAllCores is true versus false.
// Tags: barcode symbology, decoding, performance, benchmark, code128, aspnet, aspose.barcode, processorsettings, useallcores

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Common;

/// <summary>
/// Provides a simple benchmark that compares barcode decoding speed when
/// <see cref="BarCodeReader.ProcessorSettings.UseAllCores"/> is enabled versus disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// Generates sample Code128 barcodes, runs two decoding measurements,
    /// and writes the elapsed times to the console.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Generate a collection of in‑memory barcode images (PNG format)
        // --------------------------------------------------------------------
        List<MemoryStream> barcodeStreams = new List<MemoryStream>();
        for (int i = 0; i < 5; i++)
        {
            // Create a Code128 barcode with distinct text for each iteration
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                MemoryStream ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream for later reading
                barcodeStreams.Add(ms);
            }
        }

        // ---------------------------------------------------------------
        // 2. Measure decoding time with multi‑core processing enabled
        // ---------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        double timeAllCores = MeasureDecodingTime(barcodeStreams);

        // ---------------------------------------------------------------
        // 3. Measure decoding time with single‑core processing
        // ---------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        double timeSingleCore = MeasureDecodingTime(barcodeStreams);

        // ---------------------------------------------------------------
        // 4. Output benchmark results
        // ---------------------------------------------------------------
        Console.WriteLine($"Decoding time with UseAllCores = true : {timeAllCores} ms");
        Console.WriteLine($"Decoding time with UseAllCores = false: {timeSingleCore} ms");

        // ---------------------------------------------------------------
        // 5. Release all memory streams
        // ---------------------------------------------------------------
        foreach (MemoryStream ms in barcodeStreams)
        {
            ms.Dispose();
        }
    }

    /// <summary>
    /// Measures the total time required to decode a list of barcode image streams.
    /// </summary>
    /// <param name="streams">The collection of barcode image streams to decode.</param>
    /// <returns>Total elapsed time in milliseconds.</returns>
    private static double MeasureDecodingTime(List<MemoryStream> streams)
    {
        Stopwatch sw = Stopwatch.StartNew();

        foreach (MemoryStream ms in streams)
        {
            // Ensure the stream is positioned at the beginning before each read
            ms.Position = 0;

            // Initialize a reader that supports all barcode types
            using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes (results are ignored)
                foreach (var result in reader.ReadBarCodes())
                {
                    // No additional processing required; iteration forces decoding
                }
            }
        }

        sw.Stop();
        return sw.Elapsed.TotalMilliseconds;
    }
}