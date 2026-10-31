// Title: Apply Custom Foreground Color to QR Code Using HSL Values
// Description: Demonstrates how to convert HSL color values to an Aspose.Drawing.Color and apply it as the barcode's foreground color.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating color customization of barcodes. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create a QR code with a specific brand color. Developers often need to match corporate branding by setting bar and background colors, and this snippet shows the typical workflow for such customizations.
// Prompt: Apply a custom foreground color using HSL values to achieve a specific branding shade.
// Tags: qr code, color customization, hsl, aspose.barcode, barcode generation, png output, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a QR code with a custom foreground color derived from HSL values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Converts HSL to RGB, configures barcode colors, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define HSL values for the branding shade
        float hue = 210f;          // Hue: 0‑360 degrees
        float saturation = 0.70f;  // Saturation: 0‑1
        float lightness = 0.50f;   // Lightness: 0‑1

        // Convert HSL to an Aspose.Drawing.Color instance
        Color brandingColor = ColorFromHsl(hue, saturation, lightness);

        // Build the full output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "custom_color_barcode.png");

        // Create a QR code generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Branding"))
        {
            // Apply the custom foreground (bar) color
            generator.Parameters.Barcode.BarColor = brandingColor;

            // Set the background color to white for contrast
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Converts HSL color components to an Aspose.Drawing.Color.
    /// </summary>
    /// <param name="h">Hue component (0‑360).</param>
    /// <param name="s">Saturation component (0‑1).</param>
    /// <param name="l">Lightness component (0‑1).</param>
    /// <returns>A Color representing the equivalent RGB value.</returns>
    static Color ColorFromHsl(float h, float s, float l)
    {
        // Normalize hue to the range [0,360)
        h = h % 360f;
        if (h < 0) h += 360f;

        // Compute chroma, secondary component, and match value
        float c = (1f - Math.Abs(2f * l - 1f)) * s;
        float hPrime = h / 60f;
        float x = c * (1f - Math.Abs(hPrime % 2f - 1f));

        // Determine preliminary RGB values based on hue sector
        float r1 = 0f, g1 = 0f, b1 = 0f;
        if (0f <= hPrime && hPrime < 1f)
        {
            r1 = c; g1 = x; b1 = 0f;
        }
        else if (1f <= hPrime && hPrime < 2f)
        {
            r1 = x; g1 = c; b1 = 0f;
        }
        else if (2f <= hPrime && hPrime < 3f)
        {
            r1 = 0f; g1 = c; b1 = x;
        }
        else if (3f <= hPrime && hPrime < 4f)
        {
            r1 = 0f; g1 = x; b1 = c;
        }
        else if (4f <= hPrime && hPrime < 5f)
        {
            r1 = x; g1 = 0f; b1 = c;
        }
        else if (5f <= hPrime && hPrime < 6f)
        {
            r1 = c; g1 = 0f; b1 = x;
        }

        // Add the match value to shift RGB into the correct range
        float m = l - c / 2f;
        int r = (int)Math.Round((r1 + m) * 255f);
        int g = (int)Math.Round((g1 + m) * 255f);
        int b = (int)Math.Round((b1 + m) * 255f);

        // Return the final color
        return Color.FromArgb(r, g, b);
    }
}