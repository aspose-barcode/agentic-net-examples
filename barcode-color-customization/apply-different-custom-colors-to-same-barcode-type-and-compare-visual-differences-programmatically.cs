// Title: Custom Colored Barcode Comparison
// Description: Demonstrates generating a default barcode and a custom-colored version, then programmatically comparing their visual differences.
// Category-Description: This example belongs to the Aspose.BarCode generation and rendering category. It showcases how to customize barcode colors using the BarcodeGenerator.Parameters API, render the images to PNG, and compare them pixel‑by‑pixel. Developers working with barcode visual styling, branding, or automated image validation will find these patterns useful.
// Prompt: Apply different custom colors to the same barcode type and compare visual differences programmatically.
// Tags: barcode, color, comparison, png, aspose.barcode, code128, generation, image-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a default barcode and a custom‑colored barcode, then compares the two images pixel by pixel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates two barcodes (default and custom colors) and outputs the number of differing pixels.
    /// </summary>
    static void Main()
    {
        const string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Generate the default barcode (no custom colors applied)
        using (var defaultGenerator = new BarcodeGenerator(encodeType, codeText))
        {
            using (var defaultStream = new MemoryStream())
            {
                defaultGenerator.Save(defaultStream, BarCodeImageFormat.Png);
                defaultStream.Position = 0;

                using (var defaultBitmap = new Bitmap(defaultStream))
                {
                    // Generate a barcode with custom colors and captions
                    using (var customGenerator = new BarcodeGenerator(encodeType, codeText))
                    {
                        // Apply custom colors to various barcode elements
                        customGenerator.Parameters.Barcode.BarColor = Color.Red;
                        customGenerator.Parameters.BackColor = Color.Yellow;
                        customGenerator.Parameters.Barcode.CodeTextParameters.Color = Color.Blue;
                        customGenerator.Parameters.Border.Color = Color.Green;

                        // Add captions above and below the barcode
                        customGenerator.Parameters.CaptionAbove.Text = "Above";
                        customGenerator.Parameters.CaptionBelow.Text = "Below";
                        customGenerator.Parameters.CaptionAbove.TextColor = Color.Purple;
                        customGenerator.Parameters.CaptionBelow.TextColor = Color.Purple;

                        using (var customStream = new MemoryStream())
                        {
                            customGenerator.Save(customStream, BarCodeImageFormat.Png);
                            customStream.Position = 0;

                            using (var customBitmap = new Bitmap(customStream))
                            {
                                // Compare the two bitmaps and report the number of differing pixels
                                int diffPixels = CompareBitmaps(defaultBitmap, customBitmap);
                                Console.WriteLine($"Different pixels between default and custom barcode: {diffPixels}");
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Compares two bitmaps of identical dimensions and returns the count of pixels that differ.
    /// </summary>
    /// <param name="bmp1">First bitmap to compare.</param>
    /// <param name="bmp2">Second bitmap to compare.</param>
    /// <returns>Number of pixels with different ARGB values.</returns>
    /// <exception cref="ArgumentException">Thrown when the bitmap sizes do not match.</exception>
    static int CompareBitmaps(Bitmap bmp1, Bitmap bmp2)
    {
        if (bmp1.Width != bmp2.Width || bmp1.Height != bmp2.Height)
            throw new ArgumentException("Bitmap sizes do not match.");

        int diffCount = 0;
        for (int y = 0; y < bmp1.Height; y++)
        {
            for (int x = 0; x < bmp1.Width; x++)
            {
                Color c1 = bmp1.GetPixel(x, y);
                Color c2 = bmp2.GetPixel(x, y);
                if (c1.ToArgb() != c2.ToArgb())
                    diffCount++;
            }
        }
        return diffCount;
    }
}