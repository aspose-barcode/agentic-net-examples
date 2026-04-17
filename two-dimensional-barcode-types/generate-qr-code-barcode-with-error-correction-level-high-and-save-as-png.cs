using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a QR Code generator with the desired symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text to be encoded
            generator.CodeText = "https://example.com";

            // Set high error correction level (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the barcode as a PNG image
            generator.Save("qr_high.png");
        }
    }
}