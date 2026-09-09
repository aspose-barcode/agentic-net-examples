// Title: Generate a high‑resolution Code128 barcode image for label printing
// Description: Demonstrates configuring Aspose.BarCode generator settings to create a 300 DPI PNG barcode suitable for high‑resolution label printing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to adjust resolution, module size, padding, border, and other rendering parameters using the BarcodeGenerator class. Typical use cases include producing print‑ready barcodes for product labels, shipping tags, and inventory stickers where crisp, high‑density output is required. Developers often need to fine‑tune these settings to meet printer specifications and visual design guidelines.
// Prompt: Adjust generator settings to produce a barcode image suitable for high‑resolution printing on labels.
// Tags: code128, high resolution, barcode generation, png output, aspose.barcode, border, padding, xdimension

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates high‑resolution barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with custom resolution, dimensions, padding, and border, then saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine output directory (current working directory)
        string outputDir = Directory.GetCurrentDirectory();
        // Build full path for the output PNG file
        string outputPath = Path.Combine(outputDir, "HighResBarcode.png");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize barcode generator with Code128 symbology and data "ASPOSE"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Set image resolution to 300 DPI for high‑quality printing
            generator.Parameters.Resolution = 300f;

            // Define module (X‑dimension) size: 0.5 mm per barcode unit
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Reduce bar width slightly to improve readability at high DPI
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.1f;

            // Apply 2 mm padding on all sides of the barcode
            generator.Parameters.Barcode.Padding.Left.Millimeters = 2f;
            generator.Parameters.Barcode.Padding.Top.Millimeters = 2f;
            generator.Parameters.Barcode.Padding.Right.Millimeters = 2f;
            generator.Parameters.Barcode.Padding.Bottom.Millimeters = 2f;

            // Enable a visible solid black border of 2 px thickness
            generator.Parameters.Border.Visible = true;
            generator.Parameters.Border.Width.Pixels = 2f;
            generator.Parameters.Border.DashStyle = BorderDashStyle.Solid;
            generator.Parameters.Border.Color = Color.Black;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"High‑resolution barcode saved to: {outputPath}");
    }
}