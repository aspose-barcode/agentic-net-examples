// Title: Generate Postal Barcode with Custom Colors
// Description: Demonstrates creating a Postnet barcode using colors defined in a JSON file and saving it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance via the BarcodeGenerator class, set bar and background colors, and serialize output to common image formats. Developers working with postal symbologies often need to customize visual styles for branding or readability, and this snippet shows the typical workflow using Aspose.BarCode and Aspose.Drawing APIs.
// Prompt: Generate a postal barcode using a custom color scheme defined in a JSON configuration and save as JPEG.
// Tags: postal barcode, color customization, jpeg output, aspose.barcode, aspose.drawing, json configuration

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Postnet barcode using custom colors defined in a JSON configuration file
/// and saves the result as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the JSON configuration file containing color definitions
        const string configPath = "config.json";

        // Verify that the configuration file exists before proceeding
        if (!File.Exists(configPath))
        {
            Console.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Read the JSON content from the file
        string json = File.ReadAllText(configPath);

        // Deserialize JSON into a strongly‑typed configuration object
        ColorConfig? config = JsonSerializer.Deserialize<ColorConfig>(json);
        if (config == null)
        {
            Console.WriteLine("Failed to parse configuration.");
            return;
        }

        // Convert the color strings from the configuration into Aspose.Drawing.Color instances
        Aspose.Drawing.Color barColor = ParseColor(config.BarColor);
        Aspose.Drawing.Color backColor = ParseColor(config.BackColor);

        // Create a Postnet barcode generator with sample data ("12345")
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Apply the custom bar and background colors to the generator
            generator.Parameters.Barcode.BarColor = barColor;
            generator.Parameters.BackColor = backColor;

            // Save the generated barcode as a JPEG image file
            generator.Save("postal.jpg");
        }

        Console.WriteLine("Barcode generated and saved as postal.jpg");
    }

    /// <summary>
    /// Parses a color string (hex format like "#RRGGBB" or "#AARRGGBB", or a named color) into an Aspose.Drawing.Color.
    /// Returns Black if the input is null, empty, or whitespace.
    /// </summary>
    /// <param name="value">The color string to parse.</param>
    /// <returns>An Aspose.Drawing.Color representing the parsed color.</returns>
    private static Aspose.Drawing.Color ParseColor(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Aspose.Drawing.Color.Black;

        value = value.Trim();

        if (value.StartsWith("#"))
        {
            string hex = value.TrimStart('#');

            // Support #RRGGBB format (no alpha component)
            if (hex.Length == 6)
            {
                int r = Convert.ToInt32(hex.Substring(0, 2), 16);
                int g = Convert.ToInt32(hex.Substring(2, 2), 16);
                int b = Convert.ToInt32(hex.Substring(4, 2), 16);
                return Aspose.Drawing.Color.FromArgb(r, g, b);
            }
            // Support #AARRGGBB format (includes alpha component)
            else if (hex.Length == 8)
            {
                int a = Convert.ToInt32(hex.Substring(0, 2), 16);
                int r = Convert.ToInt32(hex.Substring(2, 2), 16);
                int g = Convert.ToInt32(hex.Substring(4, 2), 16);
                int b = Convert.ToInt32(hex.Substring(6, 2), 16);
                return Aspose.Drawing.Color.FromArgb(a, r, g, b);
            }
        }

        // Fallback to a named color if the string is not a hex value
        return Aspose.Drawing.Color.FromName(value);
    }

    /// <summary>
    /// Represents the JSON configuration for barcode colors.
    /// </summary>
    private class ColorConfig
    {
        public string? BarColor { get; set; }
        public string? BackColor { get; set; }
    }
}