// Title: Generate a transparent PNG barcode using Code128
// Description: Demonstrates how to set a barcode's background to transparent and save it as a PNG with an alpha channel, suitable for overlay scenarios.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers often need to create barcodes with transparent backgrounds for UI overlays, reports, or composite images, and this snippet illustrates the typical steps to achieve that.
// Prompt: Set the background color to transparent and generate a PNG with alpha channel for overlay use.
// Tags: code128, background-color, png, barcodegenerator, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with a transparent background
/// and saves it as a PNG image supporting alpha channel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, saves it, and reports the result.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "transparent_barcode.png");

        // Create a BarcodeGenerator for Code128 symbology with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
        {
            // Set the barcode background to transparent
            generator.Parameters.BackColor = Color.Transparent;

            // Save the barcode as a PNG file (PNG supports alpha channel)
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created and inform the user
        if (File.Exists(outputPath))
        {
            Console.WriteLine($"Barcode image saved with transparent background: {outputPath}");
        }
        else
        {
            Console.WriteLine("Failed to save the barcode image.");
        }
    }
}