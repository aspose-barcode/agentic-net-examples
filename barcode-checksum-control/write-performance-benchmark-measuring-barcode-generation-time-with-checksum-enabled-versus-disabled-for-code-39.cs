// Title: Benchmark Code 39 barcode generation with and without checksum
// Description: Demonstrates measuring the time required to generate Code 39 barcodes when the checksum is enabled versus disabled.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category. It showcases the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as IsChecksumEnabled. Developers often need to compare generation speed for different settings to optimize batch processing or real‑time rendering scenarios.
// Prompt: Write a performance benchmark measuring barcode generation time with checksum enabled versus disabled for Code 39.
// Tags: barcode, code39, checksum, performance, benchmark, aspose.barcode, generation, png

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains the entry point and benchmark logic for measuring Code 39 barcode generation performance with checksum variations.
/// </summary>
class Program
{
    /// <summary>
    /// Executes benchmarks for checksum enabled and disabled scenarios.
    /// </summary>
    static void Main()
    {
        // Run benchmark with checksum enabled
        BenchmarkChecksum(true);
        // Run benchmark with checksum disabled
        BenchmarkChecksum(false);
    }

    /// <summary>
    /// Generates a set of Code 39 barcodes and measures the elapsed time.
    /// </summary>
    /// <param name="enableChecksum">True to enable checksum, false to disable.</param>
    static void BenchmarkChecksum(bool enableChecksum)
    {
        // Prepare sample texts for barcode generation
        var sampleTexts = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            sampleTexts.Add($"CODE{i}");
        }

        // Start timing
        var stopwatch = Stopwatch.StartNew();

        // Generate barcodes for each sample text
        foreach (var text in sampleTexts)
        {
            // Initialize generator with Code39FullASCII symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, text))
            {
                // Set checksum option based on the method argument
                generator.Parameters.Barcode.IsChecksumEnabled = enableChecksum ? EnableChecksum.Yes : EnableChecksum.No;

                // Generate barcode image
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Save image to memory stream in PNG format (no file I/O)
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                    }
                }
            }
        }

        // Stop timing
        stopwatch.Stop();

        // Output elapsed time
        Console.WriteLine($"Checksum {(enableChecksum ? "Enabled" : "Disabled")} generation time: {stopwatch.ElapsedMilliseconds} ms");
    }
}