// Title: Generate Code128 barcode with custom magenta foreground and verify color in PNG
// Description: Demonstrates creating a Code128 barcode, applying a custom #FF00FF foreground color, exporting it as a PNG file, and confirming the color via pixel analysis.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to customize barcode appearance using the BarcodeGenerator class, set visual properties like BarColor, and export to common image formats. Developers often need to tailor barcode colors for branding or UI integration and verify output correctness through image processing techniques.
// Prompt: Generate a barcode, apply custom foreground color #FF00FF, export to PNG, and verify color accuracy with image analysis.
// Tags: code128, barcode generation, color customization, png export, image analysis, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with custom color and verification.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with magenta foreground, saves as PNG, and checks the color.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "custom_color_barcode.png");

        // Remove any existing file to ensure a clean run
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Generate barcode with custom foreground color #FF00FF (magenta)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the barcode's bar color to magenta (ARGB: 255, 255, 0, 255)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(255, 255, 0, 255);

            // Save the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the saved image contains the custom color
        bool colorFound = false;
        using (var bitmap = new Bitmap(outputPath))
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            Color targetColor = Color.FromArgb(255, 255, 0, 255);

            // Scan each pixel until the target color is found
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
            ? "Color verification passed: custom foreground color is present."
            : "Color verification failed: custom foreground color not found.");
    }
}