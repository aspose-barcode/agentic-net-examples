// Title: Generate Code128 Barcode with Custom Foreground Color
// Description: Demonstrates how to create a Code128 barcode image using Aspose.BarCode, applying a user‑specified foreground color defined by a hex string while keeping the default background.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the BarcodeGenerator class together with the Parameters.Barcode.BarColor property to customize the bar color. Typical use cases include branding, UI integration, or printing where a specific color scheme is required. Developers often need to parse hex color codes and apply them to generated barcodes, which this snippet illustrates.
// Prompt: Implement method to generate barcode with specified foreground color hex code and default background.
// Tags: barcode, code128, color, hex, generation, aspose.barcode, png, c#

using System;
using System.Globalization;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a custom foreground color.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image using a hex color for the bars and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define input parameters.
        string hexColor = "#FF5733";          // Desired bar (foreground) color in hex notation.
        string codeText = "1234567890";       // Text to encode in the barcode.
        string outputPath = "barcode.png";    // Destination file path for the generated image.

        // Convert the hex string to an Aspose.Drawing.Color instance.
        Aspose.Drawing.Color barColor = ParseHexColor(hexColor);

        // Create a barcode generator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply the custom bar color while leaving the background at its default.
            generator.Parameters.Barcode.BarColor = barColor;

            // Save the barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to {outputPath} with bar color {hexColor}");
    }

    /// <summary>
    /// Parses a hex color string (e.g., "#RRGGBB" or "#AARRGGBB") into an Aspose.Drawing.Color.
    /// </summary>
    /// <param name="hex">Hexadecimal color representation, with or without leading '#'.</param>
    /// <returns>Aspose.Drawing.Color corresponding to the supplied hex value.</returns>
    /// <exception cref="ArgumentException">Thrown when the input is null, empty, or not 6/8 characters long.</exception>
    static Aspose.Drawing.Color ParseHexColor(string hex)
    {
        // Validate input.
        if (string.IsNullOrWhiteSpace(hex))
            throw new ArgumentException("Hex color string is null or empty.", nameof(hex));

        // Remove leading '#' if present.
        string clean = hex.TrimStart('#');

        // 6‑character format: RRGGBB (assume full opacity).
        if (clean.Length == 6)
        {
            int r = int.Parse(clean.Substring(0, 2), NumberStyles.HexNumber);
            int g = int.Parse(clean.Substring(2, 2), NumberStyles.HexNumber);
            int b = int.Parse(clean.Substring(4, 2), NumberStyles.HexNumber);
            return Aspose.Drawing.Color.FromArgb(255, r, g, b);
        }
        // 8‑character format: AARRGGBB (includes alpha channel).
        else if (clean.Length == 8)
        {
            int a = int.Parse(clean.Substring(0, 2), NumberStyles.HexNumber);
            int r = int.Parse(clean.Substring(2, 2), NumberStyles.HexNumber);
            int g = int.Parse(clean.Substring(4, 2), NumberStyles.HexNumber);
            int b = int.Parse(clean.Substring(6, 2), NumberStyles.HexNumber);
            return Aspose.Drawing.Color.FromArgb(a, r, g, b);
        }
        else
        {
            throw new ArgumentException("Hex color must be 6 or 8 characters.", nameof(hex));
        }
    }
}