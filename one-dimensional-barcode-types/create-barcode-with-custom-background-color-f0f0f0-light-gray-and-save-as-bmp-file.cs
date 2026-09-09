// Title: Create Code128 Barcode with Light Gray Background and Save as BMP
// Description: This example generates a Code128 barcode with a custom light-gray background color (#F0F0F0) and saves it as a BMP image file.
// Category-Description: Demonstrates Aspose.BarCode generation features, focusing on visual customization such as background color and image format selection. It uses the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to produce bitmap output, a common requirement when integrating barcodes into Windows desktop or printing workflows.
// Prompt: Create a barcode with custom background color #F0F0F0 (light gray) and save as a BMP file.
// Tags: code128, barcode generation, bmp, background color, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a custom background color and saves it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies visual settings, and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output BMP file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.bmp");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the background color to light gray (#F0F0F0).
            generator.Parameters.BackColor = Color.FromArgb(0xF0, 0xF0, 0xF0);

            // Save the generated barcode as a BMP image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}