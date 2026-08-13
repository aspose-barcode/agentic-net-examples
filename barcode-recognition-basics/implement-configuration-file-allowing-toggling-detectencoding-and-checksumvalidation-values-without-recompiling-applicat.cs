// Title: QR Code Generation and Recognition with Configurable Encoding Detection
// Description: Demonstrates generating a QR code containing Unicode text and reading it while toggling DetectEncoding and ChecksumValidation via a JSON config file.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showcasing how to use BarcodeGenerator, BarCodeReader, and BarcodeSettings to control encoding detection and checksum validation. Developers often need to adjust these settings at runtime without recompiling, especially when processing diverse barcode sources in enterprise applications.
// Prompt: Implement a configuration file allowing toggling DetectEncoding and ChecksumValidation values without recompiling the application.
// Tags: qr, unicode, encoding, checksum, configuration, aspose.barcode, barcodegeneration, barcoderecognition

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Represents the configurable settings for barcode reading.
/// </summary>
public class Config
{
    /// <summary>
    /// Gets or sets a value indicating whether the reader should attempt to detect the text encoding.
    /// </summary>
    public bool DetectEncoding { get; set; } = true;

    /// <summary>
    /// Gets or sets the checksum validation mode for the reader.
    /// </summary>
    public ChecksumValidation ChecksumValidation { get; set; } = ChecksumValidation.Default;
}

/// <summary>
/// Example program that generates a QR code with Unicode text and reads it using configurable settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, loads configuration, and reads the barcode.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Load configuration from "config.json" if it exists; otherwise use defaults.
        // --------------------------------------------------------------------
        Config config;
        const string configPath = "config.json";

        if (File.Exists(configPath))
        {
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<Config>(json) ?? new Config();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read config file: {ex.Message}");
                config = new Config();
            }
        }
        else
        {
            config = new Config();
        }

        // --------------------------------------------------------------------
        // Define file path and sample Unicode text for the QR code.
        // --------------------------------------------------------------------
        const string barcodePath = "barcode.png";
        const string unicodeText = "Привет"; // Sample Unicode text

        // --------------------------------------------------------------------
        // Generate a QR code image containing the Unicode text.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.SetCodeText(unicodeText, Encoding.UTF8);
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Read the generated QR code using settings from the configuration.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // Apply configuration values to the reader's settings.
            reader.BarcodeSettings.DetectEncoding = config.DetectEncoding;
            reader.BarcodeSettings.ChecksumValidation = config.ChecksumValidation;

            // Iterate through all detected barcodes (single in this case).
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"DetectEncoding: {reader.BarcodeSettings.DetectEncoding}");
                Console.WriteLine($"ChecksumValidation: {reader.BarcodeSettings.ChecksumValidation}");
            }
        }
    }
}