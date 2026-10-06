// Title: Generate multiple barcode images with different color schemes using a single BarcodeGenerator
// Description: Demonstrates how to change bar and background colors of a barcode and save each variation as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce styled barcode images. Typical scenarios include creating brand‑consistent barcodes, generating assets for marketing materials, or providing multiple visual options for the same data. Developers often need to adjust colors, formats, and output locations while reusing a single generator instance for efficiency.
// Prompt: Produce multiple barcode images with varying color schemes using a single BarcodeGenerator instance.
// Tags: barcode symbology, color scheme, image generation, png, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates three PNG barcode images, each with a distinct bar and background color,
/// using a single <see cref="BarcodeGenerator"/> instance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images with different color schemes and saves them to disk.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working folder and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Create a BarcodeGenerator for Code128 symbology with the sample text "Sample123".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // -------------------------------------------------
            // First variant: black bars on a white background.
            // -------------------------------------------------
            generator.Parameters.Barcode.BarColor = Color.Black;   // Set bar color.
            generator.Parameters.BackColor = Color.White;          // Set background color.
            string path1 = Path.Combine(outputDir, "barcode_black_on_white.png");
            generator.Save(path1, BarCodeImageFormat.Png);          // Save as PNG.

            // -------------------------------------------------
            // Second variant: blue bars on a yellow background.
            // -------------------------------------------------
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.Yellow;
            string path2 = Path.Combine(outputDir, "barcode_blue_on_yellow.png");
            generator.Save(path2, BarCodeImageFormat.Png);

            // -------------------------------------------------
            // Third variant: green bars on a light gray background.
            // -------------------------------------------------
            generator.Parameters.Barcode.BarColor = Color.Green;
            generator.Parameters.BackColor = Color.LightGray;
            string path3 = Path.Combine(outputDir, "barcode_green_on_lightgray.png");
            generator.Save(path3, BarCodeImageFormat.Png);
        }

        // Inform the user that the images have been generated.
        Console.WriteLine("Barcode images generated.");
    }
}