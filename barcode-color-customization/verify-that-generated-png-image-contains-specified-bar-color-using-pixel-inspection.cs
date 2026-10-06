// Title: Generate Code128 barcode PNG with custom bar color and verify via pixel inspection
// Description: This example creates a Code128 barcode, sets the bar color to blue, saves it as a PNG file, and then inspects the image pixels to confirm that the specified bar color is present.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and image verification. It uses BarcodeGenerator to configure barcode parameters, BarCodeImageFormat for PNG output, and Aspose.Drawing classes (Bitmap, Image) to read and analyze the resulting image. Typical use cases include automated testing of barcode appearance, custom styling validation, and ensuring compliance with branding guidelines. Developers working with barcode rendering and image processing often need to programmatically verify visual attributes such as colors, sizes, and formats.
// Prompt: Verify that the generated PNG image contains the specified bar color using pixel inspection.
// Tags: barcode symbology, generation, png, color verification, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a barcode with a custom bar color, saving it as PNG,
/// and verifying the color by inspecting the image pixels.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and validates the bar color.
    /// </summary>
    static void Main()
    {
        // Define barcode parameters
        string codeText = "12345678";
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_test.png");
        Color barColor = Color.Blue;

        // Remove any existing file to ensure a clean run
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Generate the barcode with the specified bar color and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.BarColor = barColor;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the PNG file was created successfully
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the saved image and count pixels that match the target bar color
        using (Bitmap bitmap = (Bitmap)Image.FromFile(outputPath))
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            int matchingPixels = 0;
            int targetArgb = barColor.ToArgb();

            // Iterate over each pixel in the image
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    if (pixel.ToArgb() == targetArgb)
                    {
                        matchingPixels++;
                    }
                }
            }

            // Report verification result
            if (matchingPixels > 0)
            {
                Console.WriteLine($"Bar color verified. Found {matchingPixels} matching pixels.");
            }
            else
            {
                Console.WriteLine("Bar color not found in the generated image.");
            }
        }

        // Optional cleanup: delete the temporary image file
        try
        {
            File.Delete(outputPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}