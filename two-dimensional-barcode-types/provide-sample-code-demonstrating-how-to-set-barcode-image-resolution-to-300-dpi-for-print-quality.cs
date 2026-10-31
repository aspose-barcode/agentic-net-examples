// Title: Set barcode image resolution to 300 DPI for high‑quality printing
// Description: Demonstrates how to configure the Aspose.BarCode generator to produce a barcode image with a resolution of 300 DPI, suitable for print media.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to customize output properties such as resolution. Developers often need to adjust DPI for print‑ready barcodes, ensuring sharpness and compliance with packaging or labeling standards. The snippet shows creating an output folder, setting resolution, and saving the image.
// Prompt: Provide sample code demonstrating how to set barcode image resolution to 300 DPI for print quality.
// Tags: barcode, resolution, dpi, print, code128, aspnet, aspose.barcode, image-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting barcode image resolution to 300 DPI using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode image at 300 DPI and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Determine output directory relative to current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting barcode image
        string outputPath = Path.Combine(outputDir, "Barcode300DPI.png");

        // Initialize barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set image resolution to 300 DPI for high‑quality print output
            generator.Parameters.Resolution = 300f;
            // Save the barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}