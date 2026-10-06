// Title: Generate Code128 Barcode with Explicit Image Size and Interpolation AutoSizeMode
// Description: Demonstrates how to generate a Code128 barcode image with custom width and height while keeping AutoSizeMode set to Interpolation.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to control barcode dimensions using the BarcodeGenerator API. It highlights key classes such as BarcodeGenerator, EncodeTypes, AutoSizeMode, and BarCodeImageFormat, which are commonly used for creating printable or display‑ready barcodes with precise sizing requirements. Developers often need to adjust image size for layout consistency, DPI constraints, or integration into UI components, making this pattern a frequent requirement in barcode‑related projects.
// Prompt: Override default sizing by setting explicit ImageHeight and ImageWidth while AutoSizeMode remains Interpolation.
// Tags: code128, barcode generation, image size, autosizemode, interpolation, aspnet, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode with custom dimensions
/// while using the Interpolation auto‑size mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode image and saves it to the Output folder.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file.
        string outputPath = Path.Combine(outputDir, "barcode_interpolation.png");

        // Initialize the barcode generator with Code128 symbology and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Keep the auto‑size mode as Interpolation.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set explicit image dimensions (pixels).
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 150f;

            // Save the barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}