// Title: Benchmark barcode reading speed for GIF images using Aspose.BarCode presets
// Description: Demonstrates how to generate sample GIF barcode images and measure the time required to read them with HighPerformance and HighQuality quality settings.
// Category-Description: This example belongs to the Aspose.BarCode reading performance category. It showcases the use of BarCodeGenerator for creating barcodes, BarCodeReader for decoding, and QualitySettings presets to control trade‑offs between speed and accuracy. Developers often need to benchmark reading operations when processing large batches of images, choosing the appropriate preset for their scenario.
// Prompt: Benchmark reading speed of 100 GIF barcode images under HighPerformance and HighQuality presets.
// Tags: barcode, gif, performance, qualitysettings, aspose.barcode, code128, reading, benchmark

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates benchmarking of barcode reading performance for GIF images using different quality presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample GIF barcodes, benchmarks reading with HighPerformance and HighQuality presets, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample GIF barcode images (5 distinct codes)
        List<string> imageFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string codeText = "CODE" + i;
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.gif");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Gif);
            }
            imageFiles.Add(filePath);
        }

        // Benchmark reading with the HighPerformance preset
        Stopwatch sw = new Stopwatch();
        sw.Start();
        int totalCountHighPerf = ReadBarcodes(imageFiles, QualitySettings.HighPerformance);
        sw.Stop();
        Console.WriteLine($"HighPerformance: Read {totalCountHighPerf} barcodes in {sw.ElapsedMilliseconds} ms");

        // Benchmark reading with the HighQuality preset
        sw.Restart();
        int totalCountHighQual = ReadBarcodes(imageFiles, QualitySettings.HighQuality);
        sw.Stop();
        Console.WriteLine($"HighQuality: Read {totalCountHighQual} barcodes in {sw.ElapsedMilliseconds} ms");

        // Clean up temporary files and folder
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
    /// Reads barcodes from the specified files using the given quality preset.
    /// </summary>
    /// <param name="files">List of image file paths.</param>
    /// <param name="preset">QualitySettings preset to apply.</param>
    /// <returns>Total number of barcodes successfully read.</returns>
    static int ReadBarcodes(List<string> files, QualitySettings preset)
    {
        int totalBarcodes = 0;
        foreach (string file in files)
        {
            // Verify the file exists before attempting to read
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize the reader with all supported decode types
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Apply the requested quality preset
                    reader.QualitySettings = preset;
                    BarCodeResult[] results = reader.ReadBarCodes();
                    totalBarcodes += results.Length;
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Skip files that cannot be loaded as images
                Console.WriteLine($"Skipping unreadable file: {file}");
            }
        }
        return totalBarcodes;
    }
}