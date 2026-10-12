// Title: Generate Custom Barcode Image with Rotation, Padding, and Size
// Description: Demonstrates how to create a barcode image using Aspose.BarCode with configurable rotation angle, padding, and dimensions.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing the BarcodeGenerator class and its Parameters properties. Typical use cases include customizing barcode appearance for print or UI, adjusting rotation, margins, and image size. Developers often need reusable functions to produce barcodes that fit specific layout requirements.
// Prompt: Create a reusable library function that accepts rotation angle, padding, and size parameters to produce customized barcode images.
// Tags: barcode, code128, rotation, padding, size, image generation, aspose.barcode, png, csharp

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with customizable rotation, padding, and size using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that sets sample parameters, resolves symbology, and calls GenerateBarcode.
    /// </summary>
    static void Main()
    {
        // Sample barcode data and configuration
        string codeText = "1234567890";
        string symbologyName = "Code128";
        float rotationAngle = 45f;
        float paddingLeft = 10f;
        float paddingTop = 10f;
        float paddingRight = 10f;
        float paddingBottom = 10f;
        float imageWidth = 300f;
        float imageHeight = 150f;

        // Resolve symbology name to BaseEncodeType using reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Prepare a temporary output folder and file path
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "barcode.png");

        // Generate the barcode image with the specified parameters
        GenerateBarcode(codeText, encodeType, rotationAngle,
            paddingLeft, paddingTop, paddingRight, paddingBottom,
            imageWidth, imageHeight, outputPath);

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Generates a barcode image using the provided parameters and saves it to the specified path.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="encodeType">The barcode symbology type.</param>
    /// <param name="rotationAngle">Rotation angle in degrees.</param>
    /// <param name="paddingLeft">Left padding in points.</param>
    /// <param name="paddingTop">Top padding in points.</param>
    /// <param name="paddingRight">Right padding in points.</param>
    /// <param name="paddingBottom">Bottom padding in points.</param>
    /// <param name="imageWidth">Image width in points.</param>
    /// <param name="imageHeight">Image height in points.</param>
    /// <param name="outputPath">File path where the PNG image will be saved.</param>
    static void GenerateBarcode(string codeText, BaseEncodeType encodeType,
        float rotationAngle,
        float paddingLeft, float paddingTop, float paddingRight, float paddingBottom,
        float imageWidth, float imageHeight,
        string outputPath)
    {
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Apply rotation
            generator.Parameters.RotationAngle = rotationAngle;

            // Apply padding (margins) around the barcode
            generator.Parameters.Barcode.Padding.Left.Point = paddingLeft;
            generator.Parameters.Barcode.Padding.Top.Point = paddingTop;
            generator.Parameters.Barcode.Padding.Right.Point = paddingRight;
            generator.Parameters.Barcode.Padding.Bottom.Point = paddingBottom;

            // Set image dimensions
            generator.Parameters.ImageWidth.Point = imageWidth;
            generator.Parameters.ImageHeight.Point = imageHeight;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}