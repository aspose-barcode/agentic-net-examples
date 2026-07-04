// Title: Barcode Generation with Top Caption and Custom Color
// Description: Demonstrates creating a Code128 barcode, placing a caption above it, applying a custom color, and saving as PNG.
// Prompt: Set the caption position to top and apply a custom caption color before saving as PNG.
// Tags: barcode, code128, caption, top, color, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a top caption
/// and a custom caption color, then saves the image as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Enable and configure a caption positioned above the barcode (top)
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Apply a custom color to the caption text (Blue)
            generator.Parameters.CaptionAbove.TextColor = Color.Blue;

            // Optional: customize the caption font (Arial, 12pt)
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;

            // Save the generated barcode image as a PNG file
            generator.Save("barcode_with_top_caption.png");
        }
    }
}