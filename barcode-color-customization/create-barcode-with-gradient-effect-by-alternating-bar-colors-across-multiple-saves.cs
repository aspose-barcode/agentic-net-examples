// Title: Create Gradient Barcode with Multiple Saves
// Description: Generates a series of Code128 barcode images where the bar color transitions from red to blue, demonstrating a gradient effect across multiple files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes, set barcode parameters, and apply custom colors. Typical use cases include creating visually distinct barcodes for branding or UI themes. Developers often need to adjust bar colors, dimensions, and export formats, which this snippet illustrates.
/// Prompt: Create a barcode with a gradient effect by alternating bar colors across multiple saves.
/// Tags: barcode, gradient, code128, image, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a series of barcode images with a color gradient effect.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates barcode images with interpolated colors and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Define output directory for generated images
        string outputDir = "output";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Number of gradient steps (images) to create
        int steps = 5;

        // Loop through each step to calculate color and generate barcode
        for (int i = 0; i < steps; i++)
        {
            // Linear interpolation of RGB components from red (255,0,0) to blue (0,0,255)
            int r = (int)(255 - (255.0 * i / (steps - 1)));
            int g = 0;
            int b = (int)(255.0 * i / (steps - 1));
            Aspose.Drawing.Color barColor = Aspose.Drawing.Color.FromArgb(r, g, b);

            // Initialize barcode generator for Code128 symbology with the text "Gradient"
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Gradient"))
            {
                // Apply the interpolated bar color for this step
                generator.Parameters.Barcode.BarColor = barColor;

                // Configure optional sizing parameters
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 50f;

                // Build file path and save the barcode image as PNG
                string filePath = Path.Combine(outputDir, $"barcode_step_{i + 1}.png");
                generator.Save(filePath);
                Console.WriteLine($"Saved: {filePath}");
            }
        }
    }
}