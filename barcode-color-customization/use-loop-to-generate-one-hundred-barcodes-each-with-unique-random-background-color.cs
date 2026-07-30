// Title: Generate 100 Barcodes with Random Background Colors
// Description: This example creates 100 Code128 barcodes, each saved as a PNG with a unique random background color.
// Category-Description: Demonstrates Aspose.BarCode generation for bulk barcode creation. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce images. Typical use cases include batch processing of product codes, inventory labeling, or testing visual variations. Developers often need loops, randomization, and file handling to automate large‑scale barcode output.
// Prompt: Use a loop to generate one hundred barcodes each with a unique random background color.
// Tags: barcode symbology, generation, png, random background, loop, aspose.barcode, aspose.drawing, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a set of barcode images with random background colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates 100 Code128 barcodes, each with a distinct random background color,
    /// and saves them as PNG files in the "Barcodes" directory.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists
        string outputDir = "Barcodes";
        Directory.CreateDirectory(outputDir);

        // Random number generator for background colors
        Random rand = new Random();

        // Loop to generate 100 barcodes
        for (int i = 0; i < 100; i++)
        {
            // Build a unique text value for the barcode (e.g., Code001, Code002, ...)
            string codeText = $"Code{i:D3}";

            // Generate a random RGB color for the barcode background
            Aspose.Drawing.Color bgColor = Aspose.Drawing.Color.FromArgb(
                rand.Next(256), // Red component (0‑255)
                rand.Next(256), // Green component (0‑255)
                rand.Next(256)  // Blue component (0‑255)
            );

            // Initialize the barcode generator with Code128 symbology and the unique text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the random background color to the barcode image
                generator.Parameters.BackColor = bgColor;

                // Define the full file path for the PNG output
                string filePath = Path.Combine(outputDir, $"barcode_{i:D3}.png");

                // Save the generated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Notify the user that generation is complete
        Console.WriteLine("Generated 100 barcode images in the 'Barcodes' folder.");
    }
}