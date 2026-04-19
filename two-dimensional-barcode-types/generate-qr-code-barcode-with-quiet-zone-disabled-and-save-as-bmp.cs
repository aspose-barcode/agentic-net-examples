using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = "https://example.com";

            // Quiet zone disabling is not supported directly; the default quiet zone will be used.

            generator.Save("qr.bmp");
        }
    }
}