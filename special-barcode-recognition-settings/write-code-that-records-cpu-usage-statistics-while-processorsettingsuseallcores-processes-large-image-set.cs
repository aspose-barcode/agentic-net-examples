// Title: Record CPU usage while processing barcodes using all CPU cores
// Description: This example generates a set of barcode images, enables multithreaded processing with ProcessorSettings.UseAllCores, and measures CPU time consumed during recognition.
// Category-Description: Demonstrates Aspose.BarCode performance tuning by using BarCodeGenerator for image creation, BarCodeReader with ProcessorSettings for parallel decoding, and .NET Process/Stopwatch for CPU metrics. Typical use cases include bulk barcode scanning, benchmarking, and optimizing server‑side image processing pipelines. Developers often need to balance throughput and resource usage, making this pattern a common reference.
// Prompt: Write code that records CPU usage statistics while ProcessorSettings.UseAllCores processes a large image set.
// Tags: barcode symbology, generation, recognition, multithreading, performance, cpu usage, aspose.barcode, csharp

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, multithreaded recognition, and CPU usage measurement.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, processes them using all CPU cores, and reports timing statistics.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
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

        // Enable multithreaded processing using all CPU cores
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // Record CPU usage before processing
        Process currentProcess = Process.GetCurrentProcess();
        TimeSpan cpuStart = currentProcess.TotalProcessorTime;
        Stopwatch totalWatch = Stopwatch.StartNew();

        // Process each barcode image
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Skipping unsupported file: {file}");
            }
        }

        // Stop timers and calculate CPU usage
        totalWatch.Stop();
        TimeSpan cpuEnd = currentProcess.TotalProcessorTime;
        TimeSpan cpuUsed = cpuEnd - cpuStart;

        Console.WriteLine($"Total processing time: {totalWatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"CPU time used: {cpuUsed.TotalMilliseconds} ms");

        // Clean up temporary files
        foreach (string file in barcodeFiles)
        {
            try
            {
                File.Delete(file);
            }
            catch
            {
                // ignore deletion errors
            }
        }

        // Remove the temporary folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // ignore deletion errors
        }
    }
}