// Title: PDF417 Barcode Generation with Bar Width Reduction at 600 DPI
// Description: Demonstrates generating a dense PDF417 barcode, applying bar‑width reduction to improve readability, and saving it as a PNG image.
// Category-Description: Shows how to use Aspose.BarCode to configure PDF417 barcode parameters such as resolution, X‑dimension, and BarWidthReduction. This example belongs to the barcode generation category, illustrating typical use cases like creating high‑resolution barcodes for print media where dense data requires visual enhancement. Developers often need to adjust these settings to meet scanning reliability requirements.
// Prompt: Enable BarWidthReduction to improve readability of dense PDF417 barcodes at 600 dpi output.
// Tags: pdf417, barwidthreduction, resolution, barcode generation, png, aspose.barcode, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a dense PDF417 barcode with bar‑width reduction and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory to store the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "Pdf417BarWidthReductionDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the output PNG file.
        string outputPath = Path.Combine(outputDir, "Pdf417_BWR.png");

        // Create a BarcodeGenerator for PDF417 with a long text to produce a dense barcode.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Dense PDF417 barcode text that is quite long to make it dense"))
        {
            // Set the image resolution to 600 DPI for high‑quality output.
            generator.Parameters.Resolution = 600f;

            // Define the X‑dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Apply bar‑width reduction to improve readability of dense bars.
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 4f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}