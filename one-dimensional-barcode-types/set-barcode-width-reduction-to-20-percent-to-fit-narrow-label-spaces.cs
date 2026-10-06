// Title: Barcode Width Reduction Example
// Description: Demonstrates how to reduce the bar width of a Code128 barcode by 20 percent using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as X‑dimension and bar‑width reduction. Developers creating labels, tickets, or packaging often need to adjust bar widths to fit narrow spaces while maintaining scanability. The key API classes used are BarcodeGenerator, EncodeTypes, and BarCodeImageFormat.
// Prompt: Set barcode width reduction to 20 percent to fit narrow label spaces.
// Tags: barcode, width reduction, code128, aspose.barcode, image generation, png, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a demonstration of setting a barcode's bar‑width reduction to fit narrow label spaces.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with a 20 percent bar‑width reduction and saves it as a PNG image.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeWidthReductionDemo");
        Directory.CreateDirectory(outputDir);

        // Compose the full file path for the resulting barcode image.
        string outputPath = Path.Combine(outputDir, "Code128_20PercentReduction.png");

        // Create a barcode generator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Set the base module (X‑dimension) size to 10 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Reduce the bar width by 20 percent (2 pixels of the 10‑pixel module).
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 2f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}