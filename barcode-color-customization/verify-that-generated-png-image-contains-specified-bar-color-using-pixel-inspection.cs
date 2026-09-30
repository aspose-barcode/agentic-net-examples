// Title: Generate Code128 Barcode PNG with Red Bars and Verify Color via Pixel Inspection
// Description: This example creates a Code128 barcode, sets the bar (foreground) color to red, saves it as a PNG file, and then inspects a pixel to confirm the bar color.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and image verification. It uses BarcodeGenerator, BarcodeParameters, and Aspose.Drawing to produce a PNG image, then reads the bitmap to validate visual properties. Developers working with barcode rendering, custom colors, and automated image validation can reference this pattern for unit tests or CI pipelines.
// Prompt: Verify that the generated PNG image contains the specified bar color using pixel inspection.
// Tags: barcode, code128, png, color verification, aspose.barcode, aspose.drawing, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with a custom bar color,
/// saves it as a PNG file, and verifies the color by inspecting a pixel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saves the image,
    /// validates the bar color, and cleans up the temporary file.
    /// </summary>
    static void Main()
    {
        // Define the barcode content and the temporary output file path.
        string codeText = "123456";
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Create a BarcodeGenerator for Code128 and set the bar (foreground) color to red.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.BarColor = Color.Red; // Set bar color.

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Ensure the image file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the PNG image using Aspose.Drawing for pixel-level inspection.
        using (var image = Image.FromFile(outputPath))
        using (var bitmap = (Bitmap)image)
        {
            // Choose a pixel location that is likely within a barcode bar.
            int x = bitmap.Width / 4;   // Quarter of the image width.
            int y = bitmap.Height / 2;  // Vertically centered.

            // Retrieve the color of the selected pixel.
            Color pixelColor = bitmap.GetPixel(x, y);

            // Compare the pixel color with the expected red bar color.
            if (pixelColor.ToArgb() == Color.Red.ToArgb())
            {
                Console.WriteLine("Bar color verification succeeded: pixel is Red as expected.");
            }
            else
            {
                Console.WriteLine($"Bar color verification failed: pixel color is {pixelColor}.");
            }
        }

        // Optional cleanup: delete the temporary PNG file.
        try
        {
            File.Delete(outputPath);
        }
        catch
        {
            // Suppress any exceptions during cleanup.
        }
    }
}