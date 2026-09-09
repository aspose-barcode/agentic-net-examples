// Title: Switch AutoSizeMode Based on Image Dimensions
// Description: Demonstrates how to set the AutoSizeMode of Aspose.BarCode's BarcodeGenerator to Interpolation or Nearest depending on the relative width and height of the output image.
// Category-Description: This example belongs to the Aspose.BarCode image sizing and rendering category. It shows how to work with the BarcodeGenerator, EncodeTypes, and AutoSizeMode classes to control barcode image scaling. Typical use cases include generating barcodes that fit custom image dimensions while preserving readability. Developers often need to adjust AutoSizeMode to balance image quality and size for different display or printing scenarios.
// Prompt: Develop a function that switches AutoSizeMode between Interpolation and Nearest based on user‑selected image dimensions.
// Tags: barcode, autosizemode, imagesize, code128, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates switching AutoSizeMode between Interpolation and Nearest based on image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses optional width/height arguments, generates a Code128 barcode and saves it as PNG.
    /// </summary>
    /// <param name="args">Command‑line arguments: optional width and height.</param>
    static void Main(string[] args)
    {
        // Default dimensions; can be overridden via command‑line arguments.
        int imageWidth = 300;
        int imageHeight = 200;

        // If two numeric arguments are supplied, use them as width and height.
        if (args.Length >= 2 &&
            int.TryParse(args[0], out int w) &&
            int.TryParse(args[1], out int h))
        {
            imageWidth = w;
            imageHeight = h;
        }

        // Create a unique temporary folder for the output file.
        string outputFolder = Path.Combine(Path.GetTempPath(), "AutoSizeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "SampleText"))
        {
            // Adjust AutoSizeMode according to the chosen dimensions.
            SetAutoSizeModeBasedOnDimensions(generator, imageWidth, imageHeight);

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Configures image size and selects AutoSizeMode based on the relative width and height.
    /// </summary>
    /// <param name="generator">The BarcodeGenerator instance to configure.</param>
    /// <param name="width">Desired image width in pixels.</param>
    /// <param name="height">Desired image height in pixels.</param>
    static void SetAutoSizeModeBasedOnDimensions(BarcodeGenerator generator, int width, int height)
    {
        // Set explicit image dimensions.
        generator.Parameters.ImageWidth.Pixels = width;
        generator.Parameters.ImageHeight.Pixels = height;

        // Choose AutoSizeMode: Interpolation for landscape, Nearest for portrait or square.
        if (width > height)
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            Console.WriteLine("AutoSizeMode set to Interpolation (width > height).");
        }
        else
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            Console.WriteLine("AutoSizeMode set to Nearest (width <= height).");
        }

        // Set a modest XDimension to ensure the barcode is clearly visible.
        generator.Parameters.Barcode.XDimension.Pixels = 3;
    }
}