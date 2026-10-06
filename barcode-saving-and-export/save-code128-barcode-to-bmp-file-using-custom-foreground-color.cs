// Title: Save Code128 barcode as BMP with custom foreground color
// Description: Demonstrates generating a Code128 barcode, applying a custom blue foreground color, and saving it as a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class. Typical use cases include branding, visual integration, and color‑coded scanning solutions. Developers often need to set bar colors, choose image formats, and export files for downstream processing.
// Prompt: Save a Code128 barcode to a BMP file using a custom foreground color.
// Tags: code128, barcode generation, bmp output, custom color, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a Code128 barcode with a custom foreground color and saving it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, applies color, saves the image, and writes the output path to console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output BMP file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "code128_custom_color.bmp");

        // Initialize the barcode generator with Code128 symbology and the desired data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the barcode's foreground (bar) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Save the generated barcode as a BMP image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}