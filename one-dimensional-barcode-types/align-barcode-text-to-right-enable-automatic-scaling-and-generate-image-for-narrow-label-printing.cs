// Title: Align barcode text to right and generate narrow label image
// Description: Demonstrates how to right‑align human‑readable text, enable automatic scaling, and create a PNG image sized for narrow label printing.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, AutoSizeMode, and TextAlignment to produce high‑resolution barcodes for label printers. Developers often need to customize barcode dimensions, scaling, and text alignment for various printing scenarios, such as narrow labels, receipts, or product tags. The snippet shows typical API calls for setting image size, resolution, colors, and saving the result.
// Prompt: Align barcode text to right, enable automatic scaling, and generate image for narrow label printing.
// Tags: code128, text-alignment, autoscaling, png, image-generation, aspnet, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates aligning barcode text to the right, enabling automatic scaling,
/// and generating a PNG image suitable for narrow label printing using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a Code128 barcode, configures scaling,
    /// size, resolution, text alignment, colors, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 symbology with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable automatic scaling using interpolation mode.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the target image dimensions for a narrow label (150pt width x 50pt height).
            generator.Parameters.ImageWidth.Point = 150f;
            generator.Parameters.ImageHeight.Point = 50f;

            // Increase resolution to 300 DPI for better print quality.
            generator.Parameters.Resolution = 300f;

            // Align the human‑readable text to the right side of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Optional: define bar and background colors (black on white).
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode image as a PNG file.
            generator.Save("narrow_label.png");
        }

        // Output a simple confirmation message.
        Console.WriteLine("Barcode image 'narrow_label.png' generated successfully.");
    }
}