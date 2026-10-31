// Title: Generate 72 DPI Barcode Preview for Web Thumbnails
// Description: Creates a Code128 barcode image at 72 DPI, suitable for quick preview thumbnails in web applications.
// Category-Description: This example demonstrates how to use Aspose.BarCode to generate a barcode image with a specific resolution. It covers the BarcodeGenerator class, setting the Resolution parameter, and saving the image in PNG format. Developers often need to create low‑resolution barcode previews for web pages, reports, or email attachments, making this pattern useful for thumbnail generation and performance‑optimized rendering.
// Prompt: Set barcode resolution to 72 DPI for quick preview generation in a web thumbnail view.
// Tags: code128, resolution, preview, thumbnail, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a barcode image at 72 DPI for use as a quick preview thumbnail.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, sets its resolution to 72 DPI,
    /// saves it as a PNG file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the temporary output file path for the generated barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_preview.png");

        // The text to encode in the barcode.
        string codeText = "1234567890";

        // Create a BarcodeGenerator for Code128 symbology with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the image resolution to 72 DPI for a lightweight preview.
            generator.Parameters.Resolution = 72f;

            // Save the barcode image as a PNG file to the output path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}