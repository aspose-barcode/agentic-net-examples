// Title: Generate QR Code with configurable parameters from JSON
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, reading generation settings from an appsettings.json file, and saving the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and QR-specific parameters such as error correction level and ECI encoding. Developers often need to customize barcode appearance and output settings via configuration files for automated workflows or dynamic content generation.
// Prompt: Generate a QR Code barcode and configure generation parameters through appsettings JSON file.
// Tags: qr code, barcode generation, configuration, json, aspose.barcode, image output

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode using settings loaded from a JSON configuration file.
/// </summary>
class Program
{
    /// <summary>
    /// Model representing the JSON configuration structure for barcode generation parameters.
    /// </summary>
    private class BarcodeConfig
    {
        public string CodeText { get; set; }
        public string ErrorLevel { get; set; }
        public string ECIEncoding { get; set; }
        public float? ImageWidth { get; set; }
        public float? ImageHeight { get; set; }
        public int? Resolution { get; set; }
    }

    /// <summary>
    /// Entry point of the application. Reads configuration, generates a QR Code, and saves it as an image file.
    /// </summary>
    static void Main()
    {
        const string configFile = "appsettings.json";

        // Ensure a configuration file exists; create a default one if missing.
        if (!File.Exists(configFile))
        {
            var defaultConfig = new BarcodeConfig
            {
                CodeText = "Hello Aspose QR!",
                ErrorLevel = "LevelH",
                ECIEncoding = "UTF8",
                ImageWidth = 300f,
                ImageHeight = 300f,
                Resolution = 96
            };
            var json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configFile, json);
            Console.WriteLine($"Created default configuration file '{configFile}'.");
        }

        // Load configuration from the JSON file.
        BarcodeConfig config;
        try
        {
            var jsonText = File.ReadAllText(configFile);
            config = JsonSerializer.Deserialize<BarcodeConfig>(jsonText);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Validate required fields.
        if (string.IsNullOrWhiteSpace(config?.CodeText))
        {
            Console.WriteLine("CodeText is missing in configuration.");
            return;
        }

        // Create QR barcode generator with QR encode type.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text to encode.
            generator.CodeText = config.CodeText;

            // Apply error correction level if provided and valid.
            if (!string.IsNullOrWhiteSpace(config.ErrorLevel) &&
                Enum.TryParse<QRErrorLevel>(config.ErrorLevel, out var errorLevel))
            {
                generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;
            }

            // Apply ECI encoding if provided and valid.
            if (!string.IsNullOrWhiteSpace(config.ECIEncoding) &&
                Enum.TryParse<ECIEncodings>(config.ECIEncoding, out var eciEncoding))
            {
                generator.Parameters.Barcode.QR.ECIEncoding = eciEncoding;
            }

            // Set image width if a positive value is supplied.
            if (config.ImageWidth.HasValue && config.ImageWidth.Value > 0f)
            {
                generator.Parameters.ImageWidth.Point = config.ImageWidth.Value;
            }

            // Set image height if a positive value is supplied.
            if (config.ImageHeight.HasValue && config.ImageHeight.Value > 0f)
            {
                generator.Parameters.ImageHeight.Point = config.ImageHeight.Value;
            }

            // Set resolution if a positive value is supplied.
            if (config.Resolution.HasValue && config.Resolution.Value > 0)
            {
                generator.Parameters.Resolution = config.Resolution.Value;
            }

            // Save the generated QR code image to a file.
            const string outputFile = "qr.png";
            generator.Save(outputFile);
            Console.WriteLine($"QR code generated and saved to '{outputFile}'.");
        }
    }
}