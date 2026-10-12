// Title: Performance benchmark for generating Mailmark barcodes
// Description: Demonstrates measuring the time required to generate 10,000 Mailmark barcodes using Aspose.BarCode with default settings.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation performance testing category. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext classes to create Mailmark symbology images, a common task when evaluating throughput for bulk barcode creation in logistics and supply‑chain applications. Developers often need quick benchmarks to compare settings, hardware, or library versions.
// Prompt: Write a performance benchmark that measures time to generate 10,000 Mailmark barcodes with default settings.
// Tags: mailmark, barcode, performance, benchmark, aspose.barcode, complexbarcodegenerator, mailmarkcodetext, csharp

using System;
using System.Diagnostics;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Provides a simple performance benchmark that generates a set number of Mailmark barcodes
/// using Aspose.BarCode's ComplexBarcodeGenerator with default settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// Measures and reports the elapsed time for generating a configurable number of Mailmark barcodes.
    /// </summary>
    static void Main()
    {
        // Define the total number of barcodes we intend to generate for the benchmark.
        const int targetCount = 10000;

        // Use a smaller sample size when running in environments where generating the full set may be impractical.
        // This keeps the example fast while still demonstrating the timing logic.
        int count = Math.Min(targetCount, 10);

        // Start measuring elapsed time.
        var stopwatch = Stopwatch.StartNew();

        // Generate the specified number of Mailmark barcodes.
        for (int i = 0; i < count; i++)
        {
            // Configure the Mailmark codetext with default example values.
            var mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            // Create a generator for the Mailmark barcode.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Generate the barcode image; the bitmap is disposed immediately after creation.
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Image generated; no further action needed for benchmark.
                }
            }
        }

        // Stop the timer and output the results.
        stopwatch.Stop();
        Console.WriteLine($"Generated {count} Mailmark barcodes in {stopwatch.ElapsedMilliseconds} ms.");
    }
}