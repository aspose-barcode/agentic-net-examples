// Title: Save GS1 Composite barcode as JPEG with quality 90
// Description: Demonstrates generating a GS1 Composite barcode and saving it as a JPEG image with a quality setting of 90, suitable for web display.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create GS1 Composite barcodes using the BarcodeGenerator class, configure linear and 2D components, and export the result with Aspose.Drawing imaging APIs. Typical use cases include e‑commerce product labeling, inventory management, and any scenario where high‑quality barcode images are required for web pages or digital documents.
// Prompt: Save GS1 Composite barcode image as JPEG with quality level 90 for web display.
// Tags: gs1 composite, barcode generation, jpeg, quality, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a GS1 Composite barcode and saves it as a JPEG image with a quality level of 90.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its components, and writes the JPEG file.
    /// </summary>
    static void Main()
    {
        // Define the linear and 2D components of the GS1 Composite barcode.
        // The components are separated by the '|' character as required by the GS1 Composite format.
        string linearComponent = "(01)00123456789012";
        string twoDComponent = "(01)00123456789012";
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Determine the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1_composite.jpg");

        // Initialize the barcode generator for GS1 Composite symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Configure the linear component to use GS1 Code128 and the 2D component to use CC-A.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional: adjust visual size parameters for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Generate the barcode image as an Aspose.Drawing.Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Retrieve the JPEG encoder to control image quality.
                ImageCodecInfo jpegCodec = ImageCodecInfo.GetImageEncoders()
                    .First(codec => codec.FormatID == ImageFormat.Jpeg.Guid);

                // Set the encoder parameters: quality = 90 (range 0–100).
                using (EncoderParameters encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 90L);

                    // Save the bitmap to the specified file using the JPEG encoder and quality settings.
                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        bitmap.Save(fileStream, jpegCodec, encoderParams);
                    }
                }
            }
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"GS1 Composite barcode saved to: {outputPath}");
    }
}