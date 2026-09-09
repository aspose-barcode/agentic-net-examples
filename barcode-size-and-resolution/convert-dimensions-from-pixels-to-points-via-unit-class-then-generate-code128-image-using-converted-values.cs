// Title: Convert Pixels to Points and Generate Code128 Barcode Image
// Description: Demonstrates converting a barcode's X-dimension from pixels to points using the Unit class, then creates a Code128 barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to work with the BarcodeGenerator, EncodeTypes, and Unit conversion APIs. Developers often need to adjust barcode dimensions for different output media (e.g., screen vs. print) and must convert between measurement units such as pixels and points. The code shows typical usage patterns for setting dimensions, retrieving converted values, and saving the result as an image.
// Prompt: Convert dimensions from Pixels to Points via Unit class, then generate Code128 image using converted values.
// Tags: barcode, code128, conversion, pixels, points, unit, generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that converts an X-dimension value from pixels to points
/// and generates a Code128 barcode image using the converted dimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the original dimension in pixels.
        float pixelValue = 3f;

        // Text to encode in the barcode.
        string codeText = "Sample123";

        // Output file path for the generated PNG image.
        string outputPath = "code128.png";

        // Variable to hold the converted dimension in points.
        float pointValue;

        // Create a BarcodeGenerator for Code128 with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the X-dimension using the pixel value.
            generator.Parameters.Barcode.XDimension.Pixels = pixelValue;

            // Retrieve the equivalent value in points via the Unit conversion.
            pointValue = generator.Parameters.Barcode.XDimension.Point;

            // Apply the point value back to the X-dimension (optional, demonstrates setting).
            generator.Parameters.Barcode.XDimension.Point = pointValue;

            // Generate the barcode image and save it as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user about the saved image and conversion result.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
        Console.WriteLine($"Pixel value {pixelValue} converted to {pointValue} points.");
    }
}