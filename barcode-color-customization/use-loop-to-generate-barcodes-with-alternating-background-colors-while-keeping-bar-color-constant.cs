// Title: Generate multiple Code128 barcodes with alternating background colors
// Description: Demonstrates creating a series of Code128 barcodes where the background color alternates between white and light gray while the bar color stays black.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class. It shows setting background and bar colors, looping to produce multiple images, and saving them in PNG format—common tasks for developers needing batch barcode creation for inventory, labeling, or testing purposes.
// Prompt: Use a loop to generate barcodes with alternating background colors while keeping bar color constant.
// Tags: code128, barcode generation, background color, bar color, png, aspose.barcode, loop, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a set of Code128 barcodes with alternating background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates an output folder, generates barcodes in a loop, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary output folder for the generated barcode images
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        int barcodeCount = 6; // Number of barcodes to generate

        // Loop to generate each barcode with alternating background colors
        for (int i = 0; i < barcodeCount; i++)
        {
            // Determine background color: white for even indices, light gray for odd indices
            Color backColor = (i % 2 == 0) ? Color.White : Color.LightGray;

            // Bar (foreground) color remains constant (black) for all barcodes
            Color barColor = Color.Black;

            // Text to encode in the barcode
            string codeText = $"Code{i + 1}";

            // Full file path for the PNG image
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

            // Initialize the barcode generator with Code128 symbology and the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the background and bar colors to the generator parameters
                generator.Parameters.BackColor = backColor;
                generator.Parameters.Barcode.BarColor = barColor;

                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output the location of the generated barcode to the console
            Console.WriteLine($"Generated barcode {i + 1}: {filePath}");
        }

        // Indicate that all barcodes have been generated
        Console.WriteLine("Barcode generation completed.");
    }
}