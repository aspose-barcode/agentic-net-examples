// Title: Generate Code128 barcode with custom colors and caption
// Description: Creates a Code128 barcode with blue bars, a white background, and a red caption above, then saves it as a PNG file.
// Category-Description: This example demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class. It covers setting barcode symbology (EncodeTypes), customizing visual appearance (bar color, background, caption), and exporting to common image formats (BarCodeImageFormat). Developers creating product labels, inventory tags, or shipping documents often need to programmatically generate barcodes with brand‑specific colors and descriptive text.
// Prompt: Generate a Code128 barcode with blue bars, white background, and red caption, saving to PNG.
// Tags: code128, barcode, generation, png, color, caption, aspose.barcode, aspnet

using System;
using System.IO;
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
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "code128.png");

        // Initialize the BarcodeGenerator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the barcode bars to blue.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Set the image background to white.
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Enable the caption above the barcode.
            generator.Parameters.CaptionAbove.Visible = true;

            // Set the caption text.
            generator.Parameters.CaptionAbove.Text = "Sample Caption";

            // Set the caption text color to red.
            generator.Parameters.CaptionAbove.TextColor = Aspose.Drawing.Color.Red;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}