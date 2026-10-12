// Title: Calculate XDimension in Pixels for a 2 mm Module Width at 300 dpi
// Description: Demonstrates how to compute the XDimension (module width) in pixels for a barcode based on a given millimeter size and DPI, then applies it to a BarcodeGenerator.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance by setting resolution and XDimension. It uses the BarcodeGenerator class with EncodeTypes and BarCodeImageFormat to create a Code128 barcode image. Developers often need to control module size for printing precision, especially when matching physical dimensions to screen or printer DPI.
// Prompt: Calculate XDimension in Pixels for 2 mm module width at 300 dpi, then set it on generator.
// Tags: barcode, xdimension, resolution, code128, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that calculates the XDimension (module width) in pixels for a
/// specified millimeter size and DPI, then generates a Code128 barcode image
/// using those settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the calculation, configures the
    /// barcode generator, and saves the resulting image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Desired module width in millimeters and resolution in DPI
        const float moduleWidthMm = 2f;
        const float resolutionDpi = 300f;

        // Calculate XDimension in pixels: pixels = mm * dpi / 25.4
        float xDimensionPixels = moduleWidthMm * resolutionDpi / 25.4f;

        // Prepare output file path in the system's temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "XDimensionBarcode.png");
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            // Ensure the directory exists before saving the image
            Directory.CreateDirectory(outputDir);
        }

        // Create a barcode generator for Code128, set resolution and XDimension, then save the image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
        {
            generator.Parameters.Resolution = resolutionDpi;
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode and the calculated XDimension value
        Console.WriteLine($"Barcode saved to: {outputPath}");
        Console.WriteLine($"Calculated XDimension (pixels): {xDimensionPixels}");
    }
}