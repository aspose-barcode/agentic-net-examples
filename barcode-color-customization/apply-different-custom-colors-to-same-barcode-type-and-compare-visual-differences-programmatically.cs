// Title: Custom Colored Barcode Generation and Pixel Comparison
// Description: Demonstrates generating Code128 barcodes with different custom colors and programmatically comparing their visual differences.
// Prompt: Apply different custom colors to the same barcode type and compare visual differences programmatically.
// Tags: barcode, code128, color, comparison, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates two Code128 barcodes with distinct color schemes,
/// saves them as PNG files, and compares the images pixel by pixel
/// to quantify visual differences.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // Define common barcode data and target file paths
        // --------------------------------------------------------------------
        string codeText = "Sample123";
        string fileRed = Path.Combine(outputDir, "code_red.png");
        string fileBlue = Path.Combine(outputDir, "code_blue.png");

        // --------------------------------------------------------------------
        // Generate barcode with red bars on a white background
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Red;      // Set bar color to red
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;          // Set background to white
            generator.Save(fileRed, BarCodeImageFormat.Png);                       // Save as PNG
        }

        // --------------------------------------------------------------------
        // Generate barcode with blue bars on a light gray background
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;    // Set bar color to blue
            generator.Parameters.BackColor = Aspose.Drawing.Color.LightGray;      // Set background to light gray
            generator.Save(fileBlue, BarCodeImageFormat.Png);                      // Save as PNG
        }

        // --------------------------------------------------------------------
        // Load the generated images for pixel-by-pixel comparison
        // --------------------------------------------------------------------
        using (var bmpRed = new Bitmap(fileRed))
        using (var bmpBlue = new Bitmap(fileBlue))
        {
            // Verify that both images share the same dimensions
            if (bmpRed.Width != bmpBlue.Width || bmpRed.Height != bmpBlue.Height)
            {
                Console.WriteLine("Images have different dimensions; cannot compare.");
                return;
            }

            int diffPixels = 0; // Counter for differing pixels

            // Iterate over each pixel coordinate
            for (int y = 0; y < bmpRed.Height; y++)
            {
                for (int x = 0; x < bmpRed.Width; x++)
                {
                    // Compare pixel colors; increment counter if they differ
                    if (bmpRed.GetPixel(x, y) != bmpBlue.GetPixel(x, y))
                    {
                        diffPixels++;
                    }
                }
            }

            // Output the total number of differing pixels
            Console.WriteLine($"Total differing pixels between red and blue barcodes: {diffPixels}");
        }

        // --------------------------------------------------------------------
        // Inform the user where the barcode images have been saved
        // --------------------------------------------------------------------
        Console.WriteLine($"Red barcode saved to: {fileRed}");
        Console.WriteLine($"Blue barcode saved to: {fileBlue}");
    }
}