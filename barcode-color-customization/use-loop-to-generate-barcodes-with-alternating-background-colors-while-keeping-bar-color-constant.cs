// Title: Generate Multiple Barcodes with Alternating Background Colors
// Description: Demonstrates creating a series of Code128 barcodes where the foreground color stays constant while the background color alternates between white and light gray.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class together with EncodeTypes, BarCodeImageFormat, and color parameters. Typical scenarios include batch creation of barcodes for inventory, labeling, or testing visual styles where developers need to vary background colors without affecting the bar (foreground) color.
// Prompt: Use a loop to generate barcodes with alternating background colors while keeping bar color constant.
// Tags: barcode symbology, generation, code128, background color, alternating, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a set of Code128 barcodes with alternating background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, saves them as PNG files, and writes paths to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the output images.
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        int count = 5; // Number of barcodes to generate.

        // Loop to create each barcode with alternating background colors.
        for (int i = 0; i < count; i++)
        {
            // Build the text to encode in the barcode.
            string codeText = $"Sample{i + 1}";

            // Initialize the generator for Code128 symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set a constant foreground (bar) color.
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Alternate background colors: white for even indices, light gray for odd.
                if (i % 2 == 0)
                    generator.Parameters.BackColor = Color.White;
                else
                    generator.Parameters.BackColor = Color.LightGray;

                // Define the full file path for the PNG image.
                string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.png");

                // Save the generated barcode image.
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Output the location of the saved file.
                Console.WriteLine($"Generated barcode saved to: {filePath}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}