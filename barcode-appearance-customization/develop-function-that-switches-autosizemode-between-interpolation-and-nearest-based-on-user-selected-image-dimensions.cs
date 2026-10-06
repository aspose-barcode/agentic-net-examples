// Title: Switch AutoSizeMode based on image dimensions and generate a barcode
// Description: Demonstrates how to choose between Interpolation and Nearest AutoSizeMode depending on image width and height, then creates a Code128 barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, AutoSizeMode, and image dimension settings. Developers often need to adjust barcode scaling for different output sizes, selecting appropriate AutoSizeMode to maintain quality. The snippet shows typical setup, parameter configuration, and saving the barcode as PNG, useful for web, print, or inventory applications.
// Prompt: Develop a function that switches AutoSizeMode between Interpolation and Nearest based on user‑selected image dimensions.
// Tags: barcode, generation, autosizemode, interpolation, nearest, code128, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates selecting AutoSizeMode based on image dimensions and generating a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Determines the appropriate AutoSizeMode based on the provided width and height.
    /// Returns Interpolation when width > height; otherwise returns Nearest.
    /// </summary>
    /// <param name="width">Image width in pixels.</param>
    /// <param name="height">Image height in pixels.</param>
    /// <returns>Chosen AutoSizeMode value.</returns>
    static AutoSizeMode GetAutoSizeMode(int width, int height)
    {
        // Example rule: use Interpolation when width is greater than height, otherwise Nearest
        return width > height ? AutoSizeMode.Interpolation : AutoSizeMode.Nearest;
    }

    /// <summary>
    /// Entry point of the example. Generates a barcode with AutoSizeMode determined by image size.
    /// </summary>
    static void Main()
    {
        // Sample image dimensions (pixels)
        int imageWidth = 300;
        int imageHeight = 200;

        // Determine AutoSizeMode based on dimensions
        AutoSizeMode mode = GetAutoSizeMode(imageWidth, imageHeight);
        Console.WriteLine($"Selected AutoSizeMode: {mode}");

        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build output file path
        string outputPath = Path.Combine(outputDir, $"Barcode_{mode}.png");

        // Generate barcode with the selected AutoSizeMode and image size
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Parameters.AutoSizeMode = mode;
            generator.Parameters.ImageWidth.Pixels = (float)imageWidth;
            generator.Parameters.ImageHeight.Pixels = (float)imageHeight;
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}