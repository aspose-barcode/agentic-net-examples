// Title: Generate Code128 barcode PNG with custom colors and verify pixel colors
// Description: Creates a Code128 barcode image with specific background, bar, border, text, and caption colors, saves it as PNG, and checks that the generated pixels match the expected RGB values.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class and related parameter objects. It covers setting background, bar, border, code text, and caption colors, saving to PNG, and performing pixel‑level verification—common tasks for developers needing precise visual control over barcode output.
// Prompt: Verify that the generated PNG file contains the exact RGB values specified for each color property.
// Tags: barcode symbology, generation, png, color, verification, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom colors and verifying the exact RGB values in the resulting PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it, and validates color values.
    /// </summary>
    static void Main()
    {
        // Define output file path in the temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_test.png");

        // Create a barcode generator for Code128 with the data "12345678"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set background color (light gray)
            generator.Parameters.BackColor = Color.FromArgb(255, 200, 200, 200);

            // Set bar (foreground) color (dark green)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(255, 0, 128, 0);

            // Enable and configure a red border of 2 pixels
            generator.Parameters.Border.Visible = true;
            generator.Parameters.Border.Width.Pixels = 2f;
            generator.Parameters.Border.Color = Color.FromArgb(255, 255, 0, 0);

            // Set code text color (blue)
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.FromArgb(255, 0, 0, 255);

            // Configure captions with custom colors
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.TextColor = Color.FromArgb(255, 255, 0, 255);
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";
            generator.Parameters.CaptionBelow.TextColor = Color.FromArgb(255, 0, 255, 255);

            // Save the barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created successfully
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the generated PNG for pixel‑level verification
        using (var bitmap = (Bitmap)Image.FromFile(outputPath))
        {
            // Verify background color by checking the top‑left pixel
            Color bgExpected = Color.FromArgb(255, 200, 200, 200);
            Color bgActual = bitmap.GetPixel(0, 0);
            Console.WriteLine($"Background color match: {bgActual.ToArgb() == bgExpected.ToArgb()}");

            // Verify that the exact bar color appears somewhere in the image
            Color barExpected = Color.FromArgb(255, 0, 128, 0);
            Console.WriteLine($"Bar color found: {FindExactColor(bitmap, barExpected)}");

            // Verify that the exact border color appears somewhere in the image
            Color borderExpected = Color.FromArgb(255, 255, 0, 0);
            Console.WriteLine($"Border color found: {FindExactColor(bitmap, borderExpected)}");

            // Verify code text color with a tolerance (anti‑aliasing may affect exact match)
            Color textExpected = Color.FromArgb(255, 0, 0, 255);
            Console.WriteLine($"Code text color found (tolerant): {FindCloseColor(bitmap, textExpected, 40)}");

            // Verify caption above color with tolerance
            Color captionAboveExpected = Color.FromArgb(255, 255, 0, 255);
            Console.WriteLine($"Caption above color found (tolerant): {FindCloseColor(bitmap, captionAboveExpected, 40)}");

            // Verify caption below color with tolerance
            Color captionBelowExpected = Color.FromArgb(255, 0, 255, 255);
            Console.WriteLine($"Caption below color found (tolerant): {FindCloseColor(bitmap, captionBelowExpected, 40)}");
        }

        // Clean up the temporary file
        try { File.Delete(outputPath); } catch { }
    }

    // Searches the entire bitmap for a pixel that exactly matches the target color
    static bool FindExactColor(Bitmap bitmap, Color target)
    {
        for (int y = 0; y < bitmap.Height; y++)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                if (bitmap.GetPixel(x, y).ToArgb() == target.ToArgb())
                    return true;
            }
        }
        return false;
    }

    // Searches the bitmap for a pixel whose color is within the specified tolerance of the target color
    static bool FindCloseColor(Bitmap bitmap, Color target, int tolerance)
    {
        for (int y = 0; y < bitmap.Height; y++)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                if (IsCloseColor(bitmap.GetPixel(x, y), target, tolerance))
                    return true;
            }
        }
        return false;
    }

    // Determines whether two colors are within the given tolerance for each RGB component
    static bool IsCloseColor(Color a, Color b, int tolerance)
    {
        return Math.Abs(a.R - b.R) <= tolerance &&
               Math.Abs(a.G - b.G) <= tolerance &&
               Math.Abs(a.B - b.B) <= tolerance;
    }
}