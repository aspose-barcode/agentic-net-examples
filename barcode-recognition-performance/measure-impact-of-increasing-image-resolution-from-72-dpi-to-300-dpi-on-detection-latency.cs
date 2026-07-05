// Title: Barcode detection latency vs image resolution
// Description: Demonstrates how changing the image DPI from 72 to 300 affects the time required to detect a Code128 barcode.
// Prompt: Measure the impact of increasing image resolution from 72 DPI to 300 DPI on detection latency.
// Tags: barcode, code128, detection, latency, resolution, aspose.barcode, png

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates barcode images at two different DPI settings
/// and measures the detection latency for each image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates low‑ and high‑resolution barcode images,
    /// measures detection latency for each, and writes the results to the console.
    /// </summary>
    static void Main()
    {
        // Define the barcode content to be encoded.
        const string barcodeText = "1234567890";

        // Generate a low‑resolution (72 DPI) barcode image.
        byte[] lowResImage = GenerateBarcodeImage(barcodeText, 72f);

        // Generate a high‑resolution (300 DPI) barcode image.
        byte[] highResImage = GenerateBarcodeImage(barcodeText, 300f);

        // Measure how long it takes to detect the barcode in each image.
        double lowResLatency = MeasureDetectionLatency(lowResImage);
        double highResLatency = MeasureDetectionLatency(highResImage);

        // Output the measured latencies.
        Console.WriteLine($"Resolution: 72 DPI  - Detection latency: {lowResLatency:F2} ms");
        Console.WriteLine($"Resolution: 300 DPI - Detection latency: {highResLatency:F2} ms");
    }

    // Generates a barcode image with the specified resolution (DPI) and returns the PNG bytes.
    private static byte[] GenerateBarcodeImage(string text, float resolutionDpi)
    {
        // Create a barcode generator for Code128 with the supplied text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
        {
            // Apply the desired image resolution (dots per inch).
            generator.Parameters.Resolution = resolutionDpi;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the image data as a byte array.
                return ms.ToArray();
            }
        }
    }

    // Measures the time taken to detect barcodes in the provided image bytes.
    private static double MeasureDetectionLatency(byte[] imageBytes)
    {
        // Start timing.
        var stopwatch = Stopwatch.StartNew();

        // Load the image bytes into a memory stream for the reader.
        using (var ms = new MemoryStream(imageBytes))
        {
            // Initialize a barcode reader for Code128 (or use AllSupportedTypes for all symbologies).
            using (var reader = new BarCodeReader(ms, DecodeType.Code128))
            {
                // Perform the recognition operation.
                var results = reader.ReadBarCodes();

                // Iterate through results to ensure full processing and prevent optimizations.
                foreach (var result in results)
                {
                    // Output detected barcode information.
                    Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                }
            }
        }

        // Stop timing and return elapsed milliseconds.
        stopwatch.Stop();
        return stopwatch.Elapsed.TotalMilliseconds;
    }
}