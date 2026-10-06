// Title: QR Code Detection Speed Comparison with XDimension Settings
// Description: Demonstrates measuring the time required to detect a QR code when the XDimension mode is set to Normal versus UseMinimalXDimension.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a QR code image and BarCodeReader with different XDimensionMode settings (Normal and UseMinimalXDimension) to evaluate detection performance. Developers often need to benchmark barcode scanning speed under various quality settings for high‑throughput or resource‑constrained scenarios.
// Prompt: Evaluate QR code detection speed when UseMinimalXDimension is disabled versus enabled.
// Tags: qr code, detection speed, xdimension, minimalxdimension, aspose.barcode, barcode generation, barcode recognition, performance test

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code, then measures detection time using
/// normal XDimension mode and the UseMinimalXDimension mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code image, runs two detection passes with different
    /// XDimension settings, prints the timing results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary folder to store the generated QR image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "qr.png");

        // ------------------------------------------------------------
        // Generate a QR code image using BarcodeGenerator
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code Text"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Detect the QR code with normal XDimension mode
        // ------------------------------------------------------------
        long normalTimeMs;
        int normalCount;
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            reader.QualitySettings.XDimension = XDimensionMode.Normal;
            var sw = Stopwatch.StartNew();
            var results = reader.ReadBarCodes();
            sw.Stop();
            normalTimeMs = sw.ElapsedMilliseconds;
            normalCount = results.Length;
        }

        // ------------------------------------------------------------
        // Detect the QR code with UseMinimalXDimension mode
        // ------------------------------------------------------------
        long minimalTimeMs;
        int minimalCount;
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f;
            var sw = Stopwatch.StartNew();
            var results = reader.ReadBarCodes();
            sw.Stop();
            minimalTimeMs = sw.ElapsedMilliseconds;
            minimalCount = results.Length;
        }

        // ------------------------------------------------------------
        // Output the detection results
        // ------------------------------------------------------------
        Console.WriteLine($"Normal XDimension: Detected {normalCount} barcode(s) in {normalTimeMs} ms");
        Console.WriteLine($"UseMinimalXDimension: Detected {minimalCount} barcode(s) in {minimalTimeMs} ms");

        // ------------------------------------------------------------
        // Clean up temporary files and folder
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}