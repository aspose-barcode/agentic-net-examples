// Title: Apply custom foreground color using HSL values for branding
// Description: Demonstrates converting HSL color values to Aspose.Drawing.Color and applying the result as the barcode foreground (bar) color.
// Category-Description: This example belongs to the Aspose.BarCode color customization category, showing how to use BarcodeGenerator and its Parameters.Barcode.BarColor property to match corporate branding. Developers often need to adjust bar and background colors for brand consistency, and this snippet illustrates the typical workflow using Aspose.BarCode and Aspose.Drawing APIs.
// Prompt: Apply a custom foreground color using HSL values to achieve a specific branding shade.
// Tags: code128, color, png, barcodegenerator, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates applying a custom foreground color to a barcode using HSL values.
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
    static Color HslToColor(float hue, float saturation, float lightness)
    {
        // Normalize hue to the range [0,360)
        hue = hue % 360f;
        if (hue < 0) hue += 360f;

        // Compute chroma, intermediate value, and second largest component
        float c = (1f - Math.Abs(2f * lightness - 1f)) * saturation;
        float hPrime = hue / 60f;
        float x = c * (1f - Math.Abs(hPrime % 2f - 1f));

        // Determine temporary RGB values based on hue sector
        float r1 = 0f, g1 = 0f, b1 = 0f;
        if (0f <= hPrime && hPrime < 1f)      { r1 = c; g1 = x; b1 = 0f; }
        else if (1f <= hPrime && hPrime < 2f){ r1 = x; g1 = c; b1 = 0f; }
        else if (2f <= hPrime && hPrime < 3f){ r1 = 0f; g1 = c; b1 = x; }
        else if (3f <= hPrime && hPrime < 4f){ r1 = 0f; g1 = x; b1 = c; }
        else if (4f <= hPrime && hPrime < 5f){ r1 = x; g1 = 0f; b1 = c; }
        else if (5f <= hPrime && hPrime < 6f){ r1 = c; g1 = 0f; b1 = x; }

        // Add match value to shift RGB into correct lightness
        float m = lightness - c / 2f;
        int r = (int)Math.Round((r1 + m) * 255f);
        int g = (int)Math.Round((g1 + m) * 255f);
        int b = (int)Math.Round((b1 + m) * 255f);

        // Return the final color
        return Color.FromArgb(r, g, b);
    }

    /// <summary>
    /// Entry point. Generates a Code128 barcode with a branding color derived from HSL values and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define branding shade using HSL (example: hue=210°, saturation=0.75, lightness=0.4)
        float hue = 210f;          // degrees
        float saturation = 0.75f;  // 0..1
        float lightness = 0.40f;   // 0..1

        // Convert HSL to a Color object usable by Aspose.Drawing
        Color brandingColor = HslToColor(hue, saturation, lightness);

        // Initialize barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Brand123"))
        {
            // Apply the custom foreground (bar) color
            generator.Parameters.Barcode.BarColor = brandingColor;

            // Define output file path and save as PNG
            string outputPath = "branding_barcode.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}