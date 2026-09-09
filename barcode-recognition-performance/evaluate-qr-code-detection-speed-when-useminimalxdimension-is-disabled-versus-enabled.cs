// Title: QR Code Detection Speed Comparison with Minimal X Dimension Setting
// Description: Demonstrates measuring the recognition time of a QR code image using Aspose.BarCode with the XDimension mode set to normal and to UseMinimalXDimension. Shows how enabling the minimal X dimension can affect detection performance.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition performance category. It illustrates how to configure the QualitySettings.XDimension property and optionally MinimalXDimension to benchmark detection speed. Developers working with QR code scanning, performance tuning, or high‑throughput barcode processing can use this pattern to compare different recognition settings and optimize throughput.
// Prompt: Evaluate QR code detection speed when UseMinimalXDimension is disabled versus enabled.
// Tags: qr code, detection speed, performance, minimalxdimension, barcoderecognition, aspnet, aspose.barcode, csharp

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a simple benchmark for QR code recognition speed using different XDimension settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, then measures recognition time
    /// with normal XDimension mode and with UseMinimalXDimension enabled.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the path for the generated QR code image
        string qrImagePath = Path.Combine(tempFolder, "qr.png");

        // Generate a QR code image containing sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose.BarCode QR Speed Test"))
        {
            generator.Save(qrImagePath, BarCodeImageFormat.Png);
        }

        // Verify that the QR code image was successfully created
        if (!File.Exists(qrImagePath))
        {
            Console.WriteLine("Failed to generate QR code image.");
            return;
        }

        // Benchmark recognition using normal XDimension mode (minimalX not set)
        TimeSpan normalTime = MeasureRecognition(qrImagePath, XDimensionMode.Normal, minimalX: null);

        // Benchmark recognition using UseMinimalXDimension mode with a minimal X value of 1.0
        TimeSpan minimalTime = MeasureRecognition(qrImagePath, XDimensionMode.UseMinimalXDimension, minimalX: 1f);

        // Output the measured times for comparison
        Console.WriteLine($"Recognition time (Normal XDimension): {normalTime.TotalMilliseconds} ms");
        Console.WriteLine($"Recognition time (UseMinimalXDimension): {minimalTime.TotalMilliseconds} ms");
    }

    /// <summary>
    /// Measures the time required to read barcodes from an image using specified XDimension settings.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="mode">The XDimension mode to apply during recognition.</param>
    /// <param name="minimalX">Optional minimal X dimension value; used only when applicable.</param>
    /// <returns>The elapsed time taken to read the barcodes.</returns>
    static TimeSpan MeasureRecognition(string imagePath, XDimensionMode mode, float? minimalX)
    {
        // Set the decode type to QR for this test
        BaseDecodeType decodeType = DecodeType.QR;
        Stopwatch sw = new Stopwatch();

        // Initialize the barcode reader with the image and decode type
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Apply the requested XDimension mode
            reader.QualitySettings.XDimension = mode;

            // If a minimal X dimension is provided, set it on the quality settings
            if (minimalX.HasValue)
            {
                reader.QualitySettings.MinimalXDimension = minimalX.Value;
            }

            // Start timing, read all barcodes, then stop timing
            sw.Start();
            var results = reader.ReadBarCodes();
            sw.Stop();

            // Output the mode used and the number of barcodes detected
            Console.WriteLine($"Mode: {mode}, Barcodes read: {results.Length}");

            // List each detected barcode's type and text
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Return the total elapsed time for the recognition operation
        return sw.Elapsed;
    }
}