using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a QR Code generator with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Rotate the barcode image 180 degrees (upside‑down)
                generator.Parameters.RotationAngle = 180f;

                // Optionally set error correction level (high)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the rotated QR Code image
                generator.Save("qr_rotated.png");
            }

            Console.WriteLine("QR Code generated and saved as 'qr_rotated.png'.");
        }
    }
}