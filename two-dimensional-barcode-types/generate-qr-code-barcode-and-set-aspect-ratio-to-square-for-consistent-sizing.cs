using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Create a QR code generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the QR module aspect ratio to 1 (square) for consistent sizing
            generator.Parameters.Barcode.QR.AspectRatio = 1f;

            // Save the generated QR code image to a file
            generator.Save("qr_square.png");
        }
    }
}