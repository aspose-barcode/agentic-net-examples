// Title: Center-aligned Code128 barcode with auto scaling for receipt printing
// Description: Generates a Code128 barcode with centered human‑readable text, automatic scaling, and a narrow image suitable for receipt printers.
// Category-Description: This example demonstrates Aspose.BarCode generation features such as setting image dimensions, resolution, text alignment, and auto‑scaling. It uses the BarcodeGenerator, EncodeTypes, and related parameter classes to create barcodes for point‑of‑sale scenarios where narrow, high‑resolution images are required. Developers often need to customize size, DPI, and text appearance for receipt or label printing.
// Prompt: Align barcode text to center, enable automatic scaling, and generate image suitable for narrow receipt printing.
// Tags: code128, barcode, auto-scaling, receipt, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a narrow, center‑aligned Code128 barcode with automatic scaling,
/// suitable for receipt printers.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, configures scaling and alignment,
    /// and saves the result as a PNG image.
    /// </summary>
    static void Main()
    {
        // Text to be encoded in the barcode.
        const string codeText = "1234567890";

        // Initialize the barcode generator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable automatic scaling (interpolation) to fit the target image size.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define a narrow receipt width and modest height (points).
            generator.Parameters.ImageWidth.Point = 200f;   // Width in points.
            generator.Parameters.ImageHeight.Point = 50f;   // Height in points.

            // Set the printer resolution typical for receipt printers (203 DPI).
            generator.Parameters.Resolution = 203f;

            // Center‑align the human‑readable text beneath the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Ensure the text font scales automatically with the barcode size.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;

            // Set barcode bar color to black and background to white.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode image to a PNG file.
            const string outputPath = "receipt_barcode.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}