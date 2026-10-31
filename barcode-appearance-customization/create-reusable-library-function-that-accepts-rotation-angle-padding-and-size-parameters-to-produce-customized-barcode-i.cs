// Title: Generate Customizable Barcode Image with Rotation, Padding, and Size
// Description: Demonstrates how to create a barcode image with configurable rotation angle, padding, and dimensions using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode image generation category. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce customized barcode graphics. Developers often need to adjust rotation, padding, and size to fit barcodes into UI layouts, reports, or printed labels, making this pattern a common building block for barcode‑related applications.
// Prompt: Create a reusable library function that accepts rotation angle, padding, and size parameters to produce customized barcode images.
// Tags: barcode, code128, rotation, padding, size, image generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an example of generating a barcode image with custom rotation, padding, and size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a sample barcode and writes the output path.
    /// </summary>
    static void Main()
    {
        // Define sample parameters for the barcode generation
        string outputPath = Path.Combine(Path.GetTempPath(), "custom_barcode.png");
        string codeText = "Sample123";
        float rotationAngle = 45f;
        float paddingLeft = 10f;
        float paddingTop = 10f;
        float paddingRight = 10f;
        float paddingBottom = 10f;
        float imageWidth = 300f;
        float imageHeight = 150f;

        try
        {
            // Generate the barcode with the specified customizations
            CreateCustomBarcode(
                outputPath,
                codeText,
                rotationAngle,
                paddingLeft,
                paddingTop,
                paddingRight,
                paddingBottom,
                imageWidth,
                imageHeight);

            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during barcode generation
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a barcode image using the provided parameters and saves it to the given path.
    /// </summary>
    /// <param name="outputPath">File system path where the barcode image will be saved.</param>
    /// <param name="codeText">Text to encode in the barcode.</param>
    /// <param name="rotationAngle">Rotation angle (in degrees) applied to the barcode.</param>
    /// <param name="paddingLeft">Left padding (in points) around the barcode.</param>
    /// <param name="paddingTop">Top padding (in points) around the barcode.</param>
    /// <param name="paddingRight">Right padding (in points) around the barcode.</param>
    /// <param name="paddingBottom">Bottom padding (in points) around the barcode.</param>
    /// <param name="imageWidth">Desired image width (in points).</param>
    /// <param name="imageHeight">Desired image height (in points).</param>
    static void CreateCustomBarcode(
        string outputPath,
        string codeText,
        float rotationAngle,
        float paddingLeft,
        float paddingTop,
        float paddingRight,
        float paddingBottom,
        float imageWidth,
        float imageHeight)
    {
        // Initialize the barcode generator with Code128 symbology and the supplied text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply rotation to the barcode
            generator.Parameters.RotationAngle = rotationAngle;

            // Set individual padding values
            generator.Parameters.Barcode.Padding.Left.Point = paddingLeft;
            generator.Parameters.Barcode.Padding.Top.Point = paddingTop;
            generator.Parameters.Barcode.Padding.Right.Point = paddingRight;
            generator.Parameters.Barcode.Padding.Bottom.Point = paddingBottom;

            // Configure image size and auto‑size mode
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Point = imageWidth;
            generator.Parameters.ImageHeight.Point = imageHeight;

            // Optional: reduce X dimension to help fit within the canvas
            generator.Parameters.Barcode.XDimension.Point = 1f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}