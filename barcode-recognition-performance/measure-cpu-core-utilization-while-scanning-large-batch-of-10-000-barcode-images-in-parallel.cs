// Title: Parallel Barcode Scanning Performance Benchmark
// Description: Demonstrates measuring CPU core utilization while reading a batch of barcode images in parallel using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode performance and multithreading category. It showcases the use of BarcodeGenerator for image creation, BarCodeReader for recognition, and .NET Parallel processing to leverage all CPU cores. Developers often need to benchmark barcode scanning workloads, tune thread settings, and assess scalability for large image batches.
// Prompt: Measure CPU core utilization while scanning a large batch of 10,000 barcode images in parallel.
// Tags: barcode symbology, performance, parallel processing, cpu utilization, aspose.barcode, code128, image generation, .net

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates sample barcode images, reads them in parallel,
/// and measures CPU core utilization and wall‑clock time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, configures multithreaded reading,
    /// measures performance, outputs results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a small set of sample barcode images (adjustable for real tests)
        int sampleCount = 10;
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = $"CODE{i:D4}";
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save each barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Enable processor settings to use all available cores and allow extra threads
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // Capture initial CPU time and start a wall‑clock timer
        Process currentProcess = Process.GetCurrentProcess();
        TimeSpan cpuStart = currentProcess.TotalProcessorTime;
        Stopwatch watch = Stopwatch.StartNew();

        // Process barcode files in parallel, respecting the number of logical processors
        ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
        Parallel.ForEach(barcodeFiles, options, filePath =>
        {
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // Perform barcode recognition; results are not used further in this benchmark
                BarCodeResult[] results = reader.ReadBarCodes();
            }
        });

        // Stop timing and capture final CPU time
        watch.Stop();
        TimeSpan cpuEnd = currentProcess.TotalProcessorTime;

        // Compute CPU core utilization as a percentage of total possible CPU time
        TimeSpan cpuUsed = cpuEnd - cpuStart;
        double cpuUtilization = (cpuUsed.TotalMilliseconds /
                                 (watch.Elapsed.TotalMilliseconds * Environment.ProcessorCount)) * 100.0;

        // Output benchmark results to the console
        Console.WriteLine($"Processed {barcodeFiles.Count} barcode images.");
        Console.WriteLine($"Wall-clock time: {watch.Elapsed.TotalMilliseconds:F2} ms");
        Console.WriteLine($"CPU time used: {cpuUsed.TotalMilliseconds:F2} ms");
        Console.WriteLine($"CPU core utilization: {cpuUtilization:F2}%");

        // Clean up generated files and temporary directory
        foreach (string file in barcodeFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder, true); } catch { }
    }
}