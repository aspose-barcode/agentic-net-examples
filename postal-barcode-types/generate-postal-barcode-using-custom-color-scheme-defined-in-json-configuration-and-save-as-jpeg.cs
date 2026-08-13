// Title: Generate Australia Post barcode with custom colors from JSON
// Description: Demonstrates loading bar and background colors from a JSON file, applying them to an Australia Post barcode, and saving the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class. It shows typical use cases such as reading configuration files, setting bar and background colors, and exporting to common image formats. Developers working with postal barcodes or needing dynamic visual styling can reference this pattern for quick implementation.
// Prompt: Generate a postal barcode using a custom color scheme defined in a JSON configuration and save as JPEG.
// Tags: barcode, australia post, color, json, jpeg, generation, aspose.barcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating an Australia Post barcode with colors defined in a JSON configuration and saving it as a JPEG image.
/// </summary>
class Program
{
    // Represents the JSON configuration for colors.
    private class ColorConfig
    {
        public string BarColor { get; set; }
        public string BackColor { get; set; }
    }

    // Parses a hex color string (e.g., "#FF1122") into an Aspose.Drawing.Color.
    private static Color ParseHexColor(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            throw new ArgumentException("Hex color string is null or empty.");

        // Remove leading '#', if present.
        hex = hex.TrimStart('#');

        if (hex.Length != 6 && hex.Length != 8)
            throw new ArgumentException($"Invalid hex color length: {hex}");

        // If only RRGGBB is provided, assume full opacity.
        if (hex.Length == 6)
            hex = "FF" + hex; // prepend alpha

        // Parse ARGB integer.
        uint argb = Convert.ToUInt32(hex, 16);
        byte a = (byte)((argb >> 24) & 0xFF);
        byte r = (byte)((argb >> 16) & 0xFF);
        byte g = (byte)((argb >> 8) & 0xFF);
        byte b = (byte)(argb & 0xFF);
        return Color.FromArgb(a, r, g, b);
    }

    /// <summary>
    /// Entry point. Loads color settings, creates the barcode, applies colors, and saves the image.
    /// </summary>
    static void Main()
    {
        // Path to the JSON configuration file.
        const string configPath = "config.json";

        // Default colors (black bars on white background).
        Color barColor = Color.Black;
        Color backColor = Color.White;

        // Load colors from JSON if the file exists.
        if (File.Exists(configPath))
        {
            try
            {
                string json = File.ReadAllText(configPath);
                ColorConfig cfg = JsonSerializer.Deserialize<ColorConfig>(json);
                if (cfg != null)
                {
                    if (!string.IsNullOrWhiteSpace(cfg.BarColor))
                        barColor = ParseHexColor(cfg.BarColor);
                    if (!string.IsNullOrWhiteSpace(cfg.BackColor))
                        backColor = ParseHexColor(cfg.BackColor);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read or parse config file: {ex.Message}");
                Console.WriteLine("Using default colors.");
            }
        }
        else
        {
            Console.WriteLine("Config file not found. Using default colors.");
        }

        // Sample Australia Post barcode text (FCC 59, DPID 12345678, CTable "AB").
        const string codeText = "5912345678AB";

        // Generate and save the barcode.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Apply custom colors.
            generator.Parameters.Barcode.BarColor = barColor;
            generator.Parameters.BackColor = backColor;

            // Save as JPEG.
            const string outputFile = "postal_barcode.jpg";
            generator.Save(outputFile, BarCodeImageFormat.Jpeg);
            Console.WriteLine($"Barcode saved to '{outputFile}'.");
        }
    }
}