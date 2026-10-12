// Title: Generate a Planet postal barcode with transparent background and save as PNG
// Description: Demonstrates creating a Planet postal barcode, applying a fully transparent background, and saving the image as a PNG that retains the alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure visual parameters such as background transparency and bar colors using the BarcodeGenerator class. Typical use cases include generating printable or digital barcodes for postal services where image compositing or overlay requires alpha channel support. Developers often need to customize colors, formats, and output settings for integration into web or desktop applications.
// Prompt: Generate a postal barcode with transparent background and save as PNG with alpha channel.
// Tags: planet, postal barcode, transparent background, png, alpha channel, aspose.barcode, aspose.drawing, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Planet postal barcode with a transparent background and saving it as a PNG image with an alpha channel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures the barcode generator, and saves the image.
    /// </summary>
    static void Main()
    {
        // Determine output folder and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "PostalPlanetTransparent.png");

        // Initialize the barcode generator for Planet symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Set background to fully transparent (alpha = 0)
            generator.Parameters.BackColor = Color.FromArgb(0, 255, 255, 255);
            // Set the barcode (foreground) color to black
            generator.Parameters.Barcode.BarColor = Color.Black;
            // Save the barcode as PNG, which supports the alpha channel
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}