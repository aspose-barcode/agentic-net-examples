// Title: Generate Code128 barcode with custom foreground color and verify pixel color
// Description: This example creates a Code128 barcode, sets its bar (foreground) color to a specific hex value, saves it as a PNG file, and checks that the exact color appears in the resulting image.
// Category-Description: Demonstrates Aspose.BarCode generation features such as setting barcode colors, saving to image formats, and using Aspose.Drawing to inspect pixel data. The example uses BarcodeGenerator, EncodeTypes, BarCodeImageFormat, Bitmap, and Color classes—common tools for developers who need custom‑styled barcodes and validation of visual output. Ideal for scenarios like brand‑compliant barcode creation or automated UI testing of generated images.
// Prompt: Create a barcode, set ForeColor to #123456, and verify the exact color appears in the saved image.
// Tags: barcode, code128, color, verification, png, aspose.barcode, generation, bitmap

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a barcode with a custom foreground color and verifying that the color is present in the saved image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it, and validates the color.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string barcodePath = Path.Combine(outputDir, "barcode.png");

        // Define the expected foreground color #123456
        Color expectedColor = Color.FromArgb(0x12, 0x34, 0x56);

        // Generate a Code128 barcode with the specified foreground color
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.Barcode.BarColor = expectedColor;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the exact expected color appears in the saved PNG image
        bool colorFound = false;
        using (Bitmap bitmap = new Bitmap(barcodePath))
        {
            for (int y = 0; y < bitmap.Height && !colorFound; y++)
            {
                for (int x = 0; x < bitmap.Width && !colorFound; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    if (pixel.ToArgb() == expectedColor.ToArgb())
                    {
                        colorFound = true;
                    }
                }
            }
        }

        // Output results to the console
        Console.WriteLine($"Barcode saved to: {barcodePath}");
        Console.WriteLine($"Expected color #{expectedColor.R:X2}{expectedColor.G:X2}{expectedColor.B:X2} found in image: {colorFound}");
    }
}