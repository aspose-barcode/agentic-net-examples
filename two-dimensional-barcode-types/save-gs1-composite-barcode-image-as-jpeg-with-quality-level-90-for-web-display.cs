// Title: Save GS1 Composite barcode as JPEG with quality 90
// Description: Demonstrates generating a GS1 Composite barcode and saving it as a JPEG image with a quality setting of 90, suitable for web display.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure barcode parameters, generate a composite barcode, and export it using Aspose.Drawing's image codecs. Developers working with barcode creation, format conversion, and image optimization often need to adjust dimensions, component types, and compression quality for web-friendly output.
// Prompt: Save GS1 Composite barcode image as JPEG with quality level 90 for web display.
// Tags: gs1 composite barcode, jpeg export, image quality, aspose.barcode, aspose.drawing, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a GS1 Composite barcode and saving it as a JPEG image with quality 90.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures parameters, and writes the JPEG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "GS1Composite.jpg");

        // Initialize the barcode generator with GS1 Composite symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, "(01)01234567890128|HelloWorld"))
        {
            // Set the X-dimension (module width) to 2 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Hide the human‑readable text (code text) from the image.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the 2‑D component of the composite barcode.
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_B;

            // Set the linear component to GS1‑Code128.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Allow non‑GS1 encoding for the linear component (optional).
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Generate the barcode image as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the JPEG codec from the installed image encoders.
                ImageCodecInfo jpegCodec = Array.Find(ImageCodecInfo.GetImageEncoders(),
                    c => c.FormatID == ImageFormat.Jpeg.Guid);

                if (jpegCodec == null)
                {
                    Console.WriteLine("JPEG codec not found.");
                    return;
                }

                // Set the JPEG quality to 90 (out of 100) using encoder parameters.
                using (EncoderParameters encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)90);
                    // Save the bitmap as a JPEG file with the specified quality.
                    bitmap.Save(outputPath, jpegCodec, encoderParams);
                }
            }
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"GS1 Composite barcode saved as JPEG with quality 90 at: {outputPath}");
    }
}