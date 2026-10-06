// Title: Generate QR Code with Aspose.BarCode using JSON configuration
// Description: Demonstrates how to create a QR Code barcode, reading generation parameters from a JSON file and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and QR-specific parameters such as error correction level, version, and module size. Developers often need to externalize barcode settings for flexibility; this pattern reads configuration from an appsettings‑style JSON file, parses colors, and applies rotation before rendering the image.
// Prompt: Generate a QR Code barcode and configure generation parameters through appsettings JSON file.
// Tags: qr code, barcode generation, json configuration, aspose.barcode, png output

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Entry point for the QR Code generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Represents configurable QR Code generation settings that can be stored in a JSON file.
    /// </summary>
    class QrSettings
    {
        public string CodeText { get; set; } = "Hello, Aspose!";
        public string ErrorLevel { get; set; } = "LevelH";
        public string Version { get; set; } = "Version05";
        public float XDimension { get; set; } = 2f;
        public float PaddingLeft { get; set; } = 5f;
        public float PaddingTop { get; set; } = 5f;
        public float PaddingRight { get; set; } = 5f;
        public float PaddingBottom { get; set; } = 5f;
        public string BarColor { get; set; } = "#000000";
        public string BackColor { get; set; } = "#FFFFFF";
        public float RotationAngle { get; set; } = 0f;
    }

    /// <summary>
    /// Parses a hexadecimal color string (e.g., "#FF00AA") into an Aspose.Drawing.Color.
    /// Returns Black if the input is null, empty, or malformed.
    /// </summary>
    /// <param name="hex">Hexadecimal color string.</param>
    /// <returns>Corresponding Color instance.</returns>
    static Color ParseColor(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            return Color.Black;

        hex = hex.TrimStart('#');
        if (hex.Length == 6)
        {
            int r = Convert.ToInt32(hex.Substring(0, 2), 16);
            int g = Convert.ToInt32(hex.Substring(2, 2), 16);
            int b = Convert.ToInt32(hex.Substring(4, 2), 16);
            return Color.FromArgb(r, g, b);
        }
        return Color.Black;
    }

    /// <summary>
    /// Main execution method: loads or creates configuration, generates the QR Code, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine the path for the temporary JSON configuration file.
        string configPath = Path.Combine(Path.GetTempPath(), "qrsettings.json");

        // If the config file does not exist, create one with default settings.
        if (!File.Exists(configPath))
        {
            var defaultSettings = new QrSettings();
            string json = JsonSerializer.Serialize(defaultSettings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, json);
            Console.WriteLine($"Created default config at: {configPath}");
        }

        // Load settings from the JSON file, falling back to defaults on error.
        QrSettings settings;
        try
        {
            string jsonContent = File.ReadAllText(configPath);
            settings = JsonSerializer.Deserialize<QrSettings>(jsonContent) ?? new QrSettings();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read config: {ex.Message}");
            settings = new QrSettings();
        }

        // Define the output image path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.png");

        // Initialize the barcode generator with QR encoding and the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, settings.CodeText))
        {
            // Apply error correction level if the enum value parses successfully.
            if (Enum.TryParse<QRErrorLevel>(settings.ErrorLevel, out var errLevel))
                generator.Parameters.Barcode.QR.ErrorLevel = errLevel;

            // Apply QR version if the enum value parses successfully.
            if (Enum.TryParse<QRVersion>(settings.Version, out var qrVersion))
                generator.Parameters.Barcode.QR.Version = qrVersion;

            // Set the module (dot) size.
            generator.Parameters.Barcode.XDimension.Point = settings.XDimension;

            // Configure padding around the barcode.
            generator.Parameters.Barcode.Padding.Left.Point = settings.PaddingLeft;
            generator.Parameters.Barcode.Padding.Top.Point = settings.PaddingTop;
            generator.Parameters.Barcode.Padding.Right.Point = settings.PaddingRight;
            generator.Parameters.Barcode.Padding.Bottom.Point = settings.PaddingBottom;

            // Set foreground (bar) and background colors.
            generator.Parameters.Barcode.BarColor = ParseColor(settings.BarColor);
            generator.Parameters.BackColor = ParseColor(settings.BackColor);

            // Apply rotation if needed.
            generator.Parameters.RotationAngle = settings.RotationAngle;

            // Render and save the QR Code as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR Code generated and saved to: {outputPath}");
    }
}