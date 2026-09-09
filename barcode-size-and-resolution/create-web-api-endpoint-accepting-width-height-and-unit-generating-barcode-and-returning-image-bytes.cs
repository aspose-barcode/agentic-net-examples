// Title: Generate barcode image with custom dimensions and unit using Aspose.BarCode
// Description: Demonstrates how to create a barcode, set its size in various measurement units, and obtain the PNG image as a byte array.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image size parameters. Developers often need to produce barcodes with specific dimensions for web APIs, reports, or printing, and this snippet shows how to configure width, height, and measurement units (pixels, millimeters, inches, points) before saving the image.
// Prompt: Create web API endpoint accepting width, height, and unit, generating barcode and returning image bytes.
// Tags: barcode, generation, dimensions, units, png, aspose.barcode, csharp, webapi

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with custom size and unit, returning image bytes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a sample barcode, saves it to disk, and outputs details.
    /// </summary>
    static void Main()
    {
        // Sample parameters for demonstration
        int width = 300;
        int height = 150;
        string unit = "Pixels";

        try
        {
            // Generate barcode image bytes using the specified dimensions and unit
            byte[] imageBytes = GenerateBarcode(width, height, unit);

            // Save the generated image to a file for verification
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");
            File.WriteAllBytes(outputPath, imageBytes);

            Console.WriteLine($"Barcode image saved to: {outputPath}");
            Console.WriteLine($"Image byte size: {imageBytes.Length}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation or saving
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image with the given width, height, and measurement unit.
    /// </summary>
    /// <param name="width">The desired image width.</param>
    /// <param name="height">The desired image height.</param>
    /// <param name="unit">The measurement unit (Pixels, Millimeters, Inches, Points).</param>
    /// <returns>Byte array containing the PNG image data.</returns>
    static byte[] GenerateBarcode(int width, int height, string unit)
    {
        // Validate input parameters
        if (width <= 0)
            throw new ArgumentOutOfRangeException(nameof(width), "Width must be positive.");
        if (height <= 0)
            throw new ArgumentOutOfRangeException(nameof(height), "Height must be positive.");
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentException("Unit must be provided.", nameof(unit));

        // Create barcode generator with a sample symbology and code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
        {
            // Set manual image size mode to enforce explicit dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Apply the requested measurement unit to width and height
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
                    throw new ArgumentException($"Unsupported unit: {unit}", nameof(unit));
            }

            // Generate the barcode image into a memory stream and return its bytes
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}