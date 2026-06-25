using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of Mailmark barcodes using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a set number of Mailmark barcodes and measures execution time.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Number of barcodes to generate (kept small for safe execution)
        const int barcodeCount = 10;

        // Prepare a Mailmark codetext with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state format
            VersionID = 1,
            Class = "0",                    // string property
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // known‑valid sample
        };

        // Start timing the generation loop
        var stopwatch = Stopwatch.StartNew();

        // Generate the specified number of barcodes
        for (int i = 0; i < barcodeCount; i++)
        {
            // Create a generator for the current Mailmark codetext
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save the barcode image to a memory stream (PNG format) to force rendering
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }
        }

        // Stop the timer after generation completes
        stopwatch.Stop();

        // Output total and average generation times
        Console.WriteLine($"Generated {barcodeCount} Mailmark barcodes in {stopwatch.ElapsedMilliseconds} ms.");
        Console.WriteLine($"Average time per barcode: {stopwatch.ElapsedMilliseconds / (double)barcodeCount:F2} ms.");
    }
}