// Title: Generate non-square barcode using Interpolation mode
// Description: Demonstrates creating a Code128 barcode with a rectangular aspect ratio by setting ImageWidth larger than ImageHeight in Interpolation auto-size mode.
// Prompt: Generate a barcode with a non‑square aspect ratio by setting ImageHeight lower than ImageWidth in Interpolation mode.
// Tags: code128, barcode, interpolation, imagesize, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a non‑square aspect ratio
/// using the Interpolation auto‑size mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, configures size and colors, saves the image, and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Switch to Interpolation mode so ImageWidth and ImageHeight directly control the output size.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define a rectangular (non‑square) size: width larger than height.
            generator.Parameters.ImageWidth.Point = 300f;   // Width: 300 points
            generator.Parameters.ImageHeight.Point = 100f; // Height: 100 points

            // Optional visual styling: white background and black bars.
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the generated barcode as a PNG file.
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode image has been created.
        Console.WriteLine("Barcode generated: barcode.png");
    }
}