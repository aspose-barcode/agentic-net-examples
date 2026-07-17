// Title: Generate Barcode with Custom DPI for High‑Resolution Printing
// Description: Demonstrates how to create a barcode image with a specified DPI setting, useful for high‑resolution print output.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcode images. Typical use cases include creating printable barcodes for packaging, labels, and documents where precise resolution is required. Developers often need to adjust DPI to meet printing standards and ensure barcode readability.
// Prompt: Implement method to generate barcode with custom DPI setting for high‑resolution printing requirements.
// Tags: code128, generation, png, resolution, barcodegenerator, aspnet.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides functionality to generate barcode images with custom DPI settings.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image with a custom DPI (resolution) setting.
    /// The barcode is saved as a PNG file at the specified path.
    /// </summary>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    /// <param name="dpi">Desired resolution in dots per inch.</param>
    static void GenerateBarcodeWithCustomDpi(string outputPath, float dpi)
    {
        // Ensure the output directory exists.
        string directory = System.IO.Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !System.IO.Directory.Exists(directory))
        {
            System.IO.Directory.CreateDirectory(directory);
        }

        // Create a BarcodeGenerator for Code128 with sample text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the desired resolution (DPI). This affects the size of the generated image.
            generator.Parameters.Resolution = dpi;

            // Save the barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Entry point of the program. Generates a high‑resolution barcode at 300 DPI and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Example: generate a high‑resolution barcode at 300 DPI.
        string filePath = "barcode_300dpi.png";
        float dpi = 300f;

        GenerateBarcodeWithCustomDpi(filePath, dpi);

        Console.WriteLine($"Barcode generated with {dpi} DPI and saved to '{filePath}'.");
    }
}