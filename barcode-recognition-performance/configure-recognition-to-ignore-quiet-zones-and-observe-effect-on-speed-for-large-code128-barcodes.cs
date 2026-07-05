// Title: Code128 barcode generation and recognition speed comparison
// Description: Demonstrates generating a large Code128 barcode, then measuring recognition time with normal and high‑performance quality settings to observe speed impact.
// Prompt: Configure recognition to ignore quiet zones and observe effect on speed for large Code128 barcodes.
// Tags: barcode symbology, recognition speed, code128, quiet zones, qualitysettings, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a large Code128 barcode, then measures recognition speed using different quality settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, verifies the image, and times recognition with normal and high‑performance settings.
    /// </summary>
    static void Main()
    {
        // Prepare a large Code128 barcode text (200 characters)
        string codeText = new string('A', 200);

        // Output image file path
        string imagePath = "code128.png";

        // Generate the barcode image and save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -----------------------------------------------------------------
        // NOTE: Aspose.BarCode does not expose a property to ignore quiet zones
        // during recognition (generator.Parameters.Barcode.QuietZone does not exist).
        // Therefore we proceed with standard recognition and focus on measuring
        // speed differences using different QualitySettings presets.
        // -----------------------------------------------------------------

        // Measure recognition time with default (NormalQuality) settings
        TimeSpan normalTime;
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            var sw = Stopwatch.StartNew();

            // Read barcodes; break after first result to keep timing focused
            foreach (var result in reader.ReadBarCodes())
            {
                // Output detection details for verification
                Console.WriteLine($"[Normal] Detected: {result.CodeTypeName}, Text: {result.CodeText}");
                break;
            }

            sw.Stop();
            normalTime = sw.Elapsed;
        }

        // Measure recognition time with HighPerformance preset (faster, lower quality)
        TimeSpan highPerfTime;
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Apply high‑performance preset to the reader
            reader.QualitySettings = QualitySettings.HighPerformance;

            var sw = Stopwatch.StartNew();

            // Read barcodes; break after first result
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"[HighPerf] Detected: {result.CodeTypeName}, Text: {result.CodeText}");
                break;
            }

            sw.Stop();
            highPerfTime = sw.Elapsed;
        }

        // Output timing comparison between the two quality settings
        Console.WriteLine($"Recognition time (NormalQuality): {normalTime.TotalMilliseconds} ms");
        Console.WriteLine($"Recognition time (HighPerformance): {highPerfTime.TotalMilliseconds} ms");
    }
}