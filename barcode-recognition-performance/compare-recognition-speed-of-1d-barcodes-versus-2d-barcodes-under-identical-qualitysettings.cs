// Title: Compare recognition speed of 1D vs 2D barcodes
// Description: Demonstrates measuring the time required to recognize 1‑dimensional (Code128) and 2‑dimensional (QR) barcodes using Aspose.BarCode with identical quality settings.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition performance category. It shows how to generate barcodes with BarcodeGenerator, configure BarCodeReader QualitySettings, and benchmark recognition using Stopwatch. Developers working on high‑throughput scanning, batch processing, or performance tuning can use these patterns to compare different symbologies and optimize settings.
// Prompt: Compare recognition speed of 1D barcodes versus 2D barcodes under identical QualitySettings.
// Tags: barcode, recognition, performance, 1d, 2d, code128, qr, qualitysettings, aspose.barcode, csharp

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates sample 1D (Code128) and 2D (QR) barcodes, then benchmarks
/// the recognition speed of each using identical QualitySettings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images,
    /// measures recognition time for 1D and 2D symbologies, outputs the results,
    /// and cleans up the temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample data to encode in each barcode
        string codeText = "1234567890";
        int sampleCount = 5; // Number of barcode images per symbology

        // ------------------------------------------------------------
        // Generate 1D barcodes (Code128) and store file paths
        // ------------------------------------------------------------
        var oneDFiles = new string[sampleCount];
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"code128_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            oneDFiles[i] = filePath;
        }

        // ------------------------------------------------------------
        // Generate 2D barcodes (QR) and store file paths
        // ------------------------------------------------------------
        var twoDFiles = new string[sampleCount];
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"qr_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            twoDFiles[i] = filePath;
        }

        // ------------------------------------------------------------
        // Benchmark 1D barcode recognition
        // ------------------------------------------------------------
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        foreach (var file in oneDFiles)
        {
            if (!File.Exists(file))
                continue;

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Use the same high‑performance quality setting for a fair comparison
                reader.QualitySettings = QualitySettings.HighPerformance;
                var results = reader.ReadBarCodes();
                // Results can be processed here if needed
            }
        }

        stopwatch.Stop();
        long oneDTimeMs = stopwatch.ElapsedMilliseconds;

        // ------------------------------------------------------------
        // Benchmark 2D barcode recognition
        // ------------------------------------------------------------
        stopwatch.Restart();

        foreach (var file in twoDFiles)
        {
            if (!File.Exists(file))
                continue;

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Apply identical quality settings as used for 1D barcodes
                reader.QualitySettings = QualitySettings.HighPerformance;
                var results = reader.ReadBarCodes();
                // Results can be processed here if needed
            }
        }

        stopwatch.Stop();
        long twoDTimeMs = stopwatch.ElapsedMilliseconds;

        // Output the measured recognition times
        Console.WriteLine($"1D barcode recognition time (ms) for {sampleCount} samples: {oneDTimeMs}");
        Console.WriteLine($"2D barcode recognition time (ms) for {sampleCount} samples: {twoDTimeMs}");

        // ------------------------------------------------------------
        // Cleanup temporary files and folder
        // ------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignoring any cleanup errors (e.g., files in use)
        }
    }
}