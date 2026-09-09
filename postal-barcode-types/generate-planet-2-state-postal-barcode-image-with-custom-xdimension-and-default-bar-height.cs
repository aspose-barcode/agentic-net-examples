// Title: Generate Planet 2‑state postal barcode with custom XDimension
// Description: Demonstrates creating a Planet 2‑state postal barcode image, customizing its X‑dimension, and saving it as a PNG file.
// Category-Description: This example belongs to the barcode generation category of Aspose.BarCode, showcasing how to use the BarcodeGenerator class with EncodeTypes.Planet. Typical use cases include producing postal barcodes for mailing systems, adjusting visual parameters like X‑dimension, and exporting to common image formats. Developers often need to customize size, resolution, and symbology settings when integrating barcode creation into .NET applications.
// Prompt: Generate a Planet 2‑state postal barcode image with custom XDimension and default bar height.
// Tags: barcode, planet, generation, png, xdimension, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Planet 2‑state postal barcode with a custom X‑dimension and saving it as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures X‑dimension, saves the image, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Environment.CurrentDirectory, "PlanetBarcode.png");

        // Initialize barcode generator for Planet symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Set custom X‑dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}