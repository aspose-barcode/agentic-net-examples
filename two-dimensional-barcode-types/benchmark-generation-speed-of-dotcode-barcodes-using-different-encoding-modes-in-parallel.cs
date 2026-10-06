// Title: Benchmark DotCode barcode generation speed with parallel encoding modes
// Description: Demonstrates how to measure the time required to generate DotCode barcodes using various encoding modes in parallel. Useful for performance testing and optimization.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking collection, illustrating the use of BarcodeGenerator with DotCode symbology, configuring DotCodeEncodeMode, and executing parallel generation to assess throughput. Developers often need to evaluate generation speed for different symbologies and encoding options when integrating barcode creation into high‑volume applications.
// Prompt: Benchmark generation speed of DotCode barcodes using different encoding modes in parallel.
// Tags: dotcode, barcode, performance, benchmark, parallel, aspose.barcode, generation, encode-mode, png

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Contains the entry point for benchmarking DotCode barcode generation speed across different encoding modes.
/// </summary>
class Program
{
    /// <summary>
    /// Executes the benchmark: generates a set of DotCode barcodes for each encoding mode in parallel and reports the elapsed time.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for any generated files (kept for safety, though not used for output)
        string tempPath = Path.Combine(Path.GetTempPath(), "DotCodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempPath);

        // Define the DotCode encoding modes that will be benchmarked
        var modes = new List<DotCodeEncodeMode>
        {
            DotCodeEncodeMode.Auto,
            DotCodeEncodeMode.Binary,
            DotCodeEncodeMode.ECI,
            DotCodeEncodeMode.Extended
        };

        // Number of barcodes to generate per mode (small safe sample)
        const int barcodesPerMode = 5;

        // Run the benchmark for each mode concurrently
        Parallel.ForEach(modes, mode =>
        {
            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < barcodesPerMode; i++)
            {
                // Create distinct code text for each iteration
                string text = $"Sample_{mode}_{i}";

                using (var generator = new BarcodeGenerator(EncodeTypes.DotCode))
                {
                    // Apply the current encoding mode to the generator
                    generator.Parameters.Barcode.DotCode.EncodeMode = mode;

                    // Set the code text based on the encoding mode
                    if (mode == DotCodeEncodeMode.Binary)
                    {
                        // Example binary data for Binary mode
                        byte[] binaryData = { 0xFF, 0xFE, 0xFD, 0xFC, 0xFB, 0xFA, 0xF9 };
                        generator.SetCodeText(binaryData);
                    }
                    else
                    {
                        generator.CodeText = text;
                    }

                    // Save the generated barcode to a memory stream (no file I/O)
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        // The stream could be processed further; here it is simply discarded.
                    }
                }
            }

            stopwatch.Stop();
            Console.WriteLine($"Mode {mode}: generated {barcodesPerMode} barcodes in {stopwatch.ElapsedMilliseconds} ms");
        });

        // Attempt to clean up the temporary folder; ignore any errors that occur
        try
        {
            Directory.Delete(tempPath, true);
        }
        catch
        {
            // Suppress cleanup exceptions
        }
    }
}