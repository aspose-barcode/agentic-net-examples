// Title: Create barcode with custom colors for bars, background, text, and captions
// Description: Demonstrates generating a Code128 barcode with custom colors for the background, bars, human‑readable text, and both top and bottom captions, then saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize visual aspects of a barcode using the BarcodeGenerator and its Parameters API. It shows typical use cases such as branding, UI design, and report generation where specific colors for bars, background, text, and captions are required. Developers often need to adjust these properties to match corporate style guides or improve readability in printed materials.
// Prompt: Create a barcode with custom bar, background, text, and caption colors in a single generation step.
// Tags: barcode, code128, color, custom-colors, background, barcolor, textcolor, caption, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with customized colors for background, bars, text, and captions, and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies color customizations, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "custom_barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the overall background color of the barcode image
            generator.Parameters.BackColor = Color.Yellow;

            // Set the color of the barcode bars (foreground)
            generator.Parameters.Barcode.BarColor = Color.DarkBlue;

            // Set the color of the human‑readable code text displayed below the bars
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Green;

            // Enable and configure a caption displayed above the barcode
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.TextColor = Color.Red;

            // Enable and configure a caption displayed below the barcode
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";
            generator.Parameters.CaptionBelow.TextColor = Color.Purple;

            // Save the generated barcode image to the specified path in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}