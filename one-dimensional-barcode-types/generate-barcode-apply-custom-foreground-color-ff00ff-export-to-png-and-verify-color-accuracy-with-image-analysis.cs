// Title: Generate Code128 barcode with custom magenta foreground and verify color in PNG
// Description: Demonstrates creating a Code128 barcode, applying a custom foreground color #FF00FF, exporting it to a PNG file, and confirming the color via simple image analysis.
// Category-Description: This example belongs to the Aspose.BarCode generation and rendering category, showcasing how to customize barcode appearance using the BarcodeGenerator class, set visual properties like BarColor, export to common image formats via BarCodeImageFormat, and perform basic validation with Aspose.Drawing. Developers often need to tailor barcode colors for branding or UI integration and verify output correctness in automated pipelines.
// Prompt: Generate a barcode, apply custom foreground color #FF00FF, export to PNG, and verify color accuracy with image analysis.
// Tags: code128, barcode, color, png, generation, verification, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with a custom magenta foreground,
/// saves it as a PNG image, and verifies that the expected color is present in the output.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, saving, and color verification.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image.
        string outputPath = "barcode.png";

        // Create a barcode generator for Code128 with the sample text "Test123".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Apply a custom foreground color #FF00FF (magenta) to the barcode bars.
            generator.Parameters.Barcode.BarColor = Color.FromArgb(255, 0, 255);

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Ensure the image file was created before attempting verification.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the saved PNG image for pixel-level analysis.
        using (var image = Image.FromFile(outputPath))
        using (var bitmap = (Bitmap)image)
        {
            // Define the expected foreground color to look for.
            Color expectedColor = Color.FromArgb(255, 0, 255);
            bool colorFound = false;

            // Scan only a small region (up to 20x20 pixels) to keep processing fast.
            int maxX = Math.Min(bitmap.Width, 20);
            int maxY = Math.Min(bitmap.Height, 20);

            for (int y = 0; y < maxY && !colorFound; y++)
            {
                for (int x = 0; x < maxX && !colorFound; x++)
                {
                    // Compare the pixel's ARGB value with the expected color.
                    if (bitmap.GetPixel(x, y).ToArgb() == expectedColor.ToArgb())
                    {
                        colorFound = true;
                    }
                }
            }

            // Output the verification result.
            Console.WriteLine(colorFound
                ? "Color verification passed: foreground color matches #FF00FF."
                : "Color verification failed: foreground color not found.");
        }
    }
}