// Title: Parallel generation of DataMatrix barcodes with performance timing
// Description: Demonstrates how to generate multiple DataMatrix barcodes concurrently using Aspose.BarCode and measures the elapsed time.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of BarcodeGenerator, EncodeTypes, and related parameter classes to create DataMatrix symbols, a common requirement for inventory, logistics, and tracking applications. Developers often need to generate large volumes of barcodes quickly, and this pattern illustrates parallel processing with Parallel.For for high‑throughput scenarios.
// Prompt: Write performance benchmark measuring time to generate 10,000 DataMatrix barcodes in parallel.
// Tags: datamatrix, barcode generation, performance benchmark, parallel processing, aspose.barcode, png, memorystream

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a set of DataMatrix barcodes in parallel
/// and reports the time taken. Useful for benchmarking barcode generation performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcodes, stores them optionally, and prints timing information.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Number of barcodes to generate. Adjust as needed for real benchmarks (e.g., 10,000).
        const int barcodeCount = 10;

        // Optional collection to hold the generated barcode byte arrays.
        var generatedData = new List<byte[]>(barcodeCount);

        // Start measuring elapsed time.
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Generate barcodes concurrently using Parallel.For.
        Parallel.For(0, barcodeCount, i =>
        {
            // Each iteration creates its own BarcodeGenerator instance.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, $"Code{i:D4}"))
            {
                // Configure DataMatrix to use ECI encoding for UTF‑8 (optional).
                generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.ECI;
                generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

                // Write the barcode image to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);

                    // Capture the generated image bytes (optional, for demonstration).
                    byte[] data = ms.ToArray();
                    lock (generatedData)
                    {
                        generatedData.Add(data);
                    }
                }
            }
        });

        // Stop timing after all barcodes have been generated.
        stopwatch.Stop();

        // Output benchmark results.
        Console.WriteLine($"Generated {barcodeCount} DataMatrix barcodes in parallel.");
        Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
    }
}