// Title: Adjust XDimension for Code39 Barcode to Reduce Visual Density
// Description: Demonstrates how to increase the XDimension of a Code39 barcode, making the bars wider for better readability in print media.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance using the BarcodeGenerator class. It covers typical use cases such as setting symbology, adjusting dimensions, and saving to image formats. Developers often need to modify XDimension to control bar width for various output media, especially when reducing visual density for printing.
// Prompt: Adjust XDimension to increase bar width for a Code39 barcode, reducing visual density for print media.
// Tags: code39, barcode, xdimension, barwidth, print, png, aspose.barcode, generation, encode-types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code39 barcode with increased XDimension to produce wider bars,
/// which reduces visual density and improves print readability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary directory, generates the barcode,
    /// saves it as a PNG file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary folder for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "Code39Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated PNG image
        string outputPath = Path.Combine(outputDir, "code39.png");

        // Initialize the barcode generator with Code39 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "HELLO"))
        {
            // Increase XDimension to make bars wider, reducing visual density
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}