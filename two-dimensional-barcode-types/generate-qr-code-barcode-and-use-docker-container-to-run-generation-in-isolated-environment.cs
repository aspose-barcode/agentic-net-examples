using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "qr.png");

        string codeText = "https://example.com";

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR code generated at: {outputPath}");
    }
}