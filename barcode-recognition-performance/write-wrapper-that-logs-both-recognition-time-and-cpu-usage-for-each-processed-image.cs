// Title: Barcode recognition timing and CPU usage wrapper example
// Description: Demonstrates how to generate sample barcodes, recognize them, and log both elapsed recognition time and CPU processing time for each image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows usage of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, while measuring performance metrics such as wall‑clock time and processor time. Developers working on high‑throughput scanning solutions often need to benchmark recognition speed and CPU load, making this pattern useful for performance tuning and monitoring.
// Prompt: Write a wrapper that logs both recognition time and CPU usage for each processed image.
// Tags: barcode, recognition, performance, timing, cpu usage, generation, aspose.barcode, qr, csharp

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, recognition, and performance logging (recognition time and CPU usage).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them while measuring time and CPU usage, and outputs results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a set of sample barcode images
        List<string> barcodeFiles = GenerateSampleBarcodes(tempFolder, 3);

        // Process each image: measure recognition time and CPU usage
        foreach (string filePath in barcodeFiles)
        {
            // Verify the file exists before attempting to read
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Initialize the barcode reader for all supported symbologies
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Start wall‑clock timer
                Stopwatch watch = Stopwatch.StartNew();

                // Capture CPU time before recognition
                TimeSpan cpuStart = Process.GetCurrentProcess().TotalProcessorTime;

                try
                {
                    // Perform barcode recognition
                    reader.ReadBarCodes();
                }
                catch (Exception ex)
                {
                    // Log any errors encountered during reading
                    Console.WriteLine($"Error reading {Path.GetFileName(filePath)}: {ex.Message}");
                    continue;
                }

                // Capture CPU time after recognition
                TimeSpan cpuEnd = Process.GetCurrentProcess().TotalProcessorTime;
                watch.Stop();

                // Calculate CPU time spent in milliseconds
                double cpuMs = (cpuEnd - cpuStart).TotalMilliseconds;

                // Output performance metrics for the current file
                Console.WriteLine($"File: {Path.GetFileName(filePath)} - Recognition time: {watch.ElapsedMilliseconds} ms, CPU time: {cpuMs:F2} ms");

                // List all detected barcodes and their contents
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    Console.WriteLine($"  {result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors (e.g., files in use)
        }
    }

    /// <summary>
    /// Generates a specified number of QR code barcode images in the given folder.
    /// </summary>
    /// <param name="folder">The directory where barcode images will be saved.</param>
    /// <param name="count">The number of barcode images to generate.</param>
    /// <returns>A list of file paths to the generated barcode images.</returns>
    static List<string> GenerateSampleBarcodes(string folder, int count)
    {
        var files = new List<string>();
        for (int i = 0; i < count; i++)
        {
            // Define barcode text and output file path
            string text = $"Sample{i + 1}";
            string filePath = Path.Combine(folder, $"barcode_{i + 1}.png");

            // Create QR code barcode with optional appearance settings
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                generator.Parameters.Barcode.XDimension.Point = 2.0f; // Set module size
                generator.Parameters.Barcode.FilledBars = true;       // Use filled bars

                // Save the generated barcode as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            files.Add(filePath);
        }
        return files;
    }
}