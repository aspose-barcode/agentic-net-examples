using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a QR Code generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set foreground (bar) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set background color to white
            generator.Parameters.BackColor = Color.White;

            // Save the barcode as a PNG file
            generator.Save("qr_blue.png");
        }
    }
}