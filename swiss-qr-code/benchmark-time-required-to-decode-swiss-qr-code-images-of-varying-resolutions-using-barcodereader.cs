using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Define resolutions (DPI) to test
        float[] resolutions = { 72f, 150f, 300f, 600f };

        // Prepare Swiss QR bill data (same for all images)
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        Console.WriteLine("Benchmark: Decoding Swiss QR Code images at various resolutions");
        Console.WriteLine();

        foreach (float dpi in resolutions)
        {
            // Generate barcode image at specified resolution
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Parameters.Resolution = dpi;

                using (var imageStream = new MemoryStream())
                {
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    imageStream.Position = 0; // Reset for reading

                    // Measure decoding time
                    var stopwatch = Stopwatch.StartNew();

                    using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                    {
                        // Read all barcodes (there should be one Swiss QR)
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Access result to ensure decoding occurs
                            var codeText = result.CodeText;
                        }
                    }

                    stopwatch.Stop();
                    Console.WriteLine($"Resolution: {dpi} DPI - Decode time: {stopwatch.ElapsedMilliseconds} ms");
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine("Benchmark completed.");
    }
}