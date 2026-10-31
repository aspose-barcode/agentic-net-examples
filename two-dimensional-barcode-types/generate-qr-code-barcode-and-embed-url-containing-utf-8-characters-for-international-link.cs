using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        string url = "https://例子.测试/路径?查询=值";
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_utf8.png");

        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.SetCodeText(url, Encoding.UTF8);
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Parameters.Barcode.XDimension.Pixels = 8;
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = url;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}