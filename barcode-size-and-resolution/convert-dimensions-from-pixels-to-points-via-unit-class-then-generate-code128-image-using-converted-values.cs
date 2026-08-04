// Title: Convert Pixels to Points and Generate Code128 Barcode Image
// Description: Demonstrates converting barcode dimensions from pixels to points using the Unit class, then creates a Code128 barcode image.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to set size parameters in pixels and retrieve their point equivalents via the Unit class. It highlights key classes such as BarcodeGenerator, EncodeTypes, and the Parameters property, which developers commonly use to customize barcode appearance for printing and screen display.
// Prompt: Convert dimensions from Pixels to Points via Unit class, then generate Code128 image using converted values.
// Tags: code128, dimension conversion, png, barcodegenerator, unit

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates converting dimensions from pixels to points and generating a Code128 barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Sets barcode parameters in pixels, obtains point values, and saves the barcode as PNG.
    /// </summary>
    static void Main()
    {
        // Define barcode image dimensions and bar metrics in pixels
        float imageWidthPixels = 300f;
        float imageHeightPixels = 150f;
        float xDimensionPixels = 2f;
        float barHeightPixels = 40f;

        // Initialize a Code128 barcode generator with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Assign pixel values; the Unit class will automatically convert them to points
            generator.Parameters.ImageWidth.Pixels = imageWidthPixels;
            generator.Parameters.ImageHeight.Pixels = imageHeightPixels;
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;
            generator.Parameters.Barcode.BarHeight.Pixels = barHeightPixels;

            // Retrieve the converted values in points for demonstration purposes
            float imageWidthPoints = generator.Parameters.ImageWidth.Point;
            float imageHeightPoints = generator.Parameters.ImageHeight.Point;
            float xDimensionPoints = generator.Parameters.Barcode.XDimension.Point;
            float barHeightPoints = generator.Parameters.Barcode.BarHeight.Point;

            // Output the point values to the console
            Console.WriteLine($"Image size: {imageWidthPoints}pt x {imageHeightPoints}pt");
            Console.WriteLine($"XDimension: {xDimensionPoints}pt, BarHeight: {barHeightPoints}pt");

            // Save the generated barcode as a PNG image file
            generator.Save("code128.png");
        }
    }
}