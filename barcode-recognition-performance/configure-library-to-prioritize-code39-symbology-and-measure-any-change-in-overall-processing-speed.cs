// Title: Benchmark Code39 vs Code128 Generation Speed
// Description: Generates a small set of Code39 and Code128 barcodes, measures processing time to compare performance when prioritizing Code39.
// Prompt: Configure the library to prioritize Code39 symbology and measure any change in overall processing speed.
// Tags: barcode symbology, generation, performance, aspose.barcode, csharp

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to prioritize the Code39 symbology, generate barcodes,
/// and compare its processing speed against Code128 using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes for Code39 and Code128,
    /// measures the time taken for each, and outputs a simple performance comparison.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Number of barcodes to generate for each symbology (kept small for quick execution)
        const int sampleCount = 5;

        // Ensure the output directory exists
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // -------------------- Benchmark Code39 --------------------
        // Start timing for Code39 generation
        Stopwatch swCode39 = new Stopwatch();
        swCode39.Start();

        for (int i = 0; i < sampleCount; i++)
        {
            // Prioritize Code39 symbology by explicitly using EncodeTypes.Code39
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, $"CODE39_SAMPLE_{i}"))
            {
                // Example of a Code39‑specific setting: wide‑narrow ratio
                generator.Parameters.Barcode.WideNarrowRatio = 3f;

                // Save the generated barcode image
                string filePath = Path.Combine(outputDir, $"code39_{i}.png");
                generator.Save(filePath);
            }
        }

        // Stop timing for Code39
        swCode39.Stop();
        TimeSpan code39Duration = swCode39.Elapsed;

        // -------------------- Benchmark Code128 (reference) --------------------
        // Start timing for Code128 generation
        Stopwatch swCode128 = new Stopwatch();
        swCode128.Start();

        for (int i = 0; i < sampleCount; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE128_SAMPLE_{i}"))
            {
                // No special settings required for Code128 in this benchmark
                string filePath = Path.Combine(outputDir, $"code128_{i}.png");
                generator.Save(filePath);
            }
        }

        // Stop timing for Code128
        swCode128.Stop();
        TimeSpan code128Duration = swCode128.Elapsed;

        // -------------------- Output Results --------------------
        Console.WriteLine($"Generated {sampleCount} Code39 barcodes in {code39Duration.TotalMilliseconds} ms.");
        Console.WriteLine($"Generated {sampleCount} Code128 barcodes in {code128Duration.TotalMilliseconds} ms.");

        // Simple comparison of processing times
        double speedDifference = code128Duration.TotalMilliseconds - code39Duration.TotalMilliseconds;
        if (speedDifference > 0)
        {
            Console.WriteLine($"Code39 was faster by {speedDifference} ms.");
        }
        else if (speedDifference < 0)
        {
            Console.WriteLine($"Code128 was faster by {-speedDifference} ms.");
        }
        else
        {
            Console.WriteLine("Both symbologies took the same amount of time.");
        }
    }
}