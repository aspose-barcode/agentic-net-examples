// Title: DotCode Margin Influence Test
// Description: Demonstrates how the DotCode barcode margin property adds whitespace around the generated image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on margin and padding settings for 2D symbologies like DotCode. It showcases the use of BarcodeGenerator, EncodeTypes, and BarcodeParameters to control image dimensions, a common requirement for developers needing precise layout control in reports or UI components.
// Prompt: Write unit test ensuring DotCode margin property correctly influences surrounding whitespace.
// Tags: dotcode, margin, padding, barcode, generation, unit-test, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides a console application that verifies the effect of the DotCode margin property on image size.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DotCode barcode bitmap with specified padding (in points).
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="left">Left padding in points.</param>
    /// <param name="top">Top padding in points.</param>
    /// <param name="right">Right padding in points.</param>
    /// <param name="bottom">Bottom padding in points.</param>
    /// <returns>A <see cref="Bitmap"/> containing the generated barcode.</returns>
    static Bitmap GenerateDotCode(string codeText, float left, float top, float right, float bottom)
    {
        // Create a generator for the DotCode symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Disable automatic sizing to keep padding effects visible.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Apply the specified padding (margin) values.
            generator.Parameters.Barcode.Padding.Left.Point = left;
            generator.Parameters.Barcode.Padding.Top.Point = top;
            generator.Parameters.Barcode.Padding.Right.Point = right;
            generator.Parameters.Barcode.Padding.Bottom.Point = bottom;

            // Generate and return the barcode image.
            return generator.GenerateBarCodeImage();
        }
    }

    /// <summary>
    /// Entry point that generates two DotCode images with and without padding and validates the size difference.
    /// </summary>
    static void Main()
    {
        // Sample text to encode.
        const string sampleText = "Test";

        // Generate a barcode with no padding.
        using (var bmpNoPad = GenerateDotCode(sampleText, 0f, 0f, 0f, 0f))
        // Generate a barcode with 20 points of padding on each side.
        using (var bmpPad = GenerateDotCode(sampleText, 20f, 20f, 20f, 20f))
        {
            // Default DPI used by the generator (96). Adjust if generator DPI changes.
            const float dpi = 96f;

            // Convert points to pixels (1 point = dpi / 72 pixels).
            float pointsToPixels = dpi / 72f;

            // Expected pixel increase due to left+right and top+bottom padding.
            float expectedWidthIncrease = (20f + 20f) * pointsToPixels;
            float expectedHeightIncrease = (20f + 20f) * pointsToPixels;

            // Actual dimensions of the generated images.
            int widthNoPad = bmpNoPad.Width;
            int heightNoPad = bmpNoPad.Height;
            int widthPad = bmpPad.Width;
            int heightPad = bmpPad.Height;

            // Allow a tolerance of 1 pixel because of rounding.
            bool widthOk = Math.Abs(widthPad - (widthNoPad + expectedWidthIncrease)) <= 1;
            bool heightOk = Math.Abs(heightPad - (heightNoPad + expectedHeightIncrease)) <= 1;

            if (widthOk && heightOk)
            {
                Console.WriteLine("PASS: DotCode margin influences surrounding whitespace as expected.");
            }
            else
            {
                Console.WriteLine("FAIL: DotCode margin did not produce the expected image size.");
                Console.WriteLine($"No padding size: {widthNoPad}x{heightNoPad}");
                Console.WriteLine($"With padding size: {widthPad}x{heightPad}");
                Console.WriteLine($"Expected increase: {expectedWidthIncrease}x{expectedHeightIncrease} pixels");
            }
        }
    }
}