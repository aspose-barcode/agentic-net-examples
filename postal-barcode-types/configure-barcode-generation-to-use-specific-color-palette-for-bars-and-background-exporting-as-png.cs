// Title: Generate a colored Code128 barcode and save as PNG
// Description: Demonstrates how to set custom bar and background colors for a barcode using Aspose.BarCode and export it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to customize the visual appearance of barcodes. It uses the BarcodeGenerator class with EncodeTypes to define the symbology, and BarCodeImageFormat for output. Developers often need to adjust colors to match branding or UI themes, and then save the result in common image formats such as PNG.
// Prompt: Configure barcode generation to use a specific color palette for bars and background, exporting as PNG.
// Tags: code128, barcode generation, color customization, png output, aspose.barcode, aspose.drawing

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
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "ColoredBarcode.png");

        // Initialize the barcode generator with Code128 symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the foreground (bar) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to light gray
            generator.Parameters.BackColor = Color.LightGray;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}