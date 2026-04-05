using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a QR code generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = "Hello World";

            // Apply a 45‑degree rotation to the generated image
            generator.Parameters.RotationAngle = 45f;

            // Save the rotated QR code as a JPEG image
            generator.Save("qr45.jpg");
        }
    }
}