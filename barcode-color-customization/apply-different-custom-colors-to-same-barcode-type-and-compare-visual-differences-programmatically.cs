// Title: Custom Color Comparison for Code128 Barcodes
// Description: Demonstrates how to generate a Code128 barcode with default and custom colors, then programmatically compare the visual differences.
// Category-Description: This example belongs to the Aspose.BarCode image customization and analysis category. It showcases the use of BarcodeGenerator, setting BarColor and BackColor, and performing pixel‑by‑pixel image comparison with Aspose.Drawing. Developers often need to customize barcode appearance for branding and verify visual changes automatically.
// Prompt: Apply different custom colors to the same barcode type and compare visual differences programmatically.
// Tags: barcode, code128, color customization, image comparison, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with default and custom colors,
/// saving them as PNG files, and comparing the images pixel by pixel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, generates two barcodes,
    /// and outputs the percentage of differing pixels.
    /// </summary>
    static void Main()
    {
        // Define the folder where barcode images will be saved
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // File paths for the default‑color and custom‑color barcode images
        string defaultPath = Path.Combine(outputFolder, "barcode_default.png");
        string customPath = Path.Combine(outputFolder, "barcode_custom.png");

        // ------------------------------------------------------------
        // Generate barcode with default colors (black bars on white background)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(defaultPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Generate barcode with custom colors (red bars on yellow background)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Red;   // Set bar (foreground) color
            generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;      // Set background color
            generator.Save(customPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Load both images and compare them pixel by pixel
        // ------------------------------------------------------------
        using (var imgDefault = (Bitmap)Aspose.Drawing.Image.FromFile(defaultPath))
        using (var imgCustom = (Bitmap)Aspose.Drawing.Image.FromFile(customPath))
        {
            // Verify that the images share the same dimensions before comparison
            if (imgDefault.Width != imgCustom.Width || imgDefault.Height != imgCustom.Height)
            {
                Console.WriteLine("Images have different dimensions; cannot compare pixel by pixel.");
                return;
            }

            int width = imgDefault.Width;
            int height = imgDefault.Height;
            long diffPixelCount = 0;

            // Iterate over each pixel and count differences
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int argbDefault = imgDefault.GetPixel(x, y).ToArgb();
                    int argbCustom = imgCustom.GetPixel(x, y).ToArgb();
                    if (argbDefault != argbCustom)
                    {
                        diffPixelCount++;
                    }
                }
            }

            // Output comparison results
            Console.WriteLine($"Total pixels: {width * height}");
            Console.WriteLine($"Different pixels: {diffPixelCount}");
            Console.WriteLine($"Difference percentage: {diffPixelCount * 100.0 / (width * height):F2}%");
        }
    }
}