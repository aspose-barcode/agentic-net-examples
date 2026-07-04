// Title: Generate Barcodes with Alternating Background Colors
// Description: Demonstrates creating multiple Code128 barcodes where the bar color stays black while the background alternates between white and light gray.
// Prompt: Use a loop to generate barcodes with alternating background colors while keeping bar color constant.
// Tags: barcode, code128, background color, loop, aspose.barcode, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a series of Code128 barcodes with alternating background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes using a loop and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Sample codetext values to encode in each barcode
        string[] codes = { "ABC123", "DEF456", "GHI789", "JKL012", "MNO345" };

        // Define alternating background colors: white and light gray
        Color[] backgrounds = { Color.White, Color.LightGray };

        // Loop through each codetext, generate a barcode, and save it with the appropriate background
        for (int i = 0; i < codes.Length; i++)
        {
            string code = codes[i];

            // Initialize the barcode generator with Code128 symbology and the current codetext
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, code))
            {
                // Keep the bar (foreground) color constant (black)
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Apply alternating background color based on the current index
                generator.Parameters.BackColor = backgrounds[i % backgrounds.Length];

                // Construct the output file name (e.g., barcode_0.png)
                string fileName = $"barcode_{i}.png";

                // Save the generated barcode image to disk
                generator.Save(fileName);

                // Log the saved file and its background color to the console
                Console.WriteLine($"Saved {fileName} with background {generator.Parameters.BackColor}");
            }
        }
    }
}