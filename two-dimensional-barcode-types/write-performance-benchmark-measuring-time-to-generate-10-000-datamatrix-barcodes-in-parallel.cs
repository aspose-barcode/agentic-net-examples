// Title: Parallel generation of DataMatrix barcodes benchmark
// Description: Demonstrates measuring the time required to generate a large number of DataMatrix barcodes concurrently.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category, showcasing how to use BarcodeGenerator, EncodeTypes, and image handling classes (Bitmap, ImageFormat) to create barcodes in parallel. Developers often need to assess throughput when generating thousands of barcodes for batch processing, printing, or inventory systems. The snippet illustrates typical use of Parallel.For, Stopwatch, and memory streams for high‑volume barcode creation.
// Prompt: Write performance benchmark measuring time to generate 10,000 DataMatrix barcodes in parallel.
// Tags: datamatrix, performance, benchmark, parallel, generation, aspose.barcode, bitmap, png

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a simple performance benchmark that generates a configurable number of DataMatrix barcodes in parallel
/// and reports the elapsed time. Useful for evaluating throughput of the Aspose.BarCode generation API.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// Accepts an optional command‑line argument specifying how many barcodes to generate (default is 10).
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be an integer count.</param>
    static void Main(string[] args)
    {
        // Determine how many barcodes to generate; default to 10 for quick execution.
        int barcodeCount = 10;
        if (args.Length > 0 && int.TryParse(args[0], out int parsed) && parsed > 0)
        {
            barcodeCount = parsed;
        }

        // Start measuring elapsed time.
        var stopwatch = Stopwatch.StartNew();

        // Generate barcodes concurrently using Parallel.For for maximum CPU utilization.
        Parallel.For(0, barcodeCount, i =>
        {
            // Create a DataMatrix generator with a unique code text for each iteration.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, $"Sample{i:D5}"))
            {
                // Produce the barcode image as a Bitmap.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Encode the bitmap to PNG format via a memory stream (forces image encoding).
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                    }
                }
            }
        });

        // Stop timing and output the result.
        stopwatch.Stop();
        Console.WriteLine($"Generated {barcodeCount} DataMatrix barcodes in {stopwatch.ElapsedMilliseconds} ms.");
    }
}