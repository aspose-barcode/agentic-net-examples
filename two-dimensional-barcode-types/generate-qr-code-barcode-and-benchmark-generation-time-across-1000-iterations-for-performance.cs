// Title: QR Code Generation Performance Benchmark
// Description: Demonstrates generating a QR Code barcode and measuring the time required to create it over multiple iterations.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and performance measurement. It showcases the use of BarcodeGenerator, QR error correction settings, and image handling with Aspose.Drawing.Imaging. Developers often need to benchmark barcode generation for high‑throughput scenarios such as bulk printing or real‑time scanning applications.
// Prompt: Generate QR Code barcode and benchmark generation time across 1000 iterations for performance.
// Tags: qr code, generation, performance, benchmark, aspose.barcode, aspose.drawing, image, png

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode repeatedly and measures the average generation time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates QR codes and reports performance metrics.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR code
        const string qrText = "https://example.com/performance-test";

        // Number of iterations for the benchmark (reduced for CI safety)
        const int iterations = 10;

        // Initialize the barcode generator once to avoid repeated setup overhead
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Configure a moderate error correction level (Level M)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Warm‑up generation to exclude JIT compilation time from the measurement
            using (var bmp = generator.GenerateBarCodeImage())
            {
                // Discard the generated image; only the generation cost matters here
            }

            // Start timing the repeated generation loop
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < iterations; i++)
            {
                // Generate the QR code image in memory for each iteration
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Simulate full processing by saving the image to a memory stream as PNG
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        // Reset stream position if further processing were required
                        ms.Position = 0;
                    }
                }
            }

            // Stop the timer after all iterations are complete
            sw.Stop();

            // Calculate average time per barcode in milliseconds
            double avgMs = sw.Elapsed.TotalMilliseconds / iterations;

            // Output the benchmark results
            Console.WriteLine($"Generated {iterations} QR codes in {sw.Elapsed.TotalMilliseconds:F2} ms (avg {avgMs:F2} ms per barcode).");
        }

        // Program ends without waiting for user input
    }
}