// Title: Set barcode text font to Times New Roman, italic, 16 pt
// Description: Demonstrates how to change the human‑readable text font of a barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode text formatting category, showing how to customize the CodeText font properties such as family, size, and style. It uses BarcodeGenerator, EncodeTypes, and FontStyle classes, which are commonly employed when developers need to emphasize barcode data in generated images or documents.
// Prompt: Set barcode text font to Times New Roman, italic style, size 16 pt for emphasis.
// Tags: barcode, code128, font, text formatting, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates setting the barcode's human‑readable text font to Times New Roman, italic, 16 pt.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with customized text font and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "Emphasis Text"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Emphasis Text"))
        {
            // Configure the human‑readable text font:
            // - Family: Times New Roman
            // - Size: 16 points
            // - Style: Italic
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Times New Roman";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 16f;
            generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Italic;

            // Save the generated barcode image to a PNG file
            generator.Save("barcode.png");
        }
    }
}