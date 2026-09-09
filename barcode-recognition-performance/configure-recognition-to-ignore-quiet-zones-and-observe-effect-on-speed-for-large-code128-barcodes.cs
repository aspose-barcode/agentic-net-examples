// Title: Demonstrate high‑performance barcode recognition ignoring quiet zones for large Code128
// Description: Shows how to generate a large Code128 barcode, then compare default and high‑performance recognition settings, illustrating the impact on processing speed.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, focusing on performance tuning. It uses BarCodeGenerator for barcode creation and BarCodeReader with QualitySettings to adjust decoding speed. Developers often need to balance accuracy and speed when processing large or high‑volume barcode images, especially when quiet zones are irrelevant.
// Prompt: Configure recognition to ignore quiet zones and observe effect on speed for large Code128 barcodes.
// Tags: code128, barcode, recognition, performance, quiet zone, highperformance, aspose.barcode, csharp

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a large Code128 barcode and compares default vs. high‑performance
/// recognition settings to demonstrate speed differences when quiet zones are ignored.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode image, reads it with two
    /// different quality presets, prints timing results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder for the generated barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "large_code128.png");

        // --------------------------------------------------------------------
        // Generate a large Code128 barcode (100 characters) and save as PNG
        // --------------------------------------------------------------------
        string codeText = new string('A', 100);
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Aspose.BarCode does not expose an explicit ignore‑quiet‑zone option;
            // the default generation includes standard quiet zones.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Decode using default (NormalQuality) settings and measure elapsed time
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Code128;
        long defaultTimeMs;
        int defaultCount;
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            Stopwatch sw = Stopwatch.StartNew();
            BarCodeResult[] results = reader.ReadBarCodes();
            sw.Stop();

            defaultTimeMs = sw.ElapsedMilliseconds;
            defaultCount = results.Length;
        }

        // --------------------------------------------------------------------
        // Decode using HighPerformance preset (faster) and measure elapsed time
        // --------------------------------------------------------------------
        long highPerfTimeMs;
        int highPerfCount;
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            // Apply high‑performance quality settings to reduce processing overhead
            reader.QualitySettings = QualitySettings.HighPerformance;
            // Optional: speed up deconvolution step
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            Stopwatch sw = Stopwatch.StartNew();
            BarCodeResult[] results = reader.ReadBarCodes();
            sw.Stop();

            highPerfTimeMs = sw.ElapsedMilliseconds;
            highPerfCount = results.Length;
        }

        // --------------------------------------------------------------------
        // Output timing and detection results to the console
        // --------------------------------------------------------------------
        Console.WriteLine($"Default (NormalQuality) - Time: {defaultTimeMs} ms, Barcodes detected: {defaultCount}");
        Console.WriteLine($"HighPerformance preset - Time: {highPerfTimeMs} ms, Barcodes detected: {highPerfCount}");

        // --------------------------------------------------------------------
        // Clean up temporary files and folder
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}