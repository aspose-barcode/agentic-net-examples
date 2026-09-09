// Title: Generate Postal Barcode with Custom Font and Save as PNG
// Description: This example creates a Planet postal barcode, applies a custom Helvetica font to the human‑readable text, and saves the image as a PNG file.
// Category-Description: Demonstrates Aspose.BarCode generation of postal symbologies. Shows how to configure barcode dimensions, set manual font properties for code text, and export the result using BarCodeGenerator and BarCodeImageFormat. Useful for developers needing to produce printable postal barcodes with customized appearance.
// Prompt: Generate a postal barcode with a custom font for the human‑readable text and export as PNG.
// Tags: postal barcode, custom font, png output, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Planet postal barcode with a custom font and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, configures its appearance, and writes the PNG file to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "PostalBarcode.png");
        // The data to encode in the barcode.
        string codeText = "123456";

        // Initialize the barcode generator for the Planet postal symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
        {
            // Set barcode dimensions: X-dimension (module width) and bar height.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Configure the human‑readable text to use a custom font.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual; // Enable manual font settings.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica"; // Font family.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f; // Font size.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below; // Position text below the barcode.

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Postal barcode saved to: {outputPath}");
    }
}