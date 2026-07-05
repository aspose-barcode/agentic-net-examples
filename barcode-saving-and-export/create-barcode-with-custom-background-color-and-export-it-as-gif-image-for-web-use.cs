// Title: Generate Code128 Barcode with Custom Colors and Save as GIF
// Description: Demonstrates creating a Code128 barcode, applying custom background and bar colors, and exporting it as a GIF image suitable for web usage.
// Prompt: Create a barcode with custom background color and export it as a GIF image for web use.
// Tags: code128, barcode, color, gif, aspose.barcode, aspose.drawing, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with custom colors
/// and saves it as a GIF image for web use.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Creates a barcode, customizes its appearance, and writes it to a GIF file.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "HelloWorld"))
        {
            // Apply a custom background color to the image.
            generator.Parameters.BackColor = Color.LightYellow;

            // Apply a custom foreground (bar) color to the barcode.
            generator.Parameters.Barcode.BarColor = Color.DarkBlue;

            // Save the generated barcode as a GIF file, which is web‑friendly.
            generator.Save("barcode.gif");
        }

        // Inform the user that the operation completed successfully.
        Console.WriteLine("Barcode generated and saved as barcode.gif");
    }
}