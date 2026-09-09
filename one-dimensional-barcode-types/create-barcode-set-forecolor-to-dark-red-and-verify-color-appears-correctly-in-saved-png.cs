// Title: Generate Code128 Barcode with Dark Red Bars and Verify PNG Color
// Description: This example creates a Code128 barcode, sets the bar color to dark red, saves it as a PNG, and checks that the saved image contains the expected color.
// Category-Description: Demonstrates Aspose.BarCode image generation and color manipulation. It uses BarcodeGenerator, BarcodeParameters, and Aspose.Drawing to produce a barcode image, then reads the PNG with Bitmap to verify pixel colors. Developers working with barcode rendering, custom colors, and image validation can reference this pattern for testing visual output.
// Prompt: Create a barcode, set ForeColor to dark red, and verify color appears correctly in saved PNG.
// Tags: barcode, code128, color, png, verification, aspose.barcode, aspose.drawing, image-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a barcode with a custom foreground color and verifying the saved image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it as PNG, and validates the bar color.
    /// </summary>
    static void Main()
    {
        // Prepare an output directory in the temporary folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string filePath = Path.Combine(outputDir, "barcode.png");

        // Create a Code128 barcode with dark red bars and save it as PNG.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the barcode's foreground (bar) color to dark red (RGB 139,0,0).
            generator.Parameters.Barcode.BarColor = Color.FromArgb(139, 0, 0);
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Ensure the image file was created successfully.
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the saved PNG to verify that the dark red color is present.
        using (var bitmap = new Bitmap(filePath))
        {
            Color expected = Color.FromArgb(139, 0, 0);
            bool matchFound = false;

            // Scan each pixel until a matching dark red pixel is found.
            for (int y = 0; y < bitmap.Height && !matchFound; y++)
            {
                for (int x = 0; x < bitmap.Width && !matchFound; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    // Skip white background pixels; check for the expected color.
                    if (!pixel.Equals(Color.White) && pixel.ToArgb() == expected.ToArgb())
                    {
                        matchFound = true;
                    }
                }
            }

            // Output verification result.
            Console.WriteLine(matchFound
                ? "Barcode color verified as dark red."
                : "Barcode color verification failed.");
        }
    }
}