// Title: Compare CPU usage of HighPerformance vs MaxQuality barcode recognition presets
// Description: Generates a Code128 barcode image and measures the time required to recognize it using Aspose.BarCode's HighPerformance and MaxQuality quality settings.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and QualitySettings to control recognition performance versus accuracy. Developers often need to benchmark different quality presets to balance speed and reliability in high‑throughput scanning scenarios.
// Prompt: Compare CPU usage between HighPerformance and MaxQuality presets using Stopwatch timing measurements.
// Tags: barcode, code128, recognition, performance, qualitysettings, aspose.barcode, image, stopwatch

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates measuring and comparing CPU time for barcode recognition using different quality presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, measures recognition time with two quality settings, and outputs the comparison.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeComparison_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Measure recognition time with the HighPerformance preset
        long highPerfMs;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.QualitySettings = QualitySettings.HighPerformance;
            Stopwatch sw = Stopwatch.StartNew();
            reader.ReadBarCodes();
            sw.Stop();
            highPerfMs = sw.ElapsedMilliseconds;
            Console.WriteLine($"HighPerformance: Barcodes read: {reader.FoundBarCodes.Length}, Time: {highPerfMs} ms");
        }

        // Measure recognition time with the MaxQuality preset
        long maxQualityMs;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.QualitySettings = QualitySettings.MaxQuality;
            Stopwatch sw = Stopwatch.StartNew();
            reader.ReadBarCodes();
            sw.Stop();
            maxQualityMs = sw.ElapsedMilliseconds;
            Console.WriteLine($"MaxQuality: Barcodes read: {reader.FoundBarCodes.Length}, Time: {maxQualityMs} ms");
        }

        // Output the time difference between the two presets
        Console.WriteLine($"Time difference (MaxQuality - HighPerformance): {maxQualityMs - highPerfMs} ms");

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}