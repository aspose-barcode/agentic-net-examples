// Title: Generate Code128 barcode with custom magenta foreground and verify color
// Description: Creates a Code128 barcode, applies a custom magenta foreground color, saves it as PNG, and checks the pixel color to confirm accuracy.
// Category-Description: This example demonstrates Aspose.BarCode generation with color customization and basic image analysis. It uses BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and Aspose.Drawing's Bitmap/Color classes to produce a branded barcode, a common requirement for marketing materials and inventory systems. Developers often need to apply corporate colors to barcodes and verify the output programmatically.
// Prompt: Generate a barcode, apply custom foreground color #FF00FF, export to PNG, and verify color accuracy with image analysis.
// Tags: code128, barcode generation, color customization, png, image verification, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a Code128 barcode with a custom magenta foreground,
/// save it as a PNG file, and verify the color using simple image analysis.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode creation, saving, and color verification.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize the barcode generator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode.
            generator.CodeText = "Test123";

            // Apply a custom foreground color (magenta #FF00FF).
            generator.Parameters.Barcode.BarColor = Color.FromArgb(255, 0, 255);

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Ensure the image file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the saved PNG for pixel-level inspection.
        using (var bitmap = new Bitmap(outputPath))
        {
            // Choose a pixel near the image center, where barcode bars are expected.
            int x = bitmap.Width / 2;
            int y = bitmap.Height / 2;
            Color pixelColor = bitmap.GetPixel(x, y);

            // Define the expected magenta color.
            Color expectedColor = Color.FromArgb(255, 0, 255);

            // Compare the actual pixel color with the expected color.
            if (pixelColor.ToArgb() == expectedColor.ToArgb())
            {
                Console.WriteLine("Foreground color verification succeeded.");
            }
            else
            {
                Console.WriteLine($"Foreground color verification failed. Expected ARGB: {expectedColor.ToArgb()}, Actual ARGB: {pixelColor.ToArgb()}");
            }
        }
    }
}