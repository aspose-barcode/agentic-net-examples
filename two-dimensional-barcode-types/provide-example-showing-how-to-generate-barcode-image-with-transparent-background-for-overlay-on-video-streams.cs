// Title: Generate QR barcode with transparent background
// Description: Demonstrates creating a QR code image with a transparent background using Aspose.BarCode, suitable for overlaying on video streams.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure barcode appearance such as background transparency and bar colors. It uses the BarcodeGenerator class with EncodeTypes and BarCodeImageFormat to produce PNG images. Developers often need to generate barcodes for UI overlays, reports, or video streams where the background must not obscure underlying content.
// Prompt: Provide example showing how to generate barcode image with transparent background for overlay on video streams.
// Tags: qr, barcode, generation, transparent background, png, aspose.barcode, barcodegenerator, parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR code image with a transparent background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR barcode, sets a transparent background,
    /// and saves the image as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated barcode image.
        string outputPath = Path.Combine(outputDir, "transparent_barcode.png");

        // Create a barcode generator for QR code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            // Set the background to transparent so it can be overlaid on other media.
            generator.Parameters.BackColor = Color.Transparent;

            // Set the barcode (foreground) color to black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a PNG image, which supports transparency.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}