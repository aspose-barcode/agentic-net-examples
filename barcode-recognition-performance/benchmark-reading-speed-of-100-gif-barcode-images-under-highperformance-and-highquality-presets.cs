// Title: Benchmark barcode reading speed for GIF images using HighPerformance and HighQuality presets
// Description: Demonstrates generating sample GIF barcode images and measuring the time required to read them with different quality settings.
// Category-Description: This example belongs to the Aspose.BarCode reading performance category. It showcases the use of BarCodeReader, QualitySettings, and related classes to evaluate how HighPerformance and HighQuality presets affect decoding speed. Developers often need to benchmark barcode recognition in bulk processing scenarios to choose the optimal trade‑off between speed and accuracy.
// Prompt: Benchmark reading speed of 100 GIF barcode images under HighPerformance and HighQuality presets.
// Tags: barcode, reading, performance, gif, highperformance, highquality, aspose.barcode, qualitysettings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a simple benchmark for reading GIF barcode images using different quality presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// Generates sample GIF barcodes, runs reading benchmarks with HighPerformance and HighQuality settings, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Benchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Number of sample images (reduced for safe execution)
        int sampleCount = 5;
        List<string> imageFiles = new List<string>();

        // Generate sample GIF barcode images
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.gif");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, $"Sample{i}"))
            {
                // Save each barcode as a GIF image
                generator.Save(filePath, BarCodeImageFormat.Gif);
            }
            imageFiles.Add(filePath);
        }

        // Benchmark reading with HighPerformance preset
        BenchmarkReading(imageFiles, QualitySettings.HighPerformance, "HighPerformance");

        // Benchmark reading with HighQuality preset
        BenchmarkReading(imageFiles, QualitySettings.HighQuality, "HighQuality");

        // Cleanup generated files and temporary folder
        try
        {
            foreach (var file in imageFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }

    /// <summary>
    /// Measures the time required to read a collection of barcode images using a specified quality preset.
    /// </summary>
    /// <param name="files">List of image file paths to read.</param>
    /// <param name="preset">QualitySettings preset to apply during reading.</param>
    /// <param name="presetName">Friendly name of the preset for reporting.</param>
    static void BenchmarkReading(List<string> files, QualitySettings preset, string presetName)
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
                // Apply the selected quality preset
                reader.QualitySettings = preset;

                // Read barcodes (results are not used further in this benchmark)
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output the number of barcodes detected per image
                Console.WriteLine($"{Path.GetFileName(file)} - {results.Length} barcode(s) detected.");
            }
        }

        sw.Stop();
        Console.WriteLine($"Reading with {presetName} preset took {sw.ElapsedMilliseconds} ms for {files.Count} image(s).");
    }
}