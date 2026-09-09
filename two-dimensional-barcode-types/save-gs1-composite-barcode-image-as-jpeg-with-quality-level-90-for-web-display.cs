// Title: Save GS1 Composite barcode as JPEG with quality 90
// Description: Generates a GS1 Composite barcode, combines linear and 2‑D components, and saves it as a JPEG image with 90% quality for web use.
// Category-Description: This example demonstrates Aspose.BarCode's barcode generation capabilities, specifically creating a GS1 Composite barcode using EncodeTypes.GS1CompositeBar. It shows how to configure linear and 2‑D component types, adjust barcode dimensions, and export the result with Aspose.Drawing imaging classes. Developers working with product identification, inventory systems, or any GS1‑based labeling can use this pattern to produce web‑ready barcode images.
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
/// Demonstrates how to generate a GS1 Composite barcode and save it as a JPEG image
/// with a specific quality setting suitable for web display.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, configures image encoding,
    /// and writes the JPEG file to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "Gs1CompositeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "gs1composite.jpg");

        // Prepare the linear (GS1-128) and 2‑D (CC‑B) components of the composite code
        string linear = "(01)12345678901231";
        string twoD = "HelloWorld";
        string codeText = $"{linear}|{twoD}";

        // Initialize the barcode generator for GS1 Composite symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set barcode visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_B;
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Generate the barcode image as a Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the JPEG encoder
                var jpegCodec = ImageCodecInfo.GetImageEncoders()
                    .First(c => c.FormatID == ImageFormat.Jpeg.Guid);

                // Configure encoder parameters to set JPEG quality to 90
                using (var encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 90L);
                    // Save the bitmap to the specified path using the JPEG codec and quality settings
                    bitmap.Save(outputPath, jpegCodec, encoderParams);
                }
            }
        }

        // Inform the user where the file was saved
        Console.WriteLine($"GS1 Composite barcode saved to: {outputPath}");
    }
}