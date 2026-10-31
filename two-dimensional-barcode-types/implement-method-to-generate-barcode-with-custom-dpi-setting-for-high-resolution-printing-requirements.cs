// Title: Generate high‑resolution barcode with custom DPI
// Description: Demonstrates how to create a Code128 barcode image with a specified DPI for high‑resolution printing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to configure barcode parameters such as module size and image resolution using the BarcodeGenerator class. Typical use cases include producing print‑ready barcodes for labels, packaging, or documents where high DPI ensures crisp output. Developers often need to adjust DPI and dimensions to meet printer specifications, and this snippet illustrates the essential API calls.
// Prompt: Implement method to generate barcode with custom DPI setting for high‑resolution printing requirements.
// Tags: barcode, code128, dpi, high-resolution, generation, aspnet, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides entry point for the High‑Resolution Barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Creates output directory, generates a barcode with custom DPI, and writes the file path to console.
    /// </summary>
    static void Main()
    {
        // Determine the output folder relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the full path for the generated barcode image.
        string barcodePath = Path.Combine(outputDir, "HighResBarcode.png");

        // Generate a Code128 barcode with a DPI of 300.
        GenerateBarcode("ASPOSE-2024", barcodePath, 300f);

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {barcodePath}");
    }

    /// <summary>
    /// Generates a barcode image using the specified text, file path, and DPI.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="filePath">The file system path where the image will be saved.</param>
    /// <param name="dpi">The desired image resolution in dots per inch.</param>
    static void GenerateBarcode(string codeText, string filePath, float dpi)
    {
        // Initialize the barcode generator with Code128 symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Optionally set the module (X) dimension in millimeters.
            generator.Parameters.Barcode.XDimension.Millimeters = 1f;

            // Apply the custom resolution (DPI) to the output image.
            generator.Parameters.Resolution = dpi;

            // Save the generated barcode as a PNG file.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }
}