// Title: Generate Barcode Image with Transparent Background
// Description: Demonstrates creating a Code128 barcode saved as a PNG with a transparent background, suitable for overlaying on video streams.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class along with EncodeTypes and BarCodeImageFormat to produce barcode images. Typical use cases include creating barcodes for UI overlays, video streams, or any scenario where a non‑opaque background is required. Developers often need to control colors, image formats, and output paths when integrating barcodes into multimedia applications.
// Prompt: Provide example showing how to generate barcode image with transparent background for overlay on video streams.
// Tags: barcode, code128, generation, png, transparent background, aspose.barcode, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with a transparent background and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the barcode image and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the temporary file path where the barcode image will be saved.
        string outputPath = Path.Combine(Path.GetTempPath(), "transparent_barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the background to transparent and the barcode bars to black.
            generator.Parameters.BackColor = Color.Transparent;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG file, preserving transparency.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}