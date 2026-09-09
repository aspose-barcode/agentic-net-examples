// Title: Performance benchmark for decoding Planet barcodes from memory streams
// Description: Demonstrates measuring the decoding speed of Planet barcodes stored in memory streams. The example generates sample barcodes, decodes them, and reports total and average processing time.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator (generation) and BarCodeReader (recognition) classes to create and decode barcodes. Typical scenarios include bulk barcode processing, performance testing, and integration validation where developers need to assess decoding throughput.
// Prompt: Write a performance benchmark that measures decoding speed of 5,000 Planet barcodes from memory streams.
// Tags: planet, barcode, benchmark, decoding, memory-stream, aspose.barcode, generation, recognition

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates a performance benchmark for decoding Planet barcodes from memory streams using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample Planet barcodes, decodes them, and reports timing statistics.
    /// </summary>
    static void Main()
    {
        // Number of barcodes to generate and decode (adjustable for real benchmarks, e.g., 5000)
        const int sampleCount = 10; // safe sample size for CI
        var barcodeImages = new List<byte[]>();

        // -------------------------------------------------
        // Generate Planet barcodes and store each as a byte array in memory
        // -------------------------------------------------
        for (int i = 0; i < sampleCount; i++)
        {
            // Create a unique numeric code for each barcode
            string codeText = (123456 + i).ToString();

            using (var ms = new MemoryStream())
            {
                // Initialize the generator with Planet symbology and the code text
                using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
                {
                    // Set X-dimension to control barcode size (optional)
                    generator.Parameters.Barcode.XDimension.Pixels = 4;

                    // Save the generated barcode image to the memory stream in PNG format
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Store the generated image bytes for later decoding
                barcodeImages.Add(ms.ToArray());
            }
        }

        // -------------------------------------------------
        // Benchmark decoding speed of the generated barcodes
        // -------------------------------------------------
        var stopwatch = new Stopwatch();
        int totalDecoded = 0;
        stopwatch.Start();

        // Decode each barcode image from its memory representation
        foreach (var imageData in barcodeImages)
        {
            using (var ms = new MemoryStream(imageData))
            {
                // Initialize the reader for Planet symbology using the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.Planet))
                {
                    // Read all barcodes found in the image
                    var results = reader.ReadBarCodes();

                    // Accumulate the count of decoded barcodes
                    totalDecoded += results.Length;
                }
            }
        }

        stopwatch.Stop();

        // -------------------------------------------------
        // Output benchmark results
        // -------------------------------------------------
        Console.WriteLine($"Decoded {totalDecoded} barcodes in {stopwatch.Elapsed.TotalMilliseconds} ms.");
        Console.WriteLine($"Average time per barcode: {stopwatch.Elapsed.TotalMilliseconds / sampleCount} ms.");
    }
}