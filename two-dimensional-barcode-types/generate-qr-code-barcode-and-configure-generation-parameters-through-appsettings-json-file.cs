// Title: Generate QR Code Barcode Using Settings from appsettings.json
// Description: Demonstrates how to generate a QR Code barcode with Aspose.BarCode, reading generation parameters such as code text, error correction level, dimensions, and colors from an appsettings.json configuration file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and QR-specific parameters. Typical use cases include creating QR codes with custom appearance and error correction based on external configuration, a common requirement for dynamic barcode creation in web and desktop applications.
// Prompt: Generate a QR Code barcode and configure generation parameters through appsettings JSON file.
// Tags: qr code, barcode generation, configuration, json settings, aspose.barcode, png output

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Globalization;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Represents the configurable settings for QR code generation, loaded from appsettings.json.
/// </summary>
class Settings
{
    public string CodeText { get; set; } = "Hello, QR!";
    public string ErrorLevel { get; set; } = "LevelM";
    public float XDimension { get; set; } = 2f;
    public string ForegroundColor { get; set; } = "#000000";
    public string BackgroundColor { get; set; } = "#FFFFFF";
    public string OutputPath { get; set; } = "qr.png";
}

/// <summary>
/// Entry point for the QR code generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR code using parameters defined in appsettings.json and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine the base directory of the application.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string jsonPath = Path.Combine(baseDir, "appsettings.json");

        // Create a default appsettings.json if it does not exist.
        if (!File.Exists(jsonPath))
        {
            var defaultSettings = new Settings();
            string defaultJson = JsonSerializer.Serialize(defaultSettings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonPath, defaultJson);
            Console.WriteLine($"Created default appsettings.json at {jsonPath}");
        }

        // Load settings from the JSON file.
        Settings settings;
        try
        {
            string json = File.ReadAllText(jsonPath);
            settings = JsonSerializer.Deserialize<Settings>(json) ?? new Settings();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read settings: {ex.Message}");
            settings = new Settings();
        }

        // Resolve the QR error correction level enum from the string value.
        QRErrorLevel errorLevel = QRErrorLevel.LevelM;
        if (!Enum.TryParse<QRErrorLevel>(settings.ErrorLevel, true, out errorLevel))
        {
            Console.WriteLine($"Invalid ErrorLevel '{settings.ErrorLevel}', using default LevelM.");
        }

        // Parse foreground and background colors from hex strings.
        Color fgColor = ParseColor(settings.ForegroundColor, Color.Black);
        Color bgColor = ParseColor(settings.BackgroundColor, Color.White);

        // Ensure the output directory exists.
        string outputFullPath = Path.Combine(baseDir, settings.OutputPath);
        string outputDir = Path.GetDirectoryName(outputFullPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create and configure the barcode generator.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, settings.CodeText))
        {
            // Set QR-specific error correction level.
            generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;

            // Set the module (dot) size.
            generator.Parameters.Barcode.XDimension.Point = settings.XDimension;

            // Apply foreground and background colors.
            generator.Parameters.Barcode.BarColor = fgColor;
            generator.Parameters.BackColor = bgColor;

            // Save the generated QR code as a PNG image.
            generator.Save(outputFullPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"QR Code generated and saved to: {outputFullPath}");
    }

    /// <summary>
    /// Parses a hex color string (e.g., "#FF0000") into an Aspose.Drawing.Color.
    /// Returns the fallback color if parsing fails.
    /// </summary>
    /// <param name="hexString">Hexadecimal color string.</param>
    /// <param name="fallback">Fallback color to use on error.</param>
    /// <returns>Parsed Color or fallback.</returns>
    static Color ParseColor(string hexString, Color fallback)
    {
        if (string.IsNullOrWhiteSpace(hexString))
            return fallback;

        // Remove leading '#' if present.
        string hex = hexString.TrimStart('#');

        // Assume fully opaque if only RGB is provided.
        if (hex.Length == 6)
            hex = "FF" + hex;

        // Validate length (should be ARGB).
        if (hex.Length != 8)
            return fallback;

        // Convert hex to integer and create Color.
        if (int.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int argb))
        {
            return Color.FromArgb(argb);
        }

        return fallback;
    }
}