// Title: Generate Code39 barcode with red and blue bars
// Description: Demonstrates creating a Code39 barcode, saving it with a red bar color, then changing to blue and saving again.
// Prompt: Use the same BarcodeGenerator to produce a red‑bar Code39 barcode, then change to blue‑bar and save again.
// Tags: barcode, code39, color, generation, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code39 barcode in two colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code39 barcode, saves it with red bars, then with blue bars.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with Code39 symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC-123"))
        {
            // Set the bar color to red
            generator.Parameters.Barcode.BarColor = Color.Red;
            // Save the barcode image as PNG
            generator.Save("code39_red.png");

            // Change the bar color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;
            // Save the barcode image again with the new color
            generator.Save("code39_blue.png");
        }
    }
}