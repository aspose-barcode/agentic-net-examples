// Title: Generate high-contrast Code128 barcode image
// Description: Demonstrates setting barcode foreground to black and background to white for optimal scanning contrast.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize visual appearance using BarcodeGenerator, EncodeTypes, and BarCodeImageFormat. Typical use cases include creating printable barcodes with maximum readability in scanned documents. Developers often need to adjust colors, sizes, and formats to meet printing and scanning requirements.
// Prompt: Set barcode background to white and foreground to black for maximum contrast in scanned documents.
// Tags: code128, color, contrast, png, generation, aspose.barcode, barcodegenerator

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Shows how to generate a Code128 barcode with a white background and black foreground,
/// ensuring maximum contrast for scanned documents.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode bars (foreground) to black for high contrast.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the image background to white.
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}