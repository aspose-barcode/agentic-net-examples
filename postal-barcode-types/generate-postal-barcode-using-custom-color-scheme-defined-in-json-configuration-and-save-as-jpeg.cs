// Title: Generate Postal Barcode with Custom Colors and Save as JPEG
// Description: Demonstrates creating a Planet postal barcode, applying colors from a JSON configuration, and saving the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with postal symbologies, customize visual appearance via color settings, and persist the output in common image formats. Developers working with shipping, logistics, or any system that requires printable postal codes can reference this pattern to integrate custom branding or visual requirements.
// Prompt: Generate a postal barcode using a custom color scheme defined in a JSON configuration and save as JPEG.
// Tags: postal barcode, generation, jpeg, custom colors, json configuration, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Planet postal barcode, applies custom colors from a JSON file,
/// and saves the barcode as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Build the path to the JSON configuration file containing color definitions.
        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "colorConfig.json");
        ColorConfig config = LoadConfig(configPath);

        // Determine the output file path for the generated JPEG barcode image.
        string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "postal_barcode.jpg");

        // Sample postal barcode data using the Planet symbology.
        string codeText = "123456";

        // Initialize the barcode generator with the chosen symbology and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
        {
            // Apply custom bar color if defined in the configuration.
            if (config.BarColor != null)
                generator.Parameters.Barcode.BarColor = ParseColor(config.BarColor);

            // Apply custom background color if defined in the configuration.
            if (config.BackColor != null)
                generator.Parameters.BackColor = ParseColor(config.BackColor);

            // Set visual dimensions for the barcode elements.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.Postal.ShortBarHeight.Pixels = 20f;

            // Save the generated barcode as a JPEG file.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Loads the color configuration from a JSON file. Returns default colors if the file is missing or invalid.
    /// </summary>
    /// <param name="path">Full path to the JSON configuration file.</param>
    /// <returns>A <see cref="ColorConfig"/> instance with the parsed color values.</returns>
    private static ColorConfig LoadConfig(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine($"Config file not found at {path}. Using default colors.");
            return new ColorConfig();
        }

        try
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<ColorConfig>(json) ?? new ColorConfig();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read config: {ex.Message}. Using default colors.");
            return new ColorConfig();
        }
    }

    /// <summary>
    /// Parses a hexadecimal color string (e.g., "#FF112233" or "112233") into an <see cref="Color"/> object.
    /// </summary>
    /// <param name="hex">Hexadecimal representation of the color.</param>
    /// <returns>A <see cref="Color"/> with the specified ARGB values.</returns>
    /// <exception cref="ArgumentException">Thrown when the input string is null, empty, or not a valid hex color.</exception>
    private static Color ParseColor(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            throw new ArgumentException("Invalid color value.");

        // Remove any leading '#' and whitespace.
        string clean = hex.Trim().TrimStart('#');

        // Validate length (6 for RGB, 8 for ARGB).
        if (clean.Length != 6 && clean.Length != 8)
            throw new ArgumentException("Color hex must be 6 or 8 characters.");

        // Default alpha to fully opaque.
        byte a = 255;
        int start = 0;

        // If an alpha component is present, extract it.
        if (clean.Length == 8)
        {
            a = Convert.ToByte(clean.Substring(0, 2), 16);
            start = 2;
        }

        // Extract red, green, and blue components.
        byte r = Convert.ToByte(clean.Substring(start, 2), 16);
        byte g = Convert.ToByte(clean.Substring(start + 2, 2), 16);
        byte b = Convert.ToByte(clean.Substring(start + 4, 2), 16);

        return Color.FromArgb(a, r, g, b);
    }

    /// <summary>
    /// Simple DTO for deserializing color settings from JSON.
    /// </summary>
    private class ColorConfig
    {
        public string? BarColor { get; set; }
        public string? BackColor { get; set; }
    }
}