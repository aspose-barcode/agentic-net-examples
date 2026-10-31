// Title: Barcode generation with automatic bar height fallback
// Description: Demonstrates how to generate barcodes using Aspose.BarCode where the BarHeight property falls back to automatic sizing when set to zero.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as XDimension and BarHeight. Typical scenarios include creating PNG barcode images with either fixed or automatic bar heights, a common requirement for dynamic image generation in web and desktop applications.
// Prompt: Implement fallback logic to use automatic bar height when BarHeight property is set to zero.
// Tags: code128, barheight, automatic-height, png, barcodegenerator, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with optional automatic bar height using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcode images: one with automatic height and one with a fixed height.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Example 1: BarHeight set to zero (fallback to automatic height)
        GenerateBarcode("AUTOHEIGHT", 0f, Path.Combine(outputDir, "AutoHeight.png"));

        // Example 2: BarHeight set to a specific value (fixed height)
        GenerateBarcode("FIXEDHEIGHT", 80f, Path.Combine(outputDir, "FixedHeight.png"));

        // Inform the user where the images were saved
        Console.WriteLine("Barcodes generated in: " + outputDir);
    }

    /// <summary>
    /// Generates a barcode image using the specified text, bar height, and output path.
    /// If <paramref name="barHeight"/> is zero, the generator uses its automatic height calculation.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="barHeight">Desired bar height in pixels; zero triggers automatic height.</param>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    static void GenerateBarcode(string codeText, float barHeight, string outputPath)
    {
        // Use Code128 as the symbology for this example
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set a reasonable XDimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Apply BarHeight only if a positive value is provided; otherwise, let the generator decide
            if (barHeight > 0f)
            {
                generator.Parameters.Barcode.BarHeight.Pixels = barHeight;
            }

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}