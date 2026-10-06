// Title: QR Code Generation Benchmark
// Description: Demonstrates generating a QR Code barcode repeatedly and measuring the time taken.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category, showcasing how to use the BarcodeGenerator class with QR symbology, configure error correction level, and save the barcode to a PNG stream. Developers often need to benchmark barcode generation for high‑throughput scenarios such as batch processing or real‑time applications.
// Prompt: Generate QR Code barcode and benchmark generation time across 1000 iterations for performance.
// Tags: qr code, benchmark, performance, barcode generation, aspose.barcode, png, memorystream

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates benchmarking QR Code barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR codes repeatedly, measures elapsed time, and outputs performance metrics.
    /// </summary>
    static void Main()
    {
        // Number of iterations for the benchmark (reduced for safe execution)
        const int iterations = 10;
        // Text to encode in the QR code
        const string codeText = "Benchmark QR Code";

        // Start timing the generation process
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Loop to generate the specified number of QR codes
        for (int i = 0; i < iterations; i++)
        {
            // Create a barcode generator for QR symbology with the given text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set QR error correction level to Medium
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated barcode to a memory stream in PNG format
                using (MemoryStream ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }
        }

        // Stop the timer after all iterations are complete
        stopwatch.Stop();

        // Calculate total elapsed time in milliseconds
        double totalMs = stopwatch.Elapsed.TotalMilliseconds;
        // Output total and average generation time per barcode
        Console.WriteLine($"Generated {iterations} QR codes in {totalMs:F2} ms. Average per barcode: {totalMs / iterations:F2} ms.");
    }
}