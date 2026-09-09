// Title: Generate high‑resolution barcode with custom DPI
// Description: Demonstrates creating a Code128 barcode image with a specified DPI for high‑resolution printing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to configure barcode parameters such as module size and image resolution. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include producing print‑ready barcodes for labels, packaging, or documents where high DPI is required. Developers often need to adjust DPI to meet printer specifications or to ensure crisp rendering in high‑quality outputs.
// Prompt: Implement method to generate barcode with custom DPI setting for high‑resolution printing requirements.
// Tags: barcode, code128, high resolution, dpi, generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a high‑resolution Code128 barcode image with a custom DPI setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates the barcode, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the generated PNG image
        string outputPath = Path.Combine(tempDir, "high_res_barcode.png");

        // Text to encode in the barcode
        string codeText = "HIGHRES12345";

        // Desired image resolution in DPI for high‑resolution printing
        float resolutionDpi = 300f;

        // Generate the barcode with the specified parameters
        GenerateBarcode(codeText, outputPath, resolutionDpi);

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Generates a Code128 barcode image with custom DPI and saves it as PNG.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="outputPath">The file path where the PNG image will be saved.</param>
    /// <param name="resolutionDpi">The image resolution in dots per inch.</param>
    static void GenerateBarcode(string codeText, string outputPath, float resolutionDpi)
    {
        // Initialize the barcode generator with Code128 symbology and the provided text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Optionally set the module size (X dimension) in millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = 1;

            // Apply the custom resolution (DPI) for high‑resolution output
            generator.Parameters.Resolution = resolutionDpi;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}