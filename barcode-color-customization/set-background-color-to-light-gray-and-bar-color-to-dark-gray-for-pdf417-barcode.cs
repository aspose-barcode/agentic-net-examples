// Title: Set PDF417 Barcode Colors (Background Light Gray, Bar Dark Gray)
// Description: Demonstrates how to change the background and bar colors of a PDF417 barcode using Aspose.BarCode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize visual appearance of barcodes. It uses the BarcodeGenerator class with EncodeTypes.Pdf417, showing typical use cases such as setting background and foreground colors before exporting to an image format. Developers often need to adjust colors to match branding or design requirements.
// Prompt: Set the background color to light gray and bar color to dark gray for a PDF417 barcode.
// Tags: pdf417, barcode, color, background, foreground, aspose.barcode, image, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a PDF417 barcode with custom background and bar colors and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a PDF417 barcode, applies color settings, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with PDF417 symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Apply a light gray background color to the entire image
            generator.Parameters.BackColor = Color.LightGray;

            // Apply a dark gray color to the barcode bars (foreground)
            generator.Parameters.Barcode.BarColor = Color.DarkGray;

            // Save the generated barcode as a PNG file
            generator.Save("pdf417.png");
        }

        // Inform the user that the barcode image has been created
        Console.WriteLine("PDF417 barcode generated: pdf417.png");
    }
}