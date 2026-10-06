// Title: Generate low‑resolution barcode preview (72 DPI) for web thumbnails
// Description: Demonstrates how to set the barcode generator resolution to 72 DPI and save the image as PNG, useful for creating quick preview thumbnails in web applications.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcode images. Typical scenarios include generating low‑resolution previews for fast loading in web pages, email attachments, or mobile apps. Developers often need to adjust resolution, size, and format to balance quality and performance.
// Prompt: Set barcode resolution to 72 DPI for quick preview generation in a web thumbnail view.
// Tags: barcode, preview, resolution, 72dpi, png, aspnet, aspose.barcode, generation, web thumbnail

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a low‑resolution barcode image suitable for thumbnail previews.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode at 72 DPI and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodePreview");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the preview image
        string outputPath = Path.Combine(outputDir, "preview.png");

        // Create a barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Configure the generator to use a low resolution of 72 DPI for fast preview rendering
            generator.Parameters.Resolution = 72f;

            // Save the generated barcode as a PNG file at the specified location
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the preview image was saved
        Console.WriteLine($"Barcode preview saved to: {outputPath}");
    }
}