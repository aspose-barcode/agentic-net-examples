using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code16K barcode with custom colors
/// and checking WCAG contrast compliance.
/// </summary>
class Program
{
    /// <summary>
    /// Computes the relative luminance of a color (range 0..1).
    /// </summary>
    /// <param name="color">The color to evaluate.</param>
    /// <returns>Luminance value.</returns>
    static double GetLuminance(Color color)
    {
        // Normalize RGB components to 0..1 range
        double R = color.R / 255.0;
        double G = color.G / 255.0;
        double B = color.B / 255.0;

        // Convert sRGB component to linear value
        double Linear(double c)
        {
            return c <= 0.03928 ? c / 12.92 : Math.Pow((c + 0.055) / 1.055, 2.4);
        }

        double r = Linear(R);
        double g = Linear(G);
        double b = Linear(B);

        // Apply luminance coefficients
        return 0.2126 * r + 0.7152 * g + 0.0722 * b;
    }

    /// <summary>
    /// Computes the contrast ratio between two colors.
    /// </summary>
    /// <param name="fore">Foreground color.</param>
    /// <param name="back">Background color.</param>
    /// <returns>Contrast ratio.</returns>
    static double GetContrastRatio(Color fore, Color back)
    {
        double L1 = GetLuminance(fore);
        double L2 = GetLuminance(back);
        double lighter = Math.Max(L1, L2);
        double darker = Math.Min(L1, L2);
        return (lighter + 0.05) / (darker + 0.05);
    }

    /// <summary>
    /// Entry point of the program. Generates a barcode, checks contrast,
    /// and saves the image to disk.
    /// </summary>
    static void Main()
    {
        // Define custom foreground (bar) color and background color
        Color barColor = Color.FromArgb(0, 102, 204); // a shade of blue
        Color backColor = Color.White; // default background

        // Verify accessibility contrast (WCAG AA requires >= 4.5:1 for normal text)
        double contrast = GetContrastRatio(barColor, backColor);
        Console.WriteLine($"Contrast ratio (foreground/background): {contrast:F2}:1");
        if (contrast >= 4.5)
            Console.WriteLine("Contrast meets WCAG AA requirement.");
        else
            Console.WriteLine("Contrast does NOT meet WCAG AA requirement.");

        // Prepare barcode data and output path
        string codeText = "1234567890123456789012345678901234567890"; // sample data
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "code16k.png");

        // Generate Code16K barcode with the specified colors
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Apply custom colors to the barcode and background
            generator.Parameters.Barcode.BarColor = barColor;
            generator.Parameters.BackColor = backColor;

            // Optional: adjust aspect ratio if needed
            // generator.Parameters.Barcode.Code16K.AspectRatio = 2.0f;

            // Save the barcode image to the file system
            generator.Save(outputPath);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}