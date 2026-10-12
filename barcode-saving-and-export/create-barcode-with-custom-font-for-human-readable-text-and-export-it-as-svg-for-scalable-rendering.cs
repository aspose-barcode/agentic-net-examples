// Title: Generate Code39 Barcode with Custom Font and Save as SVG
// Description: Demonstrates creating a Code39 barcode, applying a custom Helvetica font to the human‑readable text, and exporting the result as an SVG file for scalable rendering.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance using the BarcodeGenerator class. It covers setting manual font parameters for the code text, positioning the text, and saving the barcode in SVG format—common tasks for developers needing high‑quality, scalable barcodes in web or print applications.
// Prompt: Create a barcode with custom font for human‑readable text and export it as SVG for scalable rendering.
// Tags: code39, barcode, custom-font, svg, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code39 barcode with a custom font for the human‑readable text
/// and saves it as an SVG file for scalable rendering.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "custom_font_barcode.svg");
        // The data to encode in the barcode.
        string codeText = "12345AB";

        // Initialize the barcode generator with Code39 symbology and the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            // Configure the human‑readable text to use a custom font.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual; // Enable manual font selection.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica"; // Set font family.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f; // Set font size in points.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below; // Position text below the barcode.

            try
            {
                // Save the generated barcode as an SVG file.
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Output any errors that occur during the save operation.
                Console.WriteLine($"Failed to save SVG: {ex.Message}");
            }
        }
    }
}