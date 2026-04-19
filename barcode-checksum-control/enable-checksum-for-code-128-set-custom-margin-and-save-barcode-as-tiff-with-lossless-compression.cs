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
        const string outputTiff = "code128.tiff";

        // Create Code128 barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Enable checksum
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set custom margins (padding) – 10 points on each side
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;

            // Render barcode to PNG in memory
            using (var pngStream = new MemoryStream())
            {
                generator.Save(pngStream, BarCodeImageFormat.Png);
                pngStream.Position = 0;

                // Load PNG into Aspose.Drawing bitmap
                using (var bitmap = new Bitmap(pngStream))
                {
                    // Locate TIFF encoder
                    var tiffCodec = ImageCodecInfo.GetImageEncoders()
                        .FirstOrDefault(c => c.FormatID == ImageFormat.Tiff.Guid);
                    if (tiffCodec == null)
                    {
                        Console.WriteLine("TIFF encoder not found.");
                        return;
                    }

                    // Set LZW compression
                    using (var encoderParams = new EncoderParameters(1))
                    {
                        encoderParams.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionLZW);
                        // Save as TIFF with lossless LZW compression
                        bitmap.Save(outputTiff, tiffCodec, encoderParams);
                    }
                }
            }
        }

        Console.WriteLine($"Barcode saved to {outputTiff}");
    }
}