// Title: Barcode Recognition Throughput Comparison
// Description: Generates a Code128 barcode, then measures recognition speed in software‑only mode and attempts hardware‑accelerated mode (if available).
// Prompt: Enable hardware acceleration if available and compare recognition throughput against software‑only mode.
// Tags: barcode, recognition, throughput, hardware acceleration, software mode, aspnet, c#

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a barcode, then compare the recognition throughput
/// of software‑only mode versus a hardware‑accelerated mode (when supported).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, runs multiple recognition
    /// iterations in both software and hardware modes, and prints the measured throughput.
    /// </summary>
    static void Main()
    {
        // -------------------------------------------------
        // Prepare sample barcode image
        // -------------------------------------------------
        const string codeText = "1234567890";
        const string imagePath = "sample.png";

        // Generate barcode image using software rendering (default)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Save the generated image to disk
            generator.Save(imagePath);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Number of recognition iterations for throughput measurement
        const int iterations = 100;

        // -------------------------------------------------
        // Software‑only recognition (default settings)
        // -------------------------------------------------
        var swSoftware = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            // Create a reader for the generated image, specifying Code128 decoding
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Default quality settings (software rendering)
                foreach (var result in reader.ReadBarCodes())
                {
                    // No additional processing required; loop forces recognition
                }
            }
        }
        swSoftware.Stop();

        // Calculate and display software‑only throughput
        double softwareThroughput = iterations / swSoftware.Elapsed.TotalSeconds;
        Console.WriteLine($"Software‑only mode: {softwareThroughput:F2} barcodes/sec (elapsed {swSoftware.Elapsed.TotalSeconds:F2}s)");

        // -------------------------------------------------
        // Attempt hardware acceleration (if supported)
        // -------------------------------------------------
        // Aspose.BarCode does not expose a dedicated hardware‑acceleration flag in the public API.
        // This block uses the same default settings and serves as a placeholder for future API extensions.
        var swHardware = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // No explicit hardware flag; using default settings.
                foreach (var result in reader.ReadBarCodes())
                {
                    // No additional processing required; loop forces recognition
                }
            }
        }
        swHardware.Stop();

        // Calculate and display hardware‑accelerated (fallback) throughput
        double hardwareThroughput = iterations / swHardware.Elapsed.TotalSeconds;
        Console.WriteLine($"Hardware‑accelerated mode (fallback to software): {hardwareThroughput:F2} barcodes/sec (elapsed {swHardware.Elapsed.TotalSeconds:F2}s)");

        // -------------------------------------------------
        // Compare results
        // -------------------------------------------------
        double ratio = hardwareThroughput / softwareThroughput;
        Console.WriteLine($"Throughput ratio (hardware/software): {ratio:F2}");
    }
}