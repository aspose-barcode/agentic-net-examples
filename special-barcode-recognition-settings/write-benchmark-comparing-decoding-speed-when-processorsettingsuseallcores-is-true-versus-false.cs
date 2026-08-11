// Title: Barcode decoding speed benchmark using ProcessorSettings.UseAllCores
// Description: Demonstrates how to measure the decoding performance of Code128 barcodes when Aspose.BarCode's ProcessorSettings.UseAllCores is enabled versus disabled.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning category, illustrating the use of BarCodeReader.ProcessorSettings to control multi‑core processing. Developers often need to benchmark decoding speed for different core utilization scenarios, especially when optimizing server‑side barcode processing pipelines. The sample shows image generation, configuration of UseAllCores and UseOnlyThisCoresCount, and timing of the decoding loop.
// Prompt: Write a benchmark comparing decoding speed when ProcessorSettings.UseAllCores is true versus false.
// Tags: barcode, decoding, benchmark, processorsettings, useallcores, code128, aspose.barcode

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a simple benchmark that compares barcode decoding speed with
/// Aspose.BarCode's ProcessorSettings.UseAllCores enabled and disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample Code128 barcodes, runs the benchmark,
    /// and outputs the elapsed time for each configuration.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark");
        if (Directory.Exists(tempFolder))
            Directory.Delete(tempFolder, true);
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images
        int sampleCount = 5;
        List<string> imagePaths = new List<string>();
        for (int i = 0; i < sampleCount; i++)
        {
            string text = $"Sample{i + 1}";
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            GenerateBarcodeImage(text, filePath);
            imagePaths.Add(filePath);
        }

        // Benchmark with UseAllCores = true
        long timeAllCores = BenchmarkDecoding(imagePaths, useAllCores: true);
        Console.WriteLine($"Decoding with UseAllCores = true took {timeAllCores} ms");

        // Benchmark with UseAllCores = false (use half of the cores)
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Math.Max(1, Environment.ProcessorCount / 2);
        long timePartialCores = BenchmarkDecoding(imagePaths, useAllCores: false);
        Console.WriteLine($"Decoding with UseAllCores = false took {timePartialCores} ms");

        // Clean up temporary files
        Directory.Delete(tempFolder, true);
    }

    // Generates a Code128 barcode image and saves it to the specified path
    static void GenerateBarcodeImage(string codeText, string filePath)
    {
        // Resolve EncodeTypes.Code128 via reflection (EncodeTypes.TryParse does not exist)
        var field = typeof(EncodeTypes).GetField("Code128");
        if (field == null)
            throw new ArgumentException("Encode type 'Code128' not found.");

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Save as PNG
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }

    // Measures the time required to decode all images with the specified ProcessorSettings
    static long BenchmarkDecoding(List<string> imagePaths, bool useAllCores)
    {
        // Configure ProcessorSettings
        BarCodeReader.ProcessorSettings.UseAllCores = useAllCores;
        if (!useAllCores)
        {
            // Example: limit to half of the available cores
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Math.Max(1, Environment.ProcessorCount / 2);
        }

        Stopwatch sw = Stopwatch.StartNew();

        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
                continue; // Skip missing files gracefully

            using (var reader = new BarCodeReader(path, DecodeType.Code128))
            {
                // Perform the decoding; results are not used further
                var results = reader.ReadBarCodes();

                // Iterate results to ensure full processing and avoid compiler optimizations
                foreach (var result in results)
                {
                    var _ = result.CodeText; // No-op access
                }
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}