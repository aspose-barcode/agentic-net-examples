// Title: Verify DotCode margin influences image whitespace
// Description: Demonstrates how setting the DotCode barcode margin (padding) changes the generated image dimensions, confirming the margin adds surrounding whitespace.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DotCode symbology and image rendering. It showcases the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings such as XDimension and Padding. Developers often need to validate that margin settings affect the output size for layout and printing purposes.
// Prompt: Write unit test ensuring DotCode margin property correctly influences surrounding whitespace.
// Tags: dotcode, barcode, margin, padding, image-size, generation, aspose.barcode, unit-test

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates verification that the DotCode barcode margin property affects the generated image size.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DotCode barcode image with the specified padding (margin) and returns its dimensions.
    /// </summary>
    /// <param name="paddingPixels">The amount of padding to apply on all sides, in pixels.</param>
    /// <returns>A tuple containing the image width and height.</returns>
    static (int Width, int Height) GetDotCodeImageSize(float paddingPixels)
    {
        // Create a barcode generator for DotCode with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "Test"))
        {
            // Set the module size (X dimension) for the barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Apply uniform padding (margin) on all four sides.
            generator.Parameters.Barcode.Padding.Left.Pixels = paddingPixels;
            generator.Parameters.Barcode.Padding.Top.Pixels = paddingPixels;
            generator.Parameters.Barcode.Padding.Right.Pixels = paddingPixels;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = paddingPixels;

            // Generate the barcode image and capture its size.
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                return (bitmap.Width, bitmap.Height);
            }
        }
    }

    /// <summary>
    /// Entry point that compares image sizes with and without margin and reports the result.
    /// </summary>
    static void Main()
    {
        // Generate image size without any margin.
        var sizeNoMargin = GetDotCodeImageSize(0f);

        // Generate image size with a 10‑pixel margin on each side.
        var sizeWithMargin = GetDotCodeImageSize(10f);

        // Determine whether the margin increased both dimensions.
        bool widthIncreased = sizeWithMargin.Width > sizeNoMargin.Width;
        bool heightIncreased = sizeWithMargin.Height > sizeNoMargin.Height;

        // Output the verification result.
        if (widthIncreased && heightIncreased)
        {
            Console.WriteLine("PASSED: Margin property correctly increased surrounding whitespace.");
        }
        else
        {
            Console.WriteLine("FAILED: Margin property did not affect image size as expected.");
            Console.WriteLine($"NoMargin Size: {sizeNoMargin.Width}x{sizeNoMargin.Height}");
            Console.WriteLine($"WithMargin Size: {sizeWithMargin.Width}x{sizeWithMargin.Height}");
        }
    }
}