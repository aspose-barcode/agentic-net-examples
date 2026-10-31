using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "QRCodeV40.png");

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            generator.Parameters.Barcode.QR.QrVersion = QRVersion.Version40;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}