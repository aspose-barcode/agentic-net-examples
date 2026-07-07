// Title: High‑Resolution Barcode Generation for Label Printing
// Description: Demonstrates configuring Aspose.BarCode generator to create a high‑resolution PNG suitable for printing labels.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to set resolution, image size, module dimensions, colors, and text options using BarcodeGenerator and related parameter classes. Developers often need to produce crisp barcodes for packaging, shipping labels, or product tags, and this snippet shows the typical API usage for such scenarios.
// Prompt: Adjust generator settings to produce a barcode image suitable for high‑resolution printing on labels.
// Tags: code128, highresolution, png, barcode generation, aspnet, aspose.barcode, image parameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a high‑resolution Code128 barcode image suitable for label printing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Configures barcode generator settings and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "HIGHRES12345"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "HIGHRES12345"))
        {
            // Set the image resolution to 300 DPI, which is appropriate for high‑quality label printing
            generator.Parameters.Resolution = 300;

            // Use interpolation mode to allow explicit pixel dimensions for the output image
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define the exact image size in pixels (width x height) for the label
            generator.Parameters.ImageWidth.Pixels = 1200f;   // label width
            generator.Parameters.ImageHeight.Pixels = 600f;   // label height

            // Configure module (X‑dimension) size and bar height (height is ignored in interpolation mode but set for completeness)
            generator.Parameters.Barcode.XDimension.Pixels = 2f;   // each module ~2 pixels wide
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;  // bar height for 1D barcodes

            // Set high‑contrast colors: black bars on a white background
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Customize human‑readable text appearance
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Add uniform padding of 5 points on all sides of the barcode
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Save the generated barcode as a PNG file
            string outputPath = "highres_label.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}