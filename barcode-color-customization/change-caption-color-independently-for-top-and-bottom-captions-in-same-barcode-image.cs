// Title: Independent Caption Colors in a Barcode
// Description: Demonstrates how to set different colors for top and bottom captions when generating a barcode image using Aspose.BarCode.
// Prompt: Change the caption color independently for top and bottom captions in the same barcode image.
// Tags: barcode, caption, color, code128, aspose.barcode, image

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with separate top and bottom captions,
/// each having its own color and font settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample code text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // ----- Configure the top caption (appears above the barcode) -----
            generator.Parameters.CaptionAbove.Visible = true;               // Make the top caption visible
            generator.Parameters.CaptionAbove.Text = "Top Caption";        // Set caption text
            generator.Parameters.CaptionAbove.TextColor = Color.Red;       // Assign independent color (red)

            // ----- Configure the bottom caption (appears below the barcode) -----
            generator.Parameters.CaptionBelow.Visible = true;               // Make the bottom caption visible
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";     // Set caption text
            generator.Parameters.CaptionBelow.TextColor = Color.Blue;      // Assign independent color (blue)

            // ----- Optional: set distinct fonts for each caption -----
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";          // Font family for top caption
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;             // Font size for top caption
            generator.Parameters.CaptionBelow.Font.FamilyName = "Times New Roman"; // Font family for bottom caption
            generator.Parameters.CaptionBelow.Font.Size.Point = 10f;             // Font size for bottom caption

            // Save the generated barcode image (including captions) to a PNG file
            string outputPath = "barcode_with_captions.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}