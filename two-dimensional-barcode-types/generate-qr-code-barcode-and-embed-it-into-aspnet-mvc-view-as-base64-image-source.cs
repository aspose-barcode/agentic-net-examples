using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Text to encode in the QR code
        const string qrText = "https://example.com";

        // Generate QR code image
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate the bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save bitmap to memory stream as PNG
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();
                    string base64 = Convert.ToBase64String(imageBytes);

                    // Output HTML img tag with base64 source
                    Console.WriteLine("<img src=\"data:image/png;base64,{0}\" alt=\"QR Code\" />", base64);
                }
            }
        }
    }
}