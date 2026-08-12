// Title: Generate QR Code barcode using Aspose.BarCode with settings from appsettings.json
// Description: Demonstrates how to read barcode generation parameters from a JSON configuration file and create a QR Code image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and QR-specific parameters such as error correction level, dimensions, margins, and colors. Developers often need to externalize barcode settings for flexibility, and this pattern shows typical configuration‑driven generation for QR codes, suitable for web services, batch processing, or CI pipelines.
// Prompt: Generate a QR Code barcode and configure generation parameters through appsettings JSON file.
// Tags: qr code, barcode generation, json configuration, aspose.barcode, encoding, image output

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code barcode using Aspose.BarCode with parameters loaded from a JSON configuration file.
/// </summary>
class Program
{
    // Configuration model matching the JSON structure
    private class BarcodeSettings
    {
        public string CodeText { get; set; }
        public string ErrorLevel { get; set; }
        public double XDimension { get; set; }
        public double Margin { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public string OutputFile { get; set; }
    }

    private class AppConfig
    {
        public BarcodeSettings BarcodeSettings { get; set; }
    }

    /// <summary>
    /// Entry point. Reads configuration, sets up the QR code generator, and saves the barcode image.
    /// </summary>
    /// <param name="args">Optional command‑line arguments; the first argument can specify the path to the appsettings JSON file.</param>
    static void Main(string[] args)
    {
        // Determine the path to the appsettings JSON file (default to "appsettings.json")
        string configPath = args.Length > 0 ? args[0] : "appsettings.json";

        // Verify that the configuration file exists
        if (!File.Exists(configPath))
        {
            Console.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load and deserialize the configuration JSON into strongly‑typed objects
        AppConfig config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<AppConfig>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (config?.BarcodeSettings == null)
                throw new InvalidOperationException("Invalid configuration structure.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        BarcodeSettings settings = config.BarcodeSettings;

        // Resolve the QR error correction level enum from the string value
        if (!Enum.TryParse<QRErrorLevel>(settings.ErrorLevel, ignoreCase: true, out QRErrorLevel errorLevel))
        {
            Console.WriteLine($"Invalid QRErrorLevel value: {settings.ErrorLevel}");
            return;
        }

        // Resolve foreground and background colors (fallback to defaults on failure)
        Color foreColor = ParseColor(settings.ForegroundColor, Aspose.Drawing.Color.Black);
        Color backColor = ParseColor(settings.BackgroundColor, Aspose.Drawing.Color.White);

        // Ensure the output directory exists before saving the image
        string outputPath = settings.OutputFile ?? "qr.png";
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create and configure the QR code generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text to encode
            generator.CodeText = settings.CodeText ?? string.Empty;

            // QR‑specific parameters
            generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;

            // General barcode parameters
            generator.Parameters.Barcode.XDimension.Point = (float)settings.XDimension;
            generator.Parameters.Barcode.Padding.Left.Point = (float)settings.Margin;
            generator.Parameters.Barcode.Padding.Top.Point = (float)settings.Margin;
            generator.Parameters.Barcode.Padding.Right.Point = (float)settings.Margin;
            generator.Parameters.Barcode.Padding.Bottom.Point = (float)settings.Margin;

            // Apply colors
            generator.Parameters.Barcode.BarColor = foreColor;
            generator.Parameters.BackColor = backColor;

            // Save the generated barcode image to the specified file
            generator.Save(outputPath);
        }

        Console.WriteLine($"QR code generated and saved to: {outputPath}");
    }

    // Helper to parse a color name or hex value; defaults to fallbackColor on failure
    private static Color ParseColor(string colorString, Color fallbackColor)
    {
        if (string.IsNullOrWhiteSpace(colorString))
            return fallbackColor;

        try
        {
            // Try known named colors first (case‑insensitive)
            var known = typeof(Color).GetProperty(colorString, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.IgnoreCase);
            if (known != null && known.PropertyType == typeof(Color))
                return (Color)known.GetValue(null);

            // Try hex format #RRGGBB or #AARRGGBB
            if (colorString.StartsWith("#"))
            {
                return ColorTranslator.FromHtml(colorString);
            }
        }
        catch
        {
            // Ignore parsing errors and fall back
        }

        return fallbackColor;
    }
}