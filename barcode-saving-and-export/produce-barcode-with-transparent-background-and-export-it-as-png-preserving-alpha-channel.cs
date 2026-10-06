// Title: Generate Code128 barcode with transparent background and save as PNG
// Description: Demonstrates how to create a Code128 barcode, set its background to transparent, and export it as a PNG image that retains the alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcodes with custom visual properties. Typical scenarios include creating graphics for web pages or UI elements where a transparent background is required. Developers often need to control colors, formats, and image settings when integrating barcodes into applications.
// Prompt: Produce a barcode with transparent background and export it as PNG preserving the alpha channel.
// Tags: code128, barcode generation, png, transparent background, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with a transparent background
/// and saves it as a PNG image preserving the alpha channel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "BarcodeTransparent.png");

        // Initialize the barcode generator with the desired symbology (Code128) and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Configure the barcode's background color to be fully transparent.
            generator.Parameters.BackColor = Color.Transparent;

            // Save the generated barcode as a PNG file, preserving the alpha channel.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}