// Title: Generate a Code128 barcode with transparent background
// Description: Demonstrates creating a PNG barcode image with an alpha‑transparent background, suitable for overlaying on video streams.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure barcode appearance, background transparency, and image size using BarcodeGenerator, EncodeTypes, and BarCodeImageFormat. Developers often need to produce barcodes that blend seamlessly into UI or video overlays, requiring PNG output with alpha channel support.
// Prompt: Provide example showing how to generate barcode image with transparent background for overlay on video streams.
// Tags: barcode, code128, transparent background, png, image generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode image with a fully transparent background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode and saves it as a PNG file with transparency.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "transparent_barcode.png");

        // Initialize the barcode generator for Code128 symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: set the color of the barcode bars.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the background to fully transparent.
            generator.Parameters.BackColor = Color.Transparent;

            // Configure size using interpolation mode (width and height in points).
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 100f;

            // Save the generated barcode as a PNG file, which supports an alpha channel.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}