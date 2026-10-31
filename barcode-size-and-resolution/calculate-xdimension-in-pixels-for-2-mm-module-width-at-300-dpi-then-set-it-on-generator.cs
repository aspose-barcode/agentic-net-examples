// Title: Calculate XDimension in Pixels for a 2 mm Module Width at 300 dpi
// Description: Demonstrates how to compute the XDimension (module width) in pixels from a millimeter measurement and apply it to an Aspose.BarCode generator.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode resolution and module size using the BarcodeGenerator class. Developers often need to match physical dimensions (e.g., millimeters) to pixel values for precise printing or display, especially when working with Code128 or other linear symbologies. The snippet shows typical steps: setting resolution, calculating XDimension, and saving the barcode image.
// Prompt: Calculate XDimension in Pixels for 2 mm module width at 300 dpi, then set it on generator.
// Tags: barcode, xdimension, resolution, code128, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that calculates the XDimension in pixels for a given module width
/// and DPI, then generates a Code128 barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the physical module width (in millimeters) and desired resolution (in DPI)
        float moduleWidthMm = 2f;      // 2 millimeters
        float dpi = 300f;              // 300 dots per inch

        // Convert the module width from millimeters to inches (1 inch = 25.4 mm)
        // and then to pixels using the DPI value
        float xDimensionPixels = (moduleWidthMm / 25.4f) * dpi;

        // Build the full path for the output PNG file in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Create a BarcodeGenerator for Code128 with the sample data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the image resolution to match the DPI used for the XDimension calculation
            generator.Parameters.Resolution = dpi;

            // Apply the calculated XDimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved and the XDimension used
        Console.WriteLine($"Barcode saved to: {outputPath}");
        Console.WriteLine($"XDimension set to {xDimensionPixels} pixels (2 mm at {dpi} dpi).");
    }
}