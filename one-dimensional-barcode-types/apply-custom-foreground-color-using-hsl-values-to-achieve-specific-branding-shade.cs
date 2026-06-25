using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode with a custom color derived from HSL values.
/// </summary>
class Program
{
    /// <summary>
    /// Converts HSL values to an <see cref="Aspose.Drawing.Color"/>.
    /// </summary>
    /// <param name="h">Hue component (0‑360).</param>
    /// <param name="s">Saturation component (0‑1).</param>
    /// <param name="l">Lightness component (0‑1).</param>
    /// <returns>A <see cref="Color"/> representing the specified HSL color.</returns>
    static Color ColorFromHsl(float h, float s, float l)
    {
        // Normalize hue to the range [0,360)
        h = h % 360f;
        if (h < 0f) h += 360f;

        // Compute chroma, second largest component, and match value
        float c = (1f - Math.Abs(2f * l - 1f)) * s;
        float x = c * (1f - Math.Abs((h / 60f) % 2f - 1f));
        float m = l - c / 2f;

        // Initialize RGB prime components
        float r1 = 0f, g1 = 0f, b1 = 0f;

        // Determine which sector of the color wheel hue falls into
        if (h < 60f)
        {
            r1 = c; g1 = x; b1 = 0f;
        }
        else if (h < 120f)
        {
            r1 = x; g1 = c; b1 = 0f;
        }
        else if (h < 180f)
        {
            r1 = 0f; g1 = c; b1 = x;
        }
        else if (h < 240f)
        {
            r1 = 0f; g1 = x; b1 = c;
        }
        else if (h < 300f)
        {
            r1 = x; g1 = 0f; b1 = c;
        }
        else
        {
            r1 = c; g1 = 0f; b1 = x;
        }

        // Convert from [0,1] range to 0‑255 integer values
        int r = (int)Math.Round((r1 + m) * 255.0);
        int g = (int)Math.Round((g1 + m) * 255.0);
        int b = (int)Math.Round((b1 + m) * 255.0);

        // Return the final color
        return Color.FromArgb(r, g, b);
    }

    /// <summary>
    /// Entry point. Generates a Code128 barcode using a brand color defined in HSL.
    /// </summary>
    static void Main()
    {
        // Define brand color in HSL (example: hue 210°, saturation 0.65, lightness 0.45)
        float hue = 210f;          // degrees
        float saturation = 0.65f;  // 0‑1
        float lightness = 0.45f;   // 0‑1

        // Convert HSL to an Aspose.Drawing.Color
        Color brandingColor = ColorFromHsl(hue, saturation, lightness);

        // Create a Code128 barcode with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Brand123"))
        {
            // Apply the custom foreground (bar) color
            generator.Parameters.Barcode.BarColor = brandingColor;

            // Save the barcode image to a file
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("Barcode generated with custom HSL color and saved as 'barcode.png'.");
    }
}