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
        // Define output file path
        string outputPath = "barcode.jpg";

        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Generate the barcode image as a Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Prepare JPEG encoder with quality = 80
                ImageCodecInfo jpegCodec = null;
                foreach (var codec in ImageCodecInfo.GetImageEncoders())
                {
                    if (codec.FormatID == ImageFormat.Jpeg.Guid)
                    {
                        jpegCodec = codec;
                        break;
                    }
                }

                if (jpegCodec == null)
                {
                    throw new InvalidOperationException("JPEG codec not found.");
                }

                // Set encoder parameters (quality = 80)
                using (var encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 80L);

                    // Save the bitmap to file with the specified JPEG quality
                    using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        bitmap.Save(fileStream, jpegCodec, encoderParams);
                    }
                }
            }
        }

        Console.WriteLine($"Barcode saved to '{Path.GetFullPath(outputPath)}' with JPEG quality 80.");
    }
}