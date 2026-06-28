using System;
using System.Globalization;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom foreground color using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode and writes its location to the console.
    /// </summary>
    static void Main()
    {
        // Sample data: Code128 barcode with red foreground color.
        string codeText = "123ABC";
        string hexColor = "#FF0000"; // Red
        string outputPath = "barcode.png";

        // Generate the barcode image with the specified parameters.
        GenerateBarcode(codeText, hexColor, outputPath);

        // Output the full path of the saved barcode image.
        Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
    }

    static void GenerateBarcode(string codeText, string hexColor, string outputPath)
    {
        // Validate that a barcode text value was supplied.
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text must be provided.", nameof(codeText));

        // Parse the hex color string to an Aspose.Drawing.Color.
        // Supports formats "#RRGGBB" or "RRGGBB". Falls back to black on failure.
        Color barColor = Color.Black; // fallback color
        if (!string.IsNullOrWhiteSpace(hexColor))
        {
            // Remove any leading '#' and whitespace.
            string cleaned = hexColor.Trim().TrimStart('#');

            // Ensure the cleaned string has exactly 6 hex digits.
            if (cleaned.Length == 6 && int.TryParse(cleaned, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int rgb))
            {
                // Extract red, green, and blue components.
                int r = (rgb >> 16) & 0xFF;
                int g = (rgb >> 8) & 0xFF;
                int b = rgb & 0xFF;

                // Create a fully opaque color from the RGB components.
                barColor = Color.FromArgb(255, r, g, b);
            }
            else
            {
                // Inform the user of an invalid color string; continue with default black.
                Console.WriteLine($"Invalid hex color '{hexColor}'. Using default black.");
            }
        }

        // Choose the barcode symbology; using Code128 as an example.
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Create a barcode generator with the selected symbology and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Apply the parsed foreground (bar) color.
            generator.Parameters.Barcode.BarColor = barColor;

            // Background color defaults to white; no explicit setting required.

            // Save the generated barcode image to the specified file path.
            generator.Save(outputPath);
        }
    }
}