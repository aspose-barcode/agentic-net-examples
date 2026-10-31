using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;
            float quietZone = 8 * generator.Parameters.Barcode.XDimension.Point;
            generator.Parameters.Barcode.Padding.Left.Point = quietZone;
            generator.Parameters.Barcode.Padding.Right.Point = quietZone;
            generator.Parameters.Barcode.Padding.Top.Point = quietZone;
            generator.Parameters.Barcode.Padding.Bottom.Point = quietZone;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}