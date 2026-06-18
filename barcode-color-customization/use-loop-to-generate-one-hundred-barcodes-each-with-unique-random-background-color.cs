using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates 100 Code128 barcodes with random background colors and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Creates output directory, generates barcodes, and logs progress.
    /// </summary>
    static void Main()
    {
        // Define output directory for barcode images
        string outputDir = "Barcodes";

        // Ensure the output directory exists; create it if missing
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Random number generator for background colors
        Random random = new Random();

        // Loop to generate 100 barcodes
        for (int i = 1; i <= 100; i++)
        {
            // Generate random RGB components (0-255)
            int r = random.Next(0, 256);
            int g = random.Next(0, 256);
            int b = random.Next(0, 256);

            // Create a Color instance from the random RGB values
            Color bgColor = Color.FromArgb(r, g, b);

            // Build the file path for the current barcode image
            string fileName = Path.Combine(outputDir, $"barcode_{i:D3}.png");

            // Initialize the barcode generator for Code128 format
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the text to encode in the barcode
                generator.CodeText = $"Code{i:D3}";

                // Apply the random background color
                generator.Parameters.BackColor = bgColor;

                // Save the barcode image as PNG
                generator.Save(fileName, BarCodeImageFormat.Png);
            }

            // Output status message to console
            Console.WriteLine($"Saved {fileName} with background RGB({r},{g},{b})");
        }

        // Indicate that all barcodes have been generated
        Console.WriteLine("Barcode generation completed.");
    }
}