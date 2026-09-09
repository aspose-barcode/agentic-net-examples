// Title: Custom Color Schemes for QR Barcodes and Pixel Comparison
// Description: Demonstrates how to apply different custom colors to a QR barcode, generate images, and programmatically compare the visual differences.
// Category-Description: This example belongs to the Aspose.BarCode image generation and analysis category. It showcases the use of BarcodeGenerator, BarCodeImageFormat, and Aspose.Drawing.Bitmap to customize barcode appearance (bar color, background, code text, captions) and then compare generated bitmaps pixel‑by‑pixel. Developers working with barcode rendering, visual testing, or automated UI validation often need to generate multiple visual variants and detect differences.
// Prompt: Apply different custom colors to the same barcode type and compare visual differences programmatically.
// Tags: barcode symbology, color customization, image comparison, qrcode, aspose.barcode, aspose.drawing, pixel analysis

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates QR barcodes with various color schemes, saves them as bitmaps,
/// and compares the images pixel by pixel to highlight visual differences.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates multiple barcode generators with distinct
    /// color configurations, renders them to in‑memory bitmaps, and reports pixel differences.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR code.
        string codeText = "Hello World";

        // Define a list of actions, each configuring a BarcodeGenerator with a different color scheme.
        var schemes = new List<Action<BarcodeGenerator>>
        {
            // Scheme 0: default colors (no modifications).
            gen => { },

            // Scheme 1: red bars on a white background.
            gen =>
            {
                gen.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Red;
                gen.Parameters.BackColor = Aspose.Drawing.Color.White;
            },

            // Scheme 2: blue bars, yellow background, green code text, and purple captions.
            gen =>
            {
                gen.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
                gen.Parameters.BackColor = Aspose.Drawing.Color.Yellow;
                gen.Parameters.Barcode.CodeTextParameters.Color = Aspose.Drawing.Color.Green;
                gen.Parameters.CaptionAbove.Text = "Above";
                gen.Parameters.CaptionAbove.TextColor = Aspose.Drawing.Color.Purple;
                gen.Parameters.CaptionBelow.Text = "Below";
                gen.Parameters.CaptionBelow.TextColor = Aspose.Drawing.Color.Purple;
            }
        };

        // Collection to hold the generated bitmap images.
        var bitmaps = new List<Aspose.Drawing.Bitmap>();

        // Generate a bitmap for each color scheme.
        for (int i = 0; i < schemes.Count; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Apply the current scheme's customizations.
                schemes[i](generator);

                // Save the barcode to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;

                    // Load the PNG into a temporary bitmap, then clone it to keep a persistent copy.
                    using (var tempBmp = new Aspose.Drawing.Bitmap(ms))
                    {
                        var bmpCopy = new Aspose.Drawing.Bitmap(tempBmp);
                        bitmaps.Add(bmpCopy);
                    }
                }
            }
        }

        // Compare each pair of generated bitmaps and output the number of differing pixels.
        for (int i = 0; i < bitmaps.Count; i++)
        {
            for (int j = i + 1; j < bitmaps.Count; j++)
            {
                int diff = CompareBitmaps(bitmaps[i], bitmaps[j]);
                if (diff == -1)
                {
                    Console.WriteLine($"Scheme {i} and Scheme {j} have different dimensions.");
                }
                else
                {
                    Console.WriteLine($"Difference between Scheme {i} and Scheme {j}: {diff} differing pixels.");
                }
            }
        }

        // Release bitmap resources.
        foreach (var bmp in bitmaps)
        {
            bmp.Dispose();
        }
    }

    /// <summary>
    /// Compares two bitmaps pixel by pixel.
    /// Returns -1 if dimensions differ; otherwise returns the count of differing pixels.
    /// </summary>
    /// <param name="bmp1">First bitmap to compare.</param>
    /// <param name="bmp2">Second bitmap to compare.</param>
    /// <returns>Number of differing pixels, or -1 if sizes are mismatched.</returns>
    static int CompareBitmaps(Aspose.Drawing.Bitmap bmp1, Aspose.Drawing.Bitmap bmp2)
    {
        // Ensure both images have the same dimensions before comparison.
        if (bmp1.Width != bmp2.Width || bmp1.Height != bmp2.Height)
            return -1;

        int diffCount = 0;
        // Iterate over each pixel coordinate.
        for (int y = 0; y < bmp1.Height; y++)
        {
            for (int x = 0; x < bmp1.Width; x++)
            {
                var c1 = bmp1.GetPixel(x, y);
                var c2 = bmp2.GetPixel(x, y);
                // Increment count when pixel colors differ.
                if (c1.ToArgb() != c2.ToArgb())
                    diffCount++;
            }
        }
        return diffCount;
    }
}