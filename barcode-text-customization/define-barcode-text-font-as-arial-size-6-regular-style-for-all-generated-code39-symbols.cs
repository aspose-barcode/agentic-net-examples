// Title: Generate Code39 barcode with custom Arial 6pt font
// Description: Demonstrates how to set the human‑readable text font to Arial, size 6, regular style for a Code39 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating font customization for barcode text. It uses BarcodeGenerator, EncodeTypes, and CodeTextParameters classes to modify text appearance, a common requirement when integrating barcodes into printed materials or UI displays. Developers often need to adjust font family, size, and style to match branding or layout specifications.
// Prompt: Define barcode text font as Arial, size 6, regular style for all generated Code39 symbols.
// Tags: code39, font, arial, size6, regular, barcode, generation, aspose.barcode

using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code39 barcode and sets the text font to Arial, 6pt, regular style.
/// </summary>
class Program
{
    /// <summary>
    /// Main entry point. Generates the barcode, applies font settings, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for Code39 with the desired value.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
        {
            // Configure the human‑readable text font: Arial family, 6 point size, regular style.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 6f;

            // Save the generated barcode as a PNG image file.
            generator.Save("code39.png");
        }
    }
}