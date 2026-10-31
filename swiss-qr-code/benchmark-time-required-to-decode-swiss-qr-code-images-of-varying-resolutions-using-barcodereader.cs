using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Prepare temporary folder
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissQRBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define resolutions (DPI) to test
        float[] resolutions = new float[] { 72f, 150f, 300f };

        // Prepare Swiss QR codetext (common for all images)
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        Console.WriteLine("Benchmarking Swiss QR Code decode times (resolution -> milliseconds):");

        foreach (float dpi in resolutions)
        {
            string imagePath = Path.Combine(tempFolder, $"SwissQR_{dpi}dpi.png");

            // Generate barcode image at specified resolution
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Parameters.Resolution = dpi;
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // Decode and measure time
            long elapsedMs = 0;
            if (File.Exists(imagePath))
            {
                using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
                {
                    var stopwatch = Stopwatch.StartNew();
                    var results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        // Decode Swiss QR specific data (optional, just to ensure full processing)
                        var decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                        // No further action needed; decoding validates the read
                    }
                    stopwatch.Stop();
                    elapsedMs = stopwatch.ElapsedMilliseconds;
                }
            }
            else
            {
                Console.WriteLine($"Image not found: {imagePath}");
            }

            Console.WriteLine($"{dpi} DPI -> {elapsedMs} ms");
        }

        // Cleanup
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore; temporary files will be removed by the OS later
        }
    }
}