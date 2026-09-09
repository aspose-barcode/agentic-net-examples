// Title: Generate Code39 Barcode with Custom Font and Save as SVG
// Description: Demonstrates creating a Code39 barcode, applying a custom Helvetica font to the human‑readable text, and exporting the result as an SVG file for scalable rendering.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance using the BarcodeGenerator class. It covers setting manual font parameters for the human‑readable text and saving the barcode in SVG format, a common requirement for web and print scenarios where vector graphics are needed. Developers working with barcode creation, visual customization, and vector output will find this pattern useful.
// Prompt: Create a barcode with custom font for human‑readable text and export it as SVG for scalable rendering.
// Tags: code39, barcode, custom-font, svg, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code39 barcode with a custom font for the human‑readable text
/// and saves it as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting SVG file
        string outputPath = Path.Combine(outputDir, "barcode.svg");

        // Initialize the barcode generator with Code39 symbology and the desired value
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            // ----- Customize human‑readable text appearance -----
            // Use manual font mode to specify a custom font
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
            // Set the font family to Helvetica
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            // Set the font size to 12 points
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
            // Position the text below the barcode
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            try
            {
                // Save the barcode as an SVG file for scalable rendering
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Output any errors that occur during the save operation
                Console.WriteLine($"Error saving SVG: {ex.Message}");
            }
        }
    }
}