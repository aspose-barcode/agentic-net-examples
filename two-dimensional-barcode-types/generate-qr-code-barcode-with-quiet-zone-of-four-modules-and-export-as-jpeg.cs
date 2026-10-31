using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qrcode.jpg");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set quiet zone to four modules
            generator.Parameters.Barcode.QR.QuietZoneModules = 4;
            // Export as JPEG
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }
        Console.WriteLine($"QR Code saved to {outputPath}");
    }
}