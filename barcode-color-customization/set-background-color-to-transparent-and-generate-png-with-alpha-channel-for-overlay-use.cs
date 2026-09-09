// Title: Generate Transparent PNG Barcode with Alpha Channel
// Description: Demonstrates how to set a barcode's background to transparent and save it as a PNG image that retains the alpha channel, suitable for overlay scenarios.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to customize visual properties such as background transparency. Developers often need to create barcodes with transparent backgrounds for UI overlays, reports, or composite images, and this snippet shows the typical steps to achieve that.
// Prompt: Set the background color to transparent and generate a PNG with alpha channel for overlay use.
// Tags: barcode, transparent background, png, alpha channel, code128, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with a transparent background
/// and saves it as a PNG image preserving the alpha channel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, configures visual settings, and writes the file to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTransparent");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "transparent_barcode.png");

        // Create a barcode generator for Code128 with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the background to fully transparent
            generator.Parameters.BackColor = Color.Transparent;

            // Set the barcode bars to black (or any desired color)
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a PNG image; PNG preserves the alpha channel
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}