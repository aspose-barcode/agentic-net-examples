// Title: Benchmark effect of AllowIncorrectBarcodes on barcode reading performance
// Description: Demonstrates how disabling the AllowIncorrectBarcodes setting speeds up barcode scanning in a high‑throughput scenario.
// Category-Description: This example belongs to the Aspose.BarCode scanning performance category, illustrating the use of BarCodeReader, QualitySettings, and DecodeType to measure processing time. Developers often need to optimize bulk barcode recognition, and toggling AllowIncorrectBarcodes is a common technique to improve throughput while maintaining accuracy.
// Prompt: Benchmark the time saved by disabling AllowIncorrectBarcodes in a high‑throughput scanning scenario.
// Tags: barcode, scanning, performance, allowincorrectbarcodes, benchmark, aspose.barcode, csharp

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates benchmarking the impact of the AllowIncorrectBarcodes setting on barcode reading speed.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, runs two benchmarks (with and without AllowIncorrectBarcodes), and reports the time saved.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Benchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images
        int sampleCount = 5;
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = "CODE" + i;
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save each barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Benchmark with AllowIncorrectBarcodes = false (disabled)
        TimeSpan timeWithoutIncorrect = BenchmarkReading(barcodeFiles, false);
        Console.WriteLine($"Reading without AllowIncorrectBarcodes: {timeWithoutIncorrect.TotalMilliseconds} ms");

        // Benchmark with AllowIncorrectBarcodes = true (enabled)
        TimeSpan timeWithIncorrect = BenchmarkReading(barcodeFiles, true);
        Console.WriteLine($"Reading with AllowIncorrectBarcodes: {timeWithIncorrect.TotalMilliseconds} ms");

        // Show time saved by disabling the setting
        double saved = timeWithoutIncorrect.TotalMilliseconds - timeWithIncorrect.TotalMilliseconds;
        Console.WriteLine($"Time saved by disabling AllowIncorrectBarcodes: {saved} ms");

        // Clean up temporary files and folder
        foreach (var file in barcodeFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder, true); } catch { }
    }

    /// <summary>
    /// Measures the time required to read a collection of barcode images with a specific AllowIncorrectBarcodes setting.
    /// </summary>
    /// <param name="files">List of image file paths containing barcodes.</param>
    /// <param name="allowIncorrect">Whether to allow incorrect barcodes during reading.</param>
    /// <returns>Elapsed time for the reading operation.</returns>
    static TimeSpan BenchmarkReading(List<string> files, bool allowIncorrect)
    {
        Stopwatch sw = Stopwatch.StartNew();

        foreach (string file in files)
        {
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Apply the quality setting for this benchmark run
                reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;

                // Read all barcodes in the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // Optionally process results (here we just count them)
                int count = results?.Length ?? 0;

                // Prevent compiler optimization removal
                if (count < 0) Console.WriteLine("Impossible");
            }
        }

        sw.Stop();
        return sw.Elapsed;
    }
}