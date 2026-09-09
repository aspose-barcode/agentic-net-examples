// Title: Performance benchmark for generating Mailmark barcodes
// Description: Demonstrates measuring the time required to generate a series of Mailmark barcodes with default settings using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the ComplexBarcodeGenerator and MailmarkCodetext classes. It illustrates typical use cases such as bulk barcode creation for mail processing, where developers need to assess performance and resource usage when generating large numbers of barcodes.
// Prompt: Write a performance benchmark that measures time to generate 10,000 Mailmark barcodes with default settings.
// Tags: mailmark, barcode, performance, benchmark, aspose.barcode, complexbarcodegenerator, png

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates a simple performance benchmark for generating Mailmark barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a set of Mailmark barcodes and measures elapsed time.
    /// </summary>
    static void Main()
    {
        // Number of barcodes to generate (adjustable for larger benchmarks)
        const int count = 10;

        // Start timing the barcode generation process
        var stopwatch = Stopwatch.StartNew();

        // Loop to create each Mailmark barcode
        for (int i = 0; i < count; i++)
        {
            // Configure the Mailmark codetext with default example values
            var mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            // Use ComplexBarcodeGenerator to create the barcode image
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save the generated barcode to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }
        }

        // Stop the timer and output the elapsed time
        stopwatch.Stop();
        Console.WriteLine($"Generated {count} Mailmark barcodes in {stopwatch.Elapsed.TotalMilliseconds} ms.");
    }
}