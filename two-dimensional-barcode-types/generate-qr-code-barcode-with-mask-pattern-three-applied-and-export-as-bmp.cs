using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        const string outputFile = "qr_mask3.bmp";

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Mask pattern setting is not available in this version of Aspose.BarCode.
            // The QR code will be generated with default settings.
            generator.Save(outputFile);
        }
    }
}