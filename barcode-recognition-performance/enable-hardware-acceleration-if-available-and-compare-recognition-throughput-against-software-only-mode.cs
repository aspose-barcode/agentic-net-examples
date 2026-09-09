// Title: Barcode recognition benchmark with hardware acceleration vs software mode
// Description: Demonstrates generating sample Code128 barcodes, then measuring recognition throughput using Aspose.BarCode with and without hardware acceleration.
// Category-Description: This example belongs to the Aspose.BarCode recognition performance category. It shows how to configure BarCodeReader processor settings to enable multi‑core and hardware‑accelerated processing, generate barcodes with BarcodeGenerator, and benchmark throughput. Developers working on high‑volume scanning or batch processing can use these APIs to optimize recognition speed.
// Prompt: Enable hardware acceleration if available and compare recognition throughput against software‑only mode.
// Tags: barcode, recognition, performance, hardware acceleration, software mode, code128, aspose.barcode, benchmark, multithreading

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and recognition performance comparison between software‑only and hardware‑accelerated modes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, runs recognition benchmarks, and outputs timing results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images (Code128) and store their file paths
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string codeText = "Sample" + i;
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Run recognition in software‑only mode (single core)
        var softwareResult = RunRecognition(barcodeFiles, useAllCores: false);
        Console.WriteLine($"Software‑only mode: Recognized {softwareResult.Item1} barcodes in {softwareResult.Item2} ms");

        // Run recognition with hardware acceleration (all available cores)
        var hardwareResult = RunRecognition(barcodeFiles, useAllCores: true);
        Console.WriteLine($"Hardware‑accelerated mode: Recognized {hardwareResult.Item1} barcodes in {hardwareResult.Item2} ms");

        // Clean up temporary files and folder
        try
        {
            foreach (var file in barcodeFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect benchmark output
        }
    }

    // Returns tuple: (total recognized barcodes, elapsed milliseconds)
    static Tuple<int, long> RunRecognition(List<string> files, bool useAllCores)
    {
        // Configure processor settings for hardware acceleration or software‑only mode
        BarCodeReader.ProcessorSettings.UseAllCores = useAllCores;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = useAllCores ? Environment.ProcessorCount : 1;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = useAllCores ? Environment.ProcessorCount * 2 : 1;

        int totalCount = 0;
        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Use all supported barcode types for decoding
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        // Process each barcode image file
        foreach (string file in files)
        {
            if (!File.Exists(file))
                continue;

            using (BarCodeReader reader = new BarCodeReader(file, decodeType))
            {
                try
                {
                    // Iterate through all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        totalCount++;
                        // Output each result for verification
                        Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
                catch (ArgumentException)
                {
                    // Handle cases where the image cannot be loaded
                    Console.WriteLine($"Warning: Unable to load image '{file}'. Skipping.");
                }
            }
        }

        sw.Stop();
        return Tuple.Create(totalCount, sw.ElapsedMilliseconds);
    }
}