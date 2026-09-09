// Title: Generate a Planet postal barcode with transparent background and save as PNG
// Description: Demonstrates creating a Planet postal barcode, setting a transparent background, and saving it as a PNG image with an alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode appearance using the BarcodeGenerator class and its Parameters. Typical use cases include creating printable or digital barcodes with custom colors and transparency for web or document integration. Developers often need to adjust background colors, bar colors, and image formats, which this snippet illustrates.
// Prompt: Generate a postal barcode with transparent background and save as PNG with alpha channel.
// Tags: postal, planet, generate, png, barcodegenerator, parameters, color

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Planet postal barcode with a transparent background and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates the barcode, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Set the full path for the resulting PNG file.
        string outputPath = Path.Combine(outputDir, "PostalPlanetTransparent.png");

        // Initialize the barcode generator for the Planet symbology with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Set the background to transparent and the bar color to black.
            generator.Parameters.BackColor = Color.Transparent;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Define the X-dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the barcode image as a PNG file, preserving the alpha channel.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}