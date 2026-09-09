// Title: CPU Usage Monitoring with Multi-Core Barcode Processing
// Description: Demonstrates how to generate barcode images, enable multi‑core processing, and record CPU usage and elapsed time while reading the barcodes.
// Category-Description: This example belongs to the Aspose.BarCode processing category, illustrating the use of BarCodeGenerator for encoding, BarCodeReader with ProcessorSettings for parallel decoding, and System.Diagnostics for performance measurement. Developers often need to process large batches of images efficiently, leveraging all CPU cores and tracking resource consumption.
// Prompt: Write code that records CPU usage statistics while ProcessorSettings.UseAllCores processes a large image set.
// Tags: barcode, code128, multithreading, cpu usage, performance, aspose.barcode, generation, recognition, .net

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates sample Code128 barcodes, reads them using all CPU cores,
/// and reports CPU time and elapsed wall‑clock time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a set of sample barcode images
        List<string> barcodeFiles = new List<string>();
        BaseEncodeType encodeType = EncodeTypes.Code128;
        for (int i = 0; i < 5; i++)
        {
            string text = $"Sample{i}";
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            BarcodeGenerator generator = new BarcodeGenerator(encodeType, text);
            generator.Save(filePath, BarCodeImageFormat.Png);
            barcodeFiles.Add(filePath);
        }

        // Enable multithreaded decoding using all available CPU cores
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // Capture initial CPU time and start a stopwatch for wall‑clock timing
        Process currentProcess = Process.GetCurrentProcess();
        TimeSpan cpuStart = currentProcess.TotalProcessorTime;
        Stopwatch watch = Stopwatch.StartNew();

        int totalFound = 0;
        BaseDecodeType decodeType = DecodeType.Code128;

        // Decode each generated barcode image
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(file, decodeType))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                totalFound += results.Length;
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Stop timing and calculate CPU usage
        watch.Stop();
        TimeSpan cpuEnd = currentProcess.TotalProcessorTime;
        TimeSpan cpuUsed = cpuEnd - cpuStart;

        // Output summary of processing results and performance metrics
        Console.WriteLine($"Processed {barcodeFiles.Count} images.");
        Console.WriteLine($"Total barcodes found: {totalFound}");
        Console.WriteLine($"Elapsed time: {watch.ElapsedMilliseconds} ms");
        Console.WriteLine($"CPU time used: {cpuUsed.TotalMilliseconds} ms");
    }
}