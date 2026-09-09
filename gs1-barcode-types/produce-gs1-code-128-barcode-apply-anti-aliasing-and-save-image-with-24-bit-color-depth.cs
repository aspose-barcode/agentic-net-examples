// Title: Generate GS1 Code 128 barcode with anti-aliasing and 24‑bit BMP output
// Description: Demonstrates creating a GS1 Code 128 barcode, enabling anti‑aliasing, and saving it as a 24‑bit BMP image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.GS1Code128. Typical use cases include producing high‑quality barcodes for packaging, labeling, and inventory systems where GS1 standards are required. Developers often need to configure rendering options such as anti‑aliasing and specify image formats and color depths for downstream processing.
// Prompt: Produce a GS1 Code 128 barcode, apply anti‑aliasing, and save the image with 24‑bit color depth.
// Tags: gs1code128, barcode generation, anti-aliasing, bmp, 24-bit, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode, applies anti‑aliasing,
/// and saves it as a 24‑bit BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, generates the barcode, and writes the file path to console.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting BMP file
        string outputPath = Path.Combine(outputDir, "GS1Code128.bmp");

        // GS1 Code 128 barcode text (example data)
        string codeText = "(01)12345678901231";

        // Create a BarcodeGenerator for GS1 Code 128 and configure rendering options
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Enable anti‑aliasing for smoother visual quality
            generator.Parameters.UseAntiAlias = true;

            // Save the barcode as a 24‑bit BMP image
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}