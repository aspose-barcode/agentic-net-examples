// Title: Barcode recognition multithreading performance benchmark
// Description: Demonstrates how to measure the impact of Aspose.BarCode's ProcessorSettings.MaxAdditionalAllowedThreads on PDF417 barcode recognition speed.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning category, showcasing the use of BarCodeReader.ProcessorSettings to control threading behavior. Developers often need to balance CPU usage and latency when processing large batches of barcodes; this snippet illustrates single‑thread versus multi‑thread configurations using key classes like BarcodeGenerator, BarCodeReader, and ProcessorSettings.
// Prompt: Write a benchmark comparing performance when ProcessorSettings.MaxAdditionalAllowedThreads is zero (single‑thread) versus greater than zero.
// Tags: pdf417, barcode-recognition, performance, multithreading, aspose-barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a benchmark for PDF417 barcode recognition using single‑thread and multi‑thread settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, runs the benchmark, and outputs timing results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a set of sample barcode images to be used in the benchmark
        int sampleCount = 5;
        string[] imagePaths = new string[sampleCount];
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, $"Sample{i + 1}"))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths[i] = filePath;
        }

        // Benchmark single‑thread recognition (MaxAdditionalAllowedThreads = 0)
        long singleThreadMs = RunBenchmark(imagePaths, useMultiThread: false);
        Console.WriteLine($"Single‑thread recognition time: {singleThreadMs} ms");

        // Benchmark multi‑thread recognition (MaxAdditionalAllowedThreads > 0)
        long multiThreadMs = RunBenchmark(imagePaths, useMultiThread: true);
        Console.WriteLine($"Multi‑thread recognition time: {multiThreadMs} ms");

        // Clean up temporary files and folder
        foreach (var path in imagePaths)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        Directory.Delete(tempFolder, true);
    }

    /// <summary>
    /// Executes the recognition benchmark for the provided images using the specified threading configuration.
    /// </summary>
    /// <param name="imagePaths">Array of barcode image file paths.</param>
    /// <param name="useMultiThread">If true, enables multi‑threading; otherwise, forces single‑thread execution.</param>
    /// <returns>Total elapsed time in milliseconds.</returns>
    static long RunBenchmark(string[] imagePaths, bool useMultiThread)
    {
        // Configure ProcessorSettings based on the desired threading mode
        if (useMultiThread)
        {
            BarCodeReader.ProcessorSettings.UseAllCores = true;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;
        }
        else
        {
            BarCodeReader.ProcessorSettings.UseAllCores = false;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;
        }

        // Start timing
        Stopwatch sw = Stopwatch.StartNew();

        // Process each barcode image
        foreach (var path in imagePaths)
        {
            using (var reader = new BarCodeReader(path, DecodeType.Pdf417))
            {
                // ReadBarCodes returns an array; results are not needed for timing
                reader.ReadBarCodes();
            }
        }

        // Stop timing and return elapsed milliseconds
        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}