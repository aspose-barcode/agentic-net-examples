// Title: Generate a Code128 barcode with transparent background and save as PNG
// Description: Demonstrates how to create a Code128 barcode, set a transparent background, and export it as a PNG image with an alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers commonly need to render barcodes with custom colors or transparency for UI overlays, reports, or web pages. The snippet shows typical steps: configure parameters, generate, and save the image.
// Prompt: Implement method to generate barcode with transparent background and save as PNG with alpha channel.
// Tags: code128, barcode generation, transparent background, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode with a transparent background and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates the barcode, and saves it.
    /// </summary>
    static void Main()
    {
        // Determine output folder path relative to current directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "transparent_barcode.png");

        // Initialize barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set background to transparent and barcode bars to black
            generator.Parameters.BackColor = Color.Transparent;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a PNG image preserving the alpha channel
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}