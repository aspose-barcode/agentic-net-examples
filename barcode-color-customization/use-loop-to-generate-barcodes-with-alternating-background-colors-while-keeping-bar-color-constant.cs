// Title: Generate multiple Code128 barcodes with alternating background colors
// Description: Demonstrates creating several Code128 barcode images where the foreground bar color stays constant while the background color alternates between two shades.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to produce customized barcode images in batch. Typical scenarios include creating a series of barcodes for product labeling, inventory systems, or marketing materials where visual consistency (constant bar color) is required alongside visual variation (alternating backgrounds). Developers often need to automate such generation, control colors, and output formats like PNG.
// Prompt: Use a loop to generate barcodes with alternating background colors while keeping bar color constant.
// Tags: code128, barcode-generation, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a set of Code128 barcode images with alternating background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary folder, generates barcodes, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated barcode images
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Number of barcodes to generate
        int barcodeCount = 5;

        // Constant bar (foreground) color
        Color barColor = Color.Black;

        // Alternate background colors
        Color[] backgroundColors = new Color[] { Color.White, Color.LightGray };

        // Loop to generate each barcode with alternating background color
        for (int i = 0; i < barcodeCount; i++)
        {
            // Prepare code text for each barcode
            string codeText = $"Sample{i + 1}";

            // Create a barcode generator for Code128 (you can change the symbology if needed)
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set constant bar color
                generator.Parameters.Barcode.BarColor = barColor;

                // Set alternating background color based on the current index
                generator.Parameters.BackColor = backgroundColors[i % backgroundColors.Length];

                // Define output file path
                string outputPath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

                // Save the barcode image as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }

        // Inform the user where the images have been saved
        Console.WriteLine($"Generated {barcodeCount} barcode images in: {outputFolder}");
    }
}