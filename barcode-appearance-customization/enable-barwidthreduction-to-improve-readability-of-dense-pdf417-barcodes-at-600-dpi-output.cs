// Title: Enable BarWidthReduction for PDF417 barcode at 600 dpi
// Description: Demonstrates how to enable bar width reduction to improve readability of dense PDF417 barcodes when generating a 600 dpi PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure high‑resolution output, adjust module size, and apply bar‑width reduction for PDF417 symbology. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers commonly use to create printable or screen‑display barcodes with fine‑tuned visual quality.
// Prompt: Enable BarWidthReduction to improve readability of dense PDF417 barcodes at 600 dpi output.
// Tags: pdf417, barwidthreduction, resolution, png, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a dense PDF417 barcode, applies bar‑width reduction, and saves it as a high‑resolution PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures the barcode generator,
    /// and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Determine a temporary folder for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the folder if it does not already exist
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "Pdf417_BarWidthReduction.png");

        // Initialize the generator with PDF417 symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Dense PDF417 barcode example with bar width reduction"))
        {
            // Set the output resolution to 600 dpi for high‑quality rendering
            generator.Parameters.Resolution = 600f;

            // Adjust the module (X) dimension to make individual bars more visible
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Enable bar‑width reduction to improve readability of dense barcodes
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 4f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}