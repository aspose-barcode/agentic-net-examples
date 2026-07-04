// Title: Generate Transparent PNG Barcode
// Description: Creates a Code128 barcode with a transparent background and saves it as a PNG file preserving the alpha channel for overlay use.
// Prompt: Set the background color to transparent and generate a PNG with alpha channel for overlay use.
// Tags: code128, barcode, transparent background, png, alpha channel, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a barcode with a transparent background and save it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode with a transparent background and saves it as a PNG.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 symbology with the sample text "Sample".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
        {
            // Configure the barcode to have a transparent background.
            generator.Parameters.BackColor = Color.Transparent;

            // Save the generated barcode as a PNG file.
            // PNG format retains the alpha channel, making the background truly transparent.
            generator.Save("transparent_barcode.png");
        }
    }
}