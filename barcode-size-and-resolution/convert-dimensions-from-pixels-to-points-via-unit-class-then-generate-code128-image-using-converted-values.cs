// Title: Convert Pixels to Points and Generate Code128 Barcode Image
// Description: Demonstrates converting barcode X-dimension from pixels to points using the Unit class, then creates a Code128 barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to work with measurement units (pixels, points) via the Unit class. It shows typical usage of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat for creating barcode images, a common task for developers integrating barcodes into documents or web applications.
// Prompt: Convert dimensions from Pixels to Points via Unit class, then generate Code128 image using converted values.
// Tags: barcode, code128, unit conversion, pixels to points, image generation, aspnet, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates converting barcode dimensions from pixels to points and generating a Code128 barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, converts its X-dimension from pixels to points, and saves the image as PNG.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file
        string outputPath = Path.Combine(Environment.CurrentDirectory, "Code128.png");

        // Initialize the barcode generator for Code128 with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the X-dimension of the barcode in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Convert the X-dimension to points using the Unit class (conversion based on resolution)
            float xDimensionInPoints = generator.Parameters.Barcode.XDimension.Point;

            // Apply the converted point value back to the X-dimension (optional, demonstrates round‑trip conversion)
            generator.Parameters.Barcode.XDimension.Point = xDimensionInPoints;

            // Save the generated barcode image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}