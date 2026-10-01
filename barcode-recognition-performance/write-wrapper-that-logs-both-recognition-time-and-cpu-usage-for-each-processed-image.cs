// Title: Barcode Recognition Timing and CPU Usage Logger
// Description: Demonstrates how to generate sample QR barcodes, recognize them, and log both the recognition duration and CPU time consumed for each image.
// Category-Description: This example belongs to the Aspose.BarCode performance monitoring category, showcasing the use of BarcodeGenerator, BarCodeReader, and system diagnostics to measure processing time and CPU usage. Developers often need to benchmark barcode recognition in batch scenarios, optimize resource consumption, or log metrics for reporting. The snippet illustrates typical patterns for generating barcodes, reading them, and capturing performance data.
// Prompt: Write a wrapper that logs both recognition time and CPU usage for each processed image.
// Tags: barcode, recognition, performance, cpu, timing, aspose.barcode, qr, generation, reading

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating QR barcodes, recognizing them, and logging performance metrics such as recognition time and CPU usage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates temporary barcode images, processes each image to log performance data, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a few sample barcode images
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 3; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, $"Sample{i}"))
            {
                // Save the generated QR code as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Process each image and log recognition time and CPU usage
        foreach (string file in barcodeFiles)
        {
            ProcessImage(file);
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

    /// <summary>
    /// Recognizes barcodes in the specified image and logs the elapsed time and CPU usage.
    /// </summary>
    /// <param name="imagePath">Full path to the barcode image file.</param>
    static void ProcessImage(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Capture CPU time before recognition
        Process currentProcess = Process.GetCurrentProcess();
        TimeSpan cpuStart = currentProcess.TotalProcessorTime;

        // Start a stopwatch to measure wall-clock recognition time
        Stopwatch timer = Stopwatch.StartNew();

        // Create a barcode reader for all supported types and assign the image source
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Explicitly set the image source as required by the rules
            reader.SetBarCodeImage(imagePath);

            // Perform recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // Stop timing after recognition completes
            timer.Stop();

            // Capture CPU time after recognition
            TimeSpan cpuEnd = currentProcess.TotalProcessorTime;
            double cpuUsedMs = (cpuEnd - cpuStart).TotalMilliseconds;

            // Output performance metrics
            Console.WriteLine($"Processed: {Path.GetFileName(imagePath)}");
            Console.WriteLine($"Recognition time: {timer.ElapsedMilliseconds} ms");
            Console.WriteLine($"CPU usage: {cpuUsedMs:F2} ms");

            // Output recognized barcode details
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"  CodeText: {result.CodeText}");
                Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
            }
        }
    }
}