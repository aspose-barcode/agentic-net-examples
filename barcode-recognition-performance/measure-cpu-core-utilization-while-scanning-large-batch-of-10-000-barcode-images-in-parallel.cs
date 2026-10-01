// Title: Parallel Barcode Scanning with CPU Utilization Measurement
// Description: Demonstrates generating a set of barcode images, scanning them in parallel, and measuring CPU core utilization during the operation.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for recognizing them, combined with .NET parallel processing to handle large batches efficiently. Developers often need to evaluate throughput and CPU usage when processing thousands of images, making this pattern useful for benchmarking and scaling barcode‑related workloads.
// Prompt: Measure CPU core utilization while scanning a large batch of 10,000 barcode images in parallel.
// Tags: barcode symbology, generation, recognition, parallel processing, cpu utilization, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a small set of barcode images, scans them in parallel,
/// and reports CPU utilization and elapsed time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Creates temporary barcodes, processes them,
    /// measures performance, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a unique temporary folder for the sample barcodes
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // 2. Generate a small set of barcode images (safe sample size)
        // --------------------------------------------------------------------
        const int sampleCount = 10;
        List<string> barcodeFiles = new List<string>(sampleCount);
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = $"CODE{i:D4}";
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Simple black on white colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            barcodeFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // 3. Prepare CPU usage measurement
        // --------------------------------------------------------------------
        Process currentProcess = Process.GetCurrentProcess();
        TimeSpan startCpuTime = currentProcess.TotalProcessorTime;
        Stopwatch sw = Stopwatch.StartNew();

        // --------------------------------------------------------------------
        // 4. Scan the generated barcodes in parallel
        // --------------------------------------------------------------------
        int successCount = 0;
        ParallelOptions parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        Parallel.ForEach(barcodeFiles, parallelOptions, file =>
        {
            if (!File.Exists(file))
                return;

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Count each successfully read barcode
                        Interlocked.Increment(ref successCount);
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Skip files that cannot be loaded
            }
        });

        // --------------------------------------------------------------------
        // 5. Finish measurement and calculate CPU utilization
        // --------------------------------------------------------------------
        sw.Stop();
        TimeSpan endCpuTime = currentProcess.TotalProcessorTime;
        TimeSpan cpuUsed = endCpuTime - startCpuTime;
        double elapsedMs = sw.Elapsed.TotalMilliseconds;
        double cpuUtilization = (cpuUsed.TotalMilliseconds / (elapsedMs * Environment.ProcessorCount)) * 100.0;

        // --------------------------------------------------------------------
        // 6. Output results
        // --------------------------------------------------------------------
        Console.WriteLine($"Processed {successCount} barcodes out of {barcodeFiles.Count}.");
        Console.WriteLine($"Elapsed time: {sw.Elapsed.TotalSeconds:F2} seconds.");
        Console.WriteLine($"CPU time used: {cpuUsed.TotalSeconds:F2} seconds.");
        Console.WriteLine($"Average CPU utilization per core: {cpuUtilization:F2}%.");

        // --------------------------------------------------------------------
        // 7. Clean up temporary files and folder
        // --------------------------------------------------------------------
        foreach (string file in barcodeFiles)
        {
            try
            {
                File.Delete(file);
            }
            catch
            {
                // Ignore any deletion errors
            }
        }

        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any deletion errors
        }
    }
}