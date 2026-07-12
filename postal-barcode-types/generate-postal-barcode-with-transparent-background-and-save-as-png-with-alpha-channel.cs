// Title: Generate a Postnet barcode with transparent background and save as PNG
// Description: Demonstrates creating a Postnet postal barcode, applying a transparent background, and exporting it as a PNG image with an alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on visual customization and image output. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and the Parameters property to modify colors and save formats. Developers often need to generate barcodes for mailing, customize appearance, and produce images with transparency for web or print integration.
// Prompt: Generate a postal barcode with transparent background and save as PNG with alpha channel.
// Tags: postnet, barcode generation, png, transparent background, aspose.barcode, aspnet

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Postnet barcode with a transparent background
/// and saves it as a PNG file preserving the alpha channel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for the Postnet symbology with a sample ZIP code.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Set the background color to transparent so the PNG will have an alpha channel.
            generator.Parameters.BackColor = Color.Transparent;

            // Ensure the barcode bars are drawn in black (default, but set explicitly for clarity).
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG file; the transparent background is retained.
            generator.Save("postal.png");
        }
    }
}