using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates benchmarking of QR code decoding at various image resolutions
/// using Aspose.BarCode library. Generates a Swiss QR bill barcode, decodes it,
/// and reports the decoding time for each DPI setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Swiss QR barcode at multiple
    /// resolutions, decodes it, and prints timing and content information.
    /// </summary>
    static void Main()
    {
        // Define different image resolutions (dots per inch) to benchmark.
        int[] resolutions = { 72, 150, 300, 600 };

        // Prepare sample Swiss QR bill data (must be valid for generation).
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Iterate over each resolution, generate, decode, and report results.
        foreach (int dpi in resolutions)
        {
            // Generate Swiss QR barcode image at the specified resolution.
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Parameters.Resolution = (float)dpi;

                // Store the generated image in a memory stream.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading.

                    // Start timing the decoding process.
                    var stopwatch = Stopwatch.StartNew();

                    // Decode the barcode from the memory stream.
                    using (var reader = new BarCodeReader(ms, DecodeType.QR))
                    {
                        var results = reader.ReadBarCodes();

                        // Stop timing after decoding completes.
                        stopwatch.Stop();

                        // Output resolution and decoding duration.
                        Console.WriteLine($"Resolution: {dpi} DPI");
                        Console.WriteLine($"Decoding time: {stopwatch.ElapsedMilliseconds} ms");

                        // Iterate over all detected barcodes (should be one in this case).
                        foreach (var result in results)
                        {
                            Console.WriteLine($"  Detected type: {result.CodeTypeName}");
                            Console.WriteLine($"  CodeText: {result.CodeText}");

                            // Decode the complex codetext to verify Swiss QR content.
                            var decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                            if (decoded != null)
                            {
                                Console.WriteLine($"  Decoded Bill Amount: {decoded.Bill.Amount}");
                            }
                        }

                        Console.WriteLine(); // Blank line for readability between resolutions.
                    }
                }
            }
        }
    }
}