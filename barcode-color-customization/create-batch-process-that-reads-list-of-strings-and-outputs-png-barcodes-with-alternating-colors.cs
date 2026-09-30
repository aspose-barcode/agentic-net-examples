// Title: Batch Generation of PNG Barcodes with Alternating Colors
// Description: Demonstrates how to generate a series of Code128 barcodes from a list of strings, saving each as a PNG file with alternating bar colors.
// Category-Description: This example belongs to the barcode generation category of Aspose.BarCode for .NET. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create image files. Developers often need to produce multiple barcodes in batch, customize appearance such as bar colors, and save them in common image formats for printing or digital distribution.
// Prompt: Create a batch process that reads a list of strings and outputs PNG barcodes with alternating colors.
// Tags: barcode generation, batch processing, code128, png, color customization, aspose.barcode, c#

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a batch of Code128 barcodes from a predefined list of strings,
/// alternating the bar color for each image, and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, iterates over values,
    /// configures barcode appearance, and writes PNG files to disk.
    /// </summary>
    static void Main()
    {
        // Define the list of strings to encode as barcodes.
        List<string> values = new List<string>
        {
            "ABC123",
            "XYZ789",
            "HELLO",
            "WORLD",
            "12345"
        };

        // Define a set of colors that will be applied cyclically to the barcodes.
        Color[] colors = new Color[]
        {
            Color.Red,
            Color.Green,
            Color.Blue,
            Color.Orange,
            Color.Purple
        };

        // Create a unique temporary folder to store the generated PNG files.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Iterate over each value, generate a barcode, apply color, and save as PNG.
        for (int i = 0; i < values.Count; i++)
        {
            string text = values[i];
            // Select a color based on the current index (wraps around the colors array).
            Color barColor = colors[i % colors.Length];
            // Build the full file path for the PNG output.
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

            // Initialize the barcode generator with Code128 symbology and the current text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Apply the selected foreground (bar) color.
                generator.Parameters.Barcode.BarColor = barColor;

                // Save the generated barcode image as a PNG file.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Saved barcode '{text}' to {filePath}");
        }

        // Indicate that the batch process has completed.
        Console.WriteLine("All barcodes generated.");
    }
}