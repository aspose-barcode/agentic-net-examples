// Title: Save GS1 Composite barcode as JPEG with quality 90
// Description: Demonstrates generating a GS1 Composite barcode and saving it as a JPEG image with a quality setting suitable for web display.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on creating composite barcodes (GS1 Composite) and exporting them to raster image formats. It uses BarcodeGenerator, EncodeTypes, and Aspose.Drawing classes to configure linear and 2D components, adjust dimensions, and control image encoding parameters such as JPEG quality. Developers often need to produce high‑quality barcode images for e‑commerce, shipping labels, or web pages, and this snippet shows the typical steps to achieve that.
// Prompt: Save GS1 Composite barcode image as JPEG with quality level 90 for web display.
// Tags: gs1, composite, barcode, generation, jpeg, quality, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a GS1 Composite barcode and saves it as a JPEG image with a quality level of 90.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its components, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Composite barcode text: linear part | 2D part
        string codeText = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Initialize the barcode generator for GS1 Composite Bar symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Define the linear component (GS1 Code128) and the 2D component (CC-A)
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional: fine‑tune the barcode dimensions
            generator.Parameters.Barcode.XDimension.Pixels = 3f;   // module width
            generator.Parameters.Barcode.BarHeight.Pixels = 100f; // linear part height

            // Generate the barcode image as a Bitmap object
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Retrieve the JPEG encoder from the system codecs
                ImageCodecInfo jpegEncoder = ImageCodecInfo.GetImageEncoders()
                    .FirstOrDefault(enc => enc.FormatID == ImageFormat.Jpeg.Guid);

                string outputPath = "gs1composite.jpg";

                if (jpegEncoder != null)
                {
                    // Configure JPEG quality to 90 (suitable for web)
                    using (EncoderParameters encoderParams = new EncoderParameters(1))
                    {
                        encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 90L);
                        bitmap.Save(outputPath, jpegEncoder, encoderParams);
                    }
                }
                else
                {
                    // Fallback: save using default JPEG settings if encoder not found
                    bitmap.Save(outputPath, ImageFormat.Jpeg);
                }

                // Inform the user where the file was saved
                Console.WriteLine($"GS1 Composite barcode saved to '{Path.GetFullPath(outputPath)}'");
            }
        }
    }
}