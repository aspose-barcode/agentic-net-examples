using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Create a QR code generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Text to encode
            generator.CodeText = "Sample QR Code";

            // Set QR version to 40 (maximum size)
            generator.Parameters.Barcode.QR.Version = QRVersion.Version40;

            // Save the barcode as a PNG file
            generator.Save("qr_version40.png");
        }
    }
}