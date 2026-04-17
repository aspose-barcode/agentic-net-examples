using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;
using Aspose.Drawing;

namespace VCardQrGenerator
{
    class Program
    {
        static void Main()
        {
            // vCard data to be encoded in the QR code
            string vCard = "BEGIN:VCARD\r\n" +
                           "VERSION:3.0\r\n" +
                           "N:Doe;John;;;\r\n" +
                           "FN:John Doe\r\n" +
                           "ORG:Example Company\r\n" +
                           "TITLE:Software Engineer\r\n" +
                           "TEL;TYPE=WORK,VOICE:+1-111-555-0100\r\n" +
                           "EMAIL:john.doe@example.com\r\n" +
                           "END:VCARD";

            // Create QR code generator
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the vCard as the code text
                generator.CodeText = vCard;

                // Use high error correction level for better readability
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Optional: set image size (pixels) if needed
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 300f;

                // Save the QR code image to a PNG file
                generator.Save("vcard_qr.png");
            }
        }
    }
}