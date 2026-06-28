using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Postnet barcode with custom colors defined in a JSON configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Model that matches the JSON configuration structure for barcode colors.
    /// </summary>
    private class ColorConfig
    {
        public string BarColor { get; set; }
        public string BackColor { get; set; }
    }

    /// <summary>
    /// Parses a hexadecimal color string (e.g., "#FF1122" or "#11223344") into an <see cref="Aspose.Drawing.Color"/>.
    /// </summary>
    /// <param name="hex">Hexadecimal color string.</param>
    /// <returns>Corresponding <see cref="Color"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the input is null, empty, or not a valid hex format.</exception>
    private static Color ParseHexColor(string hex)
    {
        // Ensure the input is not null or whitespace.
        if (string.IsNullOrWhiteSpace(hex))
            throw new ArgumentException("Color value cannot be null or empty.");

        // Remove any leading '#' character.
        hex = hex.TrimStart('#');

        // Accept only 6 (RGB) or 8 (ARGB) character strings.
        if (hex.Length != 6 && hex.Length != 8)
            throw new ArgumentException($"Invalid hex color format: {hex}");

        // Default alpha to 255 (opaque). Adjust if an alpha component is present.
        int a = 255, r, g, b;
        int start = 0;

        // If the string includes alpha (8 characters), parse it first.
        if (hex.Length == 8)
        {
            a = Convert.ToInt32(hex.Substring(0, 2), 16);
            start = 2;
        }

        // Parse red, green, and blue components.
        r = Convert.ToInt32(hex.Substring(start, 2), 16);
        g = Convert.ToInt32(hex.Substring(start + 2, 2), 16);
        b = Convert.ToInt32(hex.Substring(start + 4, 2), 16);

        // Create the Color object with the parsed ARGB values.
        return Color.FromArgb(a, r, g, b);
    }

    /// <summary>
    /// Entry point of the application. Reads color configuration, parses colors, and generates a barcode image.
    /// </summary>
    static void Main()
    {
        // Sample JSON configuration (could be read from an external file in a real scenario).
        string json = @"{
            ""BarColor"": ""#0033CC"",
            ""BackColor"": ""#FFFFFF""
        }";

        // Deserialize the JSON into a ColorConfig object.
        ColorConfig config;
        try
        {
            config = JsonSerializer.Deserialize<ColorConfig>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON configuration: {ex.Message}");
            return;
        }

        // Convert the hex color strings to Aspose.Drawing.Color instances.
        Color barColor, backColor;
        try
        {
            barColor = ParseHexColor(config.BarColor);
            backColor = ParseHexColor(config.BackColor);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Invalid color definition: {ex.Message}");
            return;
        }

        // Define output file path and sample postal code to encode.
        const string outputPath = "postal_barcode.jpg";
        const string codeText = "12345";

        // Create a barcode generator for the Postnet format.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, codeText))
        {
            // Apply the custom bar and background colors.
            generator.Parameters.Barcode.BarColor = barColor;
            generator.Parameters.BackColor = backColor;

            // Set a higher resolution for better JPEG quality.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode generated and saved to '{Path.GetFullPath(outputPath)}'.");
    }
}