// Title: Left-aligned Code128 barcode with automatic scaling for narrow columns
// Description: Generates a Code128 barcode with text aligned to the left, using automatic scaling to fit a narrow column layout, and saves it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to configure barcode text alignment, padding, and X-dimension for compact image output. It uses the BarcodeGenerator class and its Parameters property to adjust visual settings, a common requirement when embedding barcodes in tight UI spaces or printed columns.
// Prompt: Align barcode text to left, enable automatic scaling, and generate image suitable for narrow column layout.
// Tags: code128, text alignment, left, automatic scaling, narrow column, png, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a left‑aligned Code128 barcode with automatic scaling suitable for narrow column layouts.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it as PNG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the resulting image.
        string outputPath = Path.Combine(outputDir, "LeftAlignedBarcode.png");

        // Create a BarcodeGenerator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Align the human‑readable text to the left side of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Set a small X‑dimension to allow the barcode to fit into a narrow space.
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Apply uniform padding around the barcode to improve readability.
            generator.Parameters.Barcode.Padding.Left.Point = 1f;
            generator.Parameters.Barcode.Padding.Top.Point = 1f;
            generator.Parameters.Barcode.Padding.Right.Point = 1f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 1f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}