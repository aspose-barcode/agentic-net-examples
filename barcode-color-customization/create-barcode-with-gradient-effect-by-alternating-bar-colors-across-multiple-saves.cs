// Title: Generate a Code128 barcode with a color gradient across multiple images
// Description: Demonstrates how to create a barcode and apply a gradual color change from red to blue, saving each step as a separate PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. It illustrates typical scenarios where developers need to produce barcodes with custom visual styles, such as gradient effects, for branding or visual differentiation. Useful for applications that require multiple barcode images with varying colors.
// Prompt: Create a barcode with a gradient effect by alternating bar colors across multiple saves.
// Tags: code128, barcode generation, png, gradient, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a gradient color effect and saving each step as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output directory, configures gradient colors, generates barcode images with varying bar colors, and saves them.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory for the generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeGradient_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define gradient parameters: number of steps and start/end RGB colors (red to blue)
        int steps = 5;
        int startR = 255, startG = 0, startB = 0;   // Red
        int endR = 0, endG = 0, endB = 255;         // Blue

        // Initialize the barcode generator for Code128 with the text "GRADIENT"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "GRADIENT"))
        {
            // Optional: set a white background for better contrast
            generator.Parameters.BackColor = Color.White;

            // Loop through each gradient step, calculate interpolated color, and save the barcode image
            for (int i = 0; i < steps; i++)
            {
                // Compute interpolation factor (0.0 to 1.0) based on current step
                float t = (float)i / (steps - 1);

                // Linear interpolation of each RGB component
                int r = (int)(startR + (endR - startR) * t);
                int g = (int)(startG + (endG - startG) * t);
                int b = (int)(startB + (endB - startB) * t);

                // Apply the interpolated color to the barcode bars
                generator.Parameters.Barcode.BarColor = Color.FromArgb(r, g, b);

                // Build the file path for the current step and save as PNG
                string filePath = Path.Combine(outputDir, $"BarcodeStep_{i + 1}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Inform the user about the saved file and its color
                Console.WriteLine($"Saved barcode with color RGB({r},{g},{b}) to: {filePath}");
            }
        }

        // Indicate that all gradient images have been generated
        Console.WriteLine("Gradient barcode generation completed.");
    }
}