// Title: Demonstrate effect of QualitySettings on barcode decoding performance
// Description: This example generates a QR code, then measures decoding time using high‑quality and default quality settings, showing how resetting QualitySettings can reduce CPU usage.
// Category-Description: Shows how to work with Aspose.BarCode decoding performance settings. It uses BarcodeGenerator to create a barcode, BarCodeReader with QualitySettings presets, and Stopwatch to benchmark. Developers often need to balance decoding accuracy against CPU consumption, and this example illustrates typical use cases for adjusting QualitySettings in QR code recognition scenarios.
// Prompt: Validate that resetting QualitySettings to defaults restores original CPU consumption patterns during processing.
// Tags: qr, barcode, qualitysettings, performance, decoding, aspose.barcode, benchmark

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates measuring barcode decoding performance with different QualitySettings presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, measures decoding times with high and normal quality settings,
    /// and reports the impact on CPU consumption.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the test files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeQualityTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "barcode.png");

        // Generate a QR code image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Measure decoding time with the HighQuality preset
        double highQualityTime = MeasureReadingTime(barcodePath, DecodeType.QR, QualitySettings.HighQuality);
        // Measure decoding time with the NormalQuality (default) preset
        double defaultTime = MeasureReadingTime(barcodePath, DecodeType.QR, QualitySettings.NormalQuality);

        // Output the measured times
        Console.WriteLine($"Reading time with HighQuality preset: {highQualityTime} ms");
        Console.WriteLine($"Reading time with NormalQuality (default) preset: {defaultTime} ms");

        // Compare results to determine CPU consumption impact
        if (highQualityTime > defaultTime)
        {
            Console.WriteLine("Resetting QualitySettings to defaults reduces CPU consumption (faster).");
        }
        else
        {
            Console.WriteLine("No significant CPU consumption change observed after resetting QualitySettings.");
        }

        // Clean up temporary files and directory
        try { File.Delete(barcodePath); } catch { }
        try { Directory.Delete(tempDir, true); } catch { }
    }

    /// <summary>
    /// Measures the time required to read barcodes from an image using a specified QualitySettings preset.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="decodeType">The type of barcode to decode.</param>
    /// <param name="preset">QualitySettings preset to apply during decoding.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    static double MeasureReadingTime(string imagePath, BaseDecodeType decodeType, QualitySettings preset)
    {
        var stopwatch = new Stopwatch();

        // Initialize the reader with the image and desired decode type
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Apply the specified quality settings
            reader.QualitySettings = preset;

            // Start timing, read barcodes, then stop timing
            stopwatch.Start();
            var results = reader.ReadBarCodes();
            stopwatch.Stop();

            // Output each decoded result for verification
            foreach (var result in results)
            {
                Console.WriteLine($"Decoded: {result.CodeText} ({result.CodeTypeName})");
            }
        }

        // Return the total elapsed time in milliseconds
        return stopwatch.Elapsed.TotalMilliseconds;
    }
}