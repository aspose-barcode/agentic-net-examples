// Title: Barcode generation with DPI‑dependent AutoSizeMode selection
// Description: Demonstrates how to generate Code128 barcodes at different resolutions and automatically select the appropriate AutoSizeMode (None or Interpolation) based on target DPI.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to produce barcodes optimized for low‑ and high‑resolution outputs. Developers often need to adjust barcode size and rendering mode when targeting printers or screens with varying DPI; this snippet shows typical configuration steps, including setting resolution, choosing AutoSizeMode, and optionally defining canvas dimensions for interpolation mode.
// Prompt: Implement a method that sets AutoSizeMode based on target DPI, choosing Interpolation for high‑resolution outputs.
// Tags: barcode, code128, autosizemode, dpi, interpolation, lowresolution, highresolution, aspnet, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with DPI‑dependent AutoSizeMode selection using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Creates an output folder, generates low‑ and high‑resolution barcodes, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated images
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Example 1: Low resolution (96 DPI) – AutoSizeMode.None
        GenerateBarcode("LowRes", "HelloWorld", 96f, outputFolder);

        // Example 2: High resolution (300 DPI) – AutoSizeMode.Interpolation
        GenerateBarcode("HighRes", "HelloWorld", 300f, outputFolder);

        Console.WriteLine("Barcodes generated in: " + outputFolder);
    }

    /// <summary>
    /// Generates a barcode image with the specified name, text, DPI, and output folder.
    /// </summary>
    /// <param name="name">Base file name without extension.</param>
    /// <param name="codeText">Text to encode in the barcode.</param>
    /// <param name="targetDpi">Desired image resolution in dots per inch.</param>
    /// <param name="folder">Folder where the image will be saved.</param>
    static void GenerateBarcode(string name, string codeText, float targetDpi, string folder)
    {
        // Build the full file path for the PNG image
        string filePath = Path.Combine(folder, $"{name}.png");

        // Initialize the barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Configure resolution and AutoSizeMode based on the target DPI
            ConfigureAutoSizeMode(generator, targetDpi);

            // If interpolation mode is selected, define a larger canvas to preserve quality
            if (generator.Parameters.AutoSizeMode == AutoSizeMode.Interpolation)
            {
                generator.Parameters.ImageWidth.Pixels = 600f;
                generator.Parameters.ImageHeight.Pixels = 200f;
            }

            // Save the generated barcode as a PNG file
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Sets the generator's resolution and chooses an appropriate AutoSizeMode.
    /// </summary>
    /// <param name="generator">The BarcodeGenerator instance to configure.</param>
    /// <param name="targetDpi">Desired image resolution in dots per inch.</param>
    static void ConfigureAutoSizeMode(BarcodeGenerator generator, float targetDpi)
    {
        // Apply the requested DPI to the generator
        generator.Parameters.Resolution = targetDpi;

        // Select AutoSizeMode: use Interpolation for high‑resolution outputs, otherwise None
        if (targetDpi >= 300f)
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
        }
        else
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
        }
    }
}