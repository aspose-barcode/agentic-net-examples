using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation at different resolutions and measures detection latency.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode at 72 DPI and 300 DPI, then measures how long it takes
    /// to detect the barcode in each image using Aspose.BarCode.
    /// </summary>
    static void Main()
    {
        // Barcode content and type
        const string codeText = "Test12345";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // ------------------------------------------------------------
        // Generate barcode image at 72 DPI (low resolution)
        // ------------------------------------------------------------
        MemoryStream lowResStream = new MemoryStream();
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set low resolution
            generator.Parameters.Resolution = 72f;
            // Save image to memory stream in PNG format
            generator.Save(lowResStream, BarCodeImageFormat.Png);
        }
        // Reset stream position for reading
        lowResStream.Position = 0;

        // ------------------------------------------------------------
        // Generate barcode image at 300 DPI (high resolution)
        // ------------------------------------------------------------
        MemoryStream highResStream = new MemoryStream();
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set high resolution
            generator.Parameters.Resolution = 300f;
            // Save image to memory stream in PNG format
            generator.Save(highResStream, BarCodeImageFormat.Png);
        }
        // Reset stream position for reading
        highResStream.Position = 0;

        // ------------------------------------------------------------
        // Measure detection latency for the 72 DPI image
        // ------------------------------------------------------------
        long lowResLatency;
        using (var reader = new BarCodeReader(lowResStream, DecodeType.AllSupportedTypes))
        {
            // Start timing
            Stopwatch sw = Stopwatch.StartNew();
            // Perform barcode detection
            var results = reader.ReadBarCodes();
            // Stop timing
            sw.Stop();
            lowResLatency = sw.ElapsedMilliseconds;

            // Output detection results
            foreach (var result in results)
            {
                Console.WriteLine($"[72 DPI] Detected: {result.CodeTypeName} - {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Measure detection latency for the 300 DPI image
        // ------------------------------------------------------------
        long highResLatency;
        using (var reader = new BarCodeReader(highResStream, DecodeType.AllSupportedTypes))
        {
            // Start timing
            Stopwatch sw = Stopwatch.StartNew();
            // Perform barcode detection
            var results = reader.ReadBarCodes();
            // Stop timing
            sw.Stop();
            highResLatency = sw.ElapsedMilliseconds;

            // Output detection results
            foreach (var result in results)
            {
                Console.WriteLine($"[300 DPI] Detected: {result.CodeTypeName} - {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Display latency measurements
        // ------------------------------------------------------------
        Console.WriteLine($"Detection latency at 72 DPI: {lowResLatency} ms");
        Console.WriteLine($"Detection latency at 300 DPI: {highResLatency} ms");

        // ------------------------------------------------------------
        // Clean up memory streams
        // ------------------------------------------------------------
        lowResStream.Dispose();
        highResStream.Dispose();
    }
}