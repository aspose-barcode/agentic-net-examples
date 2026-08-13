// Title: Generate Code128 barcode with left-aligned main text and caption
// Description: Demonstrates how to create a Code128 barcode where both the primary code text and a custom bottom caption are displayed, each aligned to the left.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, CodeTextParameters, and CaptionBelow settings to control human‑readable text placement and alignment. Typical use cases include creating product labels, shipping tags, or inventory markers where consistent left alignment of text improves readability. Developers often need to customize text location, alignment, visibility, and colors when generating barcodes programmatically.
// Prompt: Create barcodes where both main text and bottom caption are visible, each aligned left for consistency.
// Tags: code128, barcode generation, text alignment, caption, aspose.barcode, image output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with left‑aligned code text and a visible caption below.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures text and caption alignment, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the desired code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Show the human‑readable code text below the barcode bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            // Align the code text to the left for consistent appearance.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Configure a custom caption that appears below the barcode.
            generator.Parameters.CaptionBelow.Text = "Sample Caption";
            generator.Parameters.CaptionBelow.Alignment = TextAlignment.Left;
            generator.Parameters.CaptionBelow.Visible = true;

            // Optional visual styling: set barcode and background colors.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode image to a PNG file.
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("Barcode generated: barcode.png");
    }
}