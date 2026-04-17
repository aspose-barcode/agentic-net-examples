using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const string outputFile = "qr_300dpi.png";

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Text to encode in the QR code
            generator.CodeText = "Hello, Aspose!";

            // Set image resolution to 300 DPI
            generator.Parameters.Resolution = 300f;

            // Save the QR code as a PNG file
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }
    }
}