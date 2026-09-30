// Title: Generate a Code128 barcode with a custom non‑square aspect ratio
// Description: Demonstrates how to create a Code128 barcode image where the width exceeds the height by using the Interpolation auto‑size mode.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to control canvas dimensions. Developers often need to produce barcodes with specific aspect ratios for label printing or UI display, and this snippet shows the typical API calls for setting image size, resolution, and saving to PNG.
// Prompt: Generate a barcode with a non‑square aspect ratio by setting ImageHeight lower than ImageWidth in Interpolation mode.
// Tags: code128, barcode generation, png, autosizemode, interpolation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom non‑square aspect ratio using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, configures the barcode generator, and saves the image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the output
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Generate a Code128 barcode with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Use Interpolation mode to allow custom canvas size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set a non‑square aspect ratio: width larger than height
            generator.Parameters.ImageWidth.Pixels = 400f;
            generator.Parameters.ImageHeight.Pixels = 100f;

            // Optional: set resolution for better quality
            generator.Parameters.Resolution = 300f;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}