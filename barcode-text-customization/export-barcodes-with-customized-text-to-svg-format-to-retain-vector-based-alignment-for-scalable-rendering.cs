// Title: Export Code39 barcode with custom text to SVG
// Description: Demonstrates generating a Code39 barcode, customizing its human‑readable text, and saving it as an SVG file for scalable vector rendering.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcodes with tailored appearance. Typical use cases include producing printable or web‑ready vector barcodes with customized text positioning, font, and alignment. Developers often need to adjust code‑text parameters to match branding or layout requirements.
// Prompt: Export barcodes with customized text to SVG format to retain vector‑based alignment for scalable rendering.
// Tags: code39, barcode, generation, custom text, svg, vector, aspose.barcode, encoding, text formatting

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a Code39 barcode with customized human‑readable text to SVG format.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, applies text customizations, and saves the result as an SVG file.
    /// </summary>
    static void Main()
    {
        // Build the output directory path relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Full path for the resulting SVG file
        string svgPath = Path.Combine(outputDir, "custom_text.svg");

        // Initialize the barcode generator with Code39 symbology and the data to encode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            // ----- Customize human‑readable text appearance -----
            // Position the text below the barcode
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            // Set the font family to Helvetica
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            // Set the font size to 14 points
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;
            // Center the text horizontally
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            // Add spacing of 5 points between the barcode and the text
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 5f;

            try
            {
                // Save the barcode as an SVG file to retain vector quality
                generator.Save(svgPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode saved to: {svgPath}");
            }
            catch (Exception ex)
            {
                // Output any errors that occur during the save operation
                Console.WriteLine($"Failed to save SVG: {ex.Message}");
            }
        }
    }
}