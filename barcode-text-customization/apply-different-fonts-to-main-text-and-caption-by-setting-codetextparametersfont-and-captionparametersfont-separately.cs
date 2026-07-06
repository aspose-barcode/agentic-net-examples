// Title: Apply distinct fonts to barcode text and caption
// Description: Demonstrates how to set separate fonts for the main barcode text and an above‑barcode caption using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, CodeTextParameters, and CaptionParameters to customize human‑readable text. Developers often need to style barcode text and captions differently for branding or readability, and this snippet shows the typical API calls for such customizations.
// Prompt: Apply different fonts to main text and caption by setting CodetextParameters.Font and CaptionParameters.Font separately.
// Tags: barcode, code128, font, caption, generation, aspose.barcode, codetextparameters, captionparameters, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates applying different fonts to the barcode's main text and its caption.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with custom fonts for the code text and caption, then saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the value to be encoded in the barcode
            generator.CodeText = "123ABC";

            // Configure the font for the main (human‑readable) barcode text
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Enable and configure a caption displayed above the barcode
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Sample Caption";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Times New Roman";
            generator.Parameters.CaptionAbove.Font.Size.Point = 10f;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Save the generated barcode image to a PNG file
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("Barcode generated successfully.");
    }
}