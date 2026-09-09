// Title: Generate PNG Barcode with Transparent Background
// Description: Demonstrates how to create a Code128 barcode with a transparent background and save it as a PNG image that retains the alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcodes with custom visual properties. Typical scenarios include embedding barcodes into UI designs or documents where a transparent background is required. Developers often need to control background colors and export formats while preserving transparency.
// Prompt: Produce a barcode with transparent background and export it as PNG preserving the alpha channel.
// Tags: barcode, code128, transparent background, png, alpha channel, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a transparent background and saving it as a PNG image that preserves the alpha channel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures transparency, saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file
        string outputPath = Path.Combine(Environment.CurrentDirectory, "transparent_barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
        {
            // Configure the barcode to have a transparent background
            generator.Parameters.BackColor = Color.Transparent;

            // Save the barcode as a PNG file; PNG format retains the alpha channel (transparency)
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}