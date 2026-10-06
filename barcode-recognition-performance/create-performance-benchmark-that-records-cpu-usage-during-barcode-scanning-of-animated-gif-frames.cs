// Title: Barcode Scanning CPU Benchmark for Animated GIF Frames
// Description: Demonstrates how to generate QR code images, treat them as GIF frames, and measure CPU time spent scanning each frame using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category, showcasing the use of BarcodeGenerator for image creation and BarCodeReader for recognition. It illustrates typical scenarios where developers need to benchmark scanning speed and CPU consumption, such as processing animated GIFs or video streams containing barcodes. The code highlights key API classes like BarcodeGenerator, BarCodeReader, EncodeTypes, and DecodeType, providing a reusable pattern for performance measurement in barcode‑related applications.
// Prompt: Create a performance benchmark that records CPU usage during barcode scanning of animated GIF frames.
// Tags: barcode, qr, performance, cpu, gif, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a sample program that benchmarks CPU usage while scanning barcode images
/// (simulating frames of an animated GIF) using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates sample QR code images, scans each image while measuring elapsed and CPU time,
    /// and outputs the results to the console.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "GifBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images (QR codes) with different texts
        List<string> imagePaths = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string codeText = $"Sample{i}";
            string imagePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(imagePath);
        }

        // Benchmark CPU usage for scanning each image (simulating GIF frames)
        Console.WriteLine("Barcode scanning benchmark (CPU usage per frame):");
        Process currentProcess = Process.GetCurrentProcess();

        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            // Record CPU time before scanning
            TimeSpan cpuStart = currentProcess.TotalProcessorTime;
            Stopwatch sw = Stopwatch.StartNew();

            // Perform barcode recognition
            using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
            {
                reader.ReadBarCodes();
            }

            // Stop timing
            sw.Stop();
            TimeSpan cpuEnd = currentProcess.TotalProcessorTime;
            TimeSpan cpuUsed = cpuEnd - cpuStart;

            // Output elapsed wall‑clock time and CPU time for this frame
            Console.WriteLine($"{Path.GetFileName(path)} - Elapsed: {sw.ElapsedMilliseconds} ms, CPU time: {cpuUsed.TotalMilliseconds:F2} ms");
        }

        // Clean up temporary files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}