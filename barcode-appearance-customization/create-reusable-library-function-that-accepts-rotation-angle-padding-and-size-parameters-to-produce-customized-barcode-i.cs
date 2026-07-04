// Title: Generate customizable barcode image with rotation, padding, and size
// Description: Demonstrates creating a barcode image using Aspose.BarCode with user-defined rotation, padding, and dimensions, useful for generating consistent barcode graphics.
// Prompt: Create a reusable library function that accepts rotation angle, padding, and size parameters to produce customized barcode images.
// Tags: barcode, code128, rotation, padding, size, png, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with customizable parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image with custom rotation, padding, and size.
    /// </summary>
    /// <param name="rotationAngle">Rotation in degrees (e.g., 0, 90, 180, 270).</param>
    /// <param name="padding">Uniform padding in points applied to all sides.</param>
    /// <param name="width">Image width in points.</param>
    /// <param name="height">Image height in points.</param>
    /// <param name="codeText">Text to encode in the barcode.</param>
    /// <param name="outputPath">File path to save the PNG image.</param>
    static void GenerateBarcode(float rotationAngle, float padding, float width, float height, string codeText, string outputPath)
    {
        // Validate required parameters.
        if (string.IsNullOrWhiteSpace(codeText))
            throw new ArgumentException("codeText cannot be null or empty.", nameof(codeText));
        if (string.IsNullOrWhiteSpace(outputPath))
            throw new ArgumentException("outputPath cannot be null or empty.", nameof(outputPath));

        // Use Code128 as a common 1D symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply rotation.
            generator.Parameters.RotationAngle = rotationAngle;

            // Set size control via interpolation mode.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = width;
            generator.Parameters.ImageHeight.Point = height;

            // Apply uniform padding on all sides.
            generator.Parameters.Barcode.Padding.Left.Point = padding;
            generator.Parameters.Barcode.Padding.Top.Point = padding;
            generator.Parameters.Barcode.Padding.Right.Point = padding;
            generator.Parameters.Barcode.Padding.Bottom.Point = padding;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Entry point that calls <see cref="GenerateBarcode"/> with sample parameters and handles errors.
    /// </summary>
    static void Main()
    {
        // Sample parameters.
        float rotation = 90f;          // Rotate 90 degrees.
        float padding = 5f;            // 5 points padding on each side.
        float imgWidth = 300f;         // 300 points width.
        float imgHeight = 150f;        // 150 points height.
        string text = "Sample123";     // Text to encode.
        string file = "custom_barcode.png"; // Output file name.

        try
        {
            // Generate the barcode with the specified settings.
            GenerateBarcode(rotation, padding, imgWidth, imgHeight, text, file);
            Console.WriteLine($"Barcode generated and saved to '{file}'.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}