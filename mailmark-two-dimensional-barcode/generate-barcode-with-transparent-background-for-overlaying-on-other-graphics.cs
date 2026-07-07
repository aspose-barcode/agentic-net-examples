// Title: Generate a Code128 barcode with transparent background
// Description: Demonstrates how to create a barcode image with a transparent background, suitable for overlaying on other graphics.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and drawing parameters to control visual appearance. Developers often need to produce barcodes that blend into existing UI or printed material without a solid background, and this snippet shows the typical steps for setting background transparency and saving as PNG.
// Prompt: Generate a barcode with a transparent background for overlaying on other graphics.
// Tags: code128, barcode, transparent background, png, aspose.barcode, image generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a transparent background using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, sets transparent background, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Output file path
        string outputPath = "transparent_barcode.png";

        // Initialize the barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Text to encode into the barcode
            generator.CodeText = "Sample123";

            // Set background to transparent so the image can be overlaid
            generator.Parameters.BackColor = Color.Transparent;

            // Optional: define the bar (foreground) color; default is black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as PNG, which supports transparency
            generator.Save(outputPath);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}