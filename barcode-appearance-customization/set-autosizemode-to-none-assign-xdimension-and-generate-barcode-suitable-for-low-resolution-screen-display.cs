// Title: Generate low‑resolution screen barcode with AutoSizeMode None and custom XDimension
// Description: Demonstrates how to create a Code128 barcode optimized for low‑resolution displays by disabling auto‑sizing and setting a specific X‑dimension. The barcode is saved as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and generation parameters such as AutoSizeMode, XDimension, and Resolution. Developers often need to control barcode size and DPI for screen rendering, web pages, or mobile apps where low‑resolution output is required. The snippet shows typical steps for configuring these settings and exporting the result.
// Prompt: Set AutoSizeMode to None, assign XDimension, and generate a barcode suitable for low‑resolution screen display.
// Tags: code128, barcode generation, autosizemode, xdimension, lowresolution, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a low‑resolution Code128 barcode with custom sizing.
/// </summary>
class Program
{
    /// <summary>
    /// Creates an output directory, configures the barcode generator, and saves the barcode as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder to store the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "LowResScreenBarcode.png");

        // Initialize the generator with Code128 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "LOWRES"))
        {
            // Disable automatic size adjustment
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the X‑dimension (module width) to 3 pixels for clearer low‑resolution rendering
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Define a low screen DPI (96 DPI) to match typical monitor settings
            generator.Parameters.Resolution = 96f;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}