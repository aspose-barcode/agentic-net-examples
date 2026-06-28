using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates benchmarking of different DotCode encoding modes using Aspose.BarCode.
/// Generates a small number of barcodes per mode and measures execution time.
/// </summary>
class Program
{
    // Sample text to encode in each DotCode barcode.
    private const string SampleCodeText = "DOTCODE123";

    // Number of barcodes generated per encoding mode (kept low for quick execution).
    private const int BarcodesPerMode = 5;

    /// <summary>
    /// Entry point of the application. Sets up encoding modes and runs benchmarks in parallel.
    /// </summary>
    static void Main()
    {
        // Mapping of mode names to configuration actions for the BarcodeGenerator.
        var modes = new Dictionary<string, Action<BarcodeGenerator>>
        {
            {
                "Auto", generator =>
                {
                    // Default mode; no additional configuration required.
                }
            },
            {
                "Binary", generator =>
                {
                    // Configure generator for Binary encoding.
                    generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.Binary;
                }
            },
            {
                "ECI", generator =>
                {
                    // Configure generator for ECI encoding with UTF-8 character set.
                    generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.ECI;
                    generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.UTF8;
                }
            },
            {
                "Extended", generator =>
                {
                    // Configure generator for Extended encoding.
                    generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.Extended;
                }
            }
        };

        // Launch a benchmark task for each mode concurrently.
        var tasks = new List<Task>();
        foreach (var kvp in modes)
        {
            string modeName = kvp.Key;
            Action<BarcodeGenerator> configure = kvp.Value;

            tasks.Add(Task.Run(() => BenchmarkMode(modeName, configure)));
        }

        // Wait for all benchmark tasks to complete.
        Task.WaitAll(tasks.ToArray());

        Console.WriteLine("Benchmark completed.");
    }

    /// <summary>
    /// Generates a set of barcodes for a specific encoding mode and measures the time taken.
    /// </summary>
    /// <param name="modeName">Human‑readable name of the encoding mode.</param>
    /// <param name="configure">Action that applies mode‑specific settings to a BarcodeGenerator instance.</param>
    private static void BenchmarkMode(string modeName, Action<BarcodeGenerator> configure)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Generate the defined number of barcodes for the given mode.
        for (int i = 0; i < BarcodesPerMode; i++)
        {
            // Create a fresh generator for each barcode to avoid residual state.
            using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, SampleCodeText))
            {
                // Apply the mode‑specific configuration.
                configure(generator);

                // Save the barcode image to a memory stream (no disk I/O required for benchmarking).
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // The memory stream is discarded after this point; it could be used for further processing if needed.
                }
            }
        }

        stopwatch.Stop();
        Console.WriteLine($"{modeName} mode: Generated {BarcodesPerMode} barcodes in {stopwatch.ElapsedMilliseconds} ms");
    }
}