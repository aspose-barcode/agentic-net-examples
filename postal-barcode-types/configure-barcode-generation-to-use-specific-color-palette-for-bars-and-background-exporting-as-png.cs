// Title: Generate Code128 barcode with custom colors and save as PNG
// Description: Demonstrates how to create a Code128 barcode, set custom bar and background colors, and export it as a PNG image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to customize barcode appearance. Typical scenarios include branding, UI integration, and printing where specific color palettes are required. Developers often need to adjust foreground/background colors and choose image formats for downstream processing.
// Prompt: Configure barcode generation to use a specific color palette for bars and background, exporting as PNG.
// Tags: code128, barcode-generation, color-customization, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with custom colors and PNG output.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with blue bars on a light‑gray background and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Create a BarcodeGenerator for Code128 symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the bar (foreground) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to light gray.
            generator.Parameters.BackColor = Color.LightGray;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}