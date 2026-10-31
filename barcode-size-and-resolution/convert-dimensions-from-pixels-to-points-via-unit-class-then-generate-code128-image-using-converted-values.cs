// Title: Convert Pixels to Points for XDimension and Generate Code128 Barcode
// Description: Demonstrates how to set the XDimension of a barcode in pixels, convert it to points using the Unit class, and generate a Code128 barcode image. Shows practical use of dimension conversion for precise barcode sizing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to work with measurement units when creating barcodes. It uses the BarcodeGenerator, EncodeTypes, and Unit classes to adjust barcode dimensions, a common requirement for developers needing exact sizing for printing or UI rendering. The snippet serves as a reference for converting between pixels and points before saving the barcode in common image formats.
// Prompt: Convert dimensions from Pixels to Points via Unit class, then generate Code128 image using converted values.
// Tags: code128, dimension conversion, pixels, points, unit, barcode generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates converting XDimension from pixels to points and generating a Code128 barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, sets dimensions, converts units, and saves the barcode.
    /// </summary>
    static void Main()
    {
        // Prepare output directory and file path
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "code128.png");

        // Define the XDimension in pixels
        int pixelDimension = 3;

        // Initialize the barcode generator for Code128 with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Assign the pixel value to the XDimension
            generator.Parameters.Barcode.XDimension.Pixels = pixelDimension;

            // Retrieve the equivalent value in points (1 point = 0.75 pixel)
            float pointDimension = generator.Parameters.Barcode.XDimension.Point;

            // Apply the point value back to the XDimension (demonstrates conversion usage)
            generator.Parameters.Barcode.XDimension.Point = pointDimension;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output conversion details and file location to the console
        Console.WriteLine($"Pixel XDimension: {pixelDimension} px");
        Console.WriteLine($"Converted XDimension: {pixelDimension * 0.75f} pt (approx)");
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}