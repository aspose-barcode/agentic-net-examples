using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a QR Code generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a high error correction level (Level H).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Aspose.BarCode does not provide a direct property to set the quiet zone size for QR codes.
            // The default quiet zone is 4 modules. To achieve a different quiet zone (e.g., 8 modules),
            // you would need to add padding to the saved image externally.
            Console.WriteLine("Quiet zone size configuration is not directly supported; default will be used.");

            // Save the QR Code image.
            generator.Save("qr.png");
        }
    }
}