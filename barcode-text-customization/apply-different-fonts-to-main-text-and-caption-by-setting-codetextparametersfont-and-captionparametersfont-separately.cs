// Title: Applying distinct fonts to barcode text and caption
// Description: Demonstrates how to set separate fonts for the main barcode text and an above‑barcode caption using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, CodeTextParameters, and CaptionParameters to customize text appearance. Developers often need to adjust fonts for readability or branding when creating barcodes for labels, packaging, or documents.
/// Prompt: Apply different fonts to main text and caption by setting CodetextParameters.Font and CaptionParameters.Font separately.
/// Tags: code128, font, caption, png, barcodegenerator, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates setting different fonts for barcode code text and caption.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with custom fonts for the code text and caption, then saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample code text "12345"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Configure the font for the human‑readable code text (main text)
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Set up a caption above the barcode and assign a different font
            generator.Parameters.CaptionAbove.Text = "Sample Caption";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Courier";
            generator.Parameters.CaptionAbove.Font.Size.Point = 10f;
            generator.Parameters.CaptionAbove.Visible = true;

            // Ensure the main code text appears below the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Define the output file path and save the barcode as a PNG image
            string outputPath = "barcode.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}