// Title: Barcode Generation and Recognition with Performance Logging
// Description: Generates Code128 barcodes, saves them as PNG files, then recognizes each image while logging recognition time and CPU usage.
// Category-Description: This example demonstrates core Aspose.BarCode operations: barcode generation using BarcodeGenerator and barcode recognition using BarCodeReader. It showcases typical use cases such as creating visual barcode assets and processing them in batch while measuring performance metrics—useful for developers needing to benchmark or monitor resource consumption in high‑throughput scanning scenarios.
// Prompt: Write a wrapper that logs both recognition time and CPU usage for each processed image.
// Tags: barcode, code128, generation, recognition, performance, logging, aspose.barcode, png

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, recognition, and performance logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, recognizes them, and logs timing metrics.
    /// </summary>
    static void Main()
    {
        // ----------------------------------------------------------------------
        // Setup: create output directory for generated barcode images
        // ----------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // ----------------------------------------------------------------------
        // Sample data: list of texts to encode into Code128 barcodes
        // ----------------------------------------------------------------------
        string[] sampleTexts = new string[]
        {
            "Sample001",
            "Sample002",
            "Sample003",
            "Sample004",
            "Sample005"
        };

        // ----------------------------------------------------------------------
        // Generation: create a PNG barcode image for each sample text
        // ----------------------------------------------------------------------
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, sampleTexts[i]))
            {
                // Optional visual parameters for better readability
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 40f;

                // Save the generated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // ----------------------------------------------------------------------
        // Recognition & Logging: process each generated image, measuring time and CPU usage
        // ----------------------------------------------------------------------
        foreach (string file in Directory.GetFiles(outputDir, "*.png"))
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Capture CPU time before recognition starts
            Process currentProcess = Process.GetCurrentProcess();
            TimeSpan cpuStart = currentProcess.TotalProcessorTime;

            // Capture wall‑clock time before recognition starts
            Stopwatch sw = Stopwatch.StartNew();

            // Perform barcode recognition using all supported decode types
            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)} | Detected Type: {result.CodeTypeName} | CodeText: {result.CodeText}");
                }
            }

            // Stop timing measurements
            sw.Stop();
            TimeSpan cpuEnd = currentProcess.TotalProcessorTime;

            // Compute elapsed wall‑clock and CPU times in milliseconds
            double elapsedMs = sw.Elapsed.TotalMilliseconds;
            double cpuMs = (cpuEnd - cpuStart).TotalMilliseconds;

            // Output performance metrics for the current file
            Console.WriteLine($"File: {Path.GetFileName(file)} | Recognition Time: {elapsedMs:F2} ms | CPU Time: {cpuMs:F2} ms");
            Console.WriteLine(new string('-', 80));
        }
    }
}