// Title: Parallel generation of DataMatrix barcodes benchmark
// Description: Demonstrates measuring the time required to generate a set of DataMatrix barcodes using Aspose.BarCode in parallel.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DataMatrix to create PNG images. Developers often need to evaluate throughput when generating large numbers of barcodes concurrently, such as in bulk printing or inventory systems.
// Prompt: Write performance benchmark measuring time to generate 10,000 DataMatrix barcodes in parallel.
// Tags: datamatrix, barcode, generation, parallel, performance, benchmark, aspose.barcode, png

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple performance benchmark that generates multiple DataMatrix barcodes in parallel
/// using the Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// Generates a collection of DataMatrix barcodes concurrently and reports the elapsed time.
    /// </summary>
    static void Main()
    {
        // Number of barcodes to generate (adjustable for real benchmarks, e.g., 10_000)
        const int barcodeCount = 10; // safe sample size for demo

        // Prepare a list of unique code texts for the barcodes
        var codeTexts = new List<string>(barcodeCount);
        for (int i = 0; i < barcodeCount; i++)
        {
            codeTexts.Add($"DM{i:D5}");
        }

        // Start measuring elapsed time
        var stopwatch = Stopwatch.StartNew();

        // Generate barcodes in parallel to utilize multiple CPU cores
        Parallel.ForEach(codeTexts, codeText =>
        {
            // Create a generator for DataMatrix symbology with the current code text
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Set the X-dimension (module size) to 2 points
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the generated barcode to a memory stream as PNG (no file I/O)
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }
        });

        // Stop the timer and output results
        stopwatch.Stop();
        Console.WriteLine($"Generated {barcodeCount} DataMatrix barcodes in parallel.");
        Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalMilliseconds} ms");
    }
}