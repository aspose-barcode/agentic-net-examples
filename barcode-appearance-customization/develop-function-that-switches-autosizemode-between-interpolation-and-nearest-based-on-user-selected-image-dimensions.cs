// Title: AutoSizeMode selection based on image dimensions
// Description: Demonstrates switching Aspose.BarCode AutoSizeMode between Interpolation and Nearest depending on requested barcode image size.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to control image scaling using the AutoSizeMode property of BarcodeGenerator. It shows typical use cases such as adjusting rendering quality for large versus small barcodes, using classes like BarcodeGenerator, EncodeTypes, and BarCodeImageFormat. Developers often need to balance performance and visual fidelity when generating barcodes at various dimensions.
// Prompt: Develop a function that switches AutoSizeMode between Interpolation and Nearest based on user‑selected image dimensions.
// Tags: code128, autosizemode, png, barcodegenerator, aspose.barcode, image-scaling

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates dynamic selection of AutoSizeMode for barcode generation based on image dimensions.
/// </summary>
class Program
{
    // Determines which AutoSizeMode to use based on the requested image dimensions.
    // For this example, larger images use Interpolation, smaller ones use Nearest.
    static AutoSizeMode DetermineAutoSizeMode(float width, float height)
    {
        // Thresholds can be adjusted as needed.
        if (width > 300f || height > 150f)
        {
            return AutoSizeMode.Interpolation;
        }
        else
        {
            return AutoSizeMode.Nearest;
        }
    }

    // Generates a barcode image with the specified dimensions and saves it to the given path.
    static void GenerateBarcode(string outputPath, float width, float height)
    {
        // Ensure the output directory exists.
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Choose AutoSizeMode based on dimensions.
            generator.Parameters.AutoSizeMode = DetermineAutoSizeMode(width, height);

            // Set the target image size. These unit members must be used.
            generator.Parameters.ImageWidth.Point = width;
            generator.Parameters.ImageHeight.Point = height;

            // Optional: set a background and bar color for visibility.
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the barcode as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Entry point that generates sample barcodes with different sizes to showcase AutoSizeMode switching.
    /// </summary>
    static void Main()
    {
        // Example 1: Larger dimensions -> Interpolation mode.
        GenerateBarcode("barcode_large.png", 400f, 200f);

        // Example 2: Smaller dimensions -> Nearest mode.
        GenerateBarcode("barcode_small.png", 200f, 100f);

        Console.WriteLine("Barcodes generated successfully.");
    }
}