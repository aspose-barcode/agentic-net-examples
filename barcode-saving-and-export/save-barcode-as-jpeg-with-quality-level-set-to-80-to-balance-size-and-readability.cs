// Title: Save Code128 barcode as JPEG with quality 80
// Description: Demonstrates generating a Code128 barcode and saving it as a JPEG image with a quality setting of 80 to balance file size and readability.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to use BarcodeGenerator, Bitmap, and image encoding classes to produce barcode images. Typical use cases include creating printable barcodes for inventory, shipping labels, or product packaging, where developers need control over output format and compression quality. The snippet shows how to locate the JPEG codec and apply EncoderParameters for quality settings.
// Prompt: Save a barcode as a JPEG with quality level set to 80 to balance size and readability.
// Tags: code128, barcode generation, jpeg, image quality, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode and saves it as a JPEG image with a quality level of 80.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures JPEG encoding, and writes the file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the JPEG image.
        string outputPath = "barcode.jpg";

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Generate the barcode as a Bitmap object.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the JPEG codec among the installed image encoders.
                ImageCodecInfo jpegCodec = null;
                foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
                {
                    if (codec.FormatID == ImageFormat.Jpeg.Guid)
                    {
                        jpegCodec = codec;
                        break;
                    }
                }

                // If the JPEG codec is not found, report the issue and exit.
                if (jpegCodec == null)
                {
                    Console.WriteLine("JPEG codec not found.");
                    return;
                }

                // Configure encoder parameters to set JPEG quality to 80 (range 0-100).
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 80L);

                // Save the bitmap as a JPEG file using the selected codec and quality settings.
                bitmap.Save(outputPath, jpegCodec, encoderParams);
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
        }
    }
}