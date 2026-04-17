using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a QR code generator with the desired text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Enable anti‑aliasing for smoother on‑screen rendering.
                generator.Parameters.UseAntiAlias = true;

                // Use a high error correction level for better resilience.
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Increase resolution to obtain a sharper image.
                generator.Parameters.Resolution = 300;

                // Save the generated QR code as a PNG file.
                generator.Save("qr.png");
            }

            Console.WriteLine("QR code generated: qr.png");
        }
    }
}