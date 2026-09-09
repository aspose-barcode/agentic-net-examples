// Title: Generate multiple Code128 barcodes with alternating background colors
// Description: Demonstrates creating a series of Code128 barcodes where the background color alternates between white and light gray while the bar color remains black. The images are saved as PNG files to a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class. It shows setting barcode parameters such as BarColor and BackColor, looping to produce multiple images, and saving them in PNG format. Developers working with barcode creation often need to customize visual styles for different branding or UI requirements, and this snippet provides a concise pattern for batch generation.
// Prompt: Use a loop to generate barcodes with alternating background colors while keeping bar color constant.
// Tags: code128, barcode generation, background color, bar color, loop, png, aspose.barcode, aspose.drawing

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
    /// Entry point. Creates a temporary directory, generates barcodes, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary output folder for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define how many barcodes to generate.
        int count = 6;

        // Loop to create each barcode with alternating background colors.
        for (int i = 0; i < count; i++)
        {
            // Create distinct text for each barcode.
            string codeText = "Sample" + (i + 1);

            // Initialize the barcode generator with Code128 symbology and the current text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set the bar (foreground) color to black for all barcodes.
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Alternate background color: white for even indices, light gray for odd indices.
                generator.Parameters.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;

                // Build the full file path for the PNG image.
                string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.png");

                // Save the generated barcode image to disk.
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Output the location of the generated file.
                Console.WriteLine($"Generated: {filePath}");
            }
        }
    }
}