// Title: Generate a Code128 barcode with custom foreground and background colors
// Description: This example creates a Code128 barcode, applies a blue foreground and light‑gray background to enhance contrast, and saves it as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode image generation with color customization. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce a barcode image with specified BarColor and BackColor. Typical use cases include improving print readability or matching branding colors. Developers often need to adjust barcode colors for better visual contrast in printed or displayed media.
// Prompt: Apply blue foreground color and light‑gray background color to improve printed barcode contrast.
// Tags: code128, barcode, color customization, image generation, png, aspose.barcode, foreground color, background color

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with custom colors and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, sets its colors, saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path where the barcode image will be saved.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "colored_barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode's foreground (bar) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the image background color to light gray.
            generator.Parameters.BackColor = Color.LightGray;

            // Save the generated barcode as a PNG file to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}