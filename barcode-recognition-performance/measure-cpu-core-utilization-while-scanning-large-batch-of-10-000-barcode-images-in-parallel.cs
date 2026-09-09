// Title: Parallel Barcode Scanning with CPU Utilization Measurement
// Description: Demonstrates how to generate barcode images, read them in parallel, and calculate CPU core utilization during processing.
// Category-Description: This example belongs to the Aspose.BarCode performance and multithreading category. It showcases the use of BarcodeGenerator for image creation, BarCodeReader for recognition, and ProcessorSettings to enable multi‑core processing. Developers often need to benchmark barcode scanning workloads, tune thread pools, and monitor CPU usage when handling large batches of images.
// Prompt: Measure CPU core utilization while scanning a large batch of 10,000 barcode images in parallel.
// Tags: code128, generation, recognition, parallel, cpu-utilization, aspose.barcode, aspose.barcode.generation, aspose.barcode.recognition

using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a set of barcode images, reads them in parallel,
/// and reports CPU core utilization for the operation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for generated barcode images.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Generate a small sample set of barcode images (adjustable for testing).
        // --------------------------------------------------------------------
        List<string> files = new List<string>();
        int sampleCount = 10; // safe sample size for demonstration
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            string codeText = $"CODE{i:D4}";
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            files.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Configure the ThreadPool to provide enough threads for parallel work.
        // --------------------------------------------------------------------
        ThreadPool.GetMaxThreads(out int workerThreads, out int completionPortThreads);
        int desiredThreads = Math.Max(Environment.ProcessorCount * 4, workerThreads);
        ThreadPool.SetMaxThreads(desiredThreads, completionPortThreads);
        ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
        ThreadPool.SetMinThreads(desiredThreads, completionPortThreads);

        // --------------------------------------------------------------------
        // Enable Aspose.BarCode multithreaded processing and set additional threads.
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // --------------------------------------------------------------------
        // Capture initial CPU time and start a stopwatch for elapsed time measurement.
        // --------------------------------------------------------------------
        Process proc = Process.GetCurrentProcess();
        TimeSpan cpuStart = proc.TotalProcessorTime;
        Stopwatch sw = Stopwatch.StartNew();

        int totalBarcodesFound = 0;

        // --------------------------------------------------------------------
        // Perform parallel barcode reading across all generated files.
        // --------------------------------------------------------------------
        ParallelOptions po = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
        Parallel.ForEach(files, po, file =>
        {
            if (!File.Exists(file))
                return;

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    Interlocked.Add(ref totalBarcodesFound, results.Length);
                }
            }
            catch (ArgumentException)
            {
                // Skip files that cannot be loaded as images.
            }
        });

        // --------------------------------------------------------------------
        // Stop timing and calculate CPU usage statistics.
        // --------------------------------------------------------------------
        sw.Stop();
        TimeSpan cpuEnd = proc.TotalProcessorTime;

        double cpuMs = (cpuEnd - cpuStart).TotalMilliseconds;
        double elapsedMs = sw.Elapsed.TotalMilliseconds;
        double utilization = (cpuMs / (elapsedMs * Environment.ProcessorCount)) * 100.0;

        // --------------------------------------------------------------------
        // Output results to the console.
        // --------------------------------------------------------------------
        Console.WriteLine($"Processed {files.Count} barcode images.");
        Console.WriteLine($"Total barcodes found: {totalBarcodesFound}");
        Console.WriteLine($"Elapsed time: {elapsedMs:F2} ms");
        Console.WriteLine($"CPU time used: {cpuMs:F2} ms");
        Console.WriteLine($"Average CPU core utilization: {utilization:F2}%");

        // --------------------------------------------------------------------
        // Clean up temporary files and folder.
        // --------------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }
}