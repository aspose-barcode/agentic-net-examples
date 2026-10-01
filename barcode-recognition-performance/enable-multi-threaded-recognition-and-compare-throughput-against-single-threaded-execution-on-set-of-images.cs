// Title: Multi‑Threaded vs Single‑Threaded Barcode Recognition Throughput
// Description: Demonstrates how to enable multi‑threaded barcode recognition using Aspose.BarCode and compares processing time against single‑threaded execution on a set of generated barcode images.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of BarCodeReader.ProcessorSettings to control CPU core utilization. Typical use cases include performance benchmarking, high‑volume scanning, and optimizing server‑side barcode processing. Developers often need to toggle between single‑ and multi‑core execution to balance resource usage and throughput.
// Prompt: Enable multi‑threaded recognition and compare throughput against single‑threaded execution on a set of images.
// Tags: barcode symbology, recognition, multithreading, performance, aspose.barcode, code128, png

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates multi‑threaded barcode recognition performance comparison using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, runs single‑ and multi‑threaded recognition, and outputs timing results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images (PNG format, Code128 symbology)
        List<string> barcodeFiles = GenerateSampleBarcodes(tempFolder, 5);

        // ---------- Single‑threaded recognition ----------
        // Configure processor to use only one core
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;

        var singleResult = RecognizeBarcodes(barcodeFiles);
        Console.WriteLine($"Single‑threaded: Processed {singleResult.TotalCount} barcodes in {singleResult.ElapsedMilliseconds} ms");

        // ---------- Multi‑threaded recognition ----------
        // Enable use of all available cores
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        var multiResult = RecognizeBarcodes(barcodeFiles);
        Console.WriteLine($"Multi‑threaded: Processed {multiResult.TotalCount} barcodes in {multiResult.ElapsedMilliseconds} ms");

        // Clean up temporary files
        foreach (var file in barcodeFiles)
        {
            try { File.Delete(file); } catch { /* ignore */ }
        }
        try { Directory.Delete(tempFolder, true); } catch { /* ignore */ }
    }

    // Generates a given number of barcode PNG files and returns their paths
    static List<string> GenerateSampleBarcodes(string folder, int count)
    {
        var files = new List<string>();
        for (int i = 0; i < count; i++)
        {
            string codeText = $"Sample{i + 1}";
            string filePath = Path.Combine(folder, $"barcode_{i + 1}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // No additional parameters needed for this simple example
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            files.Add(filePath);
        }
        return files;
    }

    // Reads all barcodes from the provided file list and returns total count and elapsed time
    static (int TotalCount, long ElapsedMilliseconds) RecognizeBarcodes(List<string> files)
    {
        int totalDetected = 0;
        var stopwatch = Stopwatch.StartNew();

        foreach (var file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                try
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    totalDetected += results.Length;

                    foreach (var result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)} | CodeText: {result.CodeText} | Type: {result.CodeTypeName}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading '{file}': {ex.Message}");
                }
            }
        }

        stopwatch.Stop();
        return (totalDetected, stopwatch.ElapsedMilliseconds);
    }
}