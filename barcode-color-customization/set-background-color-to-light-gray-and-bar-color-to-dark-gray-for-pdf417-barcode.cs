// Title: PDF417 Barcode with Custom Colors
// Description: Demonstrates setting background and bar colors for a PDF417 barcode and saving it as a PNG image.
// Prompt: Set the background color to light gray and bar color to dark gray for a PDF417 barcode.
// Tags: pdf417, barcode, color, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a PDF417 barcode with custom background and bar colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a PDF417 barcode, applies custom colors, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for PDF417 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Apply a light gray background to the barcode image
            generator.Parameters.BackColor = Color.LightGray;

            // Apply a dark gray color to the barcode bars (foreground)
            generator.Parameters.Barcode.BarColor = Color.DarkGray;

            // Persist the generated barcode to a PNG file
            generator.Save("pdf417_custom_colors.png");
        }
    }
}