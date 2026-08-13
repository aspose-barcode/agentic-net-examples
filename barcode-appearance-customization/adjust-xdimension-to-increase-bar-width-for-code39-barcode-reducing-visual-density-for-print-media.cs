// Title: Increase XDimension for Code39 barcode to reduce visual density
// Description: Demonstrates how to adjust the XDimension property of a Code39 barcode using Aspose.BarCode to make bars wider, which is useful for print media where lower density is desired.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating barcode parameter customization. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and barcode parameter settings such as XDimension, BarHeight, BarColor, and BackColor. Developers often need to tweak these settings to meet printing, scanning, and branding requirements.
// Prompt: Adjust XDimension to increase bar width for a Code39 barcode, reducing visual density for print media.
// Tags: code39, xdimension, barcode, generation, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code39 barcode with an increased XDimension to produce wider bars,
/// reducing visual density for better print media readability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, adjusts its dimensions,
    /// and saves it as a PNG image.
    /// </summary>
    static void Main()
    {
        // Initialize a Code39 barcode generator with the sample text "CODE39"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
        {
            // Increase XDimension (bar width) to 2 points for lower density
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Set a reasonable bar height (40 points) suitable for printing
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Define foreground (bars) and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG file
            generator.Save("code39.png");
        }

        // Output a simple confirmation message
        Console.WriteLine("Code39 barcode generated with increased XDimension.");
    }
}