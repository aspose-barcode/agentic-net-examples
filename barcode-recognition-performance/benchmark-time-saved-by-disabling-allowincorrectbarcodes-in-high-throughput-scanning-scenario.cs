// Title: Benchmarking AllowIncorrectBarcodes Impact on Scan Performance
// Description: Demonstrates how to measure the performance difference when the AllowIncorrectBarcodes quality setting is toggled during barcode recognition. Generates sample Code128 barcodes, reads them with the setting both false and true, and reports elapsed time.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition performance tuning category. It shows how to use BarCodeReader, QualitySettings, and DecodeType to evaluate scanning speed, a common need for developers building high‑throughput barcode processing pipelines. The snippet illustrates generating barcodes with BarcodeGenerator, reading them, and measuring execution time with Stopwatch, useful for benchmarking and optimization tasks.
// Prompt: Benchmark the time saved by disabling AllowIncorrectBarcodes in a high‑throughput scanning scenario.
// Tags: barcode, code128, performance, benchmark, allowincorrectbarcodes, barcodereader, qualitysettings, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a simple benchmark that compares barcode reading speed with
/// <c>AllowIncorrectBarcodes</c> set to <c>false</c> versus <c>true</c>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, runs two read scenarios,
    /// and prints the elapsed time for each configuration.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Benchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images (Code128) and collect file paths
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string codeText = "CODE" + i;
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Benchmark reading with AllowIncorrectBarcodes = false
        Stopwatch swFalse = Stopwatch.StartNew();
        foreach (string file in barcodeFiles)
        {
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                reader.QualitySettings.AllowIncorrectBarcodes = false;
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output first result to keep the compiler happy and show progress
                if (results.Length > 0)
                {
                    Console.WriteLine($"[False] {Path.GetFileName(file)}: {results[0].CodeText}");
                }
            }
        }
        swFalse.Stop();

        // Benchmark reading with AllowIncorrectBarcodes = true
        Stopwatch swTrue = Stopwatch.StartNew();
        foreach (string file in barcodeFiles)
        {
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                reader.QualitySettings.AllowIncorrectBarcodes = true;
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length > 0)
                {
                    Console.WriteLine($"[True] {Path.GetFileName(file)}: {results[0].CodeText}");
                }
            }
        }
        swTrue.Stop();

        // Output benchmark results
        Console.WriteLine();
        Console.WriteLine($"Reading with AllowIncorrectBarcodes = false took: {swFalse.ElapsedMilliseconds} ms");
        Console.WriteLine($"Reading with AllowIncorrectBarcodes = true  took: {swTrue.ElapsedMilliseconds} ms");
        Console.WriteLine($"Time saved: {Math.Max(0, swFalse.ElapsedMilliseconds - swTrue.ElapsedMilliseconds)} ms");
    }
}