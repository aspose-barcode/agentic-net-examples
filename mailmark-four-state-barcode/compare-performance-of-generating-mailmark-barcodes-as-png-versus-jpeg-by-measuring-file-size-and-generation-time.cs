// Title: Mailmark Barcode Generation Performance: PNG vs JPEG
// Description: Demonstrates measuring generation time and file size when creating Mailmark barcodes in PNG and JPEG formats.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of ComplexBarcodeGenerator to produce Mailmark symbology. It illustrates typical tasks such as configuring Mailmark codetext, rendering barcodes to different image formats, and profiling performance—common needs for developers integrating barcode creation into high‑throughput or size‑sensitive applications. The snippet highlights key API classes like MailmarkCodetext, ComplexBarcodeGenerator, and BarCodeImageFormat, serving as a reference for performance‑oriented barcode generation scenarios.
/// Prompt: Compare performance of generating Mailmark barcodes as PNG versus JPEG by measuring file size and generation time.
/// Tags: mailmark, barcode, performance, png, jpeg, generation, aspose.barcode, complexbarcodegenerator

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates Mailmark barcodes in PNG and JPEG formats and compares their generation time and file size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares Mailmark data, renders the barcode in two image formats,
    /// and outputs average generation time and file size for each format.
    /// </summary>
    static void Main()
    {
        // Prepare a valid Mailmark codetext with required fields.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                 // 4‑state barcode
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Define the image formats to be tested: PNG and JPEG.
        var formats = new[]
        {
            BarCodeImageFormat.Png,
            BarCodeImageFormat.Jpeg
        };

        const int iterations = 5; // Number of repetitions for averaging results.

        // Iterate over each format and measure performance.
        foreach (var format in formats)
        {
            long totalBytes = 0; // Accumulator for total file size.
            long totalTicks = 0; // Accumulator for total elapsed ticks.

            // Run the generation multiple times to obtain average metrics.
            for (int i = 0; i < iterations; i++)
            {
                // Create a generator for the Mailmark barcode.
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    // Use a memory stream to avoid disk I/O overhead.
                    using (var ms = new MemoryStream())
                    {
                        // Start timing the Save operation.
                        var sw = Stopwatch.StartNew();
                        generator.Save(ms, format);
                        sw.Stop();

                        // Accumulate elapsed time and resulting byte size.
                        totalTicks += sw.ElapsedTicks;
                        totalBytes += ms.Length;
                    }
                }
            }

            // Compute average generation time in milliseconds.
            double avgMs = (totalTicks * 1000.0) / Stopwatch.Frequency / iterations;
            // Compute average file size in kilobytes.
            double avgKb = (totalBytes / 1024.0) / iterations;

            // Friendly format name for output.
            string formatName = format == BarCodeImageFormat.Png ? "PNG" : "JPEG";

            // Display the averaged results.
            Console.WriteLine($"{formatName} - Average generation time: {avgMs:F2} ms, Average file size: {avgKb:F2} KB");
        }
    }
}