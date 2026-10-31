using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare output path
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_extended.bmp");

        // Build extended codetext with multiple segments
        QrExtCodetextBuilder builder = new QrExtCodetextBuilder();
        builder.AddPlainCodetext("Hello ");
        builder.AddECICodetext(ECIEncodings.UTF8, "World");
        builder.AddPlainCodetext("!");

        // Generate QR Code in Extended mode
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = builder.GetExtendedCodetext();
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Extended;
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}