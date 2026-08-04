// Title: Export Code39 Barcode with Custom Text to SVG
// Description: Demonstrates generating a Code39 barcode with customized human‑readable text and saving it as an SVG file for scalable vector rendering.
// Category-Description: This example belongs to the Aspose.BarCode generation and export category. It showcases how to use the BarcodeGenerator class together with EncodeTypes, BarCodeImageFormat, and related parameter objects to customize barcode appearance (text location, alignment, font, colors) and export to a vector format. Developers often need such patterns when creating printable or web‑ready barcodes that must retain crisp quality at any size.
// Prompt: Export barcodes with customized text to SVG format to retain vector‑based alignment for scalable rendering.
// Tags: barcode, code39, generation, svg, custom-text, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code39 barcode with customized text and exports it to SVG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it as an SVG file.
    /// </summary>
    static void Main()
    {
        // Prepare output folder
        string outputFolder = "Output";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Path for the SVG file
        string svgPath = Path.Combine(outputFolder, "custom_code39.svg");

        try
        {
            // Create a Code39 barcode generator with sample code text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "123ABC"))
            {
                // Customize human‑readable text (code text) appearance
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

                // Set barcode colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.DarkBlue;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Define module size for vector scalability
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the barcode as SVG (vector format)
                generator.Save(svgPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode saved to: {svgPath}");
            }
        }
        catch (Exception ex)
        {
            // Handle errors such as evaluation license restrictions
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}