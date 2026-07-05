// Title: QR Code detection speed comparison with and without UseMinimalXDimension
// Description: Demonstrates generating a QR code image and measuring the barcode detection time when the XDimension mode is set to Auto versus UseMinimalXDimension.
// Prompt: Evaluate QR code detection speed when UseMinimalXDimension is disabled versus enabled.
// Tags: qr code, detection speed, xdimension, minimalxdimension, aspose.barcode, performance

using System;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a QR code image and compares detection performance using different XDimension settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, then measures detection time with
    /// XDimensionMode.Auto and XDimensionMode.UseMinimalXDimension.
    /// </summary>
    static void Main()
    {
        // Path for the generated QR code image
        const string imagePath = "qr.png";

        // Generate a QR code image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test QR Code"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!System.IO.File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create QR code image.");
            return;
        }

        // Measure detection time with UseMinimalXDimension disabled (default Auto mode)
        double timeAuto = MeasureReadTime(imagePath, useMinimal: false);
        Console.WriteLine($"Detection time (XDimensionMode.Auto): {timeAuto} ms");

        // Measure detection time with UseMinimalXDimension enabled
        double timeMinimal = MeasureReadTime(imagePath, useMinimal: true);
        Console.WriteLine($"Detection time (XDimensionMode.UseMinimalXDimension): {timeMinimal} ms");
    }

    // Reads the barcode from the specified image and returns the elapsed time in milliseconds.
    static double MeasureReadTime(string imagePath, bool useMinimal)
    {
        var stopwatch = new Stopwatch();

        // Initialize the barcode reader for QR codes
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Configure quality settings based on the requested XDimension mode
            if (useMinimal)
            {
                // Enable minimal X dimension detection for higher performance
                reader.QualitySettings = QualitySettings.HighPerformance;
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 2f; // example minimal size in pixels
            }
            else
            {
                // Use default detection mode (Auto)
                reader.QualitySettings = QualitySettings.HighPerformance;
                reader.QualitySettings.XDimension = XDimensionMode.Auto;
            }

            // Start timing, perform detection, then stop timing
            stopwatch.Start();
            var results = reader.ReadBarCodes();
            stopwatch.Stop();

            // Output detected code texts (optional, ensures results are processed)
            foreach (var result in results)
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
            }
        }

        // Return elapsed time in milliseconds
        return stopwatch.Elapsed.TotalMilliseconds;
    }
}