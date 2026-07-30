// Title: Generate and verify a Code128 barcode PNG with custom colors
// Description: This example creates a Code128 barcode, sets specific foreground and background colors, saves it as a PNG, and verifies that the image contains only those exact RGB values.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and image verification using Aspose.Drawing. It covers setting bar and background colors via BarcodeGenerator, saving to PNG, and pixel‑level validation. Developers working with barcode rendering, custom color schemes, or automated image testing will find this pattern useful.
// Prompt: Verify that the generated PNG file contains the exact RGB values specified for each color property.
// Tags: barcode symbology, color customization, png output, aspose.barcode, aspose.drawing, verification

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with custom colors,
/// saves it as a PNG file, and validates the pixel colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it,
    /// and checks that only the specified foreground and background colors are present.
    /// </summary>
    static void Main()
    {
        // Define the output PNG file path.
        string outputPath = "barcode.png";

        // Create and configure the barcode generator.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the foreground (bar) color to blue (RGB 0,0,255).
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.FromArgb(255, 0, 0, 255);
            // Set the background color to yellow (RGB 255,255,0).
            generator.Parameters.BackColor = Aspose.Drawing.Color.FromArgb(255, 255, 255, 0);
            // Save the barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the PNG file was created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Error: Generated file not found.");
            return;
        }

        // Load the generated image using Aspose.Drawing.
        using (var bitmap = Aspose.Drawing.Image.FromFile(outputPath) as Aspose.Drawing.Bitmap)
        {
            if (bitmap == null)
            {
                Console.WriteLine("Error: Unable to load image as bitmap.");
                return;
            }

            // Define the expected colors for verification.
            var expectedBarColor = Aspose.Drawing.Color.FromArgb(255, 0, 0, 255);   // Blue
            var expectedBackColor = Aspose.Drawing.Color.FromArgb(255, 255, 255, 0); // Yellow

            bool barFound = false;
            bool backFound = false;

            // Scan every pixel in the bitmap.
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    int pixelArgb = pixel.ToArgb();

                    if (pixelArgb == expectedBarColor.ToArgb())
                    {
                        barFound = true;
                    }
                    else if (pixelArgb == expectedBackColor.ToArgb())
                    {
                        backFound = true;
                    }
                    else
                    {
                        // An unexpected color was found; report and abort verification.
                        Console.WriteLine($"Unexpected color at ({x},{y}): ARGB={pixelArgb:X8}");
                        Console.WriteLine("Verification failed.");
                        return;
                    }
                }
            }

            // Report the verification result based on the presence of both colors.
            if (barFound && backFound)
            {
                Console.WriteLine("Verification succeeded: image contains only the specified colors.");
            }
            else
            {
                Console.WriteLine("Verification failed: expected colors not found in the image.");
            }
        }
    }
}