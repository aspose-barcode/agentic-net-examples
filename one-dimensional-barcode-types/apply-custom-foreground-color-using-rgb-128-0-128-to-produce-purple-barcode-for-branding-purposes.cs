// Title: Generate a purple Code128 barcode using Aspose.BarCode
// Description: Demonstrates how to create a Code128 barcode with a custom purple foreground color and save it as a PNG file. Useful for branding where specific colors are required.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class. It covers setting foreground and background colors, selecting symbology, and exporting to image formats. Developers often need to match corporate branding or design guidelines when embedding barcodes in documents or web pages.
// Prompt: Apply a custom foreground color using RGB (128,0,128) to produce a purple barcode for branding purposes.
// Tags: code128, barcode generation, color customization, png output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a purple Code128 barcode and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures barcode generator,
    /// applies custom colors, saves the image, and writes the result path to console.
    /// </summary>
    static void Main()
    {
        // Determine the output folder relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "purple_barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the barcode (foreground) color to purple (RGB 128,0,128)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(128, 0, 128);

            // Set the background color to white for contrast
            generator.Parameters.BackColor = Color.White;

            // Save the barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}