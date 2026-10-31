// Title: Generate Postal Barcode with Custom Colors from JSON
// Description: Demonstrates creating a Planet postal barcode, applying colors defined in a JSON configuration, and saving the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance using the BarcodeGenerator class. It covers deserialization of color settings from JSON, conversion to System.Drawing.Color, and applying those colors to background, bars, border, and text. Developers often need such customizations for branding or visual integration of barcodes into documents and applications.
// Prompt: Generate a postal barcode using a custom color scheme defined in a JSON configuration and save as JPEG.
// Tags: barcode, symbology, generation, jpeg, json, color, aspose.barcode, planet

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Planet postal barcode with colors defined in a JSON configuration
/// and saves the barcode as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Simple POCO representing the color configuration read from JSON.
    /// </summary>
    class ColorConfig
    {
        public string BackColor { get; set; }
        public string BarColor { get; set; }
        public string BorderColor { get; set; }
        public string TextColor { get; set; }
    }

    /// <summary>
    /// Parses a hexadecimal color string (e.g., "#FF00AA") into an Aspose.Drawing.Color.
    /// </summary>
    /// <param name="hex">Hexadecimal color string.</param>
    /// <returns>Corresponding Color object.</returns>
    static Color ParseHexColor(string hex)
    {
        if (string.IsNullOrEmpty(hex))
            throw new ArgumentException("Hex color string is null or empty.");

        // Remove leading '#' if present.
        hex = hex.TrimStart('#');

        if (hex.Length != 6)
            throw new ArgumentException("Hex color must be 6 characters.");

        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);
        return Color.FromArgb(r, g, b);
    }

    /// <summary>
    /// Entry point. Generates the barcode, applies custom colors, and saves the image.
    /// </summary>
    static void Main()
    {
        // Sample JSON configuration containing color definitions.
        string jsonConfig = @"{
            ""BackColor"": ""#FFFFFF"",
            ""BarColor"": ""#FF0000"",
            ""BorderColor"": ""#00FF00"",
            ""TextColor"": ""#0000FF""
        }";

        // Deserialize the JSON into a ColorConfig object.
        ColorConfig config = JsonSerializer.Deserialize<ColorConfig>(jsonConfig);
        if (config == null)
        {
            Console.WriteLine("Failed to parse color configuration.");
            return;
        }

        // Convert each hex string to a Color instance.
        Color backColor = ParseHexColor(config.BackColor);
        Color barColor = ParseHexColor(config.BarColor);
        Color borderColor = ParseHexColor(config.BorderColor);
        Color textColor = ParseHexColor(config.TextColor);

        // Determine the output file path for the generated JPEG.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "PostalBarcode.jpg");

        // Create a BarcodeGenerator for the Planet postal symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Optional size adjustments.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.Postal.ShortBarHeight.Pixels = 20f;

            // Apply the custom colors to the barcode.
            generator.Parameters.BackColor = backColor;
            generator.Parameters.Barcode.BarColor = barColor;
            generator.Parameters.Border.Color = borderColor;
            generator.Parameters.Barcode.CodeTextParameters.Color = textColor;

            // Save the barcode image as a JPEG file.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        Console.WriteLine($"Postal barcode saved to: {outputPath}");
    }
}