// Title: Apply custom foreground color to Code 16K barcode and verify contrast
// Description: Demonstrates generating a Code 16K barcode with a custom foreground color, saving it as PNG, and checking WCAG contrast compliance.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class, set bar and background colors, and evaluate color contrast for accessibility. Developers working with barcode rendering often need to adjust visual styles to match branding while ensuring readability for users with visual impairments.
// Prompt: Apply custom foreground color to Code 16K barcodes, verify accessibility contrast compliance.
// Tags: barcode, code16k, custom color, contrast ratio, accessibility, aspose.barcode, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code 16K barcode with a custom foreground color,
/// saves it as a PNG image, and verifies that the color contrast
/// meets WCAG accessibility guidelines.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder,
    /// defines colors, generates the barcode, saves the image,
    /// and prints contrast analysis results to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output folder for the generated image
        string outputFolder = Path.Combine(Path.GetTempPath(), "Code16KExample");
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "Code16K_CustomColor.png");

        // Define the foreground (barcode) and background colors
        Color foregroundColor = Color.FromArgb(255, 255, 0, 0); // Red
        Color backgroundColor = Color.FromArgb(255, 255, 255, 255); // White

        // Generate the Code 16K barcode with the specified colors
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K, "Aspose.Barcode"))
        {
            // Set module size (pixel width of the smallest bar)
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Apply custom colors
            generator.Parameters.Barcode.BarColor = foregroundColor;
            generator.Parameters.BackColor = backgroundColor;
            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Compute the contrast ratio between foreground and background colors
        double contrast = ComputeContrastRatio(foregroundColor, backgroundColor);

        // Output results
        Console.WriteLine($"Barcode saved to: {outputPath}");
        Console.WriteLine($"Foreground color: {foregroundColor}");
        Console.WriteLine($"Background color: {backgroundColor}");
        Console.WriteLine($"Contrast ratio: {contrast:F2}:1");

        const double MinimumContrast = 4.5; // WCAG AA requirement for normal text
        if (contrast >= MinimumContrast)
        {
            Console.WriteLine("Contrast meets accessibility requirements.");
        }
        else
        {
            Console.WriteLine("Contrast does NOT meet accessibility requirements.");
        }
    }

    // Calculates the contrast ratio according to WCAG guidelines
    static double ComputeContrastRatio(Color fore, Color back)
    {
        double lumFore = RelativeLuminance(fore);
        double lumBack = RelativeLuminance(back);
        double lighter = Math.Max(lumFore, lumBack);
        double darker = Math.Min(lumFore, lumBack);
        return (lighter + 0.05) / (darker + 0.05);
    }

    // Computes the relative luminance of a color (sRGB)
    static double RelativeLuminance(Color color)
    {
        // Convert ARGB components to linear RGB (range 0..1)
        double R = Linearize(color.R / 255.0);
        double G = Linearize(color.G / 255.0);
        double B = Linearize(color.B / 255.0);
        return 0.2126 * R + 0.7152 * G + 0.0722 * B;
    }

    // Linearizes a single sRGB channel value
    static double Linearize(double channel)
    {
        return channel <= 0.03928 ? channel / 12.92 : Math.Pow((channel + 0.055) / 1.055, 2.4);
    }
}