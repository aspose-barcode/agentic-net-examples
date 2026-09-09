// Title: Generate Code128 barcode with custom foreground color
// Description: Demonstrates creating a Code128 barcode image where the bar (foreground) color is set using a hex string while keeping the default white background.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class. It shows setting bar color via the Parameters.Barcode.BarColor property, a common requirement for branding or visual integration. Developers often need to adjust colors, formats, and output options when generating barcodes programmatically.
// Prompt: Implement method to generate barcode with specified foreground color hex code and default background.
// Tags: barcode, code128, color, hex, generation, aspnet, aspose.barcode, png

using System;
using System.IO;
using System.Globalization;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom foreground color.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode and saves it to a temporary PNG file.
    /// </summary>
    static void Main()
    {
        // Define the data to encode and the desired bar color.
        string codeText = "1234567890";
        string hexColor = "#FF0000"; // Red foreground

        // Build a path in the system's temporary folder for the output image.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        try
        {
            // Generate the barcode image with the specified parameters.
            GenerateBarcode(codeText, hexColor, outputPath);
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a Code128 barcode image using the provided text and foreground color.
    /// The background remains the default (white).
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="hexColor">Hexadecimal color string for the barcode bars (e.g., "#FF0000").</param>
    /// <param name="outputPath">File path where the PNG image will be saved.</param>
    static void GenerateBarcode(string codeText, string hexColor, string outputPath)
    {
        // Convert the hex color string to an Aspose.Drawing.Color instance.
        Color barColor = ParseHexColor(hexColor);

        // Initialize the barcode generator with Code128 symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply the custom bar (foreground) color.
            generator.Parameters.Barcode.BarColor = barColor;
            // Background remains default (white).

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Parses a hex color string (e.g., "#RRGGBB" or "#AARRGGBB") into an Aspose.Drawing.Color.
    /// </summary>
    /// <param name="hex">Hexadecimal color string.</param>
    /// <returns>Corresponding Color object.</returns>
    /// <exception cref="ArgumentException">Thrown when the input format is invalid.</exception>
    static Color ParseHexColor(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            throw new ArgumentException("Hex color string is null or empty.");

        // Remove leading '#' if present.
        string clean = hex.TrimStart('#');

        if (clean.Length == 6)
        {
            // Parse as RGB and prepend full opacity.
            int rgb = int.Parse(clean, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            int argb = unchecked((int)(0xFF000000 | rgb));
            return Color.FromArgb(argb);
        }
        else if (clean.Length == 8)
        {
            // Parse as ARGB.
            int argb = int.Parse(clean, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            return Color.FromArgb(argb);
        }
        else
        {
            throw new ArgumentException("Hex color must be in format #RRGGBB or #AARRGGBB.");
        }
    }
}