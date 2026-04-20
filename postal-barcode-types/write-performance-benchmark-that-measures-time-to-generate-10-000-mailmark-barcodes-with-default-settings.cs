using System;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const int totalBarcodes = 10000;
        const int sampleCount = 10;

        Stopwatch stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < sampleCount; i++)
        {
            var mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Set image size to avoid zero‑size errors
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Set a reasonable bar height
                generator.Parameters.Barcode.BarHeight.Point = 10f;

                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // No saving needed for benchmark
                }
            }
        }

        stopwatch.Stop();

        double elapsedMs = stopwatch.Elapsed.TotalMilliseconds;
        double avgPerBarcodeMs = elapsedMs / sampleCount;
        double estimatedTotalMs = avgPerBarcodeMs * totalBarcodes;

        Console.WriteLine($"Generated {sampleCount} Mailmark barcodes in {elapsedMs:F2} ms.");
        Console.WriteLine($"Average time per barcode: {avgPerBarcodeMs:F2} ms.");
        Console.WriteLine($"Estimated time for {totalBarcodes} barcodes: {estimatedTotalMs:F2} ms.");
    }
}