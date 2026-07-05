// Title: Custom Color Scheme for Barcode Generation
// Description: Demonstrates how to apply a dark bar color and a light background to improve detection of dark barcodes on light backgrounds.
// Prompt: Apply a custom color scheme to enhance detection of dark barcodes on light backgrounds.
// Tags: code128, color, generation, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a custom dark bar color
/// and a light background to enhance contrast for detection on light surfaces.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, applies custom colors,
    /// and saves the result as a PNG image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "Sample123"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set a dark bar color (dark blue) to stand out against a light background
            generator.Parameters.Barcode.BarColor = Color.FromArgb(0, 0, 139); // DarkBlue

            // Set a light background color (light yellow) to improve contrast
            generator.Parameters.BackColor = Color.FromArgb(255, 255, 224); // LightYellow

            // Optional: increase XDimension for larger modules, improving readability
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode image to a PNG file
            generator.Save("custom_color_barcode.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("Barcode generated with custom colors: custom_color_barcode.png");
    }
}