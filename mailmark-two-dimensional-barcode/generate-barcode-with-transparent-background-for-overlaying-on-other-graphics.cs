// Title: Generate a Code128 barcode with transparent background
// Description: Demonstrates how to create a PNG barcode image with a transparent background, suitable for overlaying on other graphics.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers often need to produce barcodes that blend seamlessly into UI designs or composite images, requiring transparent backgrounds. The snippet shows directory handling, barcode configuration, and saving to PNG format.
// Prompt: Generate a barcode with a transparent background for overlaying on other graphics.
// Tags: code128, transparent background, png, barcode generation, aspose.barcode, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a transparent background and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, configures the barcode generator, and saves the image.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Determine a temporary directory for output and ensure it exists.
        string outputDirectory = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDirectory);

        // Build the full file path for the resulting PNG image.
        string outputPath = Path.Combine(outputDirectory, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the background color to transparent so the barcode can be overlaid on other graphics.
            generator.Parameters.BackColor = Color.Transparent;

            // Save the barcode image as PNG, preserving transparency.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}