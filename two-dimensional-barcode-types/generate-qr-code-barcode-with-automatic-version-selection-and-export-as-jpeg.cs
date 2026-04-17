using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create QR code generator with automatic version selection
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the data to encode
                generator.CodeText = "https://example.com";

                // Optional: set error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the barcode as a JPEG image
                generator.Save("qr_code.jpg");
            }
        }
    }
}