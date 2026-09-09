// Title: Apply Semi-Transparent Background Color to a Barcode Image
// Description: Demonstrates how to generate a Code128 barcode with a custom semi‑transparent background using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to customize visual aspects of generated barcodes. It uses the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create and save barcode images. Developers often need to adjust colors, backgrounds, and output formats to match branding or UI requirements, and this snippet illustrates those common tasks.
// Prompt: Apply a custom background color using ARGB value (255,255,255,0) to create a semi‑transparent effect.
// Tags: barcode, code128, background color, argb, semi-transparent, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode with a semi‑transparent background and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies custom colors, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set a semi‑transparent background using ARGB (alpha=255, red=255, green=255, blue=0).
            generator.Parameters.BackColor = Color.FromArgb(255, 255, 255, 0);

            // Set the barcode bars to solid black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}