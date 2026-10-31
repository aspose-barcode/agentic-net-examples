// Title: Generate a Code128 barcode with custom colors and captions
// Description: Demonstrates creating a Code128 barcode image with custom bar, background, text, and caption colors in a single generation step.
// Category-Description: Shows how to use Aspose.BarCode Generation API to customize visual aspects of a barcode, including bar color, background, human‑readable text, and captions. Typical use cases involve branding, UI integration, or printing where color differentiation is required. Developers often work with BarcodeGenerator, BarcodeParameters, and related color properties to produce tailored barcode images.
// Prompt: Create a barcode with custom bar, background, text, and caption colors in a single generation step.
// Tags: code128, barcode generation, color customization, caption, png, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with customized colors for bars,
/// background, human‑readable text, and both top and bottom captions, then saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies color customizations,
    /// configures captions, saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "custom_color_barcode.png");

        // Initialize the barcode generator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the foreground (bar) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to yellow.
            generator.Parameters.BackColor = Color.Yellow;

            // Set the human‑readable text (code text) color to green.
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Green;

            // Enable and configure the top caption with red text.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.TextColor = Color.Red;

            // Enable and configure the bottom caption with purple text.
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";
            generator.Parameters.CaptionBelow.TextColor = Color.Purple;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}