using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a series of Code128 barcodes with alternating background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates five barcode images with alternating backgrounds.
    /// </summary>
    static void Main()
    {
        // Define two alternating background colors: white and light gray.
        Color[] backgrounds = new Color[] { Color.White, Color.LightGray };

        // Define a constant foreground (bar) color: black.
        Color barColor = Color.Black;

        // Loop to generate a small set of barcodes (5 samples).
        for (int i = 0; i < 5; i++)
        {
            // Create a unique code text for each barcode (e.g., "Sample1").
            string codeText = $"Sample{i + 1}";

            // Initialize a barcode generator for Code128 using the current code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the constant bar (foreground) color.
                generator.Parameters.Barcode.BarColor = barColor;

                // Select the background color based on the current index to alternate.
                generator.Parameters.BackColor = backgrounds[i % backgrounds.Length];

                // Construct the output file name (e.g., "barcode_1.png").
                string fileName = $"barcode_{i + 1}.png";

                // Save the generated barcode image as a PNG file.
                generator.Save(fileName);

                // Output a confirmation message to the console.
                Console.WriteLine($"Generated {fileName} with background {generator.Parameters.BackColor}");
            }
        }
    }
}