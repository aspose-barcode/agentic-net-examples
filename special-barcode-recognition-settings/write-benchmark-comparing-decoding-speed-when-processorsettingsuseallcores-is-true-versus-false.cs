// Title: Benchmark decoding speed with ProcessorSettings.UseAllCores true vs false
// Description: Demonstrates measuring barcode decoding performance when enabling multi‑core processing versus single‑core mode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode decoding performance category. It showcases the use of BarCodeReader, ProcessorSettings, and common benchmarking techniques to compare single‑core and multi‑core decoding. Developers often need to evaluate throughput for bulk barcode processing, and this snippet provides a reusable pattern for such assessments.
// Prompt: Write a benchmark comparing decoding speed when ProcessorSettings.UseAllCores is true versus false.
// Tags: barcode, decoding, performance, benchmark, processorsettings, multithreading, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a simple benchmark that compares barcode decoding speed when
/// <see cref="BarCodeReader.ProcessorSettings.UseAllCores"/> is enabled versus disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// Generates sample barcode images, runs two decoding benchmarks, and cleans up temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for generated barcode images.
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "Benchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // --------------------------------------------------------------------
        // Generate a set of sample barcode images (Code128) to be used in the benchmark.
        // --------------------------------------------------------------------
        List<string> files = new List<string>();
        int sampleCount = 5;
        for (int i = 0; i < sampleCount; i++)
        {
            string text = $"Sample{i}";
            string filePath = Path.Combine(tempDir, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            files.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Local helper that runs the decoding benchmark for a given core usage setting.
        // --------------------------------------------------------------------
        void RunBenchmark(bool useAllCores, string label)
        {
            // Configure the processor to use either all cores or a single core.
            BarCodeReader.ProcessorSettings.UseAllCores = useAllCores;

            Stopwatch sw = Stopwatch.StartNew();

            // Decode each generated barcode image.
            foreach (var file in files)
            {
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // The results are intentionally ignored; we only measure execution time.
                    var results = reader.ReadBarCodes();
                }
            }

            sw.Stop();
            Console.WriteLine($"{label}: {sw.ElapsedMilliseconds} ms");
        }

        // --------------------------------------------------------------------
        // Execute benchmarks: first single‑core, then multi‑core.
        // --------------------------------------------------------------------
        RunBenchmark(false, "Single-core (UseAllCores = false)");
        RunBenchmark(true, "All-cores (UseAllCores = true)");

        // --------------------------------------------------------------------
        // Clean up generated files and temporary directory.
        // --------------------------------------------------------------------
        foreach (var f in files)
        {
            try { File.Delete(f); } catch { }
        }
        try { Directory.Delete(tempDir, true); } catch { }
    }
}