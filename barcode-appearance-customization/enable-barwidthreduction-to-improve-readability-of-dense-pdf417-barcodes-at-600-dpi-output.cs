// Title: PDF417 Barcode with Bar Width Reduction at 600 dpi
// Description: Demonstrates generating a dense PDF417 barcode, applying bar‑width reduction to improve readability, and saving the image at 600 dpi.
// Category-Description: Shows how to use Aspose.BarCode to configure PDF417 barcodes, adjust resolution, enable bar‑width reduction, and set X‑dimension. This example belongs to the barcode generation category, focusing on PDF417 customization for high‑resolution outputs, a common need for developers creating compact, machine‑readable labels.
// Prompt: Enable BarWidthReduction to improve readability of dense PDF417 barcodes at 600 dpi output.
// Tags: pdf417, barwidthreduction, png, barcodegenerator, aspose.barcode, resolution

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a dense PDF417 barcode, applies bar‑width reduction for better readability,
/// and saves the result as a 600 dpi PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures the barcode generator,
    /// and writes the barcode image to disk.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "Pdf417BarWidthReductionDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "Pdf417_BWR_600dpi.png");

        // Sample dense data to generate a dense PDF417 barcode (200 characters of 'A')
        string codeText = new string('A', 200);

        // Initialize the barcode generator for PDF417 with the sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
        {
            // Set the output resolution to 600 dots per inch
            generator.Parameters.Resolution = 600f;

            // Enable bar width reduction (reduce each bar by 4 pixels)
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 4;

            // Optional: adjust XDimension to 2 pixels for tighter bar spacing
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"PDF417 barcode saved to: {outputPath}");
    }
}