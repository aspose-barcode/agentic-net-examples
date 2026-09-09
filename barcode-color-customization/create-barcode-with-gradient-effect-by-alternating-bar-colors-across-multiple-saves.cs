// Title: Generate a series of barcode images with a color gradient
// Description: Demonstrates how to create a Code128 barcode and vary the bar color from red to blue across multiple saved PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcodes with custom visual styles. Developers often need to customize barcode appearance for branding or visual effects, such as applying color gradients, and this snippet shows the typical workflow for setting colors and saving multiple images.
// Prompt: Create a barcode with a gradient effect by alternating bar colors across multiple saves.
// Tags: barcode, code128, gradient, color, png, aspose.barcode, generation, visual customization

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a series of Code128 barcode images with a red‑to‑blue gradient applied to the bars.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates an output folder, configures the barcode generator, iterates through gradient steps,
    /// sets the bar color for each step, saves the image, and writes progress to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated images
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeGradient_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Text to encode and number of gradient steps (images) to generate
        string codeText = "GRADIENT";
        int imageCount = 5;

        // Initialize the barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Optional: set the background color to white for better contrast
            generator.Parameters.BackColor = Color.White;

            // Loop through each gradient step, adjusting the bar color each time
            for (int i = 0; i < imageCount; i++)
            {
                // Calculate a color that transitions from red (i=0) to blue (i=imageCount-1)
                int r = 255 - (int)(255.0 * i / (imageCount - 1));
                int b = (int)(255.0 * i / (imageCount - 1));
                generator.Parameters.Barcode.BarColor = Color.FromArgb(255, r, 0, b);

                // Build the file path for the current image
                string filePath = Path.Combine(outputFolder, $"Barcode_{i + 1}.png");

                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Output the location of the saved image
                Console.WriteLine($"Saved barcode image: {filePath}");
            }
        }

        // Indicate that the process has finished
        Console.WriteLine("Gradient barcode generation completed.");
    }
}