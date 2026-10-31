// Title: Left-aligned Code128 barcode with automatic scaling for narrow columns
// Description: Demonstrates how to generate a Code128 barcode, align its human‑readable text to the left, and enable automatic scaling for a compact image suitable for narrow column layouts.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to control text alignment and module width. Typical use cases include creating barcodes for reports, invoices, or mobile layouts where space is limited. Developers often need to adjust X‑dimension and text alignment while relying on the library’s automatic sizing to produce optimal images.
// Prompt: Align barcode text to left, enable automatic scaling, and generate image suitable for narrow column layout.
// Tags: code128, text-alignment, automatic-scaling, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program that creates a left‑aligned Code128 barcode with automatic scaling and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working folder
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "LeftAlignedBarcode.png");
        // Text to encode in the barcode
        string codeText = "Sample123";

        // Initialize the barcode generator with Code128 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set a narrow module width (X‑dimension) suitable for narrow columns
            generator.Parameters.Barcode.XDimension.Pixels = 1f;

            // Align the human‑readable text to the left side of the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // No explicit size settings – rely on default auto‑sizing (automatic scaling)
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}