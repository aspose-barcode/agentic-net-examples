// Title: Apply custom foreground and background colors to a Code 16K barcode
// Description: Demonstrates setting custom bar and background colors for a Code 16K barcode and checks WCAG contrast compliance.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize visual appearance using BarcodeGenerator, EncodeTypes, and color properties. Typical use cases include branding, accessibility compliance, and UI integration where developers need to ensure sufficient contrast between barcode foreground and background colors.
// Prompt: Apply custom foreground color to Code 16K barcodes, verify accessibility contrast compliance.
// Tags: barcode, code16k, color, contrast, accessibility, wcag, aspose.barcode, generation, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates applying custom foreground and background colors to a Code 16K barcode
/// and verifying WCAG contrast compliance.
/// </summary>
class Program
{
    // Calculates the relative luminance of a color according to WCAG.
    static double GetLuminance(Color color)
    {
        // Convert sRGB components (0‑255) to linear values (0‑1)
        double RsRGB = color.R / 255.0;
        double GsRGB = color.G / 255.0;
        double BsRGB = color.B / 255.0;

        double R = RsRGB <= 0.03928 ? RsRGB / 12.92 : Math.Pow((RsRGB + 0.055) / 1.055, 2.4);
        double G = GsRGB <= 0.03928 ? GsRGB / 12.92 : Math.Pow((GsRGB + 0.055) / 1.055, 2.4);
        double B = BsRGB <= 0.03928 ? BsRGB / 12.92 : Math.Pow((BsRGB + 0.055) / 1.055, 2.4);

        // Relative luminance formula
        return 0.2126 * R + 0.7152 * G + 0.0722 * B;
    }

    // Returns the contrast ratio between two colors.
    static double GetContrastRatio(Color fore, Color back)
    {
        double L1 = GetLuminance(fore);
        double L2 = GetLuminance(back);
        // Ensure L1 is the lighter luminance
        if (L2 > L1)
        {
            double temp = L1;
            L1 = L2;
            L2 = temp;
        }
        return (L1 + 0.05) / (L2 + 0.05);
    }

    /// <summary>
    /// Entry point. Calculates contrast ratio, outputs result, generates barcode with custom colors.
    /// </summary>
    static void Main()
    {
        // Define custom colors (example: dark blue foreground on light yellow background)
        Color foreground = Color.FromArgb(0, 0, 139);   // DarkBlue
        Color background = Color.FromArgb(255, 255, 224); // LightYellow

        // Verify accessibility contrast (WCAG AA requires >= 4.5 for normal text)
        double contrast = GetContrastRatio(foreground, background);
        Console.WriteLine($"Contrast ratio: {contrast:F2}:1");
        if (contrast >= 4.5)
            Console.WriteLine("Contrast meets WCAG AA requirements.");
        else
            Console.WriteLine("Contrast does NOT meet WCAG AA requirements.");

        // Create a Code 16K barcode with custom colors
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "1234567890123456"))
        {
            // Apply colors
            generator.Parameters.Barcode.BarColor = foreground; // foreground (bars)
            generator.Parameters.BackColor = background;       // background

            // Save the barcode image as PNG
            generator.Save("code16k.png");
        }

        // Program ends normally
    }
}