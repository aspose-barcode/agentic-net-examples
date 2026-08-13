// Title: Independent Top and Bottom Caption Colors in a Barcode Image
// Description: Demonstrates how to set different colors for the top and bottom captions of a barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and caption parameters. Developers often need to customize barcode appearance, such as adding distinct captions with separate styling, for branding or informational purposes. The snippet shows typical API calls for configuring caption visibility, text, font, and color before saving the image.
// Prompt: Change the caption color independently for top and bottom captions in the same barcode image.
// Tags: code128, caption-color, png, barcodegenerator, parameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates setting independent colors for top and bottom captions in a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with distinct top and bottom caption colors and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample code text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // ----- Configure the top caption (appears above the barcode) -----
            generator.Parameters.CaptionAbove.Visible = true;                     // Make the top caption visible
            generator.Parameters.CaptionAbove.Text = "Top Caption";              // Set caption text
            generator.Parameters.CaptionAbove.TextColor = Color.Blue;            // Set independent color for top caption
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";         // Choose font family
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;             // Set font size

            // ----- Configure the bottom caption (appears below the barcode) -----
            generator.Parameters.CaptionBelow.Visible = true;                     // Make the bottom caption visible
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";           // Set caption text
            generator.Parameters.CaptionBelow.TextColor = Color.Green;           // Set independent color for bottom caption
            generator.Parameters.CaptionBelow.Font.FamilyName = "Arial";         // Choose font family
            generator.Parameters.CaptionBelow.Font.Size.Point = 12f;             // Set font size

            // ----- Save the generated barcode image to a PNG file -----
            string outputPath = "barcode.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode image saved to '{outputPath}'.");
        }
    }
}