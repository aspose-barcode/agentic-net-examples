// Title: CPU Benchmark for Barcode Scanning of GIF Frames
// Description: Demonstrates measuring CPU time spent scanning barcodes in individual image files, simulating the frames of an animated GIF.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category. It showcases the use of BarcodeGenerator (for creating barcodes) and BarCodeReader (for decoding them) to evaluate CPU consumption during recognition. Developers often need such benchmarks when optimizing scanning pipelines for animated images or high‑throughput scenarios.
// Prompt: Create a performance benchmark that records CPU usage during barcode scanning of animated GIF frames.
// Tags: barcode, code128, performance, cpu, benchmark, gif, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a simple performance benchmark that records CPU usage while scanning barcodes
/// from a set of images representing frames of an animated GIF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// Generates sample barcode images, measures CPU time for each scan, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store generated barcode images.
        string tempDir = Path.Combine(Path.GetTempPath(), "GifBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Generate a collection of sample barcode PNG files.
        List<string> imageFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string filePath = Path.Combine(tempDir, $"barcode{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{i}"))
            {
                // Save each barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // Prepare for CPU usage measurement.
        Process currentProcess = Process.GetCurrentProcess();
        Stopwatch totalStopwatch = Stopwatch.StartNew();

        // Scan each image (simulating a GIF frame) and record CPU time.
        foreach (string file in imageFiles)
        {
            long cpuBefore = currentProcess.TotalProcessorTime.Ticks;

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Perform barcode recognition.
                reader.ReadBarCodes();
            }

            long cpuAfter = currentProcess.TotalProcessorTime.Ticks;
            double cpuMilliseconds = (cpuAfter - cpuBefore) * 1000.0 / Stopwatch.Frequency;
            Console.WriteLine($"File {Path.GetFileName(file)} CPU time: {cpuMilliseconds:F2} ms");
        }

        // Output total wall‑clock time for the benchmark.
        totalStopwatch.Stop();
        Console.WriteLine($"Total elapsed wall-clock time: {totalStopwatch.ElapsedMilliseconds} ms");

        // Clean up generated files and temporary directory.
        foreach (string file in imageFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempDir, true); } catch { }
    }
}