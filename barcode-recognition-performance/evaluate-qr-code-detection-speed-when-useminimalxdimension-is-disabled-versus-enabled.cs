// Title: QR Code Detection Speed with and without UseMinimalXDimension
// Description: Demonstrates measuring the time required to detect a QR code when the UseMinimalXDimension setting is disabled versus enabled.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating QR codes and the BarCodeReader for detecting them, focusing on the XDimension quality settings. Developers often need to compare detection performance under different X dimension modes to optimize scanning speed and accuracy in applications such as inventory management, ticketing, and mobile payments.
/// Prompt: Evaluate QR code detection speed when UseMinimalXDimension is disabled versus enabled.
/// Tags: qr, detection, performance, minimalxdimension, aspose.barcode, generation, recognition

using System;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code image and measures detection performance
/// with the UseMinimalXDimension quality setting both disabled and enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, then measures and prints
    /// detection times for two X dimension modes.
    /// </summary>
    static void Main()
    {
        const string imagePath = "qr.png";
        const string qrText = "Sample QR Code Text for performance test";

        // Generate a QR code image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Measure detection time with UseMinimalXDimension disabled (default Auto mode)
        long timeWithoutMinimal = MeasureDetectionTime(imagePath, useMinimal: false, out int countWithoutMinimal);

        // Measure detection time with UseMinimalXDimension enabled (custom minimal X dimension)
        long timeWithMinimal = MeasureDetectionTime(imagePath, useMinimal: true, out int countWithMinimal);

        // Output the results
        Console.WriteLine($"Detection without UseMinimalXDimension: {timeWithoutMinimal} ms, barcodes detected: {countWithoutMinimal}");
        Console.WriteLine($"Detection with UseMinimalXDimension:    {timeWithMinimal} ms, barcodes detected: {countWithMinimal}");
    }

    /// <summary>
    /// Measures the time taken to read barcodes from an image using the specified X dimension mode.
    /// </summary>
    /// <param name="imagePath">Path to the image containing the QR code.</param>
    /// <param name="useMinimal">If true, enables UseMinimalXDimension mode; otherwise uses Auto mode.</param>
    /// <param name="detectedCount">Outputs the number of barcodes detected.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    private static long MeasureDetectionTime(string imagePath, bool useMinimal, out int detectedCount)
    {
        detectedCount = 0;

        // Initialize the barcode reader for QR codes
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            if (useMinimal)
            {
                // Enable UseMinimalXDimension mode and set a minimal X dimension value
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 5f;
            }
            else
            {
                // Use default automatic X dimension detection
                reader.QualitySettings.XDimension = XDimensionMode.Auto;
            }

            // Start timing the detection process
            var stopwatch = Stopwatch.StartNew();

            // Perform barcode detection
            var results = reader.ReadBarCodes();

            // Stop timing
            stopwatch.Stop();

            // Iterate through results to ensure full processing
            foreach (var result in results)
            {
                Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                detectedCount++;
            }

            // Return elapsed time in milliseconds
            return stopwatch.ElapsedMilliseconds;
        }
    }
}