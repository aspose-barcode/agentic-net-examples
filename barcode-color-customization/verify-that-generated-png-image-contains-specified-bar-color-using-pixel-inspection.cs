// Title: Barcode generation with custom bar color and verification
// Description: Demonstrates creating a Code128 barcode PNG with a blue bar color and verifies the color by inspecting pixels.
// Prompt: Verify that the generated PNG image contains the specified bar color using pixel inspection.
// Tags: barcode, code128, color, png, verification, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode with a custom bar color, saves it as a PNG,
/// and verifies that the specified color appears in the resulting image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saving, and color verification.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define output file path
        // --------------------------------------------------------------------
        string outputPath = "barcode.png";

        // --------------------------------------------------------------------
        // Desired bar color (blue)
        // --------------------------------------------------------------------
        Color barColor = Color.Blue;

        // --------------------------------------------------------------------
        // Generate barcode with the specified bar color and save as PNG
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.Barcode.BarColor = barColor;
            generator.Save(outputPath);
        }

        // --------------------------------------------------------------------
        // Verify that the generated image file exists
        // --------------------------------------------------------------------
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Barcode image file was not created.");
            return;
        }

        // --------------------------------------------------------------------
        // Scan the image pixel by pixel to find the expected bar color
        // --------------------------------------------------------------------
        bool colorFound = false;
        using (var image = Image.FromFile(outputPath) as Bitmap)
        {
            if (image == null)
            {
                Console.WriteLine("Failed to load the barcode image as a bitmap.");
                return;
            }

            // Iterate over each pixel until the color is found
            for (int y = 0; y < image.Height && !colorFound; y++)
            {
                for (int x = 0; x < image.Width && !colorFound; x++)
                {
                    // Compare the pixel's ARGB value with the expected bar color's ARGB value
                    if (image.GetPixel(x, y).ToArgb() == barColor.ToArgb())
                    {
                        colorFound = true;
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // Output verification result
        // --------------------------------------------------------------------
        Console.WriteLine(colorFound
            ? "Verification succeeded: bar color is present in the image."
            : "Verification failed: bar color not found in the image.");
    }
}