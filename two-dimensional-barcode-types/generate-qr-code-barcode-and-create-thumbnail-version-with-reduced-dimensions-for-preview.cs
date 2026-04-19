using System;
using System.Runtime.InteropServices;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a QR Code generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set error correction level (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the full‑size QR code image
            generator.Save("qr_full.png");

            // Generate the barcode image as a Bitmap
            using (var fullImage = generator.GenerateBarCodeImage())
            {
                // Create a thumbnail (150x150 pixels)
                using (var thumbnail = fullImage.GetThumbnailImage(150, 150, null, IntPtr.Zero))
                {
                    // Save the thumbnail image
                    thumbnail.Save("qr_thumbnail.png", ImageFormat.Png);
                }
            }
        }
    }
}