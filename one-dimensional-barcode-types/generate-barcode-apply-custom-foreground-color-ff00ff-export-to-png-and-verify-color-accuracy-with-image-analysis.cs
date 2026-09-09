// Title: Generate Code128 barcode with custom magenta foreground and verify color
// Description: Demonstrates creating a Code128 barcode, applying a custom magenta foreground color (#FF00FF), saving it as a PNG file, and confirming the color exists via pixel analysis.
// Category-Description: This example belongs to the Aspose.BarCode generation and image processing category. It shows how to use BarcodeGenerator, set bar color via Parameters.Barcode.BarColor, save the image with BarCodeImageFormat, and perform basic bitmap analysis using Aspose.Drawing. Developers often need to customize barcode appearance and validate output for branding or compliance, making this pattern useful for automated testing and CI pipelines.
// Prompt: Generate a barcode, apply custom foreground color #FF00FF, export to PNG, and verify color accuracy with image analysis.
// Tags: code128, barcode, generation, color, png, verification, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with custom color and verification.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, saves it, and verifies the custom color.
    /// </summary>
    static void Main()
    {
        // Define the temporary output file path for the PNG image
        string outputPath = Path.Combine(Path.GetTempPath(), "custom_color_barcode.png");

        // Create a barcode generator for Code128 symbology with the data "12345678"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Apply a custom magenta foreground (bar) color #FF00FF
            generator.Parameters.Barcode.BarColor = Color.FromArgb(255, 255, 0, 255);

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Ensure the image file was created before attempting verification
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Prepare to scan the saved image for the target color
        bool colorFound = false;
        Color targetColor = Color.FromArgb(255, 255, 0, 255);

        // Load the PNG into a bitmap for pixel-by-pixel analysis
        using (var bitmap = new Bitmap(outputPath))
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            // Iterate over each pixel until the target color is found
            for (int y = 0; y < height && !colorFound; y++)
            {
                for (int x = 0; x < width && !colorFound; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    if (pixel.ToArgb() == targetColor.ToArgb())
                    {
                        colorFound = true;
                    }
                }
            }
        }

        // Output verification result
        Console.WriteLine(colorFound
            ? "Verification succeeded: custom foreground color is present."
            : "Verification failed: custom foreground color not found.");
    }
}