// Title: Generate Code128 Barcode and Verify Default Background Color
// Description: Creates a Code128 barcode using default colors, saves it, then explicitly sets the background to white and saves again to confirm the default behavior.
// Prompt: Generate a barcode with default colors and then change background to white to confirm default behavior.
// Tags: barcode, code128, default colors, background, aspnet, aspose.barcode, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with default colors,
/// then explicitly setting the background to white to verify the default behavior.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates and saves two barcode images.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the data "123456"
        // The generator uses default settings where the background is white.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save the barcode image using the default color scheme (white background)
            generator.Save("barcode_default.png");

            // Explicitly set the background color to white (same as the default) to confirm behavior
            generator.Parameters.BackColor = Color.White;

            // Save the barcode image after setting the background to white
            generator.Save("barcode_white.png");
        }
    }
}