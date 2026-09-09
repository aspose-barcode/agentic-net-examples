// Title: Generate Code128 Barcode with Custom Image Size Using Interpolation AutoSizeMode
// Description: Demonstrates how to generate a Code128 barcode image with explicit width and height while keeping AutoSizeMode set to Interpolation.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcode images. Developers often need to control the exact pixel dimensions of generated barcodes for UI layout or printing while still leveraging automatic sizing algorithms like Interpolation. The snippet illustrates typical steps: configuring parameters, setting image size, and saving the result.
// Prompt: Override default sizing by setting explicit ImageHeight and ImageWidth while AutoSizeMode remains Interpolation.
// Tags: barcode symbology, generation, png, autosizemode, imagesize, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode image with custom dimensions using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it to a temporary PNG file, and writes the path to console.
    /// </summary>
    static void Main()
    {
        // Determine a temporary file path for the output PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Create a BarcodeGenerator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Keep automatic sizing enabled but use the Interpolation mode.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set explicit image dimensions in pixels.
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 150f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}