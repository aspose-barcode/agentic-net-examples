using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a QR Code generator
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text to encode
            generator.CodeText = "Hello World";

            // Specify QR Code version 10
            generator.Parameters.Barcode.QR.Version = QRVersion.Version10;

            // Save the barcode as a BMP image
            generator.Save("qr.bmp");
        }
    }
}