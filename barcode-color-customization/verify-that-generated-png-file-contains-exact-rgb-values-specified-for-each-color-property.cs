// Title: Generate barcode PNG with custom colors and verify RGB values
// Description: Creates a Code128 barcode, applies specific background, bar, border, and caption colors, saves as PNG, and checks that the image contains the exact RGB values for each color.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, demonstrating how to customize barcode appearance using the BarcodeGenerator class and its Parameters properties. Typical use cases include branding, UI integration, and ensuring visual compliance. Developers often need to set colors for background, bars, borders, and text, then validate the output image.
// Prompt: Verify that the generated PNG file contains the exact RGB values specified for each color property.
// Tags: barcode symbology, color customization, png output, aspose.barcode, image verification

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a barcode with custom colors, saving it as PNG,
/// and verifying that the generated image contains the expected RGB values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and validates colors.
    /// </summary>
    static void Main()
    {
        // Prepare output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeColorTest");
        Directory.CreateDirectory(outputDir);
        string pngPath = Path.Combine(outputDir, "barcode.png");

        // Create barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Set background and bar colors
            generator.Parameters.BackColor = Color.Green;
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Configure border appearance
            generator.Parameters.Border.Visible = true;
            generator.Parameters.Border.Width.Pixels = 5f;
            generator.Parameters.Border.Color = Color.Red;

            // Set code text color
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Yellow;

            // Add captions above and below the barcode with custom colors
            generator.Parameters.CaptionAbove.Text = "Above";
            generator.Parameters.CaptionAbove.TextColor = Color.Magenta;
            generator.Parameters.CaptionBelow.Text = "Below";
            generator.Parameters.CaptionBelow.TextColor = Color.Cyan;

            // Save the barcode as a PNG image
            generator.Save(pngPath, BarCodeImageFormat.Png);
        }

        // Verify that each specified color appears in the generated PNG
        bool allOk = true;
        using (var bitmap = new Bitmap(pngPath))
        {
            allOk &= VerifyColor(bitmap, Color.Green, "Background");
            allOk &= VerifyColor(bitmap, Color.Blue, "Bar");
            allOk &= VerifyColor(bitmap, Color.Red, "Border");
            allOk &= VerifyColor(bitmap, Color.Yellow, "CodeText");
            allOk &= VerifyColor(bitmap, Color.Magenta, "CaptionAbove");
            allOk &= VerifyColor(bitmap, Color.Cyan, "CaptionBelow");
        }

        // Output verification result and exit with appropriate code
        if (allOk)
        {
            Console.WriteLine("All specified colors were found in the PNG file.");
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("One or more specified colors were NOT found in the PNG file.");
            Environment.Exit(1);
        }
    }

    /// <summary>
    /// Scans the bitmap for a pixel matching the expected color.
    /// </summary>
    /// <param name="bitmap">The image to scan.</param>
    /// <param name="expected">The expected color.</param>
    /// <param name="name">A friendly name for the color being verified.</param>
    /// <returns>True if the color is found; otherwise, false.</returns>
    static bool VerifyColor(Bitmap bitmap, Color expected, string name)
    {
        for (int y = 0; y < bitmap.Height; y++)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                if (bitmap.GetPixel(x, y).ToArgb() == expected.ToArgb())
                {
                    Console.WriteLine($"{name} color verified at ({x},{y}).");
                    return true;
                }
            }
        }
        Console.WriteLine($"{name} color NOT found.");
        return false;
    }
}