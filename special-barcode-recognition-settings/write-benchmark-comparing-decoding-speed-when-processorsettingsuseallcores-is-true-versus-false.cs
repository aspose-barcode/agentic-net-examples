// Title: Benchmark decoding speed with and without multi‑core processing
// Description: Demonstrates how to measure the barcode decoding time using Aspose.BarCode when ProcessorSettings.UseAllCores is toggled. The example generates sample PDF417 barcodes, decodes them, and reports single‑core versus multi‑core performance.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category, illustrating the use of BarCodeReader.ProcessorSettings to control parallel decoding. It shows typical usage of BarcodeGenerator for creating test images, BarCodeReader for decoding, and Stopwatch for timing. Developers often need to evaluate multi‑threaded decoding to optimize throughput in high‑volume scanning scenarios.
// Prompt: Write a benchmark comparing decoding speed when ProcessorSettings.UseAllCores is true versus false.
// Tags: pdf417, decoding, performance, multithreading, processorsettings, barcodereader, barcodegenerator

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates benchmarking of barcode decoding speed with different core usage settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, runs single‑core and multi‑core benchmarks, and outputs the timings.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Benchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images
        List<string> imagePaths = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, $"SampleText{i}"))
            {
                // Configure barcode appearance
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;
                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(filePath);
        }

        // Benchmark with single core (UseAllCores = false)
        long singleCoreTime = RunBenchmark(imagePaths, useAllCores: false);
        Console.WriteLine($"Single-core decoding time: {singleCoreTime} ms");

        // Benchmark with all cores (UseAllCores = true)
        long multiCoreTime = RunBenchmark(imagePaths, useAllCores: true);
        Console.WriteLine($"Multi-core decoding time: {multiCoreTime} ms");

        // Clean up temporary files
        foreach (var path in imagePaths)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        if (Directory.Exists(tempFolder))
        {
            Directory.Delete(tempFolder, true);
        }
    }

    /// <summary>
    /// Runs the decoding benchmark for a set of barcode images using the specified core usage setting.
    /// </summary>
    /// <param name="imagePaths">List of file paths to barcode images.</param>
    /// <param name="useAllCores">If true, enables multi‑core processing; otherwise, forces single‑core execution.</param>
    /// <returns>Total elapsed time in milliseconds.</returns>
    static long RunBenchmark(List<string> imagePaths, bool useAllCores)
    {
        // Configure processor settings based on the desired core usage
        BarCodeReader.ProcessorSettings.UseAllCores = useAllCores;
        if (useAllCores)
        {
            // Allow additional threads proportional to the number of logical processors
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;
        }
        else
        {
            // Restrict processing to a single core
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;
        }

        // Start timing
        Stopwatch watch = Stopwatch.StartNew();

        // Decode each image; results are ignored because we only measure performance
        foreach (var path in imagePaths)
        {
            using (var reader = new BarCodeReader(path, DecodeType.Pdf417))
            {
                reader.ReadBarCodes();
            }
        }

        // Stop timing and return elapsed milliseconds
        watch.Stop();
        return watch.ElapsedMilliseconds;
    }
}