// Title: Generate Code39 barcodes with red and blue bars
// Description: Demonstrates how to create a Code39 barcode using Aspose.BarCode, first with red bars then with blue bars, and save each as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to customize barcode appearance. Developers often need to change visual properties such as bar color for branding or UI requirements, and this snippet shows the typical workflow for generating and saving barcodes with different colors.
// Prompt: Use the same BarcodeGenerator to produce a red‑bar Code39 barcode, then change to blue‑bar and save again.
// Tags: barcode symbology, code39, color customization, image generation, aspnet, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code39 barcodes with different bar colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a red-bar Code39 barcode, then changes to blue bars and saves both images.
    /// </summary>
    static void Main()
    {
        // Determine the directory where the executable is running.
        string outputDir = Directory.GetCurrentDirectory();

        // Build full file paths for the red and blue barcode images.
        string redPath = Path.Combine(outputDir, "Code39_Red.png");
        string bluePath = Path.Combine(outputDir, "Code39_Blue.png");

        // Create a BarcodeGenerator for Code39 with the data "12345".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            // Set bar color to red and save the image.
            generator.Parameters.Barcode.BarColor = Color.Red;
            generator.Save(redPath, BarCodeImageFormat.Png);

            // Change bar color to blue and save the second image.
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Save(bluePath, BarCodeImageFormat.Png);
        }

        // Output the locations of the saved files.
        Console.WriteLine($"Saved red barcode to: {redPath}");
        Console.WriteLine($"Saved blue barcode to: {bluePath}");
    }
}