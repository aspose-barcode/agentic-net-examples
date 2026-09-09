// Title: Generate semi‑transparent Code128 barcode PNG using Aspose.BarCode
// Description: Demonstrates how to set a custom semi‑transparent bar color with System.Drawing.Color.FromArgb and save the barcode as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class and its Parameters.Barcode properties. Typical use cases include branding, UI overlays, or any scenario where a partially transparent barcode is required. Developers often need to adjust colors, sizes, and output formats when integrating barcodes into graphics‑rich applications.
// Prompt: Use a custom System.Drawing.Color.FromArgb value for semi‑transparent bar color and generate PNG.
// Tags: code128, barcode, color, transparency, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with a semi‑transparent bar color and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "semiTransparentBarcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the bar color to a semi‑transparent red using ARGB (alpha 128, red 255, green 0, blue 0).
            generator.Parameters.Barcode.BarColor = Color.FromArgb(128, 255, 0, 0);

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}