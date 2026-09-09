// Title: Generate 100 Barcodes with Random Background Colors
// Description: This example creates 100 Code128 barcodes, each saved as a PNG file with a unique randomly generated background color.
// Category-Description: Demonstrates batch barcode generation using Aspose.BarCode. The example utilizes the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to produce multiple barcodes in a single run. Typical scenarios include creating large sets of barcodes for inventory, shipping labels, or testing visual variations. Developers often need to customize appearance (e.g., background color) while automating file output.
// Prompt: Use a loop to generate one hundred barcodes each with a unique random background color.
// Tags: barcode, code128, random background color, batch generation, png, aspose.barcode, aspose.drawing, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a collection of 100 Code128 barcodes, each with a distinct random background color,
/// and saves them as PNG files in a temporary directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, generates barcodes in a loop,
    /// and writes the location of the generated files to the console.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary folder path for the generated barcode images
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Initialize a single Random instance for color generation
        Random rnd = new Random();

        // Loop to create 100 barcodes with unique random background colors
        for (int i = 0; i < 100; i++)
        {
            // Generate a random background color (RGB components range from 0 to 255)
            int r = rnd.Next(0, 256);
            int g = rnd.Next(0, 256);
            int b = rnd.Next(0, 256);
            Color bgColor = Color.FromArgb(r, g, b);

            // Create a BarcodeGenerator for Code128 with a unique code text (e.g., Code001)
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Code{i:D3}"))
            {
                // Apply the random background color to the barcode
                generator.Parameters.BackColor = bgColor;

                // Define the full file path for the PNG output
                string filePath = Path.Combine(outputFolder, $"barcode_{i:D3}.png");

                // Save the barcode image in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Inform the user where the barcode images have been saved
        Console.WriteLine($"Generated 100 barcodes in: {outputFolder}");
    }
}