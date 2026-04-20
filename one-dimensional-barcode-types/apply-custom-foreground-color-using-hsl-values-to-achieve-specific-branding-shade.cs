using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    // Convert HSL values to an Aspose.Drawing.Color.
    static Color ColorFromHSL(float hue, float saturation, float lightness)
    {
        // Normalize hue to [0,1]
        float h = hue / 360f;
        float s = saturation;
        float l = lightness;

        float r, g, b;

        if (s == 0f)
        {
            r = g = b = l; // Achromatic
        }
        else
        {
            float q = l < 0.5f ? l * (1f + s) : l + s - l * s;
            float p = 2f * l - q;

            r = HueToRGB(p, q, h + 1f / 3f);
            g = HueToRGB(p, q, h);
            b = HueToRGB(p, q, h - 1f / 3f);
        }

        int rByte = (int)Math.Round(r * 255f);
        int gByte = (int)Math.Round(g * 255f);
        int bByte = (int)Math.Round(b * 255f);

        return Color.FromArgb(rByte, gByte, bByte);
    }

    static float HueToRGB(float p, float q, float t)
    {
        if (t < 0f) t += 1f;
        if (t > 1f) t -= 1f;
        if (t < 1f / 6f) return p + (q - p) * 6f * t;
        if (t < 1f / 2f) return q;
        if (t < 2f / 3f) return p + (q - p) * (2f / 3f - t) * 6f;
        return p;
    }

    static void Main()
    {
        // Branding shade defined by HSL values.
        float hue = 210f;          // Example hue (0-360)
        float saturation = 0.75f;  // Example saturation (0-1)
        float lightness = 0.40f;   // Example lightness (0-1)

        Color brandingColor = ColorFromHSL(hue, saturation, lightness);

        // Create a barcode generator for Code128.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "Brand123";
            // Apply the custom foreground (bars) color.
            generator.Parameters.Barcode.BarColor = brandingColor;

            // Save the barcode image.
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode generated with custom foreground color.");
    }
}