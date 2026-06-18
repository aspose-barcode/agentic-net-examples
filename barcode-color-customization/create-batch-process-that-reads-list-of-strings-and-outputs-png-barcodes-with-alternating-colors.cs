using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code128 barcodes with alternating bar colors
/// and saving them as PNG files using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a set of barcodes from a predefined string array,
    /// applies alternating colors, and writes the images to disk.
    /// </summary>
    static void Main()
    {
        // Sample list of strings to encode into barcodes
        string[] data = new string[] { "ABC123", "DEF456", "GHI789", "JKL012", "MNO345" };

        // Define the output directory for barcode images
        string outputDir = "Barcodes";

        // Ensure the output directory exists; create it if it does not
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each string and generate a barcode with alternating bar colors
        for (int i = 0; i < data.Length; i++)
        {
            // Current text to encode
            string text = data[i];

            // Choose bar color: black for even indices, red for odd indices
            Aspose.Drawing.Color barColor = (i % 2 == 0) ? Aspose.Drawing.Color.Black : Aspose.Drawing.Color.Red;

            // Initialize the barcode generator with Code128 symbology and the text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Apply the selected bar color
                generator.Parameters.Barcode.BarColor = barColor;

                // Set the background color to white
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Construct the file path for the PNG image
                string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.png");

                // Save the generated barcode image to the specified path
                generator.Save(filePath);

                // Output a confirmation message to the console
                Console.WriteLine($"Saved barcode for \"{text}\" to {filePath}");
            }
        }
    }
}