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
        // Create a barcode generator for Code128 and set the code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123456";

            // Generate the barcode image as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Find the JPEG codec.
                ImageCodecInfo jpegCodec = ImageCodecInfo.GetImageEncoders()
                    .FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
                if (jpegCodec == null)
                    throw new InvalidOperationException("JPEG codec not found.");

                // Set JPEG quality to 80.
                using (EncoderParameters encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 80L);
                    // Save the bitmap as a JPEG file with the specified quality.
                    bitmap.Save("barcode.jpg", jpegCodec, encoderParams);
                }
            }
        }
    }
}