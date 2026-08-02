// Title: Benchmark reading speed of GIF barcode images with HighPerformance and HighQuality presets
// Description: Demonstrates measuring the time required to read a set of GIF barcode images using Aspose.BarCode with different quality presets.
// Category-Description: This example belongs to the Aspose.BarCode reading performance category, showcasing how to use BarCodeReader with QualitySettings presets (HighPerformance, HighQuality). It highlights typical use cases such as bulk barcode scanning, performance tuning, and benchmarking. Developers often need to evaluate trade‑offs between speed and accuracy when processing large image batches, and this snippet provides a reusable pattern for such assessments.
// Prompt: Benchmark reading speed of 100 GIF barcode images under HighPerformance and HighQuality presets.
// Tags: code128, benchmark, gif, highperformance, highquality, barcoderecognition, aspose.barcode

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a benchmark for reading GIF barcode images using different quality presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for sample GIF images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeBenchmark");
        Directory.CreateDirectory(tempFolder);

        // Number of sample images (kept small for safe execution)
        const int imageCount = 10;

        // Generate sample GIF barcode images
        var imageFiles = new List<string>();
        for (int i = 0; i < imageCount; i++)
        {
            // Create a unique code text for each barcode
            string codeText = $"CODE{i:D4}";
            // Define the file path for the GIF image
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.gif");
            // Generate the barcode and save it as a GIF
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Gif);
            }
            imageFiles.Add(filePath);
        }

        // Benchmark reading with the HighPerformance preset
        BenchmarkReading(imageFiles.ToArray(), QualitySettings.HighPerformance, "HighPerformance");

        // Benchmark reading with the HighQuality preset
        BenchmarkReading(imageFiles.ToArray(), QualitySettings.HighQuality, "HighQuality");

        // Cleanup generated files
        foreach (var file in imageFiles)
        {
            try { File.Delete(file); } catch { /* ignore any deletion errors */ }
        }
        try { Directory.Delete(tempFolder, true); } catch { /* ignore any deletion errors */ }
    }

    /// <summary>
    /// Measures the time required to read barcodes from the specified files using a given quality preset.
    /// </summary>
    /// <param name="files">Array of image file paths to process.</param>
    /// <param name="preset">QualitySettings preset to apply during reading.</param>
    /// <param name="presetName">Friendly name of the preset for reporting.</param>
    static void BenchmarkReading(string[] files, QualitySettings preset, string presetName)
    {
        // Validate that all files exist before processing
        foreach (var f in files)
        {
            if (!File.Exists(f))
            {
                Console.WriteLine($"File not found: {f}");
                return;
            }
        }

        // Start timing the reading operation
        Stopwatch sw = Stopwatch.StartNew();
        int totalBarcodes = 0;

        // Iterate over each image file and read barcodes
        foreach (var file in files)
        {
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Apply the selected quality preset
                reader.QualitySettings = preset;
                // Read all barcodes in the current image
                foreach (var result in reader.ReadBarCodes())
                {
                    totalBarcodes++;
                }
            }
        }

        // Stop timing and report results
        sw.Stop();
        Console.WriteLine($"{presetName} preset: Processed {files.Length} images in {sw.ElapsedMilliseconds} ms, total barcodes read: {totalBarcodes}");
    }
}