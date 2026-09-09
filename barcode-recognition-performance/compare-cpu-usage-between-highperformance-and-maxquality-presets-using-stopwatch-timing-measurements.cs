// Title: Compare CPU usage between HighPerformance and MaxQuality barcode quality presets
// Description: Demonstrates measuring the time taken to read a Code128 barcode using Aspose.BarCode with two different quality settings. The example shows how to generate a barcode, read it with HighPerformance and MaxQuality presets, and compare the elapsed times.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning category, illustrating how to use the QualitySettings property of BarCodeReader to balance speed and accuracy. It covers barcode generation with BarcodeGenerator, reading with BarCodeReader, and timing with Stopwatch—common tasks for developers optimizing barcode processing in high‑throughput or quality‑critical applications.
// Prompt: Compare CPU usage between HighPerformance and MaxQuality presets using Stopwatch timing measurements.
// Tags: barcode, performance, qualitysettings, code128, stopwatch, generation, recognition, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates measuring barcode reading performance using different quality presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, reads it with HighPerformance and MaxQuality settings, and outputs timing results.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store the generated barcode image.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodePerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "barcode.png");

        // Generate a Code128 barcode and save it as PNG.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Measure reading time using HighPerformance preset.
        long highPerfMs = MeasurePreset(barcodePath, QualitySettings.HighPerformance, "HighPerformance");
        // Measure reading time using MaxQuality preset.
        long maxQualMs = MeasurePreset(barcodePath, QualitySettings.MaxQuality, "MaxQuality");

        // Output the timing comparison.
        Console.WriteLine($"HighPerformance time: {highPerfMs} ms");
        Console.WriteLine($"MaxQuality time: {maxQualMs} ms");
        Console.WriteLine($"Difference: {maxQualMs - highPerfMs} ms");

        // Clean up temporary files and directory.
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored: cleanup failures are non‑critical for this demo.
        }
    }

    /// <summary>
    /// Reads a barcode image using the specified quality preset and returns the elapsed time in milliseconds.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="preset">QualitySettings preset to apply.</param>
    /// <param name="name">Friendly name for logging.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    static long MeasurePreset(string imagePath, QualitySettings preset, string name)
    {
        // Initialize the barcode reader for Code128 symbology.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Apply the desired quality preset.
            reader.QualitySettings = preset;

            // Start timing the read operation.
            Stopwatch sw = Stopwatch.StartNew();
            var results = reader.ReadBarCodes();
            sw.Stop();

            // Log the number of barcodes found and the elapsed time.
            Console.WriteLine($"{name}: Barcodes read: {results.Length}, Time: {sw.ElapsedMilliseconds} ms");
            return sw.ElapsedMilliseconds;
        }
    }
}