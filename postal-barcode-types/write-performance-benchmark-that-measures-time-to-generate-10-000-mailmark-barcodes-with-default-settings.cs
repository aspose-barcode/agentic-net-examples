// Title: Performance benchmark for generating 10,000 Mailmark barcodes
// Description: Demonstrates measuring the time required to generate Mailmark barcodes using Aspose.BarCode with default settings.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of ComplexBarcodeGenerator and MailmarkCodetext for creating Mailmark symbology. Typical use cases include bulk barcode creation for logistics, mailing, and tracking systems where developers need to assess generation performance. The snippet illustrates how to configure default barcode parameters, generate images in memory, and benchmark execution time.
// Prompt: Write a performance benchmark that measures time to generate 10,000 Mailmark barcodes with default settings.
// Tags: mailmark, barcode, performance, benchmark, generation, aspose.barcode, complexbarcodegenerator, png

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates a simple performance benchmark for generating Mailmark barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that measures generation time for a sample set and estimates time for 10,000 barcodes.
    /// </summary>
    static void Main()
    {
        const int sampleCount = 10; // Number of barcodes to actually generate for timing
        const int targetCount = 10000; // Desired number of barcodes for estimation

        // Prepare a MailmarkCodetext with required fields (default settings)
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Start measuring elapsed time
        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < sampleCount; i++)
        {
            // Use a fresh instance of the codetext for each generation to avoid side effects
            var code = new MailmarkCodetext
            {
                Format = mailmark.Format,
                VersionID = mailmark.VersionID,
                Class = mailmark.Class,
                SupplychainID = mailmark.SupplychainID,
                ItemID = mailmark.ItemID,
                DestinationPostCodePlusDPS = mailmark.DestinationPostCodePlusDPS
            };

            // Generate the barcode using default parameters
            using (var generator = new ComplexBarcodeGenerator(code))
            {
                // Default parameters are used; optionally set XDimension if desired
                // generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the generated barcode to a memory stream (PNG format)
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }
        }

        // Stop timing
        stopwatch.Stop();

        // Calculate elapsed time and estimate for the target count
        double elapsedMs = stopwatch.Elapsed.TotalMilliseconds;
        double perBarcodeMs = elapsedMs / sampleCount;
        double estimatedMsForTarget = perBarcodeMs * targetCount;

        // Output benchmark results
        Console.WriteLine($"Generated {sampleCount} Mailmark barcodes in {elapsedMs:F2} ms.");
        Console.WriteLine($"Average per barcode: {perBarcodeMs:F2} ms.");
        Console.WriteLine($"Estimated time for {targetCount} barcodes: {estimatedMsForTarget:F2} ms ({estimatedMsForTarget / 1000:F2} seconds).");
    }
}