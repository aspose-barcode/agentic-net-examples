// Title: Generate low‑resolution barcode preview (72 DPI)
// Description: Creates a Code128 barcode image at 72 DPI, suitable for quick thumbnail previews in web applications.
// Category-Description: Demonstrates how to use Aspose.BarCode to generate barcode images with custom resolution. This example belongs to the image generation category, highlighting the BarcodeGenerator class, EncodeTypes enumeration, and resolution settings. Developers often need low‑resolution previews for fast rendering in web UI or email attachments.
// Prompt: Set barcode resolution to 72 DPI for quick preview generation in a web thumbnail view.
// Tags: code128, resolution, preview, png, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a low‑resolution barcode image for quick preview purposes.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode at 72 DPI and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Determine a temporary file path for the output image.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_preview.png");

        // Create a barcode generator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set low resolution (72 DPI) for fast preview generation.
            generator.Parameters.Resolution = 72f;

            // Optionally adjust the X dimension to improve visibility at low resolution.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}