// Title: Generate Code128 barcode with transparent background and save as PNG
// Description: Demonstrates creating a Code128 barcode with a transparent background and exporting it to a PNG file while preserving the alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode generation and image export category. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcodes, customize visual properties such as background and bar colors, and save the result in a format that supports transparency. Developers often need to embed barcodes in UI designs or documents where a non‑opaque background is required.
// Prompt: Produce a barcode with transparent background and export it as PNG preserving the alpha channel.
// Tags: code128, barcode generation, png, transparent background, aspose.barcode, image export, alpha channel

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
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated PNG file.
        string outputPath = Path.Combine(outputDir, "transparent_barcode.png");

        // Create a barcode generator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the background to transparent.
            generator.Parameters.BackColor = Color.Transparent;

            // Set the barcode (bars) color to black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a PNG image, which retains the alpha channel.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}