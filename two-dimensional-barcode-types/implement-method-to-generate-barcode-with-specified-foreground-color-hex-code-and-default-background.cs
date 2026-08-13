// Title: Generate Code128 Barcode with Custom Foreground Color
// Description: Demonstrates how to create a Code128 barcode image using Aspose.BarCode, applying a user‑specified foreground color supplied as a hex string while keeping the background white.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to configure the BarcodeGenerator, set bar color via System.Drawing.Color, and save the result as an image. Developers working with barcode creation often need to customize colors, choose symbologies, and output PNG/JPEG files. The key classes used are BarcodeGenerator, EncodeTypes, and the Parameters property for visual settings.
// Prompt: Implement method to generate barcode with specified foreground color hex code and default background.
// Tags: barcode, code128, color, hex, generation, aspose.barcode, image, png

using System;
using System.Globalization;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode image with a custom foreground color.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode with a red foreground color and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Input data for the barcode
        string codeText = "123ABC";
        string hexColor = "#FF0000"; // Red foreground color in hex notation
        string outputPath = "barcode.png";

        try
        {
            // Generate and save the barcode image
            GenerateBarcode(codeText, hexColor, outputPath);
            Console.WriteLine($"Barcode saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during generation
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image with the specified foreground color (hex) and default background.
    /// </summary>
    /// <param name="codeText">Text to encode.</param>
    /// <param name="hexColor">Foreground color in hex format (e.g., "#RRGGBB" or "RRGGBB").</param>
    /// <param name="outputPath">File path to save the barcode image.</param>
    static void GenerateBarcode(string codeText, string hexColor, string outputPath)
    {
        // Validate input parameters
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("codeText cannot be null or empty.", nameof(codeText));

        if (string.IsNullOrEmpty(hexColor))
            throw new ArgumentException("hexColor cannot be null or empty.", nameof(hexColor));

        // Remove any leading '#' from the hex string
        string cleanHex = hexColor.TrimStart('#');

        // Ensure the hex string represents exactly 6 hexadecimal digits (RRGGBB)
        if (cleanHex.Length != 6)
            throw new ArgumentException("hexColor must be in the format RRGGBB.", nameof(hexColor));

        // Convert the hex components to integer values
        int r = int.Parse(cleanHex.Substring(0, 2), NumberStyles.HexNumber);
        int g = int.Parse(cleanHex.Substring(2, 2), NumberStyles.HexNumber);
        int b = int.Parse(cleanHex.Substring(4, 2), NumberStyles.HexNumber);

        // Initialize the barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply the custom foreground (bar) color
            generator.Parameters.Barcode.BarColor = Color.FromArgb(255, r, g, b);

            // Set the background to the default white color
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }
    }
}