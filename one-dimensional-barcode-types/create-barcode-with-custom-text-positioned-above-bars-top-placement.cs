// Title: Generate a Code128 barcode with a custom top caption
// Description: Demonstrates how to create a Code128 barcode and place a custom text caption above the bars using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator and its Parameters to customize barcode appearance. Typical use cases include adding descriptive labels, product information, or branding above barcodes. Developers often need to adjust caption visibility, alignment, font, and color to meet design requirements.
// Prompt: Create a barcode with custom text positioned above the bars (top placement).
// Tags: code128, barcode symbology, caption, top placement, png, barcodegenerator, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a Code128 barcode with a caption positioned above the bars.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 symbology with the desired code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the caption that will appear above the barcode bars.
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
            generator.Parameters.CaptionAbove.TextColor = Color.Blue;
            generator.Parameters.CaptionAbove.Font.FamilyName = "Helvetica";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;

            // Save the generated barcode image to a PNG file.
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine("Barcode image saved as 'barcode.png'.");
    }
}