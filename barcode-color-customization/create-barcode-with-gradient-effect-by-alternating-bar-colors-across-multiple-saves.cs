// Title: Create barcode with gradient bar colors
// Description: Demonstrates generating a Code128 barcode and saving multiple PNG images, each with a different bar color to simulate a gradient effect.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to customize barcode appearance using the BarcodeGenerator class. It covers setting bar colors, background color, and saving images in PNG format. Developers often need to personalize barcodes for branding or visual distinction, and this snippet illustrates typical API usage for such customizations.
// Prompt: Create a barcode with a gradient effect by alternating bar colors across multiple saves.
// Tags: code128, barcode, gradient, png, aspose.barcode, aspose.drawing, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a series of Code128 barcodes, each with a different bar color,
/// to create a gradient effect across multiple saved PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, defines colors,
    /// generates barcodes with alternating colors, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output folder for the generated images
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeGradient_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // The text to encode in the barcode
        string codeText = "GRADIENT";

        // Define a sequence of colors to use for the gradient effect
        Color[] barColors = new Color[]
        {
            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Blue,
            Color.Indigo,
            Color.Violet
        };

        // Iterate over each color, generate a barcode, and save it
        for (int i = 0; i < barColors.Length; i++)
        {
            // Build a descriptive file name that includes the color name
            string fileName = $"Barcode_{i + 1}_{barColors[i].Name}.png";
            string filePath = Path.Combine(outputFolder, fileName);

            // Initialize the barcode generator with Code128 symbology and the desired text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the current bar color
                generator.Parameters.Barcode.BarColor = barColors[i];
                // Set a white background for better contrast
                generator.Parameters.BackColor = Color.White;

                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Inform the user about the saved file
            Console.WriteLine($"Saved barcode with color {barColors[i].Name} to: {filePath}");
        }

        // Indicate that the gradient barcode generation process has finished
        Console.WriteLine("Gradient barcode generation completed.");
    }
}