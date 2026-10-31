// Title: Generate 100 Barcodes with Random Background Colors
// Description: Demonstrates creating one hundred Code128 barcodes, each saved as a PNG with a unique random background color. Useful for batch barcode generation with visual differentiation.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce images. Developers often need to generate multiple barcodes programmatically, customize appearance such as background color, and save them in common image formats. The snippet shows typical setup, random color handling, and file output for bulk operations.
// Prompt: Use a loop to generate one hundred barcodes each with a unique random background color.
// Tags: barcode generation, code128, random background color, png, aspose.barcode, aspnet, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating 100 Code128 barcodes with unique random background colors and saving them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output directory, generates barcodes, and writes the result path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a temporary output folder with a unique name.
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Random number generator for background colors.
        Random rand = new Random();

        // Keep track of used colors to ensure uniqueness.
        HashSet<int> usedColors = new HashSet<int>();

        // Generate 100 barcodes.
        for (int i = 0; i < 100; i++)
        {
            Color bgColor;
            int colorKey;

            // Find a random color that hasn't been used yet.
            do
            {
                int r = rand.Next(256);
                int g = rand.Next(256);
                int b = rand.Next(256);
                bgColor = Color.FromArgb(r, g, b);
                colorKey = (r << 16) | (g << 8) | b;
            } while (!usedColors.Add(colorKey));

            // Create a barcode generator for Code128 with a unique text value.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Code{i + 1}"))
            {
                // Apply the unique random background color.
                generator.Parameters.BackColor = bgColor;

                // Define the file path for the PNG image.
                string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.png");

                // Save the barcode image as PNG.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Inform the user where the barcodes were saved.
        Console.WriteLine($"Generated 100 barcodes in: {outputDir}");
    }
}