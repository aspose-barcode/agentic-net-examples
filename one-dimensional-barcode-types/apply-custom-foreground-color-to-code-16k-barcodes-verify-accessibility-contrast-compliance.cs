// Title: Custom Foreground Color for Code 16K Barcode with Contrast Check
// Description: Demonstrates how to generate a Code 16K barcode using Aspose.BarCode with a custom dark‑blue foreground color, save it as PNG, and evaluate WCAG contrast against a white background.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image format settings to customize visual appearance. Typical use cases include branding, UI integration, and accessibility compliance where developers need to adjust bar colors and verify contrast ratios per WCAG guidelines.
// Prompt: Apply custom foreground color to Code 16K barcodes, verify accessibility contrast compliance.
// Tags: barcode, code16k, custom color, contrast, accessibility, wcag, aspose.barcode, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code 16K barcode with a custom foreground color,
/// saves it as a PNG image, and checks WCAG contrast compliance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode,
    /// saves the image, and reports contrast information.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "Code16KExample");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "Code16K_CustomColor.png");

        // Define barcode content and visual colors
        string codeText = "Aspose.BarCode";
        Color barColor = Color.FromArgb(0, 0, 139); // Dark blue foreground
        Color backColor = Color.White;             // White background

        // Generate Code 16K barcode with the specified colors
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f; // Set module size
            generator.Parameters.Barcode.BarColor = barColor;   // Apply custom bar color
            generator.Parameters.BackColor = backColor;         // Apply background color
            generator.Save(outputPath, BarCodeImageFormat.Png); // Save as PNG
        }

        // Verify accessibility contrast compliance using WCAG formula
        double contrastRatio = CalculateContrastRatio(barColor, backColor);
        bool meetsAA = contrastRatio >= 4.5; // WCAG AA threshold for normal text

        Console.WriteLine($"Barcode saved to: {outputPath}");
        Console.WriteLine($"Contrast ratio (BarColor vs Background): {contrastRatio:F2}");
        Console.WriteLine($"Meets WCAG AA requirement (≥4.5): {meetsAA}");
    }

    // Calculates WCAG contrast ratio between two colors
    static double CalculateContrastRatio(Color fore, Color back)
    {
        double L1 = GetRelativeLuminance(fore);
        double L2 = GetRelativeLuminance(back);
        double lighter = Math.Max(L1, L2);
        double darker = Math.Min(L1, L2);
        return (lighter + 0.05) / (darker + 0.05);
    }

    // Computes relative luminance per WCAG definition
    static double GetRelativeLuminance(Color color)
    {
        double RsRGB = color.R / 255.0;
        double GsRGB = color.G / 255.0;
        double BsRGB = color.B / 255.0;

        double R = RsRGB <= 0.03928 ? RsRGB / 12.92 : Math.Pow((RsRGB + 0.055) / 1.055, 2.4);
        double G = GsRGB <= 0.03928 ? GsRGB / 12.92 : Math.Pow((GsRGB + 0.055) / 1.055, 2.4);
        double B = BsRGB <= 0.03928 ? BsRGB / 12.92 : Math.Pow((BsRGB + 0.055) / 1.055, 2.4);

        return 0.2126 * R + 0.7152 * G + 0.0722 * B;
    }
}