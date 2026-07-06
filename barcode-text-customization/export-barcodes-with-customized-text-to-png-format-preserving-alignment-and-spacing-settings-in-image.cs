// Title: Export Barcode with Custom Text to PNG
// Description: Demonstrates how to generate a Code128 barcode with customized human‑readable text, alignment, spacing, and colors, then save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and related parameter classes to control barcode appearance. Typical use cases include creating branded labels, receipts, or product tags where precise text placement and visual styling are required. Developers often need to adjust font, alignment, padding, and colors before exporting the barcode to common image formats.
// Prompt: Export barcodes with customized text to PNG format, preserving alignment and spacing settings in the image.
// Tags: code128, barcode, export, png, custom-text, alignment, spacing, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with customized human‑readable text and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures barcode parameters, applies custom text styling,
    /// and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Output file path for the generated PNG image
        string outputPath = "custom_text_barcode.png";

        // Initialize a barcode generator for Code128 with the desired code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Disable automatic sizing to allow manual dimension control
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set explicit barcode dimensions
            generator.Parameters.Barcode.BarHeight.Point = 50f;   // Height of the bars
            generator.Parameters.Barcode.XDimension.Point = 2f; // Width of a single module

            // Configure human‑readable text appearance
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Center the text horizontally relative to the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Position the text above the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Define spacing between the text and the barcode (10 points)
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 10f;

            // Apply uniform padding around the entire barcode image (5 points on each side)
            generator.Parameters.Barcode.Padding.Left.Point   = 5f;
            generator.Parameters.Barcode.Padding.Top.Point    = 5f;
            generator.Parameters.Barcode.Padding.Right.Point  = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Optional: set foreground (bars) and background colors
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.White;

            // Save the configured barcode as a PNG file
            generator.Save(outputPath);
        }

        // Inform the user that the image has been created
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}