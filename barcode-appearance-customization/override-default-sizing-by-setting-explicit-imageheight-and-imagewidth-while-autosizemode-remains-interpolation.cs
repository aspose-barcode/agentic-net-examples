// Title: Generate Code128 barcode with explicit dimensions using Interpolation auto-size mode
// Description: Demonstrates how to override default barcode sizing by setting ImageWidth and ImageHeight while keeping AutoSizeMode set to Interpolation.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to control barcode image dimensions. Developers often need to produce barcodes with specific pixel sizes for UI layouts, printing, or integration into graphics, and this pattern shows the typical API calls for such scenarios.
// Prompt: Override default sizing by setting explicit ImageHeight and ImageWidth while AutoSizeMode remains Interpolation.
// Tags: code128, barcode generation, image size, autosizemode, interpolation, aspnet, aspnetcore, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with explicit image dimensions while using Interpolation auto-size mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it to the Output folder.
    /// </summary>
    static void Main(string[] args)
    {
        // Set up the output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "barcode_interpolation.png");

        // Create a barcode generator for Code128 with the data "123456"
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Keep auto-size mode as Interpolation
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Override default sizing by setting explicit width and height in pixels
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 150f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}