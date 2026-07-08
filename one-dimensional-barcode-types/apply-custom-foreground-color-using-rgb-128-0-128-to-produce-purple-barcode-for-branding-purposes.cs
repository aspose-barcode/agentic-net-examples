// Title: Generate a Purple Code128 Barcode
// Description: Demonstrates how to set a custom foreground color for a barcode using Aspose.BarCode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize barcode appearance with the BarcodeGenerator API. It covers setting bar colors, selecting symbology, and exporting to common image formats. Developers often need to brand barcodes or match corporate colors, making use of classes like BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and System.Drawing.Color.
// Prompt: Apply a custom foreground color using RGB (128,0,128) to produce a purple barcode for branding purposes.
// Tags: code128, color, png, barcodegenerator, parameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with a custom purple foreground color
/// and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, applies the color, saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "purple_barcode.png";

        // Initialize the barcode generator with Code128 symbology and sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the foreground (bar) color to purple using RGB values (128, 0, 128)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(128, 0, 128);

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}