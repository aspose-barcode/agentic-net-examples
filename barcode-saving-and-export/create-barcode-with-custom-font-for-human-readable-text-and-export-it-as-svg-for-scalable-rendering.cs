// Title: Generate Code39 Barcode with Custom Font and Export as SVG
// Description: Demonstrates creating a Code39 barcode, applying a custom font to the human‑readable text, and saving the result as an SVG file for scalable rendering.
// Prompt: Create a barcode with custom font for human‑readable text and export it as SVG for scalable rendering.
// Tags: code39, barcode, custom font, svg, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code39 barcode with a custom font for the human‑readable text
/// and saves it as an SVG file for scalable rendering.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated SVG barcode.
        string outputPath = "custom_font_barcode.svg";

        // Create a BarcodeGenerator for Code39 symbology with the desired code text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "Sample123"))
        {
            // Set the font family and size for the human‑readable text.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Courier New";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Center the human‑readable text beneath the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the barcode as an SVG file to preserve scalability.
            generator.Save(outputPath, BarCodeImageFormat.Svg);
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}