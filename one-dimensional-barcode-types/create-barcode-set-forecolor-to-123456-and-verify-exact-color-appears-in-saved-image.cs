// Title: Generate Code128 barcode with custom foreground color and verify pixel color
// Description: This example creates a Code128 barcode, sets its bar (foreground) color to the hex value #123456, saves it as PNG, and checks that the saved image contains the exact color.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and image verification. Uses BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and Aspose.Drawing classes. Typical use cases include customizing barcode appearance and programmatically confirming rendering results. Developers often need to set bar colors, export to image formats, and validate output in automated tests.
// Prompt: Create a barcode, set ForeColor to #123456, and verify the exact color appears in the saved image.
// Tags: barcode, code128, forecolor, color verification, png, aspose.barcode, aspose.drawing, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a barcode with a custom foreground color and verify the color in the saved image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it, and validates the bar color.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Create a Color instance representing the target hex color #123456.
        Color targetColor = Color.FromArgb(0x12, 0x34, 0x56);

        // Generate a Code128 barcode with the specified bar color.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Apply the custom foreground color to the barcode bars.
            generator.Parameters.Barcode.BarColor = targetColor;

            // Save the barcode image as PNG to the defined path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the saved image contains the exact bar color.
        bool colorMatches = false;
        if (File.Exists(outputPath))
        {
            // Load the saved image from disk.
            using (Image img = Image.FromFile(outputPath))
            {
                // Cast the Image to a Bitmap for pixel access.
                using (Bitmap bitmap = (Bitmap)img)
                {
                    // Choose a pixel near the center of the image for verification.
                    int x = bitmap.Width / 2;
                    int y = bitmap.Height / 2;

                    // Retrieve the color of the selected pixel.
                    Color pixelColor = bitmap.GetPixel(x, y);

                    // Compare the pixel's ARGB value with the target color's ARGB value.
                    colorMatches = pixelColor.ToArgb() == targetColor.ToArgb();
                }
            }
        }

        // Output the verification result to the console.
        Console.WriteLine(colorMatches
            ? "Verification succeeded: bar color matches #123456."
            : "Verification failed: bar color does not match #123456.");
    }
}