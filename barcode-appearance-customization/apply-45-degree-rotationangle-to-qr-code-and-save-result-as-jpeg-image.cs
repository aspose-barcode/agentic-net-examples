using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a QR code generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Rotate the barcode image by 45 degrees
            generator.Parameters.RotationAngle = 45f;

            // Save the rotated QR code as a JPEG image
            generator.Save("qr_rotated_45.jpg");
        }

        Console.WriteLine("QR code generated and saved as 'qr_rotated_45.jpg'.");
    }
}