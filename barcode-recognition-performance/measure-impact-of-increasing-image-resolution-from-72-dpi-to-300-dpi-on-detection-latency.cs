// Title: Measure barcode detection latency at different DPI settings
// Description: Demonstrates generating a QR code barcode at 72 DPI and 300 DPI, then measuring the average detection latency over multiple runs.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, illustrating how to use BarcodeGenerator for barcode creation and BarCodeReader for barcode recognition. It shows typical use cases such as adjusting image resolution, saving to a stream, and benchmarking detection performance—common tasks for developers optimizing scanning speed in high‑throughput applications.
// Prompt: Measure the impact of increasing image resolution from 72 DPI to 300 DPI on detection latency.
// Tags: qr, barcode, generation, recognition, resolution, latency, aspose.barcode, csharp

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates QR code barcodes at different resolutions and measures detection latency using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates low‑ and high‑resolution barcodes, measures average detection latency, and outputs results.
    /// </summary>
    static void Main()
    {
        // Define barcode content and symbology
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.QR;

        // Generate barcode images at 72 DPI and 300 DPI
        MemoryStream lowResStream = GenerateBarcode(encodeType, codeText, 72f);
        MemoryStream highResStream = GenerateBarcode(encodeType, codeText, 300f);

        // Number of iterations for latency measurement
        int iterations = 10;

        // Measure average detection latency for each resolution
        double lowResLatency = MeasureDetectionLatency(lowResStream, iterations);
        double highResLatency = MeasureDetectionLatency(highResStream, iterations);

        // Output the results
        Console.WriteLine($"Average detection latency at 72 DPI: {lowResLatency:F3} ms");
        Console.WriteLine($"Average detection latency at 300 DPI: {highResLatency:F3} ms");

        // Release resources
        lowResStream.Dispose();
        highResStream.Dispose();
    }

    /// <summary>
    /// Generates a barcode image with the specified resolution and returns it as a <see cref="MemoryStream"/>.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The data to encode.</param>
    /// <param name="resolution">Image resolution in DPI.</param>
    /// <returns>A memory stream containing the generated PNG image.</returns>
    private static MemoryStream GenerateBarcode(BaseEncodeType encodeType, string codeText, float resolution)
    {
        // Initialize the generator with the desired symbology and data
        var generator = new BarcodeGenerator(encodeType, codeText);
        generator.Parameters.Resolution = resolution; // Set DPI

        var stream = new MemoryStream();

        // Save the barcode as PNG into the memory stream
        generator.Save(stream, BarCodeImageFormat.Png);
        stream.Position = 0; // Reset stream position for subsequent reading

        generator.Dispose();
        return stream;
    }

    /// <summary>
    /// Measures the average detection latency (in milliseconds) for a barcode image stream over a number of iterations.
    /// </summary>
    /// <param name="barcodeStream">The stream containing the barcode image.</param>
    /// <param name="iterations">How many times to repeat the detection.</param>
    /// <returns>The average latency per detection in milliseconds.</returns>
    private static double MeasureDetectionLatency(MemoryStream barcodeStream, int iterations)
    {
        // Preserve the original stream position to allow resetting before each read
        long originalPosition = barcodeStream.Position;
        Stopwatch sw = new Stopwatch();
        long totalTicks = 0;

        for (int i = 0; i < iterations; i++)
        {
            // Reset stream to the beginning for each iteration
            barcodeStream.Position = originalPosition;

            // Use BarCodeReader to decode the QR code
            using (var reader = new BarCodeReader(barcodeStream, DecodeType.QR))
            {
                sw.Restart();
                // Perform the read operation; results are not needed for latency measurement
                var results = reader.ReadBarCodes();
                sw.Stop();

                totalTicks += sw.ElapsedTicks;
            }
        }

        // Convert total ticks to average milliseconds
        double avgMilliseconds = (totalTicks * 1000.0) / Stopwatch.Frequency / iterations;
        return avgMilliseconds;
    }
}