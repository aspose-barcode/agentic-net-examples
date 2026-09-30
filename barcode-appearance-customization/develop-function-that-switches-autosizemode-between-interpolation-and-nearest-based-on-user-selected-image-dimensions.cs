// Title: Switch AutoSizeMode based on image dimensions for barcode generation
// Description: Demonstrates how to select the appropriate AutoSizeMode (Interpolation or Nearest) when generating barcode images with varying canvas sizes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to control image scaling. Developers often need to adjust barcode rendering quality for different output sizes, balancing clarity and performance. The snippet illustrates typical use cases such as creating high‑resolution barcodes for printing and low‑resolution barcodes for web display.
// Prompt: Develop a function that switches AutoSizeMode between Interpolation and Nearest based on user‑selected image dimensions.
// Tags: code128, autosizemode, png, generation, aspose.barcode, image, dimensions

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates switching AutoSizeMode based on image dimensions when generating barcodes.
/// </summary>
class Program
{
    // Determines which AutoSizeMode to use based on image dimensions.
    static AutoSizeMode DetermineAutoSizeMode(float widthPixels, float heightPixels)
    {
        // Use Interpolation for larger canvases, Nearest for smaller ones.
        return (widthPixels > 500f || heightPixels > 500f) ? AutoSizeMode.Interpolation : AutoSizeMode.Nearest;
    }

    // Generates a barcode image with the specified dimensions and auto‑size mode.
    static void GenerateBarcode(string outputPath, string codeText, float widthPixels, float heightPixels)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set target canvas size.
            generator.Parameters.ImageWidth.Pixels = widthPixels;
            generator.Parameters.ImageHeight.Pixels = heightPixels;

            // Choose AutoSizeMode based on dimensions.
            generator.Parameters.AutoSizeMode = DetermineAutoSizeMode(widthPixels, heightPixels);

            // Save as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Entry point that creates sample barcodes with different dimensions to show AutoSizeMode selection.
    /// </summary>
    static void Main()
    {
        // Prepare output directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Example 1: larger dimensions → Interpolation mode.
        string path1 = Path.Combine(outputDir, "barcode_large.png");
        GenerateBarcode(path1, "Large123", 800f, 200f);
        Console.WriteLine($"Generated barcode with Interpolation mode: {path1}");

        // Example 2: smaller dimensions → Nearest mode.
        string path2 = Path.Combine(outputDir, "barcode_small.png");
        GenerateBarcode(path2, "Small456", 300f, 100f);
        Console.WriteLine($"Generated barcode with Nearest mode: {path2}");
    }
}