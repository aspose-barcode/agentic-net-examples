// Title: Generate a Code128 barcode with custom human‑readable font and save as SVG
// Description: Demonstrates how to create a Code128 barcode, apply a custom font to the human‑readable text, and export the result as an SVG file for scalable rendering.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include customizing barcode appearance for branding or readability and producing vector graphics for web or print. Developers often need to adjust text location, font properties, and output formats when integrating barcodes into responsive designs.
// Prompt: Create a barcode with custom font for human‑readable text and export it as SVG for scalable rendering.
// Tags: code128, customfont, humanreadable, svg, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with customized human‑readable text
/// and saves it as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output SVG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.svg");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Position the human‑readable text below the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Apply a custom font (Helvetica, 12pt) to the human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Add extra spacing (5 points) between the text and the barcode
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 5f;

            // Attempt to save the barcode as an SVG file; handle potential license restrictions
            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to save SVG. Evaluation license may restrict this format.");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}