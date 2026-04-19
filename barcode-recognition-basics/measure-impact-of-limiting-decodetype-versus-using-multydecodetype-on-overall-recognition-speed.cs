using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(tempFolder))
            Directory.CreateDirectory(tempFolder);

        // Paths for sample images
        string code128Path = Path.Combine(tempFolder, "code128.png");
        string qrPath = Path.Combine(tempFolder, "qr.png");

        // Generate Code128 barcode image
        using (BarcodeGenerator gen128 = new BarcodeGenerator(EncodeTypes.Code128, "CODE128_SAMPLE"))
        {
            gen128.Save(code128Path);
        }

        // Generate QR barcode image
        using (BarcodeGenerator genQr = new BarcodeGenerator(EncodeTypes.QR, "QR_SAMPLE"))
        {
            genQr.Save(qrPath);
        }

        // Verify files exist
        if (!File.Exists(code128Path) || !File.Exists(qrPath))
        {
            Console.WriteLine("Failed to create sample barcode images.");
            return;
        }

        string[] files = new[] { code128Path, qrPath };

        // Measure recognition with limited DecodeType (only Code128)
        Stopwatch swLimited = Stopwatch.StartNew();
        foreach (string file in files)
        {
            using (BarCodeReader reader = new BarCodeReader(file))
            {
                // Limit to Code128 only
                reader.BarCodeReadType = DecodeType.Code128;
                reader.ReadBarCodes();
            }
        }
        swLimited.Stop();

        // Measure recognition with MultiDecodeType (Code128 + QR)
        Stopwatch swMulti = Stopwatch.StartNew();
        foreach (string file in files)
        {
            using (BarCodeReader reader = new BarCodeReader(file))
            {
                // Allow both Code128 and QR
                reader.BarCodeReadType = new MultiDecodeType(DecodeType.Code128, DecodeType.QR);
                reader.ReadBarCodes();
            }
        }
        swMulti.Stop();

        // Output results
        Console.WriteLine($"Limited DecodeType (Code128 only) total time: {swLimited.ElapsedMilliseconds} ms");
        Console.WriteLine($"MultiDecodeType (Code128 + QR) total time: {swMulti.ElapsedMilliseconds} ms");
    }
}