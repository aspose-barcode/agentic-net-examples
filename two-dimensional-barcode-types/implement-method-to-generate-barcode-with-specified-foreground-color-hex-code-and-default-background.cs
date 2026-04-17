using System;
using System.Globalization;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample data
        string codeText = "123ABC";
        string hexColor = "#FF4500"; // OrangeRed
        string outputFile = "barcode.png";

        GenerateBarcode(codeText, hexColor, outputFile);
        Console.WriteLine($"Barcode saved to {outputFile}");
    }

    /// <summary>
    /// Generates a barcode image with the specified foreground color (hex) and default background.
    /// </summary>
    /// <param name="codeText">Text to encode.</param>
    /// <param name="hexColor">Hexadecimal color string (e.g., "#RRGGBB" or "RRGGBB").</param>
    /// <param name="outputPath">File path to save the image.</param>
    static void GenerateBarcode(string codeText, string hexColor, string outputPath)
    {
        // Normalize hex string (remove leading '#')
        string cleanHex = hexColor.TrimStart('#');

        if (cleanHex.Length != 6 && cleanHex.Length != 8)
            throw new ArgumentException("Hex color must be in RRGGBB or AARRGGBB format.", nameof(hexColor));

        // Parse ARGB components
        int a = 255; // default opaque
        int r, g, b;

        if (cleanHex.Length == 8)
        {
            a = int.Parse(cleanHex.Substring(0, 2), NumberStyles.HexNumber);
            r = int.Parse(cleanHex.Substring(2, 2), NumberStyles.HexNumber);
            g = int.Parse(cleanHex.Substring(4, 2), NumberStyles.HexNumber);
            b = int.Parse(cleanHex.Substring(6, 2), NumberStyles.HexNumber);
        }
        else // 6 characters
        {
            r = int.Parse(cleanHex.Substring(0, 2), NumberStyles.HexNumber);
            g = int.Parse(cleanHex.Substring(2, 2), NumberStyles.HexNumber);
            b = int.Parse(cleanHex.Substring(4, 2), NumberStyles.HexNumber);
        }

        // Create barcode generator (using Code128 as an example)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set foreground (bar) color
            generator.Parameters.Barcode.BarColor = Color.FromArgb(a, r, g, b);

            // Background remains default (white) – no need to set BackColor

            // Save image
            generator.Save(outputPath);
        }
    }
}