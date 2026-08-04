// Title: Calculate XDimension in Pixels for 2 mm Module Width at 300 dpi
// Description: Demonstrates how to compute the XDimension (module width) in pixels for a given millimeter size and DPI, then apply it to a barcode generator.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure barcode dimensions using the BarcodeGenerator and its Parameters.Barcode.XDimension properties. Typical use cases include customizing barcode size for printing or display at specific resolutions. Developers often need to convert physical measurements (mm) to pixel units to ensure consistent rendering across devices.
// Prompt: Calculate XDimension in Pixels for 2 mm module width at 300 dpi, then set it on generator.
// Tags: barcode, xdimension, module width, dpi, pixel conversion, aspose.barcode, code128, image generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that calculates the XDimension in pixels for a 2 mm module width at 300 dpi
/// and applies the value to a barcode generator.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the conversion and saves a barcode image.
    /// </summary>
    static void Main()
    {
        // Desired module width in millimeters.
        const float moduleWidthMm = 2f;

        // Target resolution in dots per inch.
        const float dpi = 300f;

        // Convert millimeters to inches (1 inch = 25.4 mm).
        float moduleWidthInches = moduleWidthMm / 25.4f;

        // Calculate the module width in pixels: inches multiplied by DPI.
        float xDimensionPixels = moduleWidthInches * dpi;

        // Create a barcode generator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode.
            generator.CodeText = "1234567890";

            // Apply the calculated XDimension (pixel width of a single module).
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;

            // Save the generated barcode as a PNG image.
            generator.Save("barcode.png");
        }
    }
}