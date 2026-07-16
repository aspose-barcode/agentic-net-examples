// Title: Generate a Code128 barcode with custom foreground and background colors
// Description: Demonstrates how to set bar and background colors for a barcode image, useful for brand-aligned visuals.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include creating branded barcodes for packaging, marketing materials, or UI elements where color matching is required. Developers often need to customize colors, sizes, and formats to integrate barcodes seamlessly into their designs.
// Prompt: Implement method to generate barcode with custom foreground and background colors for branding purposes.
// Tags: barcode symbology, color customization, png output, aspose.barcode generation, code128

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom foreground and background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a barcode image with brand colors and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define output file path
        string outputPath = "custom_barcode.png";

        // Initialize barcode generator for Code128 symbology with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Brand123"))
        {
            // Set the bar (foreground) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to yellow
            generator.Parameters.BackColor = Color.Yellow;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode generated and saved to '{outputPath}'.");
    }
}