// Title: Barcode Scanning Benchmark for Animated GIF Frames
// Description: Demonstrates generating sample barcode images (simulating GIF frames) and measuring CPU usage while scanning them with Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for recognition, and ProcessorSettings for multithreaded execution. Developers often need to benchmark scanning speed and CPU consumption when processing large image sequences such as animated GIF frames.
// Prompt: Create a performance benchmark that records CPU usage during barcode scanning of animated GIF frames.
// Tags: barcode symbology, performance benchmark, cpu usage, animated gif, multithreading, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates sample barcode images, treats them as frames of an animated GIF,
/// and benchmarks CPU usage while scanning the frames using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store generated barcode frame images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeFrames_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images (simulating animated GIF frames)
        int frameCount = 5;
        for (int i = 0; i < frameCount; i++)
        {
            string codeText = $"Frame{i + 1}";
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                string filePath = Path.Combine(tempFolder, $"frame_{i + 1}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Configure multithreaded reading to use all available CPU cores
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 0; // 0 = let the library decide
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

        // Record CPU time before processing starts
        Process currentProcess = Process.GetCurrentProcess();
        TimeSpan cpuStart = currentProcess.TotalProcessorTime;
        Stopwatch wallClock = Stopwatch.StartNew();

        // Scan each generated frame for barcodes
        string[] files = Directory.GetFiles(tempFolder, "*.png");
        foreach (string file in files)
        {
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes in the current image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output result for demonstration (can be omitted in a real benchmark)
                    Console.WriteLine($"{Path.GetFileName(file)}: {result.CodeTypeName} -> {result.CodeText}");
                }
            }
        }

        // Stop timing and calculate CPU usage
        wallClock.Stop();
        TimeSpan cpuEnd = currentProcess.TotalProcessorTime;
        TimeSpan cpuUsed = cpuEnd - cpuStart;

        // Display benchmark results
        Console.WriteLine();
        Console.WriteLine("Benchmark Results:");
        Console.WriteLine($"Processed {files.Length} frames.");
        Console.WriteLine($"Wall-clock time: {wallClock.Elapsed.TotalMilliseconds} ms");
        Console.WriteLine($"CPU time used: {cpuUsed.TotalMilliseconds} ms");

        // Clean up temporary files and folder
        try
        {
            foreach (string file in files)
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}