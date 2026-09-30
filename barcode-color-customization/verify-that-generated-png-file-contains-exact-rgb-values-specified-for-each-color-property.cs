// Title: Barcode PNG Generation with Custom Colors and Verification
// Description: Generates a Code128 barcode PNG with specified foreground, background, and border colors, then verifies that the image contains the exact RGB values for each color.
// Category-Description: This example belongs to the Aspose.BarCode image generation and color customization category. It demonstrates using BarcodeGenerator, setting BarColor, BackColor, and Border properties, saving to PNG, and programmatically inspecting pixel colors. Developers working with barcode rendering often need to control visual appearance and validate output for compliance or testing.
// Prompt: Verify that the generated PNG file contains the exact RGB values specified for each color property.
// Tags: barcode symbology, color customization, png output, verification, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode with custom colors, saving it as PNG,
/// and verifying that the pixel colors match the specified RGB values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode image, inspects its pixels,
    /// and reports verification results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Setup output directory and file path
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeColorCheck");
        Directory.CreateDirectory(outputDir);
        string pngPath = Path.Combine(outputDir, "barcode.png");

        // --------------------------------------------------------------------
        // Define expected colors for barcode elements
        // --------------------------------------------------------------------
        Color expectedBarColor = Color.Red;          // Foreground (bars)
        Color expectedBackColor = Color.Yellow;      // Background
        Color expectedBorderColor = Color.Blue;      // Border

        // --------------------------------------------------------------------
        // Generate barcode with the specified colors
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Parameters.Barcode.BarColor = expectedBarColor;
            generator.Parameters.BackColor = expectedBackColor;
            generator.Parameters.Border.Color = expectedBorderColor;
            generator.Parameters.Border.Width.Pixels = 5f; // Make border visible

            generator.Save(pngPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Ensure the PNG file was created
        // --------------------------------------------------------------------
        if (!File.Exists(pngPath))
        {
            Console.WriteLine("Failed to generate PNG file.");
            return;
        }

        // --------------------------------------------------------------------
        // Load the image and count pixels of each expected color
        // --------------------------------------------------------------------
        using (var bitmap = new Bitmap(pngPath))
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            long totalPixels = (long)width * height;

            long barCount = 0;
            long backCount = 0;
            long borderCount = 0;
            long otherCount = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    if (pixel.ToArgb() == expectedBarColor.ToArgb())
                        barCount++;
                    else if (pixel.ToArgb() == expectedBackColor.ToArgb())
                        backCount++;
                    else if (pixel.ToArgb() == expectedBorderColor.ToArgb())
                        borderCount++;
                    else
                        otherCount++;
                }
            }

            // ----------------------------------------------------------------
            // Output verification statistics
            // ----------------------------------------------------------------
            Console.WriteLine($"Total Pixels: {totalPixels}");
            Console.WriteLine($"Bar Color (Red) Pixels: {barCount}");
            Console.WriteLine($"Background Color (Yellow) Pixels: {backCount}");
            Console.WriteLine($"Border Color (Blue) Pixels: {borderCount}");
            Console.WriteLine($"Other Color Pixels: {otherCount}");

            bool success = barCount > 0 && backCount > 0 && borderCount > 0 && otherCount == 0;
            Console.WriteLine(success
                ? "Verification succeeded: All colors match the specifications."
                : "Verification failed: Color mismatch detected.");
        }

        // --------------------------------------------------------------------
        // Optional cleanup of generated files and directories
        // --------------------------------------------------------------------
        try
        {
            File.Delete(pngPath);
            Directory.Delete(outputDir);
        }
        catch
        {
            // Ignored - cleanup not critical for verification
        }
    }
}