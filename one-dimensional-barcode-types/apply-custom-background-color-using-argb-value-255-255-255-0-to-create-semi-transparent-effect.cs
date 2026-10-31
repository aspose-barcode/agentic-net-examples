// Title: Generate Code128 barcode with semi‑transparent custom background
// Description: Demonstrates how to create a Code128 barcode image and apply a semi‑transparent background using an ARGB color.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing the use of BarcodeGenerator, its Parameters property, and color customization. Typical use cases include branding, UI overlays, or printing where a specific background opacity is required. Developers often need to adjust background colors, image formats, and encoding types when generating barcodes programmatically.
// Prompt: Apply a custom background color using ARGB value (255,255,255,0) to create a semi‑transparent effect.
// Tags: code128, background-color, argb, semi-transparent, png, aspnet, aspose.barcode, barcode-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a semi‑transparent background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, applies the custom background, saves the image, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "custom_bg_barcode.png");

        // Create a BarcodeGenerator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set a semi‑transparent background color using ARGB (255, 255, 255, 0).
            generator.Parameters.BackColor = Color.FromArgb(255, 255, 255, 0);

            // Save the generated barcode image as PNG to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}