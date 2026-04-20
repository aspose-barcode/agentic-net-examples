using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define foreground (bar) and background colors
        Color barColor = Color.FromArgb(0, 0, 139); // DarkBlue
        Color backColor = Color.White;

        // Verify WCAG contrast ratio
        double contrast = CalculateContrastRatio(barColor, backColor);
        const double MinimumContrast = 4.5; // Minimum for normal text
        if (contrast < MinimumContrast)
        {
            Console.WriteLine($"Warning: Contrast ratio {contrast:F2}:1 is below the recommended {MinimumContrast}:1.");
        }
        else
        {
            Console.WriteLine($"Contrast ratio {contrast:F2}:1 meets the recommended threshold.");
        }

        // Generate Code 16K barcode with custom colors
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "12345678901234567890"))
        {
            generator.Parameters.Barcode.BarColor = barColor;
            generator.Parameters.BackColor = backColor;

            // Optional: adjust size
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save the barcode image
            generator.Save("code16k.png");
        }

        Console.WriteLine("Barcode image saved as code16k.png");
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

    // Computes relative luminance of a color per WCAG
    static double GetRelativeLuminance(Color color)
    {
        double R = Linearize(color.R / 255.0);
        double G = Linearize(color.G / 255.0);
        double B = Linearize(color.B / 255.0);
        return 0.2126 * R + 0.7152 * G + 0.0722 * B;
    }

    // Linearizes an sRGB component
    static double Linearize(double channel)
    {
        return channel <= 0.03928
            ? channel / 12.92
            : Math.Pow((channel + 0.055) / 1.055, 2.4);
    }
}