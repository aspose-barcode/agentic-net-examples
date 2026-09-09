// Title: Generate Code128 barcode with custom colors and caption
// Description: Creates a Code128 barcode with blue bars, white background, and a red caption above, then saves it as a PNG file.
// Category-Description: This example demonstrates Aspose.BarCode generation features, focusing on customizing barcode appearance using the BarcodeGenerator class. It covers setting bar and background colors, adding a caption, and exporting the result to PNG. Developers working with barcode creation often need to tailor visual styles for branding or UI integration, and this snippet shows the typical API usage for such tasks.
// Prompt: Generate a Code128 barcode with blue bars, white background, and red caption, saving to PNG.
// Tags: code128, barcode, color, caption, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a Code128 barcode with custom colors and a caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, applies visual customizations, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "code128_blue_white_redcaption.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Set the barcode bars to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background of the image to white.
            generator.Parameters.BackColor = Color.White;

            // Enable the caption above the barcode and configure its text and color.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Sample Caption";
            generator.Parameters.CaptionAbove.TextColor = Color.Red;

            // Save the generated barcode image to the specified file in PNG format.
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode image saved to: {outputFile}");
    }
}