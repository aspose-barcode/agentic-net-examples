// Title: Generate Postal Planet Barcode with Custom Font and PNG Output
// Description: Demonstrates creating a Planet postal barcode, applying a custom Helvetica font to the human‑readable text, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.Planet, configure barcode dimensions, customize CodeTextParameters (font, location), and export the barcode to a PNG file. Typical use cases include generating postal barcodes for mailing systems where a specific font is required for readability. Developers often need to adjust visual settings and output formats, making this a common reference for barcode creation tasks.
// Prompt: Generate a postal barcode with a custom font for the human‑readable text and export as PNG.
// Tags: postal, planet, custom font, png, generation, aspose.barcode, codetextparameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Planet postal barcode with a custom font for the human‑readable text
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, configures visual parameters,
    /// and writes the output file to the local "output" folder.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "PostalPlanetCustomFont.png");

        // Initialize the barcode generator for Planet symbology with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Set barcode size parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;          // Width of a single module
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;        // Height of the main barcode bars
            generator.Parameters.Barcode.Postal.ShortBarHeight.Pixels = 20f; // Height of the short (postal) bars

            // Position the human‑readable text below the barcode
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Apply a custom Helvetica font to the human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}