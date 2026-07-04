// Title: Generate multiple barcodes with random background colors
// Description: Demonstrates creating barcode images, each with a unique random background color, and saving them as PNG files.
// Prompt: Use a loop to generate one hundred barcodes each with a unique random background color.
// Tags: barcode, code128, random background, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates barcode images with random background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a set of barcode PNG files with random backgrounds.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated barcode images
        string outputDir = "Barcodes";

        // Ensure the output directory exists; create it if it does not
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize a random number generator for creating random colors
        Random rnd = new Random();

        // Generate a safe sample of 10 barcodes (large batches should be limited)
        for (int i = 1; i <= 10; i++)
        {
            // Create a unique code text for each barcode (e.g., Sample1, Sample2, ...)
            string codeText = $"Sample{i}";

            // Generate a random background color using RGB components
            Aspose.Drawing.Color bgColor = Aspose.Drawing.Color.FromArgb(
                rnd.Next(256), // Red component (0-255)
                rnd.Next(256), // Green component (0-255)
                rnd.Next(256)  // Blue component (0-255)
            );

            // Use a using block to ensure the BarcodeGenerator is properly disposed
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the randomly generated background color to the barcode
                generator.Parameters.BackColor = bgColor;

                // Construct the full file path for the PNG output
                string filePath = Path.Combine(outputDir, $"barcode_{i}.png");

                // Save the barcode image in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Inform the user that barcode generation has completed
        Console.WriteLine("Barcode generation completed.");
    }
}