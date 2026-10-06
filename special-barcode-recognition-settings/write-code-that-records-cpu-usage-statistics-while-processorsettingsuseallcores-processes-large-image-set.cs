// Title: Record CPU Usage While Processing Barcodes with All Cores
// Description: Demonstrates how to generate barcode images, configure Aspose.BarCode to use all CPU cores for processing, and capture CPU and wall‑clock statistics.
// Category-Description: This example belongs to the Aspose.BarCode multithreading and performance category. It shows how to use BarCodeReader.ProcessorSettings to enable full‑core utilization, set thread limits, and measure processing time. Developers working with large image sets can learn typical patterns for optimizing barcode recognition using the BarCodeGenerator, BarCodeReader, and related classes.
// Prompt: Write code that records CPU usage statistics while ProcessorSettings.UseAllCores processes a large image set.
// Tags: barcode, code128, multithreading, cpu usage, performance, aspose.barcode, generation, recognition, processor settings

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, multithreaded recognition, and CPU usage measurement.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, processes them using all CPU cores, and reports performance metrics.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        List<string> barcodeFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Configure multithreaded processing to use all CPU cores
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // Record CPU time and wall‑clock time before processing starts
        Process currentProcess = Process.GetCurrentProcess();
        TimeSpan cpuStart = currentProcess.TotalProcessorTime;
        Stopwatch wallClock = Stopwatch.StartNew();

        int totalBarcodesFound = 0;

        // Process each generated barcode image
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Perform barcode recognition
                reader.ReadBarCodes();
                totalBarcodesFound += reader.FoundCount;

                // Output each recognized barcode
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Stop timing and capture final CPU usage
        wallClock.Stop();
        TimeSpan cpuEnd = currentProcess.TotalProcessorTime;

        // Compute statistics
        TimeSpan cpuUsed = cpuEnd - cpuStart;
        double cpuUsagePercent = (cpuUsed.TotalMilliseconds / wallClock.Elapsed.TotalMilliseconds) * 100.0;

        // Display processing statistics
        Console.WriteLine();
        Console.WriteLine("Processing Statistics:");
        Console.WriteLine($"Total barcodes found: {totalBarcodesFound}");
        Console.WriteLine($"Wall-clock time: {wallClock.Elapsed.TotalMilliseconds} ms");
        Console.WriteLine($"CPU time used: {cpuUsed.TotalMilliseconds} ms");
        Console.WriteLine($"CPU usage: {cpuUsagePercent:F2}%");

        // Clean up temporary files and folder
        foreach (string file in barcodeFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder, true); } catch { }
    }
}