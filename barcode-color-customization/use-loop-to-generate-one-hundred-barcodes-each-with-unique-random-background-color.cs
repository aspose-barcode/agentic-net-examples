// Title: Generate 100 Barcodes with Unique Random Background Colors
// Description: Demonstrates how to create a series of barcode images, each with a distinct random background color, and save them as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. It illustrates typical scenarios where developers need to produce multiple barcodes with varied visual styles, such as batch processing or testing visual contrast. The example highlights setting generator parameters like BackColor and saving images to disk.
// Prompt: Use a loop to generate one hundred barcodes each with a unique random background color.
// Tags: code128, background color, image generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates 100 barcode images (Code128) with unique random background colors and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary folder, generates barcodes with distinct background colors,
    /// and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode images
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Random number generator for color creation
        var random = new Random();

        // HashSet to track used ARGB values and ensure uniqueness
        var usedColors = new HashSet<int>();

        // Loop to generate 100 barcodes
        for (int i = 1; i <= 100; i++)
        {
            // Generate a unique opaque ARGB color
            int argb;
            do
            {
                int r = random.Next(256);
                int g = random.Next(256);
                int b = random.Next(256);
                argb = (255 << 24) | (r << 16) | (g << 8) | b; // opaque color
            } while (!usedColors.Add(argb)); // repeat if color already used

            // Convert ARGB integer to Aspose.Drawing.Color
            Color bgColor = Color.FromArgb(argb);

            // Initialize barcode generator with Code128 symbology and a formatted value
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{i:D3}"))
            {
                // Apply the unique random background color
                generator.Parameters.BackColor = bgColor;

                // Build the output file path for the current barcode
                string filePath = Path.Combine(outputFolder, $"barcode_{i:D3}.png");

                // Save the barcode image as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Inform the user where the barcode images have been saved
        Console.WriteLine($"Generated 100 barcode images in: {outputFolder}");
    }
}