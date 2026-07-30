// Title: Generate transparent PNG barcode for overlay
// Description: Demonstrates setting a barcode's background to transparent and saving it as a PNG with an alpha channel, suitable for overlay scenarios.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class. It shows configuring background transparency and saving to PNG, a common requirement when integrating barcodes into UI overlays or composite images. Developers often need to adjust colors, formats, and image properties for seamless visual integration.
// Prompt: Set the background color to transparent and generate a PNG with alpha channel for overlay use.
// Tags: barcode, generation, transparent background, png, alpha channel, code128, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a Code128 barcode with a transparent background and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file name and location.
        string outputPath = "transparent_barcode.png";

        // Initialize the barcode generator with the desired symbology (Code128) and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the background color to transparent so the PNG will contain an alpha channel.
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

            // Optionally set the foreground (bar) color to black.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the generated barcode as a PNG file; PNG format preserves transparency.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}