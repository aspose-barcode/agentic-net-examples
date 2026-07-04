// Title: Barcode generation and recognition with configurable settings
// Description: Demonstrates generating an EAN13 barcode, reading it, and using a JSON configuration file to toggle DetectEncoding and ChecksumValidation without recompiling.
// Prompt: Implement a configuration file allowing toggling DetectEncoding and ChecksumValidation values without recompiling the application.
// Tags: barcode, ean13, generation, recognition, configuration, json

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Represents the configurable settings for barcode reading.
/// </summary>
class Config
{
    /// <summary>
    /// Gets or sets a value indicating whether the reader should attempt to detect the encoding of the barcode.
    /// </summary>
    public bool DetectEncoding { get; set; } = true;

    /// <summary>
    /// Gets or sets the checksum validation mode for the barcode reader.
    /// </summary>
    public ChecksumValidation ChecksumValidation { get; set; } = ChecksumValidation.Default;
}

/// <summary>
/// Entry point of the application that generates a sample barcode, reads it, and applies configuration settings.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that orchestrates barcode generation, configuration loading, and barcode reading.
    /// </summary>
    static void Main()
    {
        // Path to the JSON configuration file.
        const string configPath = "barcodeConfig.json";

        // Load configuration from file or create a default one if the file does not exist.
        Config config;
        if (File.Exists(configPath))
        {
            try
            {
                // Read JSON content.
                string json = File.ReadAllText(configPath);
                // Deserialize JSON into Config object (case-insensitive).
                config = JsonSerializer.Deserialize<Config>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new Config();
            }
            catch
            {
                // If deserialization fails, fall back to default configuration.
                config = new Config();
            }
        }
        else
        {
            // Create a new default configuration and persist it to disk.
            config = new Config();
            string defaultJson = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, defaultJson);
        }

        // Path to the sample barcode image.
        const string imagePath = "sample.png";

        // Generate a sample EAN13 barcode image if it does not already exist.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
            {
                generator.Save(imagePath);
            }
        }

        // Initialize the barcode reader with the desired decode type.
        using (var reader = new BarCodeReader(imagePath, DecodeType.EAN13))
        {
            // Apply configuration values to the reader's settings.
            reader.BarcodeSettings.DetectEncoding = config.DetectEncoding;
            reader.BarcodeSettings.ChecksumValidation = config.ChecksumValidation;

            // Iterate through all detected barcodes.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");

                // For 1D barcodes, checksum information is available in the extended data.
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                }
            }
        }

        // Indicate that processing has completed.
        Console.WriteLine("Barcode processing completed.");
    }
}