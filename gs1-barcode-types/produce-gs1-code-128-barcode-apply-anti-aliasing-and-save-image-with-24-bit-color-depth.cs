// Title: Generate GS1 Code 128 barcode with anti‑aliasing and 24‑bit BMP output
// Description: Demonstrates how to create a GS1 Code 128 barcode, enable anti‑aliasing, and save it as a 24‑bit BMP image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include creating high‑quality barcode images for packaging, labeling, and inventory systems where GS1 compliance and visual clarity are required. Developers often need to configure rendering options such as anti‑aliasing and output color depth to meet printing standards.
// Prompt: Produce a GS1 Code 128 barcode, apply anti‑aliasing, and save the image with 24‑bit color depth.
// Tags: gs1code128, barcode generation, anti-aliasing, bmp, 24-bit, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode, applies anti‑aliasing,
/// and saves the result as a 24‑bit BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, generates the barcode, and writes the file path to console.
    /// </summary>
    static void Main()
    {
        // Determine the output folder relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir); // Ensure the folder exists

        // Full path for the resulting BMP file
        string outputPath = Path.Combine(outputDir, "GS1Code128.bmp");

        // GS1 Code 128 barcode with sample GS1 data (Application Identifier 01)
        string gs1CodeText = "(01)12345678901231";

        // Initialize the barcode generator with the desired symbology and data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1CodeText))
        {
            // Enable anti‑aliasing to improve visual quality
            generator.Parameters.UseAntiAlias = true;

            // Save the barcode as a BMP image with 24‑bit color depth
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}