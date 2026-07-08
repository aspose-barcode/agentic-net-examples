// Title: Apply custom foreground color to Code 16K barcode and verify contrast
// Description: Demonstrates setting a dark blue bar color on a Code 16K barcode, checking WCAG contrast against a white background, and saving the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize barcode appearance using BarcodeGenerator, set bar and background colors, adjust image size, and evaluate accessibility contrast. Developers creating branded or accessible barcodes often need to modify colors while ensuring compliance with WCAG guidelines.
// Prompt: Apply custom foreground color to Code 16K barcodes, verify accessibility contrast compliance.
// Tags: barcode, code16k, color, contrast, accessibility, aspnet, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code 16K barcode with a custom foreground color,
/// evaluates its contrast against the background for accessibility,
/// and saves the resulting image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Sets up colors, checks contrast,
    /// configures the barcode generator, and saves the image.
    /// </summary>
    static void Main()
    {
        // Sample codetext for Code 16K
        const string codeText = "1234567890123456";

        // Define custom foreground (bar) color and background color
        Color barColor = Color.FromArgb(0, 0, 139); // Dark blue
        Color backColor = Color.White;

        // Verify contrast ratio (WCAG AA minimum 4.5:1 for normal text)
        double contrast = GetContrastRatio(barColor, backColor);
        Console.WriteLine($"Contrast ratio between bar color and background: {contrast:F2}:1");
        if (contrast >= 4.5)
            Console.WriteLine("Contrast OK.");
        else
            Console.WriteLine("Warning: Contrast may not meet accessibility guidelines.");

        // Generate the barcode with the specified colors
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Apply custom colors
            generator.Parameters.Barcode.BarColor = barColor;
            generator.Parameters.BackColor = backColor;

            // Optional: set image size via interpolation mode
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode image
            const string outputPath = "code16k.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }

    // Calculates the WCAG contrast ratio between two colors
    static double GetContrastRatio(Color c1, Color c2)
    {
        double L1 = GetRelativeLuminance(c1);
        double L2 = GetRelativeLuminance(c2);
        // Ensure L1 is the lighter luminance
        if (L1 < L2)
        {
            double temp = L1;
            L1 = L2;
            L2 = temp;
        }
        return (L1 + 0.05) / (L2 + 0.05);
    }

    // Computes the relative luminance of a color per WCAG definition
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