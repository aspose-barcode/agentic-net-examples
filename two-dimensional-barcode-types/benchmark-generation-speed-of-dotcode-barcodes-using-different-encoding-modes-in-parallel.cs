// Title: Benchmark DotCode barcode generation speed across encoding modes
// Description: Demonstrates how to measure the time required to generate DotCode barcodes using different encoding modes in parallel. Useful for performance testing and optimization.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category, showcasing the use of BarcodeGenerator, EncodeTypes, and DotCodeEncodeMode classes. Developers often need to compare generation speeds for various symbology settings, especially when processing large batches in multithreaded environments. The snippet illustrates typical patterns for parallel execution, timing with Stopwatch, and temporary file handling.
// Prompt: Benchmark generation speed of DotCode barcodes using different encoding modes in parallel.
// Tags: dotcode, barcode, performance, benchmark, parallel, aspose.barcode, generation

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates benchmarking of DotCode barcode generation speed using different encoding modes in parallel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates temporary DotCode barcodes, measures generation time per encoding mode, outputs results, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "DotCodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the set of DotCode encoding modes to benchmark
        var modes = new List<DotCodeEncodeMode>
        {
            DotCodeEncodeMode.Auto,
            DotCodeEncodeMode.Binary,
            DotCodeEncodeMode.ECI,
            DotCodeEncodeMode.Extended
        };

        // Number of barcodes to generate for each mode
        const int barcodesPerMode = 5;
        // Sample text to encode (identical for all barcodes)
        const string sampleText = "AsposeBenchmark";

        // Dictionary to store elapsed time per encoding mode
        var results = new Dictionary<DotCodeEncodeMode, TimeSpan>();

        // Execute the benchmark for each mode in parallel
        Parallel.ForEach(modes, mode =>
        {
            var sw = Stopwatch.StartNew();

            // Generate the specified number of barcodes for the current mode
            for (int i = 0; i < barcodesPerMode; i++)
            {
                string filePath = Path.Combine(tempFolder, $"DotCode_{mode}_{i}.png");
                using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, sampleText))
                {
                    // Apply the specific DotCode encoding mode
                    generator.Parameters.Barcode.DotCode.EncodeMode = mode;

                    // Save the generated barcode as a PNG image
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
            }

            sw.Stop();

            // Record the elapsed time for this mode (thread‑safe)
            lock (results)
            {
                results[mode] = sw.Elapsed;
            }
        });

        // Display benchmark results to the console
        Console.WriteLine("DotCode generation benchmark (time for {0} barcodes per mode):", barcodesPerMode);
        foreach (var kvp in results)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value.TotalMilliseconds} ms");
        }

        // Attempt to delete the temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If deletion fails, ignore – the OS will eventually clean up the files
        }
    }
}