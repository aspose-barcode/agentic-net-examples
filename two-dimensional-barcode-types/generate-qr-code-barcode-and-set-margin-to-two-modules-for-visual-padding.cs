using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a QR Code generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set padding (margin) to two modules on each side.
            // Here we approximate a module size with 2 points.
            generator.Parameters.Barcode.Padding.Left.Point = 2f;
            generator.Parameters.Barcode.Padding.Top.Point = 2f;
            generator.Parameters.Barcode.Padding.Right.Point = 2f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 2f;

            // Save the generated QR Code image
            generator.Save("qr.png");
        }
    }
}