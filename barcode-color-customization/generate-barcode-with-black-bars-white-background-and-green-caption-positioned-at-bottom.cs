// Title: Generate Code128 barcode with custom colors and bottom caption
// Description: Creates a Code128 barcode image with black bars, white background, and a green caption displayed below the barcode.
// Category-Description: This example demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class. It shows how to set bar and background colors, configure a caption below the barcode, and save the result as a PNG image. Typical use cases include creating product labels, inventory tags, and shipping documents where visual customization of barcodes is required. Developers often need to adjust colors, add human‑readable text, and export to common image formats.
// Prompt: Generate a barcode with black bars, white background, and green caption positioned at the bottom.
// Tags: code128, barcode, generation, png, color, caption, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a Code128 barcode with custom colors and a bottom caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode image and saves it to the Output folder.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode bar color to black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the image background color to white
            generator.Parameters.BackColor = Color.White;

            // Enable and configure the caption displayed below the barcode
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Green Caption";
            generator.Parameters.CaptionBelow.TextColor = Color.Green;
            generator.Parameters.CaptionBelow.Font.Size.Point = 12f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}