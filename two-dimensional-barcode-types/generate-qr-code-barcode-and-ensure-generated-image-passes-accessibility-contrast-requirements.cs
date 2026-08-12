// Title: Generate QR Code with High Contrast for Accessibility
// Description: Creates a QR Code barcode, applies black‑on‑white colors to meet WCAG contrast requirements, and saves it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to configure QR Code parameters, set foreground and background colors, and verify accessibility contrast using the BarcodeGenerator, QRErrorLevel, and color utilities. Typical use cases include producing scannable QR codes for web links or product information while ensuring the output complies with accessibility standards. Developers often need to adjust colors and validate contrast ratios for inclusive design.
// Prompt: Generate QR Code barcode and ensure generated image passes accessibility contrast requirements.
// Tags: qr code, barcode generation, png, contrast, accessibility, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code barcode with maximum contrast (black on white) to satisfy
/// WCAG accessibility guidelines, and saving the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code, checks contrast, and writes the image to a temporary file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output file path in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_contrast.png");

        // Text to encode in the QR Code (e.g., a URL).
        string codeText = "https://example.com";

        // Initialize the QR Code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Use the highest error correction level for maximum robustness.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set foreground (barcode) and background colors to achieve maximum contrast.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Compute the contrast ratio between the chosen colors.
            double contrast = ComputeContrastRatio(generator.Parameters.Barcode.BarColor, generator.Parameters.BackColor);

            // If the contrast is below the WCAG AA threshold (4.5:1), enforce black on white.
            if (contrast < 4.5)
            {
                Console.WriteLine($"Contrast ratio {contrast:F2} is below 4.5. Adjusting to black/white.");
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;
            }

            // Save the generated QR Code as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR code saved to: {outputPath}");
    }

    // Calculates the contrast ratio between two colors according to WCAG guidelines.
    static double ComputeContrastRatio(Color fore, Color back)
    {
        double lumFore = RelativeLuminance(fore);
        double lumBack = RelativeLuminance(back);

        // Ensure lumFore represents the lighter color for correct ratio calculation.
        if (lumFore < lumBack)
        {
            double temp = lumFore;
            lumFore = lumBack;
            lumBack = temp;
        }

        return (lumFore + 0.05) / (lumBack + 0.05);
    }

    // Computes the relative luminance of an sRGB color per WCAG specifications.
    static double RelativeLuminance(Color color)
    {
        double r = color.R / 255.0;
        double g = color.G / 255.0;
        double b = color.B / 255.0;

        r = (r <= 0.03928) ? r / 12.92 : Math.Pow((r + 0.055) / 1.055, 2.4);
        g = (g <= 0.03928) ? g / 12.92 : Math.Pow((g + 0.055) / 1.055, 2.4);
        b = (b <= 0.03928) ? b / 12.92 : Math.Pow((b + 0.055) / 1.055, 2.4);

        return 0.2126 * r + 0.7152 * g + 0.0722 * b;
    }
}