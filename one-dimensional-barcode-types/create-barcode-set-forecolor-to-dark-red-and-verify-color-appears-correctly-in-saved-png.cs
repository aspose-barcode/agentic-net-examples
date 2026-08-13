// Title: Generate Code128 barcode with dark red bars and verify PNG output
// Description: This example creates a Code128 barcode, sets the bar color to dark red, saves it as a PNG file, and programmatically checks that the saved image contains the expected color.
// Category-Description: Demonstrates Aspose.BarCode generation features such as customizing barcode appearance (foreground color) and exporting to PNG. It uses BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and Aspose.Drawing classes to create and verify the image. Ideal for developers needing to apply branding colors to barcodes and ensure visual correctness in automated pipelines.
// Prompt: Create a barcode, set ForeColor to dark red, and verify color appears correctly in saved PNG.
// Tags: barcode, code128, colormodification, png, aspose.barcode, generation, verification, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a barcode with a custom foreground color and verifying the saved image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, applies a dark red bar color,
    /// saves it as PNG, and validates that the color appears in the output file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Create a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Set the foreground (bars) color to dark red.
            generator.Parameters.Barcode.BarColor = Color.FromArgb(139, 0, 0); // DarkRed

            // Save the barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the saved image file exists.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        bool colorFound = false;

        // Load the saved PNG and scan its pixels to locate a dark red bar.
        using (var bitmap = new Bitmap(outputPath))
        {
            // Iterate over each pixel until a matching color is found.
            for (int y = 0; y < bitmap.Height && !colorFound; y++)
            {
                for (int x = 0; x < bitmap.Width && !colorFound; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);

                    // Skip background pixels (assumed white).
                    if (pixel.ToArgb() != Color.White.ToArgb())
                    {
                        // Check if the pixel matches the expected dark red color.
                        if (pixel.ToArgb() == Color.FromArgb(139, 0, 0).ToArgb())
                        {
                            colorFound = true;
                        }
                    }
                }
            }
        }

        // Output the verification result.
        Console.WriteLine(colorFound
            ? "Color verification passed: dark red bars detected."
            : "Color verification failed: dark red bars not detected.");
    }
}