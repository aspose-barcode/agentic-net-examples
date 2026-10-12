// Title: Benchmark decoding speed with and without multi‑core processing
// Description: Demonstrates how to measure the time required to decode Code128 barcodes using Aspose.BarCode when ProcessorSettings.UseAllCores is toggled.
// Category-Description: This example belongs to the Aspose.BarCode decoding performance category. It showcases the use of BarCodeReader, ProcessorSettings, and related classes to evaluate single‑thread versus multi‑thread decoding. Developers often need to benchmark barcode recognition speed for large image batches or real‑time applications, adjusting core usage to balance performance and resource consumption.
// Prompt: Write a benchmark comparing decoding speed when ProcessorSettings.UseAllCores is true versus false.
// Tags: code128, decoding, png, barcodereader, barcodegenerator

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a simple benchmark that compares barcode decoding times when
/// Aspose.BarCode's ProcessorSettings.UseAllCores is enabled versus disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// Generates sample Code128 barcodes, runs decoding benchmarks,
    /// and outputs the elapsed times for single‑thread and multi‑thread modes.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store generated barcode images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a set of sample barcode PNG files.
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string filePath = Path.Combine(tempFolder, $"sample_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Benchmark decoding using a single thread (UseAllCores = false).
        long singleThreadTime = BenchmarkDecoding(barcodeFiles, useAllCores: false);
        Console.WriteLine($"Single-thread decoding time: {singleThreadTime} ms");

        // Benchmark decoding using all available cores (UseAllCores = true).
        long multiThreadTime = BenchmarkDecoding(barcodeFiles, useAllCores: true);
        Console.WriteLine($"Multi-thread decoding time: {multiThreadTime} ms");

        // Clean up temporary files and folder.
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup.
        }
    }

    /// <summary>
    /// Executes the decoding benchmark for a list of barcode image files.
    /// </summary>
    /// <param name="files">Paths to barcode image files.</param>
    /// <param name="useAllCores">If true, enables multi‑core processing; otherwise forces single‑thread execution.</param>
    /// <returns>Total elapsed time in milliseconds.</returns>
    static long BenchmarkDecoding(List<string> files, bool useAllCores)
    {
        // Configure processor settings based on the desired core usage.
        BarCodeReader.ProcessorSettings.UseAllCores = useAllCores;
        if (!useAllCores)
        {
            // Restrict processing to a single core.
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;
        }
        else
        {
            // Allow additional threads up to twice the number of logical processors.
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;
        }

        // Start timing the decoding loop.
        Stopwatch sw = Stopwatch.StartNew();

        // Decode each barcode image file.
        foreach (string file in files)
        {
            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Read all barcodes in the image (expected one per image).
                BarCodeResult[] results = reader.ReadBarCodes();
                // Results are not processed further in this benchmark.
            }
        }

        // Stop timing and return the elapsed milliseconds.
        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}