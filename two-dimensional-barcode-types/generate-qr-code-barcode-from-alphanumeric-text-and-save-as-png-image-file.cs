using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Define the text to encode
        string codeText = "Sample123";

        // Create a QR Code generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text to be encoded
            generator.CodeText = codeText;

            // Optional: set error correction level to Medium
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the barcode as a PNG image
            generator.Save("qr.png");
        }
    }
}