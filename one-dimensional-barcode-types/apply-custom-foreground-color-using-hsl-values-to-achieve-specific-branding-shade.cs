// Title: Apply custom HSL foreground color to a QR barcode
// Description: Demonstrates how to convert HSL values to an RGB color and apply it as the foreground color of a QR barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode color customization category, showing how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to generate barcodes with brand-specific colors. Developers often need to match corporate branding by adjusting barcode colors, and this snippet illustrates the typical workflow for applying custom colors via the BarColor property.
// Prompt: Apply a custom foreground color using HSL values to achieve a specific branding shade.
// Tags: qr, color, png, barcodegenerator, encodetypes, barcodeimageformat, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeColorExample
{
    /// <summary>
    /// Demonstrates applying a custom HSL-based foreground color to a QR barcode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates a QR barcode with a branding color and saves it as PNG.
        /// </summary>
        static void Main()
        {
            // Define HSL values for the branding shade
            float hue = 210f;            // example hue (0-360)
            float saturation = 0.75f;    // example saturation (0-1)
            float lightness = 0.40f;     // example lightness (0-1)

            // Convert HSL to an Aspose.Drawing.Color instance
            Color brandingColor = ColorFromHsl(hue, saturation, lightness);

            // Determine the output file path for the generated barcode image
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "branding_barcode.png");

            // Create a QR barcode generator with the desired text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Branding"))
            {
                // Apply the custom foreground color to the barcode
                generator.Parameters.Barcode.BarColor = brandingColor;

                // Save the barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }

        // Converts HSL values to an Aspose.Drawing.Color object
        private static Color ColorFromHsl(float h, float s, float l)
        {
            // Normalize hue to the range [0,360)
            h = h % 360f;
            if (h < 0) h += 360f;

            // Clamp saturation and lightness to the range [0,1]
            s = Math.Clamp(s, 0f, 1f);
            l = Math.Clamp(l, 0f, 1f);

            // Compute chroma, intermediate value, and second largest component
            float c = (1f - Math.Abs(2f * l - 1f)) * s;
            float hPrime = h / 60f;
            float x = c * (1f - Math.Abs(hPrime % 2f - 1f));

            // Determine preliminary RGB values based on hue sector
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

            // Add match value to shift RGB components into the correct range
            float m = l - c / 2f;
            int r = (int)Math.Round((r1 + m) * 255f);
            int g = (int)Math.Round((g1 + m) * 255f);
            int b = (int)Math.Round((b1 + m) * 255f);

            // Return the final color
            return Color.FromArgb(r, g, b);
        }
    }
}