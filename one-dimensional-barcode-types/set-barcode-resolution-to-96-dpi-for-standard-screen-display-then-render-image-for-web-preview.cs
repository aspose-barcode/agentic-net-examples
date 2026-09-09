// Title: Generate 96 DPI Code128 Barcode and Output Base64 PNG for Web Preview
// Description: This example creates a Code128 barcode image at 96 DPI, saves it as PNG, and prints a Base64 string suitable for embedding in web pages.
// Category-Description: Demonstrates Aspose.BarCode image generation with resolution settings. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce raster images. Typical use cases include creating barcodes for web display, e‑commerce, or mobile apps where screen resolution matters. Developers often need to adjust DPI, choose output formats, and obtain Base64 data for client‑side rendering.
// Prompt: Set barcode resolution to 96 DPI for standard screen display, then render image for web preview.
// Tags: code128, barcode, resolution, dpi, png, base64, aspose.barcode, image generation, web preview

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode at 96 DPI and outputting a Base64 PNG string for web preview.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it, and writes the Base64 representation to console.
    /// </summary>
    static void Main()
    {
        // Define output file path in the system's temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_96dpi.png");

        // Create a barcode generator for Code128, set the resolution to 96 DPI, and save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.Resolution = 96f; // 96 DPI for standard screen display
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // If the image was successfully created, read it and output a Base64 string for web preview
        if (File.Exists(outputPath))
        {
            byte[] imageBytes = File.ReadAllBytes(outputPath);
            string base64 = Convert.ToBase64String(imageBytes);
            Console.WriteLine("Base64 PNG for web preview:");
            Console.WriteLine(base64);
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
        }
    }
}