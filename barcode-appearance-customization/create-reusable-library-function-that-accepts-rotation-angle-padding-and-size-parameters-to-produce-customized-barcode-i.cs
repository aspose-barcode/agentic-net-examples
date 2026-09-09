// Title: Generate customizable barcode image with rotation, padding, and size
// Description: Demonstrates how to create a barcode image using Aspose.BarCode with specific rotation, padding, and dimensions.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and image format classes. Typical use cases include generating product labels, inventory tags, or QR codes with precise layout requirements. Developers often need to control rotation, padding, and image size to integrate barcodes into existing graphics or UI designs.
// Prompt: Create a reusable library function that accepts rotation angle, padding, and size parameters to produce customized barcode images.
// Tags: barcode, code128, rotation, padding, image size, aspose.barcode, image generation, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an example of generating a barcode image with custom rotation, padding, and dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Sets sample parameters, calls the generator, and reports the result.
    /// </summary>
    static void Main()
    {
        // Sample barcode content
        string codeText = "ASPOSE123";

        // Desired rotation angle (degrees)
        float rotationAngle = 45f;

        // Padding values (points) around the barcode
        float paddingLeft = 10f;
        float paddingTop = 10f;
        float paddingRight = 10f;
        float paddingBottom = 10f;

        // Target image dimensions (pixels)
        float imageWidth = 300f;
        float imageHeight = 150f;

        // Output file path
        string outputPath = "custom_barcode.png";

        try
        {
            // Generate and save the barcode image with the specified settings
            GenerateBarcode(
                codeText,
                rotationAngle,
                paddingLeft,
                paddingTop,
                paddingRight,
                paddingBottom,
                imageWidth,
                imageHeight,
                outputPath);

            Console.WriteLine($"Barcode saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during generation
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image using Aspose.BarCode with custom rotation, padding, and size.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="rotationAngle">Rotation angle in degrees (0‑360).</param>
    /// <param name="paddingLeft">Left padding in points.</param>
    /// <param name="paddingTop">Top padding in points.</param>
    /// <param name="paddingRight">Right padding in points.</param>
    /// <param name="paddingBottom">Bottom padding in points.</param>
    /// <param name="imageWidth">Desired image width in pixels.</param>
    /// <param name="imageHeight">Desired image height in pixels.</param>
    /// <param name="outputPath">File path where the image will be saved.</param>
    public static void GenerateBarcode(
        string codeText,
        float rotationAngle,
        float paddingLeft,
        float paddingTop,
        float paddingRight,
        float paddingBottom,
        float imageWidth,
        float imageHeight,
        string outputPath)
    {
        // Validate required parameters
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text must be provided.", nameof(codeText));

        if (rotationAngle < 0f || rotationAngle > 360f)
            throw new ArgumentOutOfRangeException(nameof(rotationAngle), "Rotation angle must be between 0 and 360 degrees.");

        if (paddingLeft < 0f || paddingTop < 0f || paddingRight < 0f || paddingBottom < 0f)
            throw new ArgumentOutOfRangeException("Padding values must be non‑negative.");

        if (imageWidth <= 0f || imageHeight <= 0f)
            throw new ArgumentOutOfRangeException("Image width and height must be greater than zero.");

        // Initialize the barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply rotation
            generator.Parameters.RotationAngle = rotationAngle;

            // Apply padding (points)
            generator.Parameters.Barcode.Padding.Left.Point = paddingLeft;
            generator.Parameters.Barcode.Padding.Top.Point = paddingTop;
            generator.Parameters.Barcode.Padding.Right.Point = paddingRight;
            generator.Parameters.Barcode.Padding.Bottom.Point = paddingBottom;

            // Set fixed image size (pixels)
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = imageWidth;
            generator.Parameters.ImageHeight.Pixels = imageHeight;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}