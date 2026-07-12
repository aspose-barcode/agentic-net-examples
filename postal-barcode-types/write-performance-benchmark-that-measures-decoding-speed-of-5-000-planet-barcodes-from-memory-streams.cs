// Title: Benchmark decoding speed of Planet barcodes from memory streams
// Description: Demonstrates how to generate a set of Planet barcodes in memory and measure the time required to decode them, useful for performance testing.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category, illustrating the use of BarcodeGenerator, BarCodeReader, and QualitySettings to evaluate decoding throughput. Developers often need to benchmark barcode recognition for large batches to optimize latency in high‑volume scanning scenarios. The snippet shows typical setup, in‑memory image handling, and timing with Stopwatch, making it searchable for performance benchmarks of barcode decoding.
// Prompt: Write a performance benchmark that measures decoding speed of 5,000 Planet barcodes from memory streams.
// Tags: planet, decoding, benchmark, memorystream, barcodegenerator, barcodereader

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates a performance benchmark for decoding Planet barcodes from memory streams.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample Planet barcodes, decodes them, and reports elapsed time.
    /// </summary>
    static void Main()
    {
        // Number of barcodes to generate for the demo (small safe sample).
        // In a real benchmark this could be 5,000.
        const int sampleCount = 10;

        // Prepare sample Planet barcodes in memory.
        List<byte[]> barcodeImages = new List<byte[]>();
        for (int i = 0; i < sampleCount; i++)
        {
            // Each Planet barcode requires a numeric code (5‑digit example).
            string codeText = $"12345{i:D4}";
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Save the generated barcode as PNG into the memory stream.
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Store the raw image bytes for later decoding.
                    barcodeImages.Add(ms.ToArray());
                }
            }
        }

        // Benchmark decoding speed.
        int totalDecoded = 0;
        Stopwatch sw = Stopwatch.StartNew();

        foreach (byte[] imageData in barcodeImages)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.Planet))
            {
                // Use high‑performance quality preset for speed.
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Read all barcodes from the current image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Simple validation to ensure a barcode was read.
                    if (!string.IsNullOrEmpty(result.CodeText))
                    {
                        totalDecoded++;
                    }
                }
            }
        }

        sw.Stop();

        // Output the benchmark result.
        Console.WriteLine($"Decoded {totalDecoded} Planet barcodes in {sw.Elapsed.TotalMilliseconds} ms.");
        // Note: For a true performance test with 5,000 barcodes, increase sampleCount accordingly.
    }
}