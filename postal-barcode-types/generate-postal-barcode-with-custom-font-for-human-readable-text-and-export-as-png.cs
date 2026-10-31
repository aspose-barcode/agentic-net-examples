// Title: Generate a Planet Postal Barcode with Custom Font and Save as PNG
// Description: This example creates a Planet postal barcode, applies a custom Helvetica font to the human‑readable text, and saves the result as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for postal symbologies, focusing on customizing code‑text appearance and exporting to raster formats. Uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include creating mail‑sorting barcodes with branded text styling for printing or digital distribution.
// Prompt: Generate a postal barcode with a custom font for the human‑readable text and export as PNG.
// Tags: postal barcode, planet symbology, custom font, png output, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Planet postal barcode with a custom font for the human‑readable text
/// and saves it as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image
        string outputPath = Path.Combine(Environment.CurrentDirectory, "PostalBarcode.png");

        // Ensure the target directory exists; create it if necessary
        string directory = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize the barcode generator for the Planet postal symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Configure barcode dimensions
            generator.Parameters.Barcode.XDimension.Pixels = 4;               // Width of a single module
            generator.Parameters.Barcode.BarHeight.Pixels = 50;              // Height of the main bars
            generator.Parameters.Barcode.Postal.ShortBarHeight.Pixels = 20; // Height of the short bars used in postal codes

            // Customize the human‑readable text appearance
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica"; // Font family
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;        // Font size in points
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below; // Position below the barcode

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Postal barcode saved to: {outputPath}");
    }
}