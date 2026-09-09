// Title: Apply custom foreground color to barcode using HSL values
// Description: Demonstrates how to generate a Code128 barcode and set its foreground color using HSL values to match a branding shade.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to customize barcode appearance with the BarcodeGenerator class. It covers setting the BarColor property, converting HSL to RGB, and saving the image. Developers often need to match corporate branding or design guidelines when creating barcodes for print or digital media.
// Prompt: Apply a custom foreground color using HSL values to achieve a specific branding shade.
// Tags: barcode, code128, color, hsl, branding, aspose.barcode, generation, png, custom color

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace CustomBarcodeColorExample
{
    /// <summary>
    /// Demonstrates applying a custom foreground color to a barcode using HSL values.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates a Code128 barcode with a branding color and saves it as PNG.
        /// </summary>
        static void Main()
        {
            // Define the barcode data and symbology
            string codeText = "1234567890";
            BaseEncodeType encodeType = EncodeTypes.Code128;

            // Branding color expressed in HSL (example: hue 210°, saturation 0.75, lightness 0.4)
            float hue = 210f;          // 0‑360 degrees
            float saturation = 0.75f;  // 0‑1 range
            float lightness = 0.40f;   // 0‑1 range

            // Convert HSL to an Aspose.Drawing.Color (RGB)
            Color brandingColor = ColorFromHsl(hue, saturation, lightness);

            // Determine a temporary file path for the output PNG
            string outputPath = Path.Combine(Path.GetTempPath(), "custom_barcode.png");

            // Create the barcode generator with the specified symbology and data
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply the custom foreground color
                generator.Parameters.Barcode.BarColor = brandingColor;

                // Generate the barcode image and save it as PNG
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    bitmap.Save(outputPath, ImageFormat.Png);
                }
            }

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }

        // Converts HSL values to an Aspose.Drawing.Color (RGB)
        private static Color ColorFromHsl(float h, float s, float l)
        {
            // Normalize hue to [0,360)
            h = h % 360f;
            if (h < 0) h += 360f;

            // Clamp saturation and lightness to [0,1]
            s = Math.Clamp(s, 0f, 1f);
            l = Math.Clamp(l, 0f, 1f);

            // Compute chroma
            float c = (1f - Math.Abs(2f * l - 1f)) * s;
            float hPrime = h / 60f;
            float x = c * (1f - Math.Abs(hPrime % 2f - 1f));

            // Determine intermediate RGB values
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

            // Add match value to shift from chroma to lightness
            float m = l - c / 2f;
            int r = (int)Math.Round((r1 + m) * 255f);
            int g = (int)Math.Round((g1 + m) * 255f);
            int b = (int)Math.Round((b1 + m) * 255f);

            // Return the final ARGB color (fully opaque)
            return Color.FromArgb(255, r, g, b);
        }
    }
}