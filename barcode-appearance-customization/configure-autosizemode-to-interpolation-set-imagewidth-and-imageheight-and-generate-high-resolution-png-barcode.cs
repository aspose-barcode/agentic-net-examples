// Title: Generate High‑Resolution PNG Barcode with Fixed Size Using AutoSizeMode.Interpolation
// Description: Demonstrates how to configure Aspose.BarCode to produce a high‑resolution PNG barcode image with a fixed width and height using the Interpolation auto‑size mode.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, its Parameters, and AutoSizeMode to control image dimensions and resolution. Typical use cases include creating printable barcodes for packaging, labels, or documents where precise size and quality are required. Developers often need to set resolution, image size, and rendering mode to meet printing standards.
// Prompt: Configure AutoSizeMode to Interpolation, set ImageWidth and ImageHeight, and generate a high‑resolution PNG barcode.
// Tags: code128, barcode generation, png, autosizemode, high resolution

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a high‑resolution PNG barcode image
/// with a fixed size using <c>AutoSizeMode.Interpolation</c>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Configures the barcode generator, sets resolution and image dimensions,
    /// and saves the result as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the full output path for the generated PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "HighResBarcode.png");

        // Ensure the target directory exists before attempting to save the file
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the rendering resolution to 300 DPI for high‑quality output
            generator.Parameters.Resolution = 300f;

            // Use Interpolation mode to scale the barcode while preserving quality
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Specify the exact image dimensions in pixels
            generator.Parameters.ImageWidth.Pixels = 1200f;
            generator.Parameters.ImageHeight.Pixels = 600f;

            // Optional: adjust the module (X) dimension for finer control over barcode size
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Save the generated barcode as a PNG image at the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}