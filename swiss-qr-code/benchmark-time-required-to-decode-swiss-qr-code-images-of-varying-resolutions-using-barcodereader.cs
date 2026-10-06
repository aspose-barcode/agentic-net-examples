// Title: Benchmark decoding Swiss QR Code at multiple DPI settings
// Description: Demonstrates measuring the time required to decode Swiss QR Code images generated at different resolutions using Aspose.BarCode's BarCodeReader. Useful for performance analysis across DPI variations.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on decoding performance of QR Code symbologies. It showcases the use of ComplexBarcodeGenerator for creating Swiss QR Code images and BarCodeReader for reading them, a common scenario when evaluating processing speed for high‑resolution scans in financial applications. Developers often need to benchmark decode times to optimize scanning workflows.
// Prompt: Benchmark the time required to decode Swiss QR Code images of varying resolutions using BarCodeReader.
// Tags: swiss qr code, barcode decoding, performance benchmark, dpi, aspnet.barcode, complexbarcodegenerator, barcodereader

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program that benchmarks decoding time of Swiss QR Code images at various DPI resolutions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates Swiss QR Code images at different resolutions, decodes them, and reports elapsed time.
    /// </summary>
    static void Main()
    {
        // Define the DPI values to test. Higher DPI yields larger images and may affect decode speed.
        float[] resolutions = { 72f, 150f, 300f, 600f };

        // Prepare the Swiss QR Code payload with sample billing data.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        Console.WriteLine("Benchmarking Swiss QR Code decoding at various resolutions (DPI):");

        // Iterate over each DPI setting, generate the barcode, decode it, and measure the time taken.
        foreach (float dpi in resolutions)
        {
            // Generate a Swiss QR Code image in memory at the current DPI.
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Parameters.Resolution = dpi;

                using (var ms = new MemoryStream())
                {
                    // Save the generated barcode as PNG into the memory stream.
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading.

                    // Start timing the decode operation.
                    var stopwatch = Stopwatch.StartNew();

                    // Decode the barcode from the memory stream using QR decode type.
                    using (var reader = new BarCodeReader(ms, DecodeType.QR))
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();

                        // Access the results to ensure the operation is not optimized away.
                        int count = results?.Length ?? 0;

                        stopwatch.Stop();

                        // Output the DPI, elapsed time, and number of barcodes detected.
                        Console.WriteLine($"Resolution {dpi} DPI - Decode Time: {stopwatch.ElapsedMilliseconds} ms, Barcodes Found: {count}");
                    }
                }
            }
        }
    }
}