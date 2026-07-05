// Title: Benchmark Stacked vs Single Linear Barcode Recognition
// Description: Demonstrates generating stacked DataBar and single Code128 barcodes, then measures recognition time to compare scalability.
// Prompt: Benchmark recognition of stacked linear barcodes versus single barcodes to evaluate algorithm scalability.
// Tags: barcode, recognition, benchmark, stacked, linear, databar, code128, aspnet, aspose

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates stacked DataBar and single Code128 barcodes, then benchmarks their recognition performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// </summary>
    static void Main()
    {
        const int sampleCount = 5; // safe sample size for benchmarking

        // ------------------------------------------------------------
        // Generate stacked linear DataBar barcodes (EncodeTypes.DatabarStacked)
        // ------------------------------------------------------------
        var stackedImages = new List<byte[]>();
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = $"(01)1234567890123{i}";
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, codeText))
            {
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    stackedImages.Add(ms.ToArray());
                }
            }
        }

        // ------------------------------------------------------------
        // Generate single linear barcodes (EncodeTypes.Code128)
        // ------------------------------------------------------------
        var singleImages = new List<byte[]>();
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = $"CODE{i:D4}";
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    singleImages.Add(ms.ToArray());
                }
            }
        }

        // ------------------------------------------------------------
        // Benchmark recognition of stacked barcodes
        // ------------------------------------------------------------
        var stackedTimer = new Stopwatch();
        stackedTimer.Start();
        foreach (var imageData in stackedImages)
        {
            using (var ms = new MemoryStream(imageData))
            using (var reader = new BarCodeReader(ms, DecodeType.DatabarStacked))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Result processing can be added here if needed
                }
            }
        }
        stackedTimer.Stop();

        // ------------------------------------------------------------
        // Benchmark recognition of single barcodes
        // ------------------------------------------------------------
        var singleTimer = new Stopwatch();
        singleTimer.Start();
        foreach (var imageData in singleImages)
        {
            using (var ms = new MemoryStream(imageData))
            using (var reader = new BarCodeReader(ms, DecodeType.Code128))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Result processing can be added here if needed
                }
            }
        }
        singleTimer.Stop();

        // ------------------------------------------------------------
        // Output benchmark results
        // ------------------------------------------------------------
        Console.WriteLine($"Stacked DataBar recognition time for {sampleCount} barcodes: {stackedTimer.Elapsed.TotalMilliseconds} ms");
        Console.WriteLine($"Average per barcode: {stackedTimer.Elapsed.TotalMilliseconds / sampleCount:F2} ms");
        Console.WriteLine($"Single Code128 recognition time for {sampleCount} barcodes: {singleTimer.Elapsed.TotalMilliseconds} ms");
        Console.WriteLine($"Average per barcode: {singleTimer.Elapsed.TotalMilliseconds / sampleCount:F2} ms");
    }
}