// Title: Parallel generation of DataMatrix barcodes benchmark
// Description: Demonstrates measuring the time required to generate multiple DataMatrix barcodes concurrently using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create barcodes in parallel for high‑throughput scenarios. It uses the BarcodeGenerator class with EncodeTypes.DataMatrix and saves images in PNG format via BarCodeImageFormat. Developers often need to benchmark barcode creation performance when processing large batches, such as bulk label printing or real‑time encoding services.
// Prompt: Write performance benchmark measuring time to generate 10,000 DataMatrix barcodes in parallel.
// Tags: datamatrix, barcode generation, performance benchmark, parallel processing, aspose.barcode, png, memorystream

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that benchmarks parallel generation of DataMatrix barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a set of DataMatrix barcodes in parallel and reports the elapsed time.
    /// </summary>
    static void Main()
    {
        // Number of barcodes to generate (reduced for demo purposes)
        const int barcodeCount = 10; // safe sample size for demo

        // Thread‑safe collection to store generated barcode image bytes
        var results = new ConcurrentBag<byte[]>();

        // Start measuring elapsed time
        var stopwatch = Stopwatch.StartNew();

        // Generate barcodes in parallel
        Parallel.For(0, barcodeCount, i =>
        {
            // Create a barcode generator for a DataMatrix symbol with a unique value
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, $"Data{i:D4}"))
            {
                // Encode the barcode to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Store the generated image bytes
                    results.Add(ms.ToArray());
                }
            }
        });

        // Stop timing
        stopwatch.Stop();

        // Output benchmark result
        Console.WriteLine($"Generated {barcodeCount} DataMatrix barcodes in {stopwatch.ElapsedMilliseconds} ms.");
    }
}