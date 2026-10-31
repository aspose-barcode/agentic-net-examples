// Title: Export Code39 barcode with custom text to SVG
// Description: This example generates a Code39 barcode, applies custom human‑readable text, and saves it as an SVG file, preserving vector quality for scalable rendering.
// Category-Description: Demonstrates Aspose.BarCode generation and export capabilities, focusing on customizing barcode text and saving to a vector format. Key classes include BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and CodeTextParameters. Typical scenarios involve creating printable or web‑ready barcodes with tailored appearance while retaining scalability.
// Prompt: Export barcodes with customized text to SVG format to retain vector‑based alignment for scalable rendering.
// Tags: code39, barcode, export, svg, custom-text, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an entry point that generates a Code39 barcode with custom text and saves it as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Generates the barcode, customizes its appearance, and writes the result to an SVG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and SVG file path
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSvgDemo");
        Directory.CreateDirectory(outputDir);
        string svgPath = Path.Combine(outputDir, "CustomTextBarcode.svg");

        // Initialize the barcode generator for Code39 (supported for SVG in evaluation mode)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            // Set custom displayed text (different from the encoded value if needed)
            generator.SetCodeText("12345", System.Text.Encoding.UTF8);

            // Customize human‑readable text appearance
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 5f;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Optional visual tweaks: module size and colors
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the barcode as SVG; handle potential evaluation license restrictions
            try
            {
                generator.Save(svgPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode saved to SVG: {svgPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving SVG: {ex.Message}");
            }
        }
    }
}