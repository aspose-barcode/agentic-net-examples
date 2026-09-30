// Title: Generate a Code128 barcode series with a color gradient
// Description: Demonstrates creating multiple barcode images where the bar color transitions from red to blue, illustrating how to apply dynamic color settings and save each step as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and color manipulation to produce visual effects. Developers often need to customize barcode appearance for branding or UI integration, and this snippet provides a clear pattern for applying per‑image styling while iterating over a set of generated barcodes.
// Prompt: Create a barcode with a gradient effect by alternating bar colors across multiple saves.
// Tags: code128, gradient, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a series of Code128 barcode images with a smooth red‑to‑blue gradient applied to the bars.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the gradient barcode images and writes the output folder path to the console.
    /// </summary>
    static void Main()
    {
        // Define the text to encode and the folder where images will be saved.
        string codeText = "GRADIENT123";
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeGradient_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Configure gradient: 5 incremental steps from red to blue.
        int steps = 5;
        for (int i = 0; i < steps; i++)
        {
            // Compute interpolation factor (t) ranging from 0 (red) to 1 (blue).
            float t = steps == 1 ? 0f : (float)i / (steps - 1);

            // Linearly interpolate the red and blue channel values.
            int r = (int)(255 * (1 - t));
            int g = 0;
            int b = (int)(255 * t);

            // Initialize the barcode generator for Code128 with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the interpolated color to the barcode bars.
                generator.Parameters.Barcode.BarColor = Color.FromArgb(255, r, g, b);

                // Set a white background for better contrast.
                generator.Parameters.BackColor = Color.White;

                // Build the file name for the current gradient step.
                string filePath = Path.Combine(outputFolder, $"barcode_step_{i + 1}.png");

                // Save the barcode image as a PNG file.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Output the location of the generated images.
        Console.WriteLine($"Generated {steps} barcode images with gradient effect in:");
        Console.WriteLine(outputFolder);
    }
}