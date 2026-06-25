using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom foreground color,
/// saving it as PNG, and verifying that the color appears in the saved image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it, and checks for the target color in the image.
    /// </summary>
    static void Main()
    {
        // Path where the barcode image will be saved
        string outputPath = "barcode.png";

        // Define the desired foreground color (hex #123456)
        Color targetColor = Color.FromArgb(0x12, 0x34, 0x56);

        // Create a barcode generator for Code128 with the specified text
        // and set the barcode's foreground color to the target color.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Parameters.Barcode.BarColor = targetColor;
            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Ensure the image file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Flag indicating whether the target color was found in the image.
        bool colorFound = false;

        // Load the saved image and scan each pixel for the target color.
        using (var bitmap = new Bitmap(outputPath))
        {
            for (int y = 0; y < bitmap.Height && !colorFound; y++)
            {
                for (int x = 0; x < bitmap.Width && !colorFound; x++)
                {
                    // Retrieve the color of the current pixel.
                    Color pixel = bitmap.GetPixel(x, y);
                    // Compare the pixel's ARGB value with the target color's ARGB value.
                    if (pixel.ToArgb() == targetColor.ToArgb())
                    {
                        colorFound = true; // Target color found; exit loops.
                    }
                }
            }
        }

        // Output verification result to the console.
        Console.WriteLine(colorFound
            ? "Verification succeeded: target color #123456 is present in the barcode image."
            : "Verification failed: target color #123456 was not found in the barcode image.");
    }
}