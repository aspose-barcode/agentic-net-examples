// Title: PDF417 Barcode with Top Caption
// Description: Demonstrates how to display a top caption on a PDF417 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on caption customization for 2D symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and CaptionParameters to add and style captions above the barcode. Developers often need to add descriptive text above or below barcodes for better readability in documents and labels.
// Prompt: Show top caption for PDF417 barcodes by setting CaptionParameters.Top.Visible to true.
// Tags: pdf417, barcode, caption, top caption, aspnet, aspnetcore, aspose.barcode, image generation, 2d barcode, c#

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a PDF417 barcode with a visible top caption and saves it as an image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a PDF417 barcode, configures a top caption, and saves the result.
    /// </summary>
    static void Main()
    {
        // Initialize a PDF417 barcode generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Enable the top caption (CaptionAbove) and assign its display text.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Customize the appearance of the top caption.
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
            generator.Parameters.CaptionAbove.TextColor = Aspose.Drawing.Color.Black;

            // Save the generated barcode image to a PNG file.
            generator.Save("pdf417_with_top_caption.png");
        }

        // Inform the user that the barcode image has been created.
        Console.WriteLine("Barcode image generated: pdf417_with_top_caption.png");
    }
}