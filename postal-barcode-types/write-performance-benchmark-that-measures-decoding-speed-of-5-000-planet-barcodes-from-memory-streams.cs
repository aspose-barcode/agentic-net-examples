// Title: Performance benchmark for decoding Planet barcodes from memory streams
// Description: Demonstrates generating Planet barcodes in memory and measuring the time required to decode them using Aspose.BarCode APIs.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes, the BarCodeReader for decoding, and common performance‑testing patterns. Developers working with bulk barcode processing, automated scanning, or benchmarking barcode libraries will find this pattern useful for evaluating throughput and latency.
// Prompt: Write a performance benchmark that measures decoding speed of 5,000 Planet barcodes from memory streams.
// Tags: planet, barcode, performance, benchmark, memory, generation, recognition, aspose.barcode

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
/// Example program that generates a set of Planet barcodes in memory,
/// then benchmarks the time required to decode them using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// </summary>
    static void Main()
    {
        // Number of barcodes to generate and decode (adjustable for real benchmarks)
        const int barcodeCount = 10; // safe sample size
        var images = new List<byte[]>();

        // ------------------------------------------------------------
        // Generate Planet barcodes and store each image as a PNG byte array
        // ------------------------------------------------------------
        for (int i = 0; i < barcodeCount; i++)
        {
            // Create a numeric code padded to 12 digits (e.g., "000000000001")
            string codeText = (i + 1).ToString("D12");

            // Initialize the generator for Planet symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
            {
                // Set X-dimension to 4 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Generate the barcode image as a Bitmap
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the bitmap to a memory stream in PNG format
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        // Store the raw PNG bytes for later decoding
                        images.Add(ms.ToArray());
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // Benchmark decoding of the in‑memory barcode images
        // ------------------------------------------------------------
        var stopwatch = Stopwatch.StartNew();
        int decodedCount = 0;
        BaseDecodeType decodeType = DecodeType.Planet;

        foreach (var imgBytes in images)
        {
            // Load the PNG bytes into a memory stream for the reader
            using (var ms = new MemoryStream(imgBytes))
            {
                // Initialize the barcode reader for Planet symbology
                using (var reader = new BarCodeReader(ms, decodeType))
                {
                    // Attempt to read all barcodes in the image
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length > 0)
                        decodedCount++;
                }
            }
        }

        stopwatch.Stop();

        // Output benchmark results
        Console.WriteLine($"Decoded {decodedCount}/{barcodeCount} barcodes in {stopwatch.ElapsedMilliseconds} ms.");
        Console.WriteLine($"Average per barcode: {stopwatch.Elapsed.TotalMilliseconds / barcodeCount:F2} ms.");
    }
}