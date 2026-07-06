// Title: Left-aligned main text and bottom caption in Code128 barcode
// Description: Demonstrates how to generate a Code128 barcode with visible main text and a bottom caption, both left‑aligned.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize human‑readable text and captions using the BarcodeGenerator and its Parameters properties. Developers often need to control text alignment, visibility, and positioning when creating barcodes for labels, packaging, or documentation. The snippet shows typical usage of EncodeTypes, TextAlignment, and Caption settings.
/// Prompt: Create barcodes where both main text and bottom caption are visible, each aligned left for consistency.
/// Tags: code128, barcode, text-alignment, caption, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode with left‑aligned main text and a visible bottom caption.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, configures text alignment, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the main text "123ABC"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Align the human‑readable main text to the left
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Make the bottom caption visible and set its text
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Sample Caption";

            // Align the bottom caption text to the left
            generator.Parameters.CaptionBelow.Alignment = TextAlignment.Left;

            // Save the generated barcode as a PNG image
            generator.Save("barcode.png");
        }
    }
}