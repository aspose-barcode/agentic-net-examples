// Title: Generate Code128 Barcode with Custom Foreground Color
// Description: Demonstrates how to create a Code128 barcode image using Aspose.BarCode, applying a user‑specified foreground color while keeping the default white background.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance via the BarcodeGenerator class. It shows setting the BarColor property to a custom ARGB value derived from a hex string, a common task when integrating barcodes into branding‑aware applications. Developers often need to customize colors, sizes, and formats of generated barcodes for reports, labels, or UI elements.
// Prompt: Implement method to generate barcode with specified foreground color hex code and default background.
// Tags: barcode, code128, color, hex, generation, aspnet, aspose.barcode, png, image

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a custom foreground color.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and saves it to a temporary PNG file.
    /// </summary>
    static void Main()
    {
        // Barcode data to encode
        string codeText = "1234567890";

        // Desired foreground color in hex notation (green)
        string hexColor = "#00FF00";

        // Output file path (temporary directory)
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Generate the barcode image with the specified parameters
        GenerateBarcode(codeText, hexColor, outputPath);

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }

    static void GenerateBarcode(string codeText, string hexColor, string outputPath)
    {
        // Validate input parameters
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text must be provided.", nameof(codeText));

        if (string.IsNullOrEmpty(hexColor))
            throw new ArgumentException("Hex color must be provided.", nameof(hexColor));

        // Remove leading '#' if present and ensure the string has exactly 6 hex digits
        string cleaned = hexColor.TrimStart('#');
        if (cleaned.Length != 6)
            throw new ArgumentException("Hex color must be in RRGGBB format.", nameof(hexColor));

        // Convert the hex string to an integer RGB value
        int rgb = Convert.ToInt32(cleaned, 16);

        // Combine with full opacity (alpha = 255) to create an ARGB value
        int argb = unchecked((int)(0xFF000000 | rgb));

        // Create a Color object from the ARGB value
        Color barColor = Color.FromArgb(argb);

        // Initialize the barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply the custom foreground color; background remains default (white)
            generator.Parameters.Barcode.BarColor = barColor;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}