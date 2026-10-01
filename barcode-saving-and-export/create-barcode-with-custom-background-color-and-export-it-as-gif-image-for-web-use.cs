// Title: Generate a Code128 barcode with a custom background and save as GIF
// Description: Demonstrates creating a Code128 barcode, applying a light gray background, and exporting it as a GIF image suitable for web usage.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance using the BarcodeGenerator class. Typical use cases include branding barcodes with corporate colors and producing web‑friendly image formats. Developers often need to adjust colors, select symbologies, and choose appropriate image formats for online deployment.
// Prompt: Create a barcode with custom background color and export it as a GIF image for web use.
// Tags: code128, background color, gif, barcode generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a custom background color
/// and saves it as a GIF image, demonstrating basic Aspose.BarCode customization.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output directory, configures the barcode,
    /// and writes the resulting GIF file to disk.
    /// </summary>
    static void Main()
    {
        // Prepare output directory and file path
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "custom_bg_barcode.gif");

        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set custom background color (e.g., LightGray)
            generator.Parameters.BackColor = Color.LightGray;

            // Set bar (foreground) color if desired
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a GIF image suitable for web use
            generator.Save(outputPath, BarCodeImageFormat.Gif);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}