// Title: CPU Usage Statistics while processing barcodes with multi‑core support
// Description: Demonstrates recording CPU usage while reading a set of barcode images using ProcessorSettings.UseAllCores.
// Category-Description: This example belongs to the Aspose.BarCode processing category, showcasing multi‑core barcode reading with BarCodeReader and ProcessorSettings. It illustrates generating barcode images, enabling parallel processing, and measuring performance metrics—common tasks for developers optimizing barcode recognition workloads.
// Prompt: Write code that records CPU usage statistics while ProcessorSettings.UseAllCores processes a large image set.
// Tags: barcode symbology, code128, cpu usage, multithreading, performance, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a set of Code128 barcodes, reads them using
/// multi‑core processing, and records CPU usage statistics for the operation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates temporary barcode images, enables multi‑core reading,
    /// measures CPU and wall‑clock time, outputs results, and cleans up.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder for barcode images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodes");
        if (!Directory.Exists(tempFolder))
        {
            Directory.CreateDirectory(tempFolder);
        }

        // --------------------------------------------------------------------
        // Define sample data to encode and allocate array for image paths
        // --------------------------------------------------------------------
        string[] sampleTexts = new string[] { "ABC123", "DEF456", "GHI789", "JKL012", "MNO345" };
        string[] imagePaths = new string[sampleTexts.Length];

        // --------------------------------------------------------------------
        // Generate barcode images using BarcodeGenerator
        // --------------------------------------------------------------------
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sampleTexts[i]))
            {
                // Simple visual settings
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 100f;
                generator.Save(filePath);
            }
            imagePaths[i] = filePath;
        }

        // --------------------------------------------------------------------
        // Enable multi‑core processing for barcode reading
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        // --------------------------------------------------------------------
        // Record CPU usage and wall‑clock time before processing
        // --------------------------------------------------------------------
        Process currentProcess = Process.GetCurrentProcess();
        TimeSpan cpuStart = currentProcess.TotalProcessorTime;
        Stopwatch wallClock = Stopwatch.StartNew();

        // --------------------------------------------------------------------
        // Read each generated barcode image
        // --------------------------------------------------------------------
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            using (var reader = new BarCodeReader(path))
            {
                // Iterate through all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(path)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Stop timing and calculate CPU usage statistics
        // --------------------------------------------------------------------
        wallClock.Stop();
        TimeSpan cpuEnd = currentProcess.TotalProcessorTime;
        TimeSpan cpuUsed = cpuEnd - cpuStart;

        // --------------------------------------------------------------------
        // Output performance metrics
        // --------------------------------------------------------------------
        Console.WriteLine();
        Console.WriteLine("CPU Usage Statistics:");
        Console.WriteLine($"Wall‑clock time: {wallClock.Elapsed.TotalSeconds:F2} seconds");
        Console.WriteLine($"CPU time used : {cpuUsed.TotalSeconds:F2} seconds");
        Console.WriteLine($"CPU usage ratio (CPU time / wall time): {(cpuUsed.TotalSeconds / wallClock.Elapsed.TotalSeconds):P2}");

        // --------------------------------------------------------------------
        // Clean up temporary barcode image files
        // --------------------------------------------------------------------
        foreach (string path in imagePaths)
        {
            try
            {
                File.Delete(path);
            }
            catch
            {
                // Ignore any deletion errors
            }
        }

        // --------------------------------------------------------------------
        // Reset processor settings (optional)
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = false;
    }
}