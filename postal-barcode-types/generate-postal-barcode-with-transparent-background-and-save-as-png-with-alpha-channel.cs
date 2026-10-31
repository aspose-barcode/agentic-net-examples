// Title: Generate a Planet postal barcode with transparent background and save as PNG
// Description: This example creates a Planet postal barcode, sets a transparent background, and saves the image as a PNG file that retains the alpha channel.
// Category-Description: Demonstrates Aspose.BarCode generation of postal symbologies with image formatting options. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to configure colors, dimensions, and output format. Developers often need to produce barcode images with custom backgrounds for web or print, and this snippet shows how to achieve transparency in PNG output.
// Prompt: Generate a postal barcode with transparent background and save as PNG with alpha channel.
// Tags: planet,postal,barcode,generation,transparent,background,png,alpha,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Planet postal barcode with a transparent background
/// and saves it as a PNG image preserving the alpha channel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the full file path for the resulting PNG image.
        string outputPath = Path.Combine(outputDir, "PostalPlanetTransparent.png");

        // Initialize the barcode generator for the Planet postal symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Set the background to transparent and the barcode bars to black.
            generator.Parameters.BackColor = Color.Transparent;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Define the X-dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the barcode image as a PNG file, preserving the alpha channel.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}