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
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Generate the barcode image as a bitmap
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the TIFF encoder
                var tiffCodec = ImageCodecInfo.GetImageEncoders()
                    .FirstOrDefault(c => c.FormatID == ImageFormat.Tiff.Guid);
                if (tiffCodec == null)
                {
                    Console.WriteLine("TIFF encoder not found.");
                    return;
                }

                // Configure LZW compression
                using (var encoderParams = new EncoderParameters(1))
                {
                    var compressionEncoder = Encoder.Compression;
                    var compressionParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionLZW);
                    encoderParams.Param[0] = compressionParam;

                    // Save the bitmap to a TIFF file with LZW compression
                    using (var fileStream = new FileStream("barcode_lzw.tif", FileMode.Create, FileAccess.Write))
                    {
                        bitmap.Save(fileStream, tiffCodec, encoderParams);
                    }
                }
            }
        }
    }
}