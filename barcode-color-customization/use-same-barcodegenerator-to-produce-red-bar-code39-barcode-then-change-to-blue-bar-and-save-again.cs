// Title: Generate Code39 barcode with red and blue bars using Aspose.BarCode
// Description: Demonstrates how to create a Code39 barcode, set its bar color to red, save it, then change the color to blue and save again.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class and its Parameters.Barcode properties. Typical use cases include generating barcodes with specific color schemes for branding or visual distinction. Developers often need to modify bar colors, sizes, and formats before saving to image files.
// Prompt: Use the same BarcodeGenerator to produce a red‑bar Code39 barcode, then change to blue‑bar and save again.
// Tags: code39, barcode generation, color customization, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code39 barcode with different bar colors and saving them as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, changes its bar color, and saves the images.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code39 with the sample text "123ABC"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "123ABC"))
        {
            // Set the bar (foreground) color to red
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Red;
            // Save the red barcode as a PNG file
            generator.Save("code39_red.png");

            // Change the bar color to blue
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            // Save the blue barcode as a PNG file
            generator.Save("code39_blue.png");
        }

        // Inform the user that the barcode images have been generated
        Console.WriteLine("Red and blue Code39 barcodes have been generated.");
    }
}