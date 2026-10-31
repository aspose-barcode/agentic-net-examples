// Title: Generate high‑resolution Code128 barcode image at 600 DPI
// Description: Demonstrates how to create a Code128 barcode image with a resolution of 600 DPI, suitable for high‑quality glossy label printing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers often need to produce high‑resolution barcodes for print media, adjust module size, and export to common image formats. The snippet shows typical steps for setting resolution, customizing dimensions, and saving the image.
// Prompt: Generate a barcode image at 600 DPI resolution for high‑quality glossy label printing.
// Tags: code128, barcode generation, png, resolution, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode image at 600 DPI and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Determine the output directory (creates "Output" folder in the current working directory)
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            // Ensure the directory exists before saving the image
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "barcode_600dpi.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image resolution to 600 DPI for high‑quality glossy label printing
            generator.Parameters.Resolution = 600f;

            // Optionally adjust the module (X) dimension; here set to 0.5 points
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}