// Title: Impact of Image Resolution on Barcode Detection Latency
// Description: Demonstrates how changing barcode image DPI from 72 to 300 affects the time required to recognize the barcode.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It showcases the use of BarcodeGenerator for creating barcodes at different resolutions and BarCodeReader for detecting them, a common task when optimizing performance in scanning applications. Developers often need to balance image quality against processing speed, and this snippet provides a measurable comparison.
// Prompt: Measure the impact of increasing image resolution from 72 DPI to 300 DPI on detection latency.
// Tags: code128, barcode generation, barcode recognition, resolution, latency, aspose.barcode, png

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates measuring barcode detection latency at different image resolutions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes at 72 DPI and 300 DPI, measures and prints detection latency for each.
    /// </summary>
    static void Main()
    {
        // Prepare test data: barcode content and symbology types for encoding and decoding
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;
        BaseDecodeType decodeType = DecodeType.Code128;

        // ------------------------------------------------------------
        // Generate barcode image at 72 DPI and measure detection latency
        // ------------------------------------------------------------
        using (var ms72 = new MemoryStream())
        {
            // Create barcode generator with specified symbology and text
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Set image resolution to 72 DPI (low resolution)
                generator.Parameters.Resolution = 72f;
                // Save generated barcode to memory stream in PNG format
                generator.Save(ms72, BarCodeImageFormat.Png);
            }

            // Measure how long it takes to recognize the barcode in the 72 DPI image
            double latency72 = MeasureLatency(ms72, decodeType);
            Console.WriteLine($"Detection latency at 72 DPI: {latency72} ms");
        }

        // ------------------------------------------------------------
        // Generate barcode image at 300 DPI and measure detection latency
        // ------------------------------------------------------------
        using (var ms300 = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Set image resolution to 300 DPI (high resolution)
                generator.Parameters.Resolution = 300f;
                generator.Save(ms300, BarCodeImageFormat.Png);
            }

            // Measure how long it takes to recognize the barcode in the 300 DPI image
            double latency300 = MeasureLatency(ms300, decodeType);
            Console.WriteLine($"Detection latency at 300 DPI: {latency300} ms");
        }
    }

    /// <summary>
    /// Measures the time required to recognize a barcode from an image stream.
    /// </summary>
    /// <param name="imageStream">Memory stream containing the barcode image.</param>
    /// <param name="decodeType">The barcode symbology to decode.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    static double MeasureLatency(MemoryStream imageStream, BaseDecodeType decodeType)
    {
        // Reset stream position to the beginning before reading
        imageStream.Position = 0;

        // Start timing the recognition process
        var stopwatch = Stopwatch.StartNew();

        // Perform barcode recognition using BarCodeReader
        using (var reader = new BarCodeReader(imageStream, decodeType))
        {
            // Iterate through all detected barcodes (expected to be one)
            foreach (var result in reader.ReadBarCodes())
            {
                // Output detected text to ensure full processing of the result
                Console.WriteLine($"Detected: {result.CodeText}");
            }
        }

        // Stop timing and return elapsed milliseconds
        stopwatch.Stop();
        return stopwatch.Elapsed.TotalMilliseconds;
    }
}