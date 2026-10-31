// Title: Barcode Recognition Performance with QualitySettings Presets
// Description: Demonstrates generating QR code images, recognizing them with different QualitySettings presets, and logging the duration for each image to evaluate performance impact.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the QualitySettings class to control recognition accuracy versus speed. Developers often need to benchmark these presets to choose the optimal balance for high‑throughput scanning scenarios or resource‑constrained environments. The snippet serves as a reference for performance testing across multiple images.
/// Prompt: Log recognition duration for each image when varying QualitySettings presets to evaluate performance impact.
/// Tags: qr, recognition, performance, qualitysettings, aspose.barcode, csharp

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates sample QR barcodes, runs recognition with various QualitySettings presets,
/// and logs the elapsed time for each image. Useful for performance benchmarking of
/// Aspose.BarCode recognition settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, iterates over
    /// quality presets, measures recognition time, and cleans up generated files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodePerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        var imagePaths = new List<string>();

        // Generate sample QR barcode images
        for (int i = 0; i < 3; i++)
        {
            string codeText = "Sample" + i;
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(filePath);
        }

        // Define quality presets to test
        var presets = new (QualitySettings settings, string name)[]
        {
            (QualitySettings.NormalQuality, "NormalQuality"),
            (QualitySettings.HighPerformance, "HighPerformance"),
            (QualitySettings.HighQuality, "HighQuality"),
            (QualitySettings.MaxQuality, "MaxQuality")
        };

        // Perform recognition with each preset and log duration
        foreach (var preset in presets)
        {
            foreach (string imagePath in imagePaths)
            {
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"File not found: {imagePath}");
                    continue;
                }

                var stopwatch = Stopwatch.StartNew();
                int resultCount = 0;
                try
                {
                    using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                    {
                        // Apply the current quality setting
                        reader.QualitySettings = preset.settings;
                        BarCodeResult[] results = reader.ReadBarCodes();
                        resultCount = results?.Length ?? 0;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error loading image '{Path.GetFileName(imagePath)}': {ex.Message}");
                }
                stopwatch.Stop();

                Console.WriteLine($"Preset: {preset.name}, Image: {Path.GetFileName(imagePath)}, Duration: {stopwatch.ElapsedMilliseconds} ms, Detected: {resultCount}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            foreach (string file in imagePaths)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}