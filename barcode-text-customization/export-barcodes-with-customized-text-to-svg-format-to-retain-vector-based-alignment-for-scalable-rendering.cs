// Title: Export Code128 barcode with custom text to SVG
// Description: Demonstrates generating a Code128 barcode with custom display text and saving it as an SVG file for scalable vector rendering.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, set barcode parameters such as colors, text location, and font, and export the result to a vector format. Developers working with barcode creation often need to customize appearance and output to SVG for high‑resolution, resolution‑independent graphics in web or print workflows.
// Prompt: Export barcodes with customized text to SVG format to retain vector‑based alignment for scalable rendering.
// Tags: code128, custom text, svg, barcode generation, aspose.barcode, vector output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with custom text and saves it as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and SVG file path
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSvgDemo");
        Directory.CreateDirectory(outputDir);
        string svgPath = Path.Combine(outputDir, "custom_text_barcode.svg");

        // Initialize the barcode generator with Code128 symbology and custom code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Custom Text"))
        {
            // Customize barcode appearance: set bar and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Position the human‑readable text below the barcode and set its font
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Attempt to save the barcode as an SVG file; handle potential licensing restrictions
            try
            {
                generator.Save(svgPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode saved to SVG: {svgPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save SVG: {ex.Message}");
            }
        }
    }
}