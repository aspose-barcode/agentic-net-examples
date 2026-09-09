// Title: Generate Code128 barcode with custom colors and bottom caption
// Description: This example creates a Code128 barcode with black bars on a white background and adds a green caption below the barcode.
// Category-Description: Demonstrates Aspose.BarCode generation features such as setting bar and background colors, configuring caption visibility and style, and saving the image. Uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Useful for developers needing customized barcode appearance for labeling, packaging, or inventory systems.
// Prompt: Generate a barcode with black bars, white background, and green caption positioned at the bottom.
// Tags: code128, barcode generation, color customization, caption, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode image with customized colors and a bottom caption.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a Code128 barcode, applies visual customizations,
    /// and saves the result as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the background to white and the bars to black.
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Enable and configure the caption that appears below the barcode.
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Sample Caption";
            generator.Parameters.CaptionBelow.TextColor = Color.Green;

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}