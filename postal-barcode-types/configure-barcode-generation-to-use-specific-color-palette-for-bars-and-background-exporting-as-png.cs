// Title: Generate a colored Code128 barcode and save as PNG
// Description: Demonstrates how to set custom bar and background colors for a barcode using Aspose.BarCode and export it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize the visual appearance of barcodes. It uses the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create a barcode, apply specific colors, and save the result. Developers often need to match corporate branding or improve scan reliability by adjusting bar and background colors, making this pattern common in reporting, packaging, and inventory systems.
// Prompt: Configure barcode generation to use a specific color palette for bars and background, exporting as PNG.
// Tags: barcode, generation, code128, color, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with custom colors and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, applies colors, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare the output directory and file path
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
        string outputPath = Path.Combine(outputDir, "ColoredBarcode.png");

        // --------------------------------------------------------------------
        // Create a barcode generator for Code128 with the sample text "Sample"
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
        {
            // Set the bar (foreground) color to blue
            generator.Parameters.Barcode.BarColor = Color.FromArgb(0, 0, 255);
            // Set the background color to a light gray
            generator.Parameters.BackColor = Color.FromArgb(200, 200, 200);

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}