// Title: Generate Code128 Barcode with Dark Red Bars and Verify PNG Output
// Description: This example creates a Code128 barcode, sets the bar color to dark red, saves it as a PNG, and checks that the saved image contains the expected color.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and image verification. It uses BarcodeGenerator, BarcodeParameters, and Aspose.Drawing to customize bar color, save the image, and read pixel data. Typical use cases include customizing barcode appearance for branding and validating generated images in automated tests. Developers working with barcode rendering and image processing often need to adjust colors and verify output programmatically.
// Prompt: Create a barcode, set ForeColor to dark red, and verify color appears correctly in saved PNG.
// Tags: barcode, code128, colormodification, png, verification, aspose.barcode, aspose.drawing, imageprocessing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a Code128 barcode with a custom dark red bar color,
/// saving it as a PNG, and verifying the color in the saved image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and validates the bar color.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");
        // Text to encode in the barcode
        string codeText = "123456";

        // Create a barcode generator for Code128 and set the bar color to dark red
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.BarColor = Color.FromArgb(139, 0, 0); // dark red
            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the saved PNG contains the expected dark red bar color
        bool colorMatches = false;
        using (var bitmap = new Bitmap(outputPath))
        {
            // Sample a pixel near the center of the image where a bar is likely present
            int x = bitmap.Width / 2;
            int y = bitmap.Height / 2;
            Color pixelColor = bitmap.GetPixel(x, y);
            Color expectedColor = Color.FromArgb(139, 0, 0);
            // Compare the actual pixel color with the expected dark red color
            colorMatches = pixelColor.ToArgb() == expectedColor.ToArgb();
        }

        // Output the verification result to the console
        Console.WriteLine(colorMatches
            ? "Bar color verification succeeded: dark red detected."
            : "Bar color verification failed: expected dark red not found.");
    }
}