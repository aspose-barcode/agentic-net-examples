// Title: QR Code Generation Benchmark Example
// Description: Demonstrates generating a QR Code barcode using Aspose.BarCode and measuring the time required to create multiple barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with QR Code symbology, configure error correction, render the barcode to a bitmap, and benchmark performance across repeated iterations. Developers commonly need to generate barcodes in bulk for high‑throughput applications, evaluate rendering speed, and avoid file I/O by using memory streams.
// Prompt: Generate QR Code barcode and benchmark generation time across 1000 iterations for performance.
// Tags: qr code, barcode generation, performance benchmark, aspose.barcode, encode types, bitmap, memory stream

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an example that generates QR Code barcodes and benchmarks the generation time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates QR Code barcodes a specified number of times,
    /// measures the total elapsed time, and outputs average generation time.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the number of iterations.</param>
    static void Main(string[] args)
    {
        // Determine iteration count (capped to 10 for safe execution)
        int requestedIterations = 1000;
        if (args.Length > 0 && int.TryParse(args[0], out int parsed) && parsed > 0)
        {
            requestedIterations = parsed;
        }
        int iterations = Math.Min(requestedIterations, 10);

        // Prepare a stopwatch for benchmarking
        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Generate QR Code barcodes repeatedly
        for (int i = 0; i < iterations; i++)
        {
            // Create a QR Code generator with sample text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Optional: set error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Generate the barcode image as a bitmap
                Bitmap bitmap = generator.GenerateBarCodeImage();

                // Save the bitmap to a memory stream (avoids file I/O)
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                }

                // Dispose the bitmap explicitly to free resources
                bitmap.Dispose();
            }
        }

        sw.Stop();

        // Calculate average time per iteration in milliseconds
        double averageMs = sw.Elapsed.TotalMilliseconds / iterations;

        // Output benchmark results
        Console.WriteLine($"Generated {iterations} QR Code barcodes.");
        Console.WriteLine($"Total time: {sw.Elapsed.TotalMilliseconds:F2} ms");
        Console.WriteLine($"Average time per barcode: {averageMs:F2} ms");
    }
}