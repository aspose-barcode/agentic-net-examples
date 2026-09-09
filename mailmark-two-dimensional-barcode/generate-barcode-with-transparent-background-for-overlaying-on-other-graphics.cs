// Title: Generate Code128 barcode with transparent background
// Description: Creates a Code128 barcode image with a transparent background and saves it as PNG, suitable for overlaying on other graphics.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, demonstrating how to configure barcode appearance such as background transparency using the BarcodeGenerator class. Developers often need to produce barcode images that can be composited onto existing designs, requiring formats like PNG with alpha channel support.
// Prompt: Generate a barcode with a transparent background for overlaying on other graphics.
// Tags: code128, generate, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode with a transparent background using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, sets a transparent background,
    /// saves it as a PNG file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting PNG file.
        string outputPath = Path.Combine(outputDir, "transparent_barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the background to transparent and the barcode bars to black.
            generator.Parameters.BackColor = Color.Transparent;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode image as a PNG file (supports transparency).
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}