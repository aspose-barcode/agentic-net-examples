// Title: Benchmarking Barcode Recognition Threading Performance
// Description: Demonstrates how to measure the execution time of Aspose.BarCode barcode recognition when using single‑threaded versus multi‑threaded processing.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning category. It shows how to configure BarCodeReader.ProcessorSettings, generate sample Code128 barcodes, and benchmark recognition using different MaxAdditionalAllowedThreads values. Developers often need to evaluate threading impact on barcode scanning workloads, especially when processing large image batches in server or desktop applications.
// Prompt: Write a benchmark comparing performance when ProcessorSettings.MaxAdditionalAllowedThreads is zero (single‑thread) versus greater than zero.
// Tags: barcode symbology, performance benchmark, threading, processor settings, aspose.barcode, code128, image generation, recognition

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a simple benchmark that compares single‑threaded and multi‑threaded barcode recognition
/// using Aspose.BarCode's <c>BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads</c> setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// Generates sample barcode images, runs recognition with different threading settings,
    /// and outputs the elapsed time for each configuration.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for barcode images
        string folderPath = Path.Combine(Path.GetTempPath(), "AsposeBarcodeBenchmark");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Generate sample barcode images (5 items)
        List<string> imagePaths = GenerateSampleBarcodes(folderPath, 5);

        // Benchmark with single‑thread (MaxAdditionalAllowedThreads = 0)
        long singleThreadMs = RunRecognitionBenchmark(imagePaths, 0);
        Console.WriteLine($"Single‑thread recognition time: {singleThreadMs} ms");

        // Benchmark with multi‑thread (MaxAdditionalAllowedThreads = Environment.ProcessorCount)
        long multiThreadMs = RunRecognitionBenchmark(imagePaths, Environment.ProcessorCount);
        Console.WriteLine($"Multi‑thread recognition time (threads={Environment.ProcessorCount}): {multiThreadMs} ms");
    }

    // Generates 'count' barcode PNG files and returns their full paths
    private static List<string> GenerateSampleBarcodes(string folder, int count)
    {
        var paths = new List<string>();
        for (int i = 1; i <= count; i++)
        {
            string codeText = $"Sample{i:D3}";
            string filePath = Path.Combine(folder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save directly to PNG file
                generator.Save(filePath);
            }
            paths.Add(filePath);
        }
        return paths;
    }

    // Runs recognition on all provided images with the specified MaxAdditionalAllowedThreads
    private static long RunRecognitionBenchmark(List<string> imagePaths, int maxAdditionalThreads)
    {
        // Configure processor settings
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = maxAdditionalThreads;

        var stopwatch = Stopwatch.StartNew();

        foreach (string path in imagePaths)
        {
            // Ensure the file exists before processing
            if (!File.Exists(path))
                continue;

            using (var reader = new BarCodeReader(path, DecodeType.Code128))
            {
                // Read all barcodes in the image (there is only one per image)
                foreach (var result in reader.ReadBarCodes())
                {
                    // Access result properties to prevent compiler optimizations from removing the loop
                    string typeName = result.CodeTypeName;
                    string codeText = result.CodeText;
                    // Variables are intentionally unused; they demonstrate access to result data
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}