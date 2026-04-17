using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a QR Code generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Rotate the barcode image 90 degrees clockwise
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated QR Code as a PNG file
            generator.Save("qr_rotated.png");
        }
    }
}