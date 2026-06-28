using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates benchmarking QR code generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code multiple times and measures performance.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR code.
        const string qrText = "https://example.com";

        // Number of iterations for the benchmark.
        // Using a small count (10) to keep the demo fast and within execution limits.
        const int iterations = 10;

        // Stopwatch for measuring total generation time.
        var stopwatch = new Stopwatch();

        // Create the QR Code generator once and reuse it for all iterations.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Optional: set a higher resolution for clearer output.
            generator.Parameters.Resolution = 300f;

            // Start timing before the loop.
            stopwatch.Start();

            // Generate the QR code repeatedly.
            for (int i = 0; i < iterations; i++)
            {
                // Generate the barcode image for the current iteration.
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Force rendering by saving the image to a memory stream.
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                    }
                }
            }

            // Stop timing after all iterations are complete.
            stopwatch.Stop();
        }

        // Calculate average time per generation in milliseconds.
        double averageMs = stopwatch.Elapsed.TotalMilliseconds / iterations;

        // Output total and average generation times.
        Console.WriteLine($"Generated {iterations} QR codes in {stopwatch.Elapsed.TotalMilliseconds:F2} ms.");
        Console.WriteLine($"Average time per QR code: {averageMs:F2} ms.");
    }
}