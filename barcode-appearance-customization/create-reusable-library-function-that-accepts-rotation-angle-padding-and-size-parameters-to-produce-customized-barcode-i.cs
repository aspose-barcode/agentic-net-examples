// Title: Generate Custom Barcode Image with Rotation, Padding, and Size
// Description: Demonstrates how to create a barcode image using Aspose.BarCode with configurable rotation, padding, and canvas dimensions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, rotation settings, padding adjustments, and manual image sizing. Developers often need to customize barcode appearance for branding, layout constraints, or printing requirements; this snippet illustrates the key API classes and typical parameters for such tasks.
// Prompt: Create a reusable library function that accepts rotation angle, padding, and size parameters to produce customized barcode images.
// Tags: barcode, symbology, generation, rotation, padding, size, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a barcode image with custom rotation, padding, and size using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Sets up sample parameters and invokes the barcode generation routine.
    /// </summary>
    static void Main()
    {
        // Sample parameters for the barcode
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128; // Code128 barcode symbology
        float rotationAngle = 90f; // Valid values: 0, 90, 180, 270
        float paddingLeft = 10f;
        float paddingTop = 10f;
        float paddingRight = 10f;
        float paddingBottom = 10f;
        float imageWidth = 300f;   // Width in points
        float imageHeight = 150f;  // Height in points
        string outputPath = "barcode.png";

        try
        {
            // Generate the barcode image with the specified customizations
            GenerateBarcode(
                codeText,
                encodeType,
                rotationAngle,
                paddingLeft,
                paddingTop,
                paddingRight,
                paddingBottom,
                imageWidth,
                imageHeight,
                outputPath);
            Console.WriteLine($"Barcode saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during generation
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image with custom rotation, padding, and size.
    /// </summary>
    /// <param name="codeText">The text to encode.</param>
    /// <param name="encodeType">The barcode symbology.</param>
    /// <param name="rotationAngle">Rotation angle (0, 90, 180, 270).</param>
    /// <param name="paddingLeft">Left padding (points).</param>
    /// <param name="paddingTop">Top padding (points).</param>
    /// <param name="paddingRight">Right padding (points).</param>
    /// <param name="paddingBottom">Bottom padding (points).</param>
    /// <param name="imageWidth">Image width (points).</param>
    /// <param name="imageHeight">Image height (points).</param>
    /// <param name="outputPath">File path to save the image.</param>
    static void GenerateBarcode(
        string codeText,
        BaseEncodeType encodeType,
        float rotationAngle,
        float paddingLeft,
        float paddingTop,
        float paddingRight,
        float paddingBottom,
        float imageWidth,
        float imageHeight,
        string outputPath)
    {
        // Validate required input
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text must not be null or empty.", nameof(codeText));

        // Validate rotation angle
        if (rotationAngle != 0f && rotationAngle != 90f && rotationAngle != 180f && rotationAngle != 270f)
            throw new ArgumentOutOfRangeException(nameof(rotationAngle), "Rotation angle must be 0, 90, 180, or 270 degrees.");

        // Ensure the output directory exists
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        // Initialize the barcode generator with the specified symbology and data
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Apply rotation
            generator.Parameters.RotationAngle = rotationAngle;

            // Apply padding on all sides
            generator.Parameters.Barcode.Padding.Left.Point = paddingLeft;
            generator.Parameters.Barcode.Padding.Top.Point = paddingTop;
            generator.Parameters.Barcode.Padding.Right.Point = paddingRight;
            generator.Parameters.Barcode.Padding.Bottom.Point = paddingBottom;

            // Set manual canvas size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Point = imageWidth;
            generator.Parameters.ImageHeight.Point = imageHeight;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}