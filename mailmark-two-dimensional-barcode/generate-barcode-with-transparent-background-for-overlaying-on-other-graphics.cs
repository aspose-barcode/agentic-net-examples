// Title: Generate a Barcode with Transparent Background
// Description: Creates a Code128 barcode saved as a PNG image with a transparent background, suitable for overlaying on other graphics.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to customize barcode appearance. It demonstrates setting foreground and background colors, specifically applying a transparent background, and saving the result in a format (PNG) that supports transparency. Developers working with barcode overlays, UI graphics, or printable labels often need to generate transparent barcodes for seamless integration with existing images.
// Prompt: Generate a barcode with a transparent background for overlaying on other graphics.
// Tags: code128, barcode generation, transparent background, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a transparent background using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Define the file path where the PNG image will be saved.
        string outputPath = "transparent_barcode.png";

        // Initialize the barcode generator for Code128 symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the color of the barcode bars (foreground) to black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Configure the background color to be transparent.
            generator.Parameters.BackColor = Color.Transparent;

            // Save the generated barcode as a PNG file, which preserves transparency.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}