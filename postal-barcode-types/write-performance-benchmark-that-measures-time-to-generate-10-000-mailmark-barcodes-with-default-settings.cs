// Title: Mailmark Barcode Generation Performance Benchmark
// Description: Demonstrates measuring the time required to generate 10,000 Mailmark barcodes using default settings.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category, showcasing how to use ComplexBarcodeGenerator and MailmarkCodetext to create high‑volume barcodes. Developers often need to benchmark barcode generation for scalability, evaluate processing time, and optimize resource usage in bulk‑printing or real‑time systems.
// Prompt: Write a performance benchmark that measures time to generate 10,000 Mailmark barcodes with default settings.
// Tags: mailmark, barcode, performance, benchmark, generation, aspnet, aspose.barcode, complexbarcodegenerator

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Contains the entry point for the Mailmark barcode generation benchmark.
/// </summary>
class Program
{
    /// <summary>
    /// Generates 10,000 Mailmark barcodes and measures the elapsed time.
    /// </summary>
    static void Main()
    {
        // Number of barcodes to generate for the benchmark.
        const int barcodeCount = 10000;

        // Prepare a stopwatch to measure total generation time.
        Stopwatch sw = new Stopwatch();
        sw.Start();

        for (int i = 0; i < barcodeCount; i++)
        {
            // Create and populate MailmarkCodetext with required fields.
            var mailmark = new MailmarkCodetext
            {
                Format = 4,                     // 4‑state format
                VersionID = 1,                  // version identifier
                Class = "0",                    // class identifier
                SupplychainID = 384224,         // supply chain ID
                ItemID = 16563762 + i,          // unique item ID per barcode
                DestinationPostCodePlusDPS = "EF61AH8T " // valid postcode+DP
            };

            // Generate the barcode using ComplexBarcodeGenerator.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save the barcode image to a memory stream (in PNG format) and discard it.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }
        }

        sw.Stop();

        // Output the benchmark result.
        Console.WriteLine($"Generated {barcodeCount} Mailmark barcodes in {sw.ElapsedMilliseconds} ms.");
    }
}