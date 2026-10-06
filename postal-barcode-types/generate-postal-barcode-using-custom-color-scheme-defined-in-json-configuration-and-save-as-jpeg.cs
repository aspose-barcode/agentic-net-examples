// Title: Generate a postal barcode with custom colors from JSON and save as JPEG
// Description: Demonstrates loading bar and background colors from a JSON file, applying them to a Planet postal barcode, and saving the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using Aspose.BarCode.Generation.BarcodeGenerator and Aspose.Drawing.Color. Typical use cases include creating branded postal barcodes, applying corporate color schemes, and exporting to common image formats. Developers often need to read configuration files, set barcode parameters, and render images for printing or digital distribution.
// Prompt: Generate a postal barcode using a custom color scheme defined in a JSON configuration and save as JPEG.
// Tags: barcode, postal, generation, jpeg, json, color, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Represents the color configuration for the barcode, loaded from a JSON file.
/// </summary>
class Config
{
    public string BarColor { get; set; }
    public string BackgroundColor { get; set; }
}

/// <summary>
/// Demonstrates generating a Planet postal barcode with colors defined in a JSON configuration and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Parses a hexadecimal color string (e.g., "#FF112233") into an Aspose.Drawing.Color.
    /// Supports 6‑character (RGB) and 8‑character (ARGB) formats.
    /// </summary>
    /// <param name="hex">Hexadecimal color string.</param>
    /// <returns>Corresponding Aspose.Drawing.Color instance.</returns>
    static Aspose.Drawing.Color ParseColor(string hex)
    {
        // Return an empty color if the input is null or whitespace.
        if (string.IsNullOrWhiteSpace(hex))
            return Aspose.Drawing.Color.Empty;

        // Remove leading '#' if present.
        hex = hex.TrimStart('#');

        // 6‑character format: RRGGBB (alpha defaults to 255).
        if (hex.Length == 6)
        {
            int r = Convert.ToInt32(hex.Substring(0, 2), 16);
            int g = Convert.ToInt32(hex.Substring(2, 2), 16);
            int b = Convert.ToInt32(hex.Substring(4, 2), 16);
            return Aspose.Drawing.Color.FromArgb(255, r, g, b);
        }
        // 8‑character format: AARRGGBB.
        else if (hex.Length == 8)
        {
            int a = Convert.ToInt32(hex.Substring(0, 2), 16);
            int r = Convert.ToInt32(hex.Substring(2, 2), 16);
            int g = Convert.ToInt32(hex.Substring(4, 2), 16);
            int b = Convert.ToInt32(hex.Substring(6, 2), 16);
            return Aspose.Drawing.Color.FromArgb(a, r, g, b);
        }
        else
        {
            throw new ArgumentException("Invalid color hex format.");
        }
    }

    /// <summary>
    /// Entry point of the example. Loads color settings, creates a barcode, and saves it as a JPEG file.
    /// </summary>
    static void Main()
    {
        const string configFile = "config.json";

        // Load color configuration from JSON if the file exists; otherwise use defaults.
        Config config;
        if (File.Exists(configFile))
        {
            string json = File.ReadAllText(configFile);
            config = JsonSerializer.Deserialize<Config>(json);
        }
        else
        {
            config = new Config
            {
                BarColor = "#000000",          // Default bar (foreground) color: black
                BackgroundColor = "#FFFFFF"   // Default background color: white
            };
        }

        // Convert hex strings to Aspose.Drawing.Color objects.
        Aspose.Drawing.Color barColor = ParseColor(config.BarColor);
        Aspose.Drawing.Color backColor = ParseColor(config.BackgroundColor);

        const string outputFile = "postal_barcode.jpg";
        const string codeText = "123456"; // Sample postal code data.

        // Create and configure the barcode generator.
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
        {
            generator.Parameters.Barcode.BarColor = barColor;   // Apply custom bar color.
            generator.Parameters.BackColor = backColor;        // Apply custom background color.
            generator.Parameters.Barcode.XDimension.Pixels = 4f; // Set module size.
            generator.Save(outputFile, BarCodeImageFormat.Jpeg); // Save as JPEG.
        }

        Console.WriteLine($"Postal barcode saved to '{outputFile}'.");
    }
}