using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a QR Code generator with sample text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Set the resolution to 300 DPI for high‑resolution output.
                generator.Parameters.Resolution = 300f;

                // Optional: increase error correction level for better readability.
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the generated QR Code image.
                generator.Save("qr_300dpi.png");
            }
        }
    }
}