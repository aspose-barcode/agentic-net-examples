// Title: Apply Custom HSL Foreground Color to a Code128 Barcode
// Description: Demonstrates how to convert HSL values to an Aspose.Drawing.Color and apply it as the foreground bar color of a Code128 barcode, then save the image as PNG.
// Category-Description: This example belongs to the barcode appearance customization category of Aspose.BarCode. It shows how to use the BarcodeGenerator class together with the Parameters.Barcode.BarColor property to modify bar colors, and how to set background colors. Developers often need to match corporate branding by applying specific colors using standard color models such as HSL.
// Prompt: Apply a custom foreground color using HSL values to achieve a specific branding shade.
// Tags: code128, color, png, barcodegenerator, parameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates applying a custom foreground color defined by HSL values to a barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Converts HSL (Hue 0‑360, Saturation 0‑1, Lightness 0‑1) to an Aspose.Drawing.Color.
    /// </summary>
    /// <param name="hue">Hue component in degrees.</param>
    /// <param name="saturation">Saturation component (0‑1).</param>
    /// <param name="lightness">Lightness component (0‑1).</param>
    /// <returns>Corresponding Color instance.</returns>
    static Color ColorFromHsl(float hue, float saturation, float lightness)
    {
        // Normalize hue to the range [0,360)
        hue = hue % 360f;
        if (hue < 0) hue += 360f;

        float c = (1f - Math.Abs(2f * lightness - 1f)) * saturation;
        float hPrime = hue / 60f;
        float x = c * (1f - Math.Abs(hPrime % 2f - 1f));

        float r1 = 0, g1 = 0, b1 = 0;
        if (0 <= hPrime && hPrime < 1)
        {
            r1 = c; g1 = x; b1 = 0;
        }
        else if (1 <= hPrime && hPrime < 2)
        {
            r1 = x; g1 = c; b1 = 0;
        }
        else if (2 <= hPrime && hPrime < 3)
        {
            r1 = 0; g1 = c; b1 = x;
        }
        else if (3 <= hPrime && hPrime < 4)
        {
            r1 = 0; g1 = x; b1 = c;
        }
        else if (4 <= hPrime && hPrime < 5)
        {
            r1 = x; g1 = 0; b1 = c;
        }
        else if (5 <= hPrime && hPrime < 6)
        {
            r1 = c; g1 = 0; b1 = x;
        }

        float m = lightness - c / 2f;
        int r = (int)Math.Round((r1 + m) * 255f);
        int g = (int)Math.Round((g1 + m) * 255f);
        int b = (int)Math.Round((b1 + m) * 255f);

        // Clamp RGB values to valid byte range
        r = Math.Clamp(r, 0, 255);
        g = Math.Clamp(g, 0, 255);
        b = Math.Clamp(b, 0, 255);

        return Color.FromArgb(r, g, b);
    }

    /// <summary>
    /// Generates a Code128 barcode with a custom HSL foreground color and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define branding shade using HSL (example: hue=210°, saturation=0.75, lightness=0.4)
        float hue = 210f;          // degrees
        float saturation = 0.75f;  // 0..1
        float lightness = 0.40f;   // 0..1

        // Convert HSL to a Color object usable by Aspose.BarCode
        Color brandingColor = ColorFromHsl(hue, saturation, lightness);

        // Initialize a Code128 barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "BRAND123"))
        {
            // Apply the custom foreground (bar) color
            generator.Parameters.Barcode.BarColor = brandingColor;

            // Optional: set a white background for contrast
            generator.Parameters.BackColor = Color.White;

            // Define output path and save the barcode image as PNG
            string outputPath = "custom_color_barcode.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);

            Console.WriteLine($"Barcode saved to {outputPath} with custom color (HSL {hue}, {saturation}, {lightness}).");
        }
    }
}