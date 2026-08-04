// Title: Mailmark Barcode Generation: PNG vs JPEG Performance Comparison
// Description: Demonstrates generating Mailmark barcodes as PNG and JPEG images, measuring file size and generation time for each format.
// Category-Description: Shows how to use Aspose.BarCode's ComplexBarcodeGenerator to create Mailmark barcodes, covering image format selection, stream handling, and performance measurement. This example belongs to the barcode generation and image export category, where developers often need to compare output formats for size and speed.
// Prompt: Compare performance of generating Mailmark barcodes as PNG versus JPEG by measuring file size and generation time.
// Tags: mailmark, barcode, performance, png, jpeg, imageformat, complexbarcodegenerator, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a series of Mailmark barcodes in PNG and JPEG formats,
/// then reports file size and generation time for each format.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates barcodes,
    /// saves them as PNG and JPEG, and prints performance metrics.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists
        string outputDir = "MailmarkOutput";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Number of barcode samples to generate
        int sampleCount = 5;

        for (int i = 0; i < sampleCount; i++)
        {
            // Prepare Mailmark codetext with unique ItemID for each sample
            var mailmark = new MailmarkCodetext
            {
                Format = 4,                     // 4‑state Mailmark
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762 + i,          // vary ItemID to keep records unique
                DestinationPostCodePlusDPS = "EF61AH8T " // trailing space is required
            };

            // ---------- PNG generation ----------
            string pngPath = Path.Combine(outputDir, $"mailmark_{i}_png.png");
            long pngSize;
            double pngMs;

            using (var generator = new ComplexBarcodeGenerator(mailmark))
            using (var pngStream = new MemoryStream())
            {
                // Measure time to render PNG
                var sw = Stopwatch.StartNew();
                generator.Save(pngStream, BarCodeImageFormat.Png);
                sw.Stop();
                pngMs = sw.Elapsed.TotalMilliseconds;

                // Write PNG stream to file
                pngStream.Position = 0;
                using (var file = new FileStream(pngPath, FileMode.Create, FileAccess.Write))
                {
                    pngStream.CopyTo(file);
                }
            }

            // Retrieve PNG file size
            pngSize = new FileInfo(pngPath).Length;

            // ---------- JPEG generation ----------
            string jpegPath = Path.Combine(outputDir, $"mailmark_{i}_jpeg.jpg");
            long jpegSize;
            double jpegMs;

            using (var generator = new ComplexBarcodeGenerator(mailmark))
            using (var jpegStream = new MemoryStream())
            {
                // Measure time to render JPEG
                var sw = Stopwatch.StartNew();
                generator.Save(jpegStream, BarCodeImageFormat.Jpeg);
                sw.Stop();
                jpegMs = sw.Elapsed.TotalMilliseconds;

                // Write JPEG stream to file
                jpegStream.Position = 0;
                using (var file = new FileStream(jpegPath, FileMode.Create, FileAccess.Write))
                {
                    jpegStream.CopyTo(file);
                }
            }

            // Retrieve JPEG file size
            jpegSize = new FileInfo(jpegPath).Length;

            // Output comparison results for the current record
            Console.WriteLine($"Record {i}: PNG - {pngSize} bytes, {pngMs:F2} ms; JPEG - {jpegSize} bytes, {jpegMs:F2} ms");
        }

        // Indicate that the performance comparison has finished
        Console.WriteLine("Performance comparison completed.");
    }
}