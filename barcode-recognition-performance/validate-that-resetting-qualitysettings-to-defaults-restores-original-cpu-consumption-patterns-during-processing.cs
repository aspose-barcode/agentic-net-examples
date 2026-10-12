// Title: Reset QualitySettings to default after high‑performance barcode reading
// Description: Demonstrates generating a QR barcode, measuring read times with different QualitySettings presets, and verifying that resetting to NormalQuality restores the original CPU consumption pattern.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning collection. It showcases the use of BarcodeGenerator, BarCodeReader, and the QualitySettings class to compare NormalQuality and HighPerformance presets. Developers often need to balance speed and accuracy when processing barcodes; this snippet illustrates how to measure impact and safely revert to default settings, a common requirement in batch processing or real‑time scanning scenarios.
// Prompt: Validate that resetting QualitySettings to defaults restores original CPU consumption patterns during processing.
// Tags: barcode, qr, qualitysettings, performance, generate, read, aspose.barcode, csharp

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a console demonstration of how QualitySettings affect barcode reading performance
/// and how resetting to the default preset restores the original CPU usage pattern.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, measures read times with different
    /// QualitySettings presets, and validates that resetting to NormalQuality returns the
    /// baseline performance.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeQualityDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Measure baseline read time using the default (NormalQuality) preset
        long baselineMs = MeasureReadTime(barcodePath, DecodeType.QR, QualitySettings.NormalQuality);
        Console.WriteLine($"Baseline (NormalQuality) read time: {baselineMs} ms");

        // Measure read time using the HighPerformance preset (lower CPU usage, potentially reduced accuracy)
        long highPerfMs = MeasureReadTime(barcodePath, DecodeType.QR, QualitySettings.HighPerformance);
        Console.WriteLine($"HighPerformance read time: {highPerfMs} ms");

        // Reset QualitySettings to the default preset and measure again
        long resetMs = MeasureReadTime(barcodePath, DecodeType.QR, QualitySettings.NormalQuality);
        Console.WriteLine($"After reset (NormalQuality) read time: {resetMs} ms");

        // Validate that the reset time is within 10 % of the baseline measurement
        double tolerance = 0.10; // 10 %
        bool isRestored = Math.Abs(resetMs - baselineMs) <= baselineMs * tolerance;
        Console.WriteLine(isRestored
            ? "QualitySettings reset restored original CPU consumption pattern."
            : "QualitySettings reset did NOT restore original CPU consumption pattern.");

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect validation outcome
        }
    }

    /// <summary>
    /// Measures the time required to read all barcodes from an image using a specified QualitySettings preset.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="decodeType">The type of barcode to decode.</param>
    /// <param name="preset">The QualitySettings preset to apply.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    static long MeasureReadTime(string imagePath, BaseDecodeType decodeType, QualitySettings preset)
    {
        Stopwatch sw = new Stopwatch();

        // Initialize the reader with the image and desired decode type
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Apply the specified quality preset
            reader.QualitySettings = preset;

            // Start timing and read all barcodes
            sw.Start();
            foreach (var result in reader.ReadBarCodes())
            {
                // Access result properties to ensure full processing occurs
                string code = result.CodeText;
                string type = result.CodeTypeName;
            }
            sw.Stop();
        }

        return sw.ElapsedMilliseconds;
    }
}