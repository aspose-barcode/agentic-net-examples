// Title: QR Code Generation Benchmark Example
// Description: Demonstrates generating a QR Code barcode using Aspose.BarCode and measuring the time taken to create multiple images.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with QR symbology, configure QR-specific parameters, and render images to streams. Typical use cases include performance testing, batch barcode creation, and integration into automated pipelines where developers need to assess generation speed and resource usage.
// Prompt: Generate QR Code barcode and benchmark generation time across 1000 iterations for performance.
// Tags: qr code, barcode generation, performance benchmark, aspose.barcode, encode types, image output, png, c#

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a console application that generates QR Code barcodes and benchmarks the generation time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates QR codes for a specified number of iterations and reports timing statistics.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the number of iterations (default 1000).</param>
    static void Main(string[] args)
    {
        // Default number of iterations; can be overridden via command‑line argument.
        int requestedIterations = 1000;
        if (args.Length > 0 && int.TryParse(args[0], out int parsed) && parsed > 0)
        {
            requestedIterations = parsed;
        }

        // Safety cap to keep execution time reasonable in CI environments.
        int iterations = Math.Min(requestedIterations, 10);

        // Start measuring elapsed time.
        var stopwatch = Stopwatch.StartNew();

        // Initialize the barcode generator for QR symbology with the target data.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set QR‑specific error correction level.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Generate the barcode image repeatedly.
            for (int i = 0; i < iterations; i++)
            {
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the generated image to a memory stream in PNG format.
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                    }
                }
            }
        }

        // Stop timing and output results.
        stopwatch.Stop();

        Console.WriteLine($"Generated {iterations} QR code(s) in {stopwatch.ElapsedMilliseconds} ms.");
        Console.WriteLine($"Average time per generation: {stopwatch.ElapsedMilliseconds / (double)iterations:F2} ms.");
    }
}