// Title: QR Code Detection Speed Comparison with XDimension Settings
// Description: Demonstrates measuring the detection time of a QR code when the XDimension quality setting is set to Normal versus UseMinimalXDimension.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category. It showcases the use of BarcodeGenerator for QR code creation and BarCodeReader with QualitySettings to adjust XDimension modes. Developers often need to evaluate recognition speed and accuracy when tweaking rendering parameters, especially for high‑throughput scanning scenarios.
// Prompt: Evaluate QR code detection speed when UseMinimalXDimension is disabled versus enabled.
// Tags: qr code, detection speed, xdimension, aspose.barcode, generation, recognition, performance

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code, then measures detection time using
/// normal XDimension and the UseMinimalXDimension mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code image, runs two
    /// detection tests with different XDimension settings, and outputs the
    /// elapsed time for each test.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store the generated QR code image.
        string tempDir = Path.Combine(Path.GetTempPath(), "QrSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "qr.png");

        // Generate QR code image using BarcodeGenerator.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose QR Test"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to generate QR code image.");
            return;
        }

        // ------------------------------------------------------------
        // Test 1: Normal XDimension
        // ------------------------------------------------------------
        using (var readerNormal = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Set quality setting to Normal XDimension.
            readerNormal.QualitySettings.XDimension = XDimensionMode.Normal;

            // Measure detection time.
            var sw = Stopwatch.StartNew();
            BarCodeResult[] results = readerNormal.ReadBarCodes();
            sw.Stop();

            Console.WriteLine($"Normal XDimension: Detected {results.Length} barcode(s) in {sw.ElapsedMilliseconds} ms");
        }

        // ------------------------------------------------------------
        // Test 2: UseMinimalXDimension
        // ------------------------------------------------------------
        using (var readerMinimal = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Enable minimal XDimension mode and set a custom minimal value.
            readerMinimal.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            readerMinimal.QualitySettings.MinimalXDimension = 1f;

            // Measure detection time.
            var sw = Stopwatch.StartNew();
            BarCodeResult[] results = readerMinimal.ReadBarCodes();
            sw.Stop();

            Console.WriteLine($"UseMinimalXDimension: Detected {results.Length} barcode(s) in {sw.ElapsedMilliseconds} ms");
        }

        // Cleanup temporary files and directory.
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any errors during cleanup.
        }
    }
}