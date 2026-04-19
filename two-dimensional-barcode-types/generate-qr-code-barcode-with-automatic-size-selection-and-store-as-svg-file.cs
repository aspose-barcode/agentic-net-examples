using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string qrText = "https://example.com";
        const string outputFile = "qr.png";

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;
            generator.Parameters.Resolution = 96;
            generator.Save(outputFile);
        }

        Console.WriteLine($"QR Code saved to {outputFile}");
    }
}