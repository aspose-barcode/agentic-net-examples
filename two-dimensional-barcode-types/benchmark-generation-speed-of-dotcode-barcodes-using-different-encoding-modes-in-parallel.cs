// Title: Benchmark DotCode barcode generation speed across encoding modes in parallel
// Description: Demonstrates measuring the time required to generate DotCode barcodes using various encoding modes, running each mode concurrently.
// Category-Description: This example belongs to the Aspose.BarCode generation performance category. It showcases the use of BarcodeGenerator, EncodeTypes, and DotCodeEncodeMode to create DotCode symbols. Developers often need to benchmark different encoding settings to choose the optimal configuration for high‑throughput applications, such as bulk label printing or real‑time scanning systems.
// Prompt: Benchmark generation speed of DotCode barcodes using different encoding modes in parallel.
// Tags: dotcode, benchmark, parallel, generation, aspnet, aspose.barcode, png

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a benchmark for generating DotCode barcodes using various encoding modes in parallel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// </summary>
    static void Main()
    {
        // Define the DotCode encoding modes to benchmark.
        var modes = new Dictionary<string, DotCodeEncodeMode>
        {
            { "Auto", DotCodeEncodeMode.Auto },
            { "Binary", DotCodeEncodeMode.Binary },
            { "ECI", DotCodeEncodeMode.ECI },
            { "Extended", DotCodeEncodeMode.Extended },
            { "ExtendedCodetext", DotCodeEncodeMode.ExtendedCodetext }
        };

        // List to store benchmark results (mode name and elapsed time in milliseconds).
        var results = new List<(string Mode, long ElapsedMs)>();
        // Collection of tasks that will run each mode concurrently.
        var tasks = new List<Task>();

        // Create a task for each encoding mode.
        foreach (var kvp in modes)
        {
            string modeName = kvp.Key;
            DotCodeEncodeMode mode = kvp.Value;

            tasks.Add(Task.Run(() =>
            {
                var stopwatch = Stopwatch.StartNew();

                // Generate a small number of barcodes for the current mode.
                for (int i = 0; i < 5; i++)
                {
                    using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "Sample"))
                    {
                        // Apply the specific DotCode encoding mode.
                        generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = mode;

                        // Set ECI encoding when the mode requires it.
                        if (mode == DotCodeEncodeMode.ECI)
                        {
                            generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.UTF8;
                        }

                        // Save the barcode image to a memory stream (no file I/O).
                        using (var ms = new MemoryStream())
                        {
                            generator.Save(ms, BarCodeImageFormat.Png);
                        }
                    }
                }

                stopwatch.Stop();

                // Record the elapsed time for this mode in a thread‑safe manner.
                lock (results)
                {
                    results.Add((modeName, stopwatch.ElapsedMilliseconds));
                }
            }));
        }

        // Wait for all parallel tasks to finish.
        Task.WaitAll(tasks.ToArray());

        // Output the benchmark results to the console.
        Console.WriteLine("DotCode generation benchmark (5 barcodes per mode):");
        foreach (var result in results)
        {
            Console.WriteLine($"{result.Mode}: {result.ElapsedMs} ms");
        }
    }
}