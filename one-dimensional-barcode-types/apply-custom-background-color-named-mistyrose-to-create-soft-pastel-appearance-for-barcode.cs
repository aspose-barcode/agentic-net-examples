// Title: Generate a Code128 barcode with a MistyRose background
// Description: Demonstrates how to set a custom pastel background color for a barcode image using Aspose.BarCode and save it as PNG.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and color parameters. Developers often need to customize barcode appearance for branding or UI integration, adjusting background and foreground colors before exporting to common image formats.
// Prompt: Apply a custom background color named “MistyRose” to create a soft pastel appearance for the barcode.
// Tags: barcode symbology, background color, png output, aspose.barcode, code128, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a MistyRose background and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, applies colors, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        const string outputPath = "barcode_mistyrose.png";

        // Initialize a BarcodeGenerator for Code128 symbology with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set a soft pastel background color named "MistyRose"
            generator.Parameters.BackColor = Color.MistyRose;

            // Optionally, set the barcode (foreground) color to a contrasting dark shade
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode image to the specified file in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to '{outputPath}'.");
    }
}