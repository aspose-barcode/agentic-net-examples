// Title: Batch generation of PNG barcodes with alternating colors
// Description: Demonstrates how to generate a series of Code128 barcodes from a list of strings, saving each as a PNG file with alternating bar colors.
// Category-Description: Shows Aspose.BarCode batch processing – using BarcodeGenerator, EncodeTypes, and image format classes to create multiple barcodes. Typical use cases include generating product labels, inventory tags, or QR code sets where visual distinction is needed. Developers often need to loop through data, set barcode parameters such as BarColor and BackColor, and save images in PNG format.
// Prompt: Create a batch process that reads a list of strings and outputs PNG barcodes with alternating colors.
// Tags: barcode symbology, batch processing, png output, color customization, aspose.barcode, code128

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a batch of Code128 barcodes from a predefined list,
/// alternating the bar color between red and blue, and saves each
/// barcode as a PNG image in a temporary output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates sample data, configures output,
    /// iterates through the data to generate barcodes, and writes the
    /// location of the saved files to the console.
    /// </summary>
    static void Main(string[] args)
    {
        // Sample list of strings to encode as barcodes
        var data = new List<string>
        {
            "ABC123",
            "DEF456",
            "GHI789",
            "JKL012",
            "MNO345"
        };

        // Create a unique temporary output folder for the generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "BatchBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the alternating colors for the barcode bars (red, then blue)
        var barColors = new[] { Color.Red, Color.Blue };

        // Loop through each data item, generate a barcode, and save it as PNG
        for (int i = 0; i < data.Count; i++)
        {
            string text = data[i];
            // Select bar color based on the current index (alternating)
            Color barColor = barColors[i % barColors.Length];

            // Initialize the barcode generator with Code128 symbology and the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Apply the selected bar color and set a white background
                generator.Parameters.Barcode.BarColor = barColor;
                generator.Parameters.BackColor = Color.White;

                // Build the full file path for the PNG image
                string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.png");

                // Save the generated barcode image in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Output the location of the saved barcode images
        Console.WriteLine($"Barcodes saved to: {outputDir}");
    }
}