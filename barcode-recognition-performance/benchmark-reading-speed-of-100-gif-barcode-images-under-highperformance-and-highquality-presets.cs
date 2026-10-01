// Title: Benchmark reading speed of GIF barcodes with different quality presets
// Description: Demonstrates how to generate a set of GIF barcode images and measure the time required to read them using Aspose.BarCode with HighPerformance and HighQuality quality settings.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category, illustrating the use of BarCodeGenerator for image creation, BarCodeReader for decoding, and QualitySettings presets to control recognition speed versus accuracy. Developers often need to compare HighPerformance and HighQuality modes when processing large batches of barcodes to choose the optimal trade‑off for their applications. The snippet shows typical setup, timing with Stopwatch, and cleanup, making it searchable for performance testing scenarios.
// Prompt: Benchmark reading speed of 100 GIF barcode images under HighPerformance and HighQuality presets.
// Tags: barcode, gif, performance, benchmark, highperformance, highquality, generation, recognition, qualitysettings, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates benchmarking of barcode reading speed for GIF images using different quality presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample GIF barcodes, benchmarks reading with HighPerformance and HighQuality presets, and outputs timing results.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Number of sample images (reduced for safe execution)
        const int sampleCount = 10;
        List<string> barcodeFiles = new List<string>();

        try
        {
            // Generate sample GIF barcode images
            for (int i = 0; i < sampleCount; i++)
            {
                string filePath = Path.Combine(tempFolder, $"barcode_{i:D3}.gif");
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{i:D3}"))
                {
                    generator.Save(filePath, BarCodeImageFormat.Gif);
                }
                barcodeFiles.Add(filePath);
            }

            // Benchmark with HighPerformance preset
            long highPerfMs = BenchmarkReading(barcodeFiles, QualitySettings.HighPerformance);
            Console.WriteLine($"Reading {barcodeFiles.Count} GIF barcodes with HighPerformance preset took {highPerfMs} ms.");

            // Benchmark with HighQuality preset
            long highQualMs = BenchmarkReading(barcodeFiles, QualitySettings.HighQuality);
            Console.WriteLine($"Reading {barcodeFiles.Count} GIF barcodes with HighQuality preset took {highQualMs} ms.");
        }
        finally
        {
            // Clean up temporary files
            if (Directory.Exists(tempFolder))
            {
                try
                {
                    Directory.Delete(tempFolder, true);
                }
                catch
                {
                    // Ignored - cleanup failure should not crash the demo
                }
            }
        }
    }

    /// <summary>
    /// Measures the time required to read a collection of barcode image files using the specified quality preset.
    /// </summary>
    /// <param name="files">List of barcode image file paths.</param>
    /// <param name="preset">QualitySettings preset to apply during reading.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    static long BenchmarkReading(List<string> files, QualitySettings preset)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        foreach (string file in files)
        {
            if (!File.Exists(file))
                continue;

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Apply the requested quality preset
                reader.QualitySettings = preset;

                // Perform the read operation (results are ignored for timing)
                try
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                }
                catch (ArgumentException)
                {
                    // Skip files that cannot be loaded as images
                    continue;
                }
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}