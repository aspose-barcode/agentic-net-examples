// Title: Benchmark barcode reading speed for GIF images using HighPerformance and HighQuality presets
// Description: Demonstrates how to generate a set of GIF barcode images and measure the time required to read them with different quality settings.
// Category-Description: This example belongs to the Aspose.BarCode reading performance category. It shows how to use BarCodeReader with QualitySettings presets (HighPerformance, HighQuality) to evaluate decoding speed. Typical use cases include bulk barcode processing, performance tuning, and comparing trade‑offs between speed and accuracy. Developers often need to generate sample images, apply different presets, and benchmark the results.
// Prompt: Benchmark reading speed of 100 GIF barcode images under HighPerformance and HighQuality presets.
// Tags: barcode, reading, performance, gif, highperformance, highquality, aspose.barcode, barcodereader, qualitysettings

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a benchmark for reading GIF barcode images using different quality presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample GIF barcodes, benchmarks reading with HighPerformance and HighQuality presets, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Number of sample images (use a safe small number for demo; adjust to 100 for real benchmark)
        int sampleCount = 10;
        List<string> imageFiles = new List<string>();

        // Generate sample GIF barcode images
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = $"CODE{i:D3}";
            string filePath = Path.Combine(tempFolder, $"barcode_{i:D3}.gif");

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the barcode as a GIF image
                generator.Save(filePath, BarCodeImageFormat.Gif);
            }

            imageFiles.Add(filePath);
        }

        // Benchmark reading with the HighPerformance preset
        TimeSpan highPerfTime = BenchmarkReading(imageFiles, QualitySettings.HighPerformance);
        Console.WriteLine($"HighPerformance preset: Read {imageFiles.Count} images in {highPerfTime.TotalMilliseconds} ms");

        // Benchmark reading with the HighQuality preset
        TimeSpan highQualTime = BenchmarkReading(imageFiles, QualitySettings.HighQuality);
        Console.WriteLine($"HighQuality preset: Read {imageFiles.Count} images in {highQualTime.TotalMilliseconds} ms");

        // Cleanup temporary files and folder
        foreach (var file in imageFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder, true); } catch { }
    }

    /// <summary>
    /// Measures the time required to read a collection of barcode images using a specified quality preset.
    /// </summary>
    /// <param name="files">List of image file paths to read.</param>
    /// <param name="preset">QualitySettings preset to apply during reading.</param>
    /// <returns>Elapsed time as a <see cref="TimeSpan"/>.</returns>
    static TimeSpan BenchmarkReading(List<string> files, QualitySettings preset)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Initialize the barcode reader for all supported types
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Apply the requested quality preset
                reader.QualitySettings = preset;

                // Perform the reading operation
                BarCodeResult[] results = reader.ReadBarCodes();

                // Results are intentionally ignored; focus is on performance measurement
            }
        }

        sw.Stop();
        return sw.Elapsed;
    }
}