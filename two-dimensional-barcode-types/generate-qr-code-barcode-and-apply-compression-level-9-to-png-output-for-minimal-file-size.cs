using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a QR code generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate the barcode image as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Prepare output file stream
                using (var fileStream = new FileStream("qr.png", FileMode.Create, FileAccess.Write))
                {
                    // Find PNG encoder
                    ImageCodecInfo pngEncoder = ImageCodecInfo.GetImageEncoders()
                        .FirstOrDefault(enc => enc.FormatID == ImageFormat.Png.Guid);

                    if (pngEncoder != null)
                    {
                        // Set compression level to 9 (maximum compression)
                        EncoderParameters encoderParams = new EncoderParameters(1);
                        encoderParams.Param[0] = new EncoderParameter(Encoder.Compression, 9L);

                        // Save bitmap with compression settings
                        bitmap.Save(fileStream, pngEncoder, encoderParams);
                    }
                    else
                    {
                        // Fallback: save without explicit compression settings
                        bitmap.Save(fileStream, ImageFormat.Png);
                    }
                }
            }
        }
    }
}