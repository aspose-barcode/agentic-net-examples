// Title: Generate Code39 barcodes with different bar colors using Aspose.BarCode
// Description: Demonstrates how to create a Code39 barcode, set its bar (foreground) color, and save multiple images with different colors.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the BarcodeGenerator class and its Parameters.Barcode properties. Typical use cases include customizing barcode appearance such as bar color, background, and size before exporting to image formats. Developers often need to generate multiple variants of the same barcode for branding or visual distinction.
// Prompt: Use the same BarcodeGenerator to produce a red‑bar Code39 barcode, then change to blue‑bar and save again.
// Tags: barcode, code39, barcolor, image, aspose.barcode, generation, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code39 barcodes with different bar colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a BarcodeGenerator, sets bar colors, and saves PNG images.
    /// </summary>
    static void Main()
    {
        // Initialize a Code39 barcode generator with the sample text "123ABC"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "123ABC"))
        {
            // Set the bar (foreground) color to red
            generator.Parameters.Barcode.BarColor = Color.Red;
            // Save the red-bar barcode image
            generator.Save("code39_red.png");

            // Change the bar color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;
            // Save the blue-bar barcode image
            generator.Save("code39_blue.png");
        }

        // Inform the user that the barcode images have been generated
        Console.WriteLine("Barcodes generated: code39_red.png, code39_blue.png");
    }
}