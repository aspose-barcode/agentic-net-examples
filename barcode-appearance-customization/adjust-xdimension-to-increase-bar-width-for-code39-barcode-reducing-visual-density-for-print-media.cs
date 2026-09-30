// Title: Adjust XDimension for Code39 Barcode to Reduce Visual Density
// Description: Demonstrates how to increase the XDimension of a Code39 barcode, making each bar wider for better readability in print media.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class. Typical use cases include customizing bar width, height, and other visual parameters for various symbologies before saving to image formats. Developers often need to adjust XDimension to meet printing requirements or design guidelines.
// Prompt: Adjust XDimension to increase bar width for a Code39 barcode, reducing visual density for print media.
// Tags: code39, xdimension, barcode, aspose.barcode, image, png, generation, print, symbology, visual‑adjustment

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code39 barcode with an increased XDimension
/// to reduce visual density, suitable for print media.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output folder, generates the barcode,
    /// and saves it as a PNG image.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where the barcode image will be saved
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "code39.png");

        // Create a Code39 barcode generator with the sample text "HELLO123"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "HELLO123"))
        {
            // Increase XDimension to make each bar wider (value is in points)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode image to the specified path in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}