// Title: Batch generation of PNG Code128 barcodes with alternating colors
// Description: Demonstrates how to generate a series of Code128 barcodes from a list of strings, saving each as a PNG file with alternating bar colors.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include bulk creation of barcode images for inventory, shipping labels, or product catalogs where visual distinction (e.g., alternating colors) is desired. Developers often need to automate barcode output to files in various formats, and this snippet shows a concise pattern for doing so.
// Prompt: Create a batch process that reads a list of strings and outputs PNG barcodes with alternating colors.
// Tags: code128, barcode generation, png, alternating colors, aspose.barcode, batch processing

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch creation of Code128 barcode PNG images with alternating colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for a predefined list of strings and saves them to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define the list of strings that will be encoded into barcodes.
        List<string> codes = new List<string>
        {
            "ABC123",
            "XYZ789",
            "HELLO",
            "WORLD",
            "12345"
        };

        // Create a unique temporary folder to store the generated PNG files.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define the colors that will be used alternately for the barcode bars.
        Color[] colors = new Color[] { Color.Black, Color.Red };

        // Iterate over each code, generate a barcode, and save it as a PNG.
        for (int i = 0; i < codes.Count; i++)
        {
            string text = codes[i];
            // Select the bar color based on the current index (alternating).
            Color barColor = colors[i % colors.Length];

            // Initialize the barcode generator with Code128 symbology and the current text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Apply the selected bar color.
                generator.Parameters.Barcode.BarColor = barColor;

                // Build the full file path for the output image.
                string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

                // Save the barcode image in PNG format.
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Inform the user about the saved file.
                Console.WriteLine($"Saved barcode for \"{text}\" to {filePath}");
            }
        }

        // Indicate that the batch process has finished.
        Console.WriteLine("Batch barcode generation completed.");
    }
}