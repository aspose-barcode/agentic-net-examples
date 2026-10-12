// Title: Create a Code128 barcode with custom background color and save as GIF
// Description: Demonstrates how to generate a Code128 barcode, apply a light gray background, and export it as a GIF image suitable for web pages.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating barcode creation, visual customization, and image format conversion. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, common for developers who need to produce barcodes with specific styling and web‑friendly formats.
// Prompt: Create a barcode with custom background color and export it as a GIF image for web use.
// Tags: code128, barcode generation, background color, gif, aspose.barcode, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a barcode with a custom background color and saving it as a GIF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, sets a light gray background,
    /// saves it as a GIF file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output GIF file
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "custom_background_barcode.gif");

        // Initialize the barcode generator with Code128 symbology and the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Apply a custom light gray background color to the barcode image
            generator.Parameters.BackColor = Color.LightGray;

            // Save the generated barcode as a GIF image, ideal for web usage
            generator.Save(outputFile, BarCodeImageFormat.Gif);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputFile}");
    }
}