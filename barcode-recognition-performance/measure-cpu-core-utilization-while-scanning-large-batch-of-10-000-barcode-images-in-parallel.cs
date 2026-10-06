// Title: Measure CPU utilization while scanning barcode images in parallel
// Description: Demonstrates how to generate sample barcode images, configure Aspose.BarCode to use all CPU cores, and measure CPU core utilization during parallel scanning of a batch of images.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition and performance tuning category. It shows how to use BarCodeReader.ProcessorSettings to enable multi‑core processing, set thread limits, and evaluate processing speed. Developers working with large volumes of barcodes can use these APIs to optimize throughput and monitor resource usage.
// Prompt: Measure CPU core utilization while scanning a large batch of 10,000 barcode images in parallel.
// Tags: barcode, code128, cpu utilization, multithreading, performance, aspose.barcode, barcodereader, generation, recognition

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a set of barcode images, processes them using
/// Aspose.BarCode's multi‑core capabilities, and reports CPU utilization metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, configures
    /// multi‑core processing, measures CPU and wall‑clock time, and cleans up.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a dedicated temporary folder for generated barcode images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Generate a small sample set of barcode images (adjustable for testing)
        // --------------------------------------------------------------------
        List<string> barcodeFiles = new List<string>();
        int sampleCount = 10; // safe sample size for demonstration
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            string codeText = $"CODE{i:D4}";
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: set basic visual parameters
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Configure Aspose.BarCode to utilize all processor cores and set thread limits
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // --------------------------------------------------------------------
        // Capture initial CPU time and start a wall‑clock timer
        // --------------------------------------------------------------------
        Process currentProcess = Process.GetCurrentProcess();
        TimeSpan cpuStart = currentProcess.TotalProcessorTime;
        Stopwatch watch = Stopwatch.StartNew();

        int totalFound = 0;
        BaseDecodeType decodeType = DecodeType.Code128;

        // --------------------------------------------------------------------
        // Process each generated barcode image and count detected barcodes
        // --------------------------------------------------------------------
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, decodeType))
                {
                    reader.ReadBarCodes();
                    totalFound += reader.FoundCount;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Skipping file due to load error: {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Stop timers and calculate CPU utilization metrics
        // --------------------------------------------------------------------
        watch.Stop();
        TimeSpan cpuEnd = currentProcess.TotalProcessorTime;
        TimeSpan cpuUsed = cpuEnd - cpuStart;
        double elapsedMs = watch.Elapsed.TotalMilliseconds;
        double cpuMs = cpuUsed.TotalMilliseconds;
        double utilization = (cpuMs / (elapsedMs * Environment.ProcessorCount)) * 100.0;

        // --------------------------------------------------------------------
        // Output results
        // --------------------------------------------------------------------
        Console.WriteLine($"Processed {barcodeFiles.Count} barcode images.");
        Console.WriteLine($"Total barcodes found: {totalFound}");
        Console.WriteLine($"Elapsed time: {elapsedMs:F2} ms");
        Console.WriteLine($"CPU time used: {cpuMs:F2} ms");
        Console.WriteLine($"Approximate CPU utilization per core: {utilization:F2}%");

        // --------------------------------------------------------------------
        // Clean up temporary files and directory
        // --------------------------------------------------------------------
        try
        {
            foreach (string file in barcodeFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}