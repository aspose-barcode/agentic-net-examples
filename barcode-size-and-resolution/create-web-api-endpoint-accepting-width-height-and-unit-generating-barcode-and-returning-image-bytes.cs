// Title: Generate barcode image with custom dimensions and unit handling
// Description: Demonstrates creating a Code128 barcode with Aspose.BarCode, configuring image size in pixels, millimeters, inches, or points, and retrieving the PNG byte array.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the BarcodeGenerator class, EncodeTypes enumeration, and image size parameters. Typical use cases include web API endpoints that accept size specifications and return barcode images as byte streams. Developers often need to adjust dimensions in different measurement units to meet layout requirements.
// Prompt: Create web API endpoint accepting width, height, and unit, generating barcode and returning image bytes.
// Tags: barcode, code128, image, png, aspose.barcode, generation, dimensions, units, api, bytes

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample console application that generates a barcode image with configurable size and unit,
/// then outputs the length of the resulting PNG byte array (simulating an API response).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses optional command‑line arguments for width, height, and unit,
    /// creates a barcode, sets the requested image dimensions, and writes the byte array size.
    /// </summary>
    /// <param name="args">Optional arguments: width, height, unit.</param>
    static void Main(string[] args)
    {
        // Default dimensions (pixels) used when no arguments are supplied
        int width = 300;
        int height = 150;
        string unit = "Pixels";

        // Parse command‑line arguments if provided (order: width height unit)
        if (args.Length >= 3)
        {
            int.TryParse(args[0], out width);
            int.TryParse(args[1], out height);
            unit = args[2];
        }

        // Validate that dimensions are positive numbers
        if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width), "Width must be positive.");
        if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height), "Height must be positive.");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply the requested size unit to the generator's image parameters
            switch (unit.Trim().ToLowerInvariant())
            {
                case "pixels":
                    generator.Parameters.ImageWidth.Pixels = width;
                    generator.Parameters.ImageHeight.Pixels = height;
                    break;
                case "millimeters":
                    generator.Parameters.ImageWidth.Millimeters = width;
                    generator.Parameters.ImageHeight.Millimeters = height;
                    break;
                case "inches":
                    generator.Parameters.ImageWidth.Inches = width;
                    generator.Parameters.ImageHeight.Inches = height;
                    break;
                case "point":
                case "points":
                    generator.Parameters.ImageWidth.Point = width;
                    generator.Parameters.ImageHeight.Point = height;
                    break;
                default:
                    // Fallback to pixels if the unit is not recognized
                    Console.WriteLine($"Unsupported unit '{unit}'. Using Pixels as default.");
                    generator.Parameters.ImageWidth.Pixels = width;
                    generator.Parameters.ImageHeight.Pixels = height;
                    break;
            }

            // Generate the barcode image into a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Simulate an API response by outputting the byte array length
                Console.WriteLine($"Generated barcode image bytes: {imageBytes.Length}");
            }
        }
    }
}