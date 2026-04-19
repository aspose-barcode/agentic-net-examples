using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Generate the barcode image as a Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Prepare output file stream
                using (var fileStream = new FileStream("barcode_compressed.jpg", FileMode.Create, FileAccess.Write))
                {
                    // Locate the JPEG encoder
                    ImageCodecInfo jpegEncoder = null;
                    foreach (var codec in ImageCodecInfo.GetImageEncoders())
                    {
                        if (codec.FormatID == ImageFormat.Jpeg.Guid)
                        {
                            jpegEncoder = codec;
                            break;
                        }
                    }

                    if (jpegEncoder == null)
                    {
                        throw new InvalidOperationException("JPEG encoder not found.");
                    }

                    // Set JPEG quality to 50 (range 0-100, lower = higher compression)
                    using (var encoderParams = new EncoderParameters(1))
                    {
                        encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 50L);
                        // Save the bitmap with compression settings
                        bitmap.Save(fileStream, jpegEncoder, encoderParams);
                    }
                }
            }
        }
    }
}