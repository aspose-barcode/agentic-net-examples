// Title: Convert Pixels to Points and Generate Code128 Barcode Image
// Description: Demonstrates converting image dimensions from pixels to points using Aspose.BarCode's Unit class and generating a Code128 barcode with those dimensions.
// Category-Description: This example belongs to the Aspose.BarCode image sizing and barcode generation category. It showcases the use of the BarcodeGenerator class, AutoSizeMode, and Unit properties (ImageWidth, ImageHeight) to control output size. Developers often need to convert between measurement units (pixels, points, inches) when creating barcodes for print or screen, and this snippet illustrates the typical workflow for such scenarios.
// Prompt: Convert dimensions from Pixels to Points via Unit class, then generate Code128 image using converted values.
// Tags: barcode, code128, image sizing, unit conversion, points, pixels, aspnet, aspose.barcode, generation, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that converts pixel dimensions to points and generates a Code128 barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Desired dimensions in pixels
        float widthPixels = 300f;
        float heightPixels = 150f;

        // Convert pixels to points (1 point = 1/72 inch, 1 pixel = 1/96 inch)
        // points = pixels * (72 / 96) = pixels * 0.75
        float widthPoints = widthPixels * 0.75f;
        float heightPoints = heightPixels * 0.75f;

        // Create a Code128 barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Use interpolation mode so ImageWidth/ImageHeight control the size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Apply the converted dimensions (points) to the generator
            generator.Parameters.ImageWidth.Point = widthPoints;
            generator.Parameters.ImageHeight.Point = heightPoints;

            // Optional: set resolution (dpi) if needed
            generator.Parameters.Resolution = 96f;

            // Save the barcode image as PNG
            generator.Save("code128.png");
        }

        Console.WriteLine("Barcode generated and saved as code128.png");
    }
}