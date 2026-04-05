using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a QR code generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text to encode
            generator.CodeText = "https://example.com";

            // Make the background transparent
            generator.Parameters.BackColor = Color.Transparent;

            // Optional: set the barcode (foreground) color
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save as PNG which supports alpha channel
            generator.Save("transparent_qr.png");
        }
    }
}