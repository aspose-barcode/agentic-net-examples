// Title: Generate Code128 Barcode with Custom Foreground Color and Verify It
// Description: This example creates a Code128 barcode, sets its bar color to the hexadecimal value #123456, saves it as a PNG file, and then checks the saved image to confirm the exact color is present.
// Category-Description: Aspose.BarCode barcode generation examples demonstrating color customization. Shows how to use BarcodeGenerator, set BarColor via Parameters.Barcode, save to PNG, and read the image with Aspose.Drawing.Bitmap for verification. Useful for developers needing precise visual styling of barcodes in .NET applications.
// Prompt: Create a barcode, set ForeColor to #123456, and verify the exact color appears in the saved image.
// Tags: barcode, code128, color, verification, png, aspose.barcode, aspose.drawing, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a Code128 barcode with a custom foreground color,
/// saving it to a PNG file, and verifying the color in the output image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        const string outputPath = "barcode.png";

        // Initialize a barcode generator for Code128 symbology with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Define the desired bar (foreground) color using its RGB components.
            var barColor = Color.FromArgb(0x12, 0x34, 0x56);
            // Apply the custom color to the barcode.
            generator.Parameters.Barcode.BarColor = barColor;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Ensure the image file was created before attempting verification.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        bool colorFound = false;
        var expectedColor = Color.FromArgb(0x12, 0x34, 0x56);

        // Load the saved image and scan its pixels for the expected color.
        using (var bitmap = new Bitmap(outputPath))
        {
            for (int y = 0; y < bitmap.Height && !colorFound; y++)
            {
                for (int x = 0; x < bitmap.Width && !colorFound; x++)
                {
                    // Compare each pixel's ARGB value with the expected color.
                    if (bitmap.GetPixel(x, y).ToArgb() == expectedColor.ToArgb())
                    {
                        colorFound = true;
                    }
                }
            }
        }

        // Output the verification result.
        Console.WriteLine(colorFound
            ? "Bar color #123456 verified in the saved image."
            : "Bar color #123456 not found in the saved image.");
    }
}