using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of barcode images and measures software‑only recognition throughput.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a set of barcode images, reads them using Aspose.BarCode, and reports processing speed.
    /// </summary>
    static void Main()
    {
        // -------------------------------------------------
        // 1. Generate a small set of barcode images (5 items)
        // -------------------------------------------------
        var barcodeStreams = new List<MemoryStream>();

        for (int i = 0; i < 5; i++)
        {
            // Create a barcode generator for Code128 with unique text per iteration
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Test{i}"))
            {
                // Configure basic size parameters
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 40f;

                // Generate the barcode image into a bitmap
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Save bitmap to a memory stream in PNG format
                    var ms = new MemoryStream();
                    bitmap.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // reset for subsequent reading
                    barcodeStreams.Add(ms);
                }
            }
        }

        // -------------------------------------------------
        // 2. Software‑only recognition throughput measurement
        // -------------------------------------------------
        int softwareCount = 0;                     // total number of decoded barcodes
        var swSoftware = Stopwatch.StartNew();    // start timing

        foreach (var ms in barcodeStreams)
        {
            ms.Position = 0; // ensure stream is at the beginning before each read

            // Initialize a barcode reader for Code128
            using (var reader = new BarCodeReader(ms, DecodeType.Code128))
            {
                // Iterate over all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    softwareCount++; // increment count for each successful decode

                    // Optional: output the decoded text
                    // Console.WriteLine($"Software mode: {result.CodeText}");
                }
            }
        }

        swSoftware.Stop(); // stop timing

        // Calculate throughput (barcodes per second)
        double softwareThroughput = softwareCount / swSoftware.Elapsed.TotalSeconds;
        Console.WriteLine(
            $"Software‑only mode: Processed {softwareCount} barcodes in {swSoftware.Elapsed.TotalSeconds:F3}s ({softwareThroughput:F2} barcodes/sec)");

        // -------------------------------------------------
        // 3. Hardware acceleration (if supported)
        // -------------------------------------------------
        // Aspose.BarCode does not expose a public API to enable hardware acceleration directly.
        // Therefore this example reports that hardware acceleration is unavailable in the current context.
        Console.WriteLine("Hardware acceleration: Not available via public API; proceeding with software‑only mode.");

        // -------------------------------------------------
        // 4. Clean up memory streams
        // -------------------------------------------------
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }
    }
}