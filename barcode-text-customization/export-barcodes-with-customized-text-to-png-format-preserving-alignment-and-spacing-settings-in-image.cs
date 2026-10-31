// Title: Export PDF417 Barcodes with Custom Text Alignment to PNG
// Description: Demonstrates how to generate a PDF417 barcode with customized code text, adjust its alignment and spacing, and save the results as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to control text appearance. Typical use cases include creating barcodes with readable human‑readable text positioned below the symbol, adjusting alignment (center, left, right) and spacing for layout requirements. Developers often need to export such barcodes to image formats like PNG for web or print integration.
// Prompt: Export barcodes with customized text to PNG format, preserving alignment and spacing settings in the image.
// Tags: pdf417, barcode, text alignment, spacing, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates PDF417 barcodes with customized code‑text settings and saves them as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures barcode parameters,
    /// generates images with different text alignments and spacing, and writes the output path to console.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for the generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeExportDemo");
        Directory.CreateDirectory(outputDir);
        string basePath = Path.Combine(outputDir, "barcode");

        // Text that will appear below the barcode
        string codeText = "Custom Text Example";

        // Initialize the barcode generator for PDF417 symbology with the custom text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
        {
            // Set common barcode properties (rows and module size)
            generator.Parameters.Barcode.Pdf417.Rows = 12;
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Configure manual font for the code text
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Position the code text below the barcode symbol
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // ----- Center alignment -----
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Save($"{basePath}_Center.png", BarCodeImageFormat.Png);

            // ----- Left alignment -----
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;
            generator.Save($"{basePath}_Left.png", BarCodeImageFormat.Png);

            // ----- Right alignment -----
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;
            generator.Save($"{basePath}_Right.png", BarCodeImageFormat.Png);

            // ----- Increased spacing between barcode and text (40 pixels) -----
            generator.Parameters.Barcode.CodeTextParameters.Space.Pixels = 40f;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Save($"{basePath}_Spacing40.png", BarCodeImageFormat.Png);
        }

        // Inform the user where the images have been saved
        Console.WriteLine($"Barcodes saved to: {outputDir}");
    }
}