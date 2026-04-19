using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample GS1 Composite barcode data: linear part and 2D part separated by '|'
        const string codetext = "(01)03212345678906|(21)A12345678";

        // Create the barcode generator for GS1 Composite Bar
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Set linear component type (GS1 Code128) and 2D component type (CC-A)
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Example sizing: X‑Dimension and bar height
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Generate the barcode image as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the JPEG encoder
                ImageCodecInfo jpegCodec = ImageCodecInfo.GetImageEncoders()
                    .FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                if (jpegCodec == null)
                {
                    throw new InvalidOperationException("JPEG encoder not found.");
                }

                // Set JPEG quality to 90
                using (EncoderParameters encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 90L);

                    // Save the image to a file with the specified quality
                    using (FileStream stream = new FileStream("gs1composite.jpg", FileMode.Create))
                    {
                        bitmap.Save(stream, jpegCodec, encoderParams);
                    }
                }
            }
        }
    }
}