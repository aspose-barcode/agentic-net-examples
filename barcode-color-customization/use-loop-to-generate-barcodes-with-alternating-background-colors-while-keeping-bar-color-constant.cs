// Title: Generate Code128 barcodes with alternating background colors
// Description: Demonstrates creating multiple Code128 barcodes where the background color alternates while the bar color stays black, saving each as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class together with the Parameters property to customize visual appearance such as bar and background colors. Typical use cases include batch creation of barcodes for product labeling or testing visual themes. Developers often need to programmatically vary colors, formats, or symbologies across multiple images.
// Prompt: Use a loop to generate barcodes with alternating background colors while keeping bar color constant.
// Tags: code128, barcode generation, background color, bar color, png, aspose.barcode, barcodegenerator, parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a series of Code128 barcode images with alternating background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates five barcode PNG files with alternating backgrounds.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists
        string outputDir = "Barcodes";
        Directory.CreateDirectory(outputDir);

        // Define the two background colors to alternate between
        Color[] backgroundColors = new Color[] { Color.White, Color.LightGray };

        // Loop to generate a small set of barcodes (5 samples)
        for (int i = 0; i < 5; i++)
        {
            // Build the text to encode for the current barcode
            string codeText = $"Sample{i + 1}";

            // Determine the file path for the generated image
            string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.png");

            // Initialize the barcode generator with Code128 symbology and the sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set the bar (foreground) color to black (constant for all barcodes)
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Alternate the background color based on the loop index
                generator.Parameters.BackColor = backgroundColors[i % backgroundColors.Length];

                // Save the barcode image as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Inform the user that generation is complete
        Console.WriteLine("Barcode images have been generated in the 'Barcodes' folder.");
    }
}