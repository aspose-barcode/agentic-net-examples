using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a QR Code generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // The default quiet zone for QR Code in Aspose.BarCode is 4 modules,
            // which satisfies the requirement. If needed, it can be adjusted via
            // the QR parameters (e.g., generator.Parameters.Barcode.QR.QuietZone = 4).

            // Optional: set an error correction level.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code as a JPEG image.
            generator.Save("qr_code.jpeg");
        }
    }
}