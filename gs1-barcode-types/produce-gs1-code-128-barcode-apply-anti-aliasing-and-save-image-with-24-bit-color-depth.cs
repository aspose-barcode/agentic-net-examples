// Title: Generate GS1 Code 128 barcode with anti-aliasing and 24-bit BMP output
// Description: Demonstrates creating a GS1 Code 128 barcode, enabling anti‑aliasing for smoother rendering, and saving it as a 24‑bit BMP image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1Code128. Typical use cases include producing high‑quality barcodes for packaging, labeling, and inventory systems where GS1 standards are required. Developers often need to configure rendering options such as anti‑aliasing and specify image formats and color depth.
// Prompt: Produce a GS1 Code 128 barcode, apply anti‑aliasing, and save the image with 24‑bit color depth.
// Tags: gs1code128, barcode generation, anti-aliasing, bmp, 24-bit, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode with anti‑aliasing and saving it as a 24‑bit BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, configures rendering, and writes the file path to console.
    /// </summary>
    static void Main()
    {
        // Determine the output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1code128.bmp");

        // Initialize the barcode generator with GS1 Code 128 symbology and the required data string
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231"))
        {
            // Enable anti‑aliasing to produce a smoother barcode image
            generator.Parameters.UseAntiAlias = true;

            // Save the barcode as a BMP image (default 24‑bit color depth)
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"GS1 Code 128 barcode saved to: {outputPath}");
    }
}