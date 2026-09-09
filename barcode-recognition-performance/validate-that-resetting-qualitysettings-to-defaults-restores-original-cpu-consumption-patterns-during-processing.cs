// Title: Barcode Quality Settings Performance Comparison
// Description: Demonstrates how resetting Aspose.BarCode QualitySettings to default (NormalQuality) affects CPU consumption by measuring recognition time for a QR code.
// Category-Description: Shows Aspose.BarCode barcode generation and recognition workflow, focusing on QualitySettings presets. Uses BarcodeGenerator, BarCodeReader, and QualitySettings classes to illustrate typical performance tuning scenarios for developers optimizing barcode processing speed versus accuracy.
// Prompt: Validate that resetting QualitySettings to defaults restores original CPU consumption patterns during processing.
// Tags: qr, barcode, qualitysettings, performance, generation, recognition, aspnet, csharp

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR barcode, then measures recognition time using different
/// QualitySettings presets to verify that the default (NormalQuality) yields lower CPU consumption.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, measures recognition times with high and normal quality,
    /// compares the results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working directory
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeQualityTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Path for the generated barcode image
        string imagePath = Path.Combine(tempDir, "sample.png");

        // Generate a simple QR barcode image and save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Measure recognition time with the HighQuality preset
        long highQualityTicks = MeasureRecognitionTime(imagePath, QualitySettings.HighQuality);

        // Measure recognition time with the default (NormalQuality) preset
        long normalQualityTicks = MeasureRecognitionTime(imagePath, QualitySettings.NormalQuality);

        // Output the measured times
        Console.WriteLine($"HighQuality recognition time: {highQualityTicks} ticks");
        Console.WriteLine($"NormalQuality (default) recognition time: {normalQualityTicks} ticks");

        // Evaluate whether resetting to defaults reduced processing time
        if (normalQualityTicks <= highQualityTicks)
        {
            Console.WriteLine("Success: Resetting QualitySettings to defaults restored lower CPU consumption (faster processing).");
        }
        else
        {
            Console.WriteLine("Warning: Resetting QualitySettings did not reduce processing time as expected.");
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Measures the time required to read barcodes from an image using a specified QualitySettings preset.
    /// </summary>
    /// <param name="imagePath">Full path to the barcode image.</param>
    /// <param name="preset">QualitySettings preset to apply during recognition.</param>
    /// <returns>Elapsed ticks measured by Stopwatch.</returns>
    static long MeasureRecognitionTime(string imagePath, QualitySettings preset)
    {
        Stopwatch sw = new Stopwatch();

        // Specify the barcode type to decode (QR in this case)
        BaseDecodeType decode = DecodeType.QR;

        // Initialize the barcode reader with the image and decode type
        using (var reader = new BarCodeReader(imagePath, decode))
        {
            // Apply the specified quality preset
            reader.QualitySettings = preset;

            // Start timing, read barcodes, then stop timing
            sw.Start();
            BarCodeResult[] results = reader.ReadBarCodes();
            sw.Stop();

            // Output result count for verification (optional)
            Console.WriteLine($"Preset {preset.GetType().Name}.{preset}: Detected {results?.Length ?? 0} barcode(s).");
        }

        return sw.ElapsedTicks;
    }
}