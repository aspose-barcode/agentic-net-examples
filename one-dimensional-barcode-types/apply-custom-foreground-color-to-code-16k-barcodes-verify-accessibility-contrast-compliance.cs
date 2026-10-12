// Title: Custom foreground color for Code 16K barcode with contrast verification
// Description: Demonstrates how to generate a Code 16K barcode using Aspose.BarCode with a custom blue foreground and white background, then calculates the WCAG contrast ratio to ensure accessibility.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on visual customization and accessibility compliance. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and image saving options, which developers commonly use to tailor barcode appearance and meet accessibility standards.
// Prompt: Apply custom foreground color to Code 16K barcodes, verify accessibility contrast compliance.
// Tags: barcode, code16k, custom color, contrast ratio, accessibility, wcag, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code 16K barcode with a custom foreground color and checking WCAG contrast compliance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates the barcode, computes contrast ratio, and outputs results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code16KDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode content and output file path
        string codeText = "Aspose.Barcode";
        string outputPath = Path.Combine(tempFolder, "Code16K.png");

        // Define custom colors: blue foreground and white background
        Color barColor = Color.FromArgb(0, 0, 255); // Blue foreground
        Color backColor = Color.White; // White background

        // Generate the Code 16K barcode with the specified colors
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f; // Set module size
            generator.Parameters.Barcode.BarColor = barColor;   // Apply foreground color
            generator.Parameters.BackColor = backColor;        // Apply background color
            generator.Save(outputPath, BarCodeImageFormat.Png); // Save as PNG
        }

        // Calculate and display WCAG contrast ratio between foreground and background
        double contrastRatio = CalculateContrastRatio(barColor, backColor);
        Console.WriteLine($"Barcode saved to: {outputPath}");
        Console.WriteLine($"Bar color: RGB({barColor.R},{barColor.G},{barColor.B})");
        Console.WriteLine($"Background color: RGB({backColor.R},{backColor.G},{backColor.B})");
        Console.WriteLine($"Contrast ratio: {contrastRatio:F2}:1");

        // Evaluate contrast against WCAG AA thresholds
        if (contrastRatio >= 4.5)
        {
            Console.WriteLine("Contrast meets WCAG AA requirement for normal text.");
        }
        else if (contrastRatio >= 3.0)
        {
            Console.WriteLine("Contrast meets WCAG AA requirement for large text.");
        }
        else
        {
            Console.WriteLine("Contrast does NOT meet WCAG AA requirements.");
        }

        // Optional cleanup: delete temporary folder
        // Directory.Delete(tempFolder, true);
    }

    // Calculates the contrast ratio between two colors using the WCAG formula
    static double CalculateContrastRatio(Color fore, Color back)
    {
        double lumFore = GetRelativeLuminance(fore);
        double lumBack = GetRelativeLuminance(back);
        double lighter = Math.Max(lumFore, lumBack);
        double darker = Math.Min(lumFore, lumBack);
        return (lighter + 0.05) / (darker + 0.05);
    }

    // Computes the relative luminance of a color per WCAG specifications
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