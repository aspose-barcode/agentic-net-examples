// Title: Custom Text Color for Barcode Generation
// Description: Demonstrates how to set a custom color for the human‑readable text of a barcode while keeping the bar and background colors at their default values.
// Prompt: Apply a custom text color to a barcode while leaving bar and background colors at defaults.
// Tags: barcode, code128, textcolor, aspose.barcode, image generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a custom text color.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image with red human‑readable text.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator with default bar and background colors
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the human‑readable text color to red (bars and background stay default)
            generator.Parameters.Barcode.CodeTextParameters.Color = Aspose.Drawing.Color.Red;

            // Save the generated barcode as a PNG file
            generator.Save("barcode.png");
        }

        // Inform the user that the image has been saved
        Console.WriteLine("Barcode image saved as barcode.png");
    }
}