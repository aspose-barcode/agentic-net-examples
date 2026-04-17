using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Generate QR code and save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = "https://example.com";
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Save("qr_code.png");
        }

        // Create a 100x100 thumbnail from the generated QR code image
        using (var qrImage = Image.FromFile("qr_code.png"))
        {
            const int thumbWidth = 100;
            const int thumbHeight = 100;

            using (var thumbnail = qrImage.GetThumbnailImage(thumbWidth, thumbHeight, null, IntPtr.Zero))
            {
                thumbnail.Save("cloud_thumbnail.png", ImageFormat.Png);
            }
        }
    }
}