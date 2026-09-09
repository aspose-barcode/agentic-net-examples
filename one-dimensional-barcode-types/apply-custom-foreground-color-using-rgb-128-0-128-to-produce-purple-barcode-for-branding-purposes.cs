// Title: Generate a purple Code128 barcode image
// Description: Demonstrates how to set a custom foreground color (RGB 128,0,128) for a Code128 barcode and save it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to customize barcode appearance. Typical use cases include branding, product labeling, and creating visually distinct barcodes. Developers often need to adjust colors, sizes, and formats to match corporate identity.
// Prompt: Apply a custom foreground color using RGB (128,0,128) to produce a purple barcode for branding purposes.
// Tags: code128, color, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom purple foreground color and saving it as a PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a BarcodeGenerator, sets the bar color, saves the image, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "purple_barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode's foreground (bar) color to purple using RGB values.
            generator.Parameters.Barcode.BarColor = Color.FromArgb(128, 0, 128);

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}