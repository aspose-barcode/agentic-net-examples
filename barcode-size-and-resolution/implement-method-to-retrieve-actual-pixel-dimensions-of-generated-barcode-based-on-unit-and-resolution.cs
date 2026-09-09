// Title: Retrieve Barcode Pixel Dimensions Based on Unit and Resolution
// Description: Demonstrates how to generate a barcode with a specific X-dimension unit and resolution, then obtain its actual pixel width and height.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, its Parameters, XDimension, and Resolution properties. Typical scenarios include rendering barcodes to bitmap images, calculating layout sizes, and integrating barcodes into graphics where exact pixel dimensions are required. Developers working with barcode rendering often need to convert logical units (pixels, millimeters, points) to physical pixel sizes based on a chosen DPI.
// Prompt: Implement method to retrieve actual pixel dimensions of generated barcode based on unit and resolution.
// Tags: barcode, dimensions, resolution, unit, aspose.barcode, generation, bitmap

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an example that generates a barcode, configures its X‑dimension unit and resolution,
/// and returns the resulting image's pixel dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode and prints its pixel width and height.
    /// </summary>
    static void Main()
    {
        // Define sample barcode parameters
        string codeText = "ASPOSE";
        BaseEncodeType encodeType = EncodeTypes.DataMatrix;
        string unit = "Millimeters";
        float resolution = 300f;

        // Retrieve pixel dimensions based on the specified unit and resolution
        var (width, height) = GetBarcodePixelDimensions(codeText, encodeType, unit, resolution);

        // Output the dimensions to the console
        Console.WriteLine($"Generated barcode dimensions: {width} x {height} pixels (unit={unit}, resolution={resolution} dpi)");
    }

    /// <summary>
    /// Generates a barcode with the given text, symbology, unit, and resolution,
    /// then returns the width and height of the resulting bitmap in pixels.
    /// </summary>
    /// <param name="codeText">The data to encode in the barcode.</param>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="unit">The unit for X‑dimension (e.g., Pixels, Millimeters, Points).</param>
    /// <param name="resolution">The image resolution in DPI.</param>
    /// <returns>A tuple containing the bitmap width and height in pixels.</returns>
    static (int width, int height) GetBarcodePixelDimensions(string codeText, BaseEncodeType encodeType, string unit, float resolution)
    {
        // Initialize the barcode generator with the specified symbology and data
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Configure XDimension based on the requested unit
            switch (unit?.Trim().ToLowerInvariant())
            {
                case "pixels":
                    generator.Parameters.Barcode.XDimension.Pixels = 3f;
                    break;
                case "millimeters":
                    generator.Parameters.Barcode.XDimension.Millimeters = 1f;
                    break;
                case "points":
                    generator.Parameters.Barcode.XDimension.Point = 12f;
                    break;
                default:
                    // Fallback to a default of 2 pixels for unrecognized units
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    break;
            }

            // Apply the desired resolution (dots per inch)
            generator.Parameters.Resolution = resolution;

            // Generate the barcode image and capture its pixel dimensions
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                return (bitmap.Width, bitmap.Height);
            }
        }
    }
}