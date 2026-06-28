using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code using Aspose.BarCode with configuration loaded from a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Model representing the configuration options for the QR code generation.
    /// </summary>
    private class BarcodeConfig
    {
        public string CodeText { get; set; } = "https://example.com";
        public string ErrorLevel { get; set; } = "LevelH";
        public string EncodeMode { get; set; } = "ECIEncoding";
        public string ECIEncoding { get; set; } = "UTF8";
        public float ImageWidth { get; set; } = 300f;
        public float ImageHeight { get; set; } = 300f;
        public float Resolution { get; set; } = 300f;
        public string OutputPath { get; set; } = "qr.png";
    }

    /// <summary>
    /// Entry point of the application. Loads configuration, generates a QR code, and saves it to a file.
    /// </summary>
    static void Main()
    {
        const string configPath = "appsettings.json";

        // --------------------------------------------------------------------
        // Ensure a configuration file exists; create a default one if missing.
        // --------------------------------------------------------------------
        if (!File.Exists(configPath))
        {
            var defaultConfig = new BarcodeConfig();
            var json = JsonSerializer.Serialize(
                defaultConfig,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(configPath, json);
            Console.WriteLine($"Created default config file at '{configPath}'.");
        }

        // -------------------------------------------------
        // Load configuration from the JSON file into model.
        // -------------------------------------------------
        BarcodeConfig config;
        try
        {
            var jsonText = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<BarcodeConfig>(jsonText) ?? new BarcodeConfig();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read config: {ex.Message}");
            config = new BarcodeConfig(); // Fallback to defaults on error.
        }

        // -------------------------------------------------
        // Resolve enum values from strings safely, using defaults if parsing fails.
        // -------------------------------------------------
        QRErrorLevel errorLevel = QRErrorLevel.LevelH;
        if (Enum.TryParse<QRErrorLevel>(config.ErrorLevel, out var parsedError))
            errorLevel = parsedError;

        QREncodeMode encodeMode = QREncodeMode.ECIEncoding;
        if (Enum.TryParse<QREncodeMode>(config.EncodeMode, out var parsedMode))
            encodeMode = parsedMode;

        ECIEncodings eciEncoding = ECIEncodings.UTF8;
        if (Enum.TryParse<ECIEncodings>(config.ECIEncoding, out var parsedEci))
            eciEncoding = parsedEci;

        // -------------------------------------------------
        // Generate the QR code using Aspose.BarCode.
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, config.CodeText))
        {
            // Set QR-specific parameters.
            generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;
            generator.Parameters.Barcode.QR.EncodeMode = encodeMode;
            generator.Parameters.Barcode.QR.ECIEncoding = eciEncoding;

            // Set image dimensions and resolution.
            generator.Parameters.ImageWidth.Point = config.ImageWidth;
            generator.Parameters.ImageHeight.Point = config.ImageHeight;
            generator.Parameters.Resolution = config.Resolution;

            // Save the generated barcode image to the specified path.
            generator.Save(config.OutputPath);
        }

        Console.WriteLine($"QR code generated and saved to '{config.OutputPath}'.");
    }
}