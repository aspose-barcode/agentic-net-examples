using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace QRCodeBinaryExample
{
    class Program
    {
        static void Main()
        {
            // Sample byte array to encode
            byte[] data = new byte[] { 0x01, 0x02, 0xFF, 0x00, 0x10, 0x20 };

            // Create a QR Code generator
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set QR encoding mode to Binary
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

                // Assign the byte array as the code text
                generator.SetCodeText(data);

                // Save the barcode as PNG
                generator.Save("qr_binary.png");
            }
        }
    }
}