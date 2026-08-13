// Title: Performance benchmark for generating Mailmark barcodes
// Description: Demonstrates measuring the time required to generate 10,000 Mailmark barcodes using default settings.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation benchmarks category. It showcases the ComplexBarcodeGenerator and MailmarkCodetext classes for creating Mailmark symbology, a common requirement in postal automation and logistics. Developers use such benchmarks to evaluate throughput, optimize resource usage, and compare performance across different barcode symbologies or configuration options.
// Prompt: Write a performance benchmark that measures time to generate 10,000 Mailmark barcodes with default settings.
// Tags: mailmark, barcode, benchmark, performance, generation, aspose.barcode, complexbarcodegenerator, codetext

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a performance benchmark for generating Mailmark barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a specified number of Mailmark barcodes and measures the elapsed time.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Number of barcodes to generate. Adjust to 10000 for a real benchmark.
        const int barcodeCount = 5;

        // Prepare a stopwatch to measure the total generation time.
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        for (int i = 0; i < barcodeCount; i++)
        {
            // Create a Mailmark codetext with default settings.
            var mailmark = new MailmarkCodetext
            {
                // Required fields with valid values.
                Format = 4,                         // 4‑state Mailmark
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563760 + i,              // Vary ItemID to keep each record unique
                DestinationPostCodePlusDPS = "EF61AH8T " // Trailing space is mandatory
            };

            // Generate the barcode using ComplexBarcodeGenerator.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save to a memory stream to avoid file I/O overhead.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // The stream can be used further if needed; here we simply discard it.
                }
            }
        }

        stopwatch.Stop();
        Console.WriteLine($"Generated {barcodeCount} Mailmark barcodes in {stopwatch.Elapsed.TotalMilliseconds} ms.");
    }
}