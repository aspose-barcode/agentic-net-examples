// Title: Generate Code128 barcode with Verdana font and left-aligned text
// Description: Demonstrates creating a Code128 barcode, setting the human‑readable text to Verdana 10 pt, and aligning it to the left of the bars.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode text appearance using BarcodeGenerator, EncodeTypes, and CodeTextParameters. Developers often need to adjust font, size, and alignment for branding or readability when embedding barcodes in documents or labels.
// Prompt: Create a barcode with custom text font Verdana, size 10 pt, and align text left of the bars.
// Tags: code128, barcode generation, png output, font customization, codetextparameters, barcodgenerator

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a barcode with custom font and left-aligned text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, applies Verdana 10pt font, left-aligns the text, and saves as PNG.
    /// </summary>
    static void Main()
    {
        // Define output file path
        string outputPath = "custom_font_barcode.png";

        // Initialize barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Text to encode in the barcode
            generator.CodeText = "Sample123";

            // Set human‑readable text font to Verdana, 10 pt
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Verdana";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;

            // Align the human‑readable text to the left of the bars
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}