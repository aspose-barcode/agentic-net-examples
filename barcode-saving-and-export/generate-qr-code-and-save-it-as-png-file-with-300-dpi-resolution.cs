using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a QR code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = "https://example.com";

            // Set the image resolution to 300 DPI.
            generator.Parameters.Resolution = 300f;

            // Save the QR code as a PNG file.
            generator.Save("qr_300dpi.png");
        }
    }
}