// Title: Barcode PNG Color Verification Example
// Description: Generates a Code128 barcode PNG with custom colors and verifies that the output image contains the specified RGB values.
// Category-Description: This example belongs to the Aspose.BarCode generation and image processing category. It demonstrates how to customize barcode appearance using key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, and how to programmatically verify pixel colors in the resulting PNG. Developers often need to ensure visual fidelity of generated barcodes for branding or compliance purposes.
// Prompt: Verify that the generated PNG file contains the exact RGB values specified for each color property.
// Tags: barcode symbology, color customization, png output, aspose.barcode, image verification

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode with custom colors, saving it as PNG,
/// and verifying that the image contains the expected RGB values for each color element.
/// </summary>
class Program
{
    /// <summary>
    /// Determines whether two colors are within a specified tolerance.
    /// </summary>
    /// <param name="a">First color to compare.</param>
    /// <param name="b">Second color to compare.</param>
    /// <param name="tolerance">Maximum allowed difference per channel.</param>
    /// <returns>True if the colors are within tolerance; otherwise, false.</returns>
    static bool IsCloseColor(Color a, Color b, int tolerance = 40)
    {
        return Math.Abs(a.R - b.R) <= tolerance &&
               Math.Abs(a.G - b.G) <= tolerance &&
               Math.Abs(a.B - b.B) <= tolerance;
    }

    /// <summary>
    /// Entry point of the example. Generates the barcode, verifies colors, and cleans up.
    /// </summary>
    static void Main()
    {
        // Define the expected colors for each barcode element.
        Color expectedBackground = Color.FromArgb(255, 255, 255); // white
        Color expectedBar = Color.FromArgb(255, 0, 0); // red
        Color expectedBorder = Color.FromArgb(0, 255, 0); // green
        Color expectedCodeText = Color.FromArgb(0, 0, 255); // blue
        Color expectedCaptionAbove = Color.FromArgb(255, 0, 255); // magenta
        Color expectedCaptionBelow = Color.FromArgb(0, 255, 255); // cyan

        // Prepare the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_test.png");
        if (File.Exists(outputPath))
            File.Delete(outputPath);

        // Generate the barcode with the custom color settings.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            generator.Parameters.BackColor = expectedBackground;
            generator.Parameters.Barcode.BarColor = expectedBar;
            generator.Parameters.Border.Visible = true;
            generator.Parameters.Border.Width.Pixels = 2f;
            generator.Parameters.Border.Color = expectedBorder;

            generator.Parameters.Barcode.CodeTextParameters.Color = expectedCodeText;
            generator.Parameters.CaptionAbove.Text = "Above";
            generator.Parameters.CaptionAbove.TextColor = expectedCaptionAbove;
            generator.Parameters.CaptionBelow.Text = "Below";
            generator.Parameters.CaptionBelow.TextColor = expectedCaptionBelow;

            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Counters for matching pixels of each element.
        int borderCount = 0, barCount = 0, backgroundCount = 0;
        int codeTextCloseCount = 0, captionAboveCloseCount = 0, captionBelowCloseCount = 0;

        // Load the generated PNG and iterate over each pixel.
        using (Bitmap bitmap = new Bitmap(outputPath))
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);

                    // Exact matches for border, bar, and background colors.
                    if (pixel.ToArgb() == expectedBorder.ToArgb())
                        borderCount++;

                    if (pixel.ToArgb() == expectedBar.ToArgb())
                        barCount++;

                    if (pixel.ToArgb() == expectedBackground.ToArgb())
                        backgroundCount++;

                    // Approximate matches for text and caption colors.
                    if (IsCloseColor(pixel, expectedCodeText))
                        codeTextCloseCount++;

                    if (IsCloseColor(pixel, expectedCaptionAbove))
                        captionAboveCloseCount++;

                    if (IsCloseColor(pixel, expectedCaptionBelow))
                        captionBelowCloseCount++;
                }
            }
        }

        // Output verification results to the console.
        Console.WriteLine($"Border color matches: {(borderCount > 0 ? "PASS" : "FAIL")}");
        Console.WriteLine($"Bar color matches: {(barCount > 0 ? "PASS" : "FAIL")}");
        Console.WriteLine($"Background color matches: {(backgroundCount > 0 ? "PASS" : "FAIL")}");
        Console.WriteLine($"Code text color close match: {(codeTextCloseCount > 0 ? "PASS" : "FAIL")}");
        Console.WriteLine($"Caption above color close match: {(captionAboveCloseCount > 0 ? "PASS" : "FAIL")}");
        Console.WriteLine($"Caption below color close match: {(captionBelowCloseCount > 0 ? "PASS" : "FAIL")}");

        // Clean up the temporary PNG file.
        try
        {
            if (File.Exists(outputPath))
                File.Delete(outputPath);
        }
        catch
        {
            // Ignore any cleanup errors.
        }
    }
}