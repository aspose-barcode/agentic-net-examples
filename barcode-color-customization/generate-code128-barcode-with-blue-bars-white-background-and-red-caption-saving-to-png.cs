// Title: Generate Code128 barcode with custom colors and caption
// Description: Creates a Code128 barcode with blue bars, white background, and a red caption, then saves it as a PNG file.
// Category-Description: This example demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class. It shows how to set symbology (EncodeTypes), customize visual appearance (bar color, background, caption), and export to common image formats (BarCodeImageFormat). Typical use cases include creating product labels, inventory tags, and shipping documents where developers need fine‑grained control over barcode styling.
// Prompt: Generate a Code128 barcode with blue bars, white background, and red caption, saving to PNG.
// Tags: code128, barcode, generation, png, color, caption, aspose.barcode, aspnet

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a Code128 barcode with custom colors and a caption,
/// then save it as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // The data to encode in the barcode.
        string codeText = "1234567890";

        // Destination file for the generated PNG image.
        string outputPath = "code128.png";

        // Initialize the barcode generator with Code128 symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the color of the barcode bars to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the image background to white.
            generator.Parameters.BackColor = Color.White;

            // Configure a caption that appears above the barcode.
            generator.Parameters.CaptionAbove.Text = "Sample Caption";
            generator.Parameters.CaptionAbove.TextColor = Color.Red;
            // Set the caption font size to 12 points.
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}