// Title: Generate semi‑transparent barcode PNG using Aspose.BarCode
// Description: Demonstrates how to set a custom semi‑transparent bar color with System.Drawing.Color.FromArgb and save the barcode as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to customize barcode appearance. Developers often need to adjust bar colors, transparency, and output formats for branding or UI integration. The snippet shows typical steps: create generator, set parameters, and save the image.
// Prompt: Use a custom System.Drawing.Color.FromArgb value for semi‑transparent bar color and generate PNG.
// Tags: code128, barcode, color, transparency, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Code128 barcode with a semi‑transparent bar color
/// and saves it as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
        {
            // Set semi‑transparent blue bar color (alpha 128, RGB 0,0,255).
            generator.Parameters.Barcode.BarColor = Color.FromArgb(128, 0, 0, 255);

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}