// Title: Generate 100 Barcodes with Random Background Colors
// Description: Creates 100 Code128 barcodes, each saved as a PNG file with a unique random background color.
// Category-Description: This example belongs to the Aspose.BarCode batch generation category. It demonstrates how to use the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to produce multiple barcodes programmatically. Typical use cases include creating large sets of barcodes for inventory, shipping labels, or promotional materials where visual customization (e.g., background colors) is required. Developers often need to vary appearance attributes while ensuring each barcode remains distinct, and this snippet shows a common pattern for achieving that.
// Prompt: Use a loop to generate one hundred barcodes each with a unique random background color.
// Tags: barcode, code128, random background color, batch generation, png, aspose.barcode, aspose.drawing, barcodegenerator, encode types

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a batch of Code128 barcodes with unique random background colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates 100 barcodes, each saved as a PNG with a distinct background color.
    /// </summary>
    static void Main()
    {
        // Number of barcodes to generate
        const int totalBarcodes = 100;

        // Create a temporary output folder with a unique name
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Random number generator for background colors
        Random rand = new Random();

        // Keep track of already used ARGB values to ensure uniqueness
        HashSet<int> usedColors = new HashSet<int>();

        // Loop to generate each barcode
        for (int i = 1; i <= totalBarcodes; i++)
        {
            int argb;

            // Generate a unique random ARGB color (fully opaque)
            do
            {
                int r = rand.Next(0, 256);
                int g = rand.Next(0, 256);
                int b = rand.Next(0, 256);
                argb = (255 << 24) | (r << 16) | (g << 8) | b;
            } while (!usedColors.Add(argb)); // Repeat if color already used

            // Convert ARGB integer to Aspose.Drawing.Color
            Color bgColor = Color.FromArgb(argb);

            // Barcode text (e.g., Code001, Code002, ...)
            string codeText = $"Code{i:D3}";

            // Initialize the barcode generator with Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the unique background color
                generator.Parameters.BackColor = bgColor;

                // Define the output file path
                string filePath = Path.Combine(outputFolder, $"barcode_{i:D3}.png");

                // Save the barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Inform the user where the barcodes were saved
        Console.WriteLine($"Generated {totalBarcodes} barcodes in folder: {outputFolder}");
    }
}