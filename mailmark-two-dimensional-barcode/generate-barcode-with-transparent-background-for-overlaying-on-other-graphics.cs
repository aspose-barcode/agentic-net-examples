// Title: Generate a Code128 barcode with a transparent background
// Description: Demonstrates how to create a Code128 barcode image with a transparent background, suitable for overlaying on other graphics.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include creating barcodes for UI overlays, reports, or composite images where background transparency is required. Developers often need to control colors and output formats when integrating barcodes into existing graphics.
// Prompt: Generate a barcode with a transparent background for overlaying on other graphics.
// Tags: code128, barcode, transparent background, png, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode with a transparent background using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a Code128 barcode image with a transparent background and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder to store the generated barcode image
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputFolder);

        // Full path for the output PNG file
        string outputPath = Path.Combine(outputFolder, "barcode_transparent.png");

        // Initialize the barcode generator for Code128 with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Configure the background to be fully transparent
            generator.Parameters.BackColor = Color.Transparent;

            // Set the barcode (foreground) color to black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a PNG file, which supports transparency
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}