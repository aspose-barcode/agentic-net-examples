// Title: Batch generation of PNG Code128 barcodes with alternating colors
// Description: Demonstrates how to create multiple Code128 barcodes from a list of strings, saving each as a PNG file with alternating foreground and background colors.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and image format settings. It illustrates typical batch processing scenarios where developers need to produce a series of barcodes with varied visual styles for labeling, inventory, or marketing purposes. Common use cases include generating product labels, tickets, or QR codes in bulk with customized appearance.
// Prompt: Create a batch process that reads a list of strings and outputs PNG barcodes with alternating colors.
// Tags: barcode symbology, generation, png, aspose.barcode, code128, color, batch

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a set of Code128 barcodes from predefined strings,
/// saving each as a PNG file with alternating bar and background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the batch barcode generation process.
    /// </summary>
    static void Main()
    {
        // Define the list of text values to encode into barcodes.
        List<string> codeTexts = new List<string>
        {
            "ABC123",
            "XYZ789",
            "HELLO",
            "WORLD",
            "12345"
        };

        // Create a unique temporary folder to store the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define two alternating color schemes for bars and backgrounds.
        Color[] barColors = { Color.Black, Color.Red };
        Color[] backColors = { Color.White, Color.Yellow };

        // Iterate over each text value, generate a barcode, and save it as PNG.
        for (int i = 0; i < codeTexts.Count; i++)
        {
            string text = codeTexts[i];
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

            // Select colors based on the current index to achieve alternation.
            Color barColor = barColors[i % barColors.Length];
            Color backColor = backColors[i % backColors.Length];

            // Initialize the barcode generator with Code128 symbology and the current text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Apply the chosen foreground (bar) and background colors.
                generator.Parameters.Barcode.BarColor = barColor;
                generator.Parameters.BackColor = backColor;

                // Save the generated barcode image in PNG format.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Output the location of the generated barcode files.
        Console.WriteLine($"Barcodes generated in: {outputFolder}");
    }
}