// Title: Barcode generation with custom colors and pixel verification
// Description: Demonstrates creating a Code128 barcode PNG with specific bar and background colors, then verifies that the image contains only those RGB values.
// Prompt: Verify that the generated PNG file contains the exact RGB values specified for each color property.
// Tags: barcode symbology, generation, png, color verification, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with custom colors,
/// saves it as PNG, and verifies the pixel colors match the specified values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it, and validates pixel colors.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG
        const string outputPath = "barcode.png";

        // Define the expected bar (foreground) and background colors using ARGB values
        Color expectedBarColor = Color.FromArgb(255, 255, 0, 0);      // Red
        Color expectedBackColor = Color.FromArgb(255, 200, 200, 200); // Light gray

        // Create and configure the barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Apply the custom colors to the barcode parameters
            generator.Parameters.Barcode.BarColor = expectedBarColor; // Set bar (foreground) color
            generator.Parameters.BackColor = expectedBackColor;      // Set background color

            // Save the barcode image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the PNG file was created successfully
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Error: File '{outputPath}' was not created.");
            return;
        }

        // Load the generated image as a bitmap for pixel-level inspection
        using (var image = Aspose.Drawing.Image.FromFile(outputPath) as Bitmap)
        {
            if (image == null)
            {
                Console.WriteLine("Error: Unable to load the image as a bitmap.");
                return;
            }

            bool verificationPassed = true;

            // Iterate over each pixel to ensure it matches one of the expected colors
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);

                    // Pixel must be either the bar color or the background color
                    if (!pixelColor.Equals(expectedBarColor) && !pixelColor.Equals(expectedBackColor))
                    {
                        Console.WriteLine($"Mismatch at ({x},{y}): Expected BarColor or BackColor, found R={pixelColor.R}, G={pixelColor.G}, B={pixelColor.B}");
                        verificationPassed = false;
                        // Continue checking remaining pixels to report all mismatches
                    }
                }
            }

            // Output the verification result
            Console.WriteLine(verificationPassed
                ? "Verification passed: All pixels match the specified BarColor or BackColor."
                : "Verification failed: Some pixels do not match the specified colors.");
        }
    }
}