// Title: Generate Code128 barcode with custom foreground color
// Description: Demonstrates creating a Code128 barcode image, applying a specific foreground color via hex code while using the default background.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class and its Parameters.Barcode properties. Typical use cases include branding, UI integration, and printing where specific colors are required. Developers often need to set bar colors, background colors, and output formats for various symbologies.
// Prompt: Implement method to generate barcode with specified foreground color hex code and default background.
// Tags: code128, barcode generation, color customization, png, aspose.barcode, aspose.drawing

using System;
using System.Globalization;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom foreground color.
/// </summary>
class Program
{
    /// <summary>
    /// Parses a hex color string (e.g., "#FF1122" or "FF1122") into an <see cref="Aspose.Drawing.Color"/>.
    /// </summary>
    /// <param name="hex">Hexadecimal color representation.</param>
    /// <returns>Corresponding <see cref="Color"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the input is null, empty, or not in a recognized format.</exception>
    static Color ParseHexColor(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            throw new ArgumentException("Hex color string cannot be null or empty.", nameof(hex));

        // Remove optional leading '#'
        hex = hex.TrimStart('#');

        if (hex.Length == 6) // RRGGBB format
        {
            int r = int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            int g = int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            int b = int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            return Color.FromArgb(r, g, b);
        }
        else if (hex.Length == 8) // AARRGGBB format
        {
            int a = int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            int r = int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            int g = int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            int b = int.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
            return Color.FromArgb(a, r, g, b);
        }
        else
        {
            throw new ArgumentException("Hex color must be in format RRGGBB or AARRGGBB.", nameof(hex));
        }
    }

    /// <summary>
    /// Entry point. Generates the barcode image and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Define barcode parameters
        string codeText = "1234567890";
        string foregroundHex = "#0066CC"; // Desired foreground (bar) color
        string outputPath = "barcode.png";

        // Create and configure the barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply the custom foreground color parsed from hex
            generator.Parameters.Barcode.BarColor = ParseHexColor(foregroundHex);

            // Background color defaults to white; no explicit setting required

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to '{outputPath}'.");
    }
}