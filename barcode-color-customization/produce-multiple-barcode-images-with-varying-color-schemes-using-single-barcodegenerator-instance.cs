// Title: Generate Multiple Barcode Images with Different Color Schemes
// Description: Demonstrates how to produce several barcode PNG files, each using a distinct foreground and background color, while reusing a single BarcodeGenerator instance.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance using the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include creating brand‑compliant barcodes, generating visual variants for testing, or producing assets for different UI themes. Developers often need to adjust bar and background colors without recreating the generator for each image.
// Prompt: Produce multiple barcode images with varying color schemes using a single BarcodeGenerator instance.
// Tags: barcode symbology, color scheme, image generation, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates several barcode images with different color schemes
/// using a single <see cref="BarcodeGenerator"/> instance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates PNG barcodes with varied foreground
    /// and background colors and saves them to a temporary directory.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the output images.
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the color schemes (foreground bar color, background color, and file name suffix).
        var colorSchemes = new[]
        {
            new { Bar = Color.Black, Back = Color.White, Name = "BlackOnWhite" },
            new { Bar = Color.White, Back = Color.Black, Name = "WhiteOnBlack" },
            new { Bar = Color.Blue, Back = Color.LightGray, Name = "BlueOnLightGray" }
        };

        // Initialize the generator once with the desired symbology and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ColorDemo"))
        {
            // Iterate over each color scheme, apply colors, and save the barcode image.
            foreach (var scheme in colorSchemes)
            {
                // Set the barcode (bar) color and the image background color.
                generator.Parameters.Barcode.BarColor = scheme.Bar;
                generator.Parameters.BackColor = scheme.Back;

                // Build the full file path for the current scheme.
                string filePath = Path.Combine(outputDir, $"{scheme.Name}.png");

                // Save the barcode as a PNG file.
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Output the location of the saved file.
                Console.WriteLine($"Saved barcode: {filePath}");
            }
        }
    }
}