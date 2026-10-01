// Title: Export barcode generator settings to XML using a configurable directory
// Description: Demonstrates loading a JSON configuration to determine the default export folder and exporting Aspose.BarCode generator settings to an XML file.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to persist generator parameters via ExportToXml. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and the Parameters property. Developers often need to store barcode settings for reuse, auditing, or batch processing, and this pattern provides a simple way to manage export locations through external configuration.
// Prompt: Design a configuration file that specifies the default XML export directory and integrates it with ExportToXml calls.
// Tags: barcode, export, xml, configuration, json, aspose.barcode, code128, generator

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Represents application configuration, currently holding the default export directory for XML files.
/// </summary>
class Config
{
    public string ExportDirectory { get; set; } = string.Empty;
}

/// <summary>
/// Sample console application that loads a JSON configuration, ensures the export directory exists,
/// generates a Code128 barcode, and exports the generator settings to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads configuration, creates export folder, generates a barcode, and writes its settings to XML.
    /// </summary>
    static void Main()
    {
        // Determine the path of the JSON configuration file located beside the executable.
        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appconfig.json");

        // Load existing configuration or create a default one if the file is missing or invalid.
        Config config = LoadOrCreateConfig(configPath);

        // Ensure the configured export directory exists; create it if necessary.
        if (!Directory.Exists(config.ExportDirectory))
        {
            Directory.CreateDirectory(config.ExportDirectory);
        }

        // Generate a simple Code128 barcode with the text "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Adjust the font size of the barcode's human‑readable text.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Build the full file path for the XML export using the configured directory.
            string xmlFilePath = Path.Combine(config.ExportDirectory, "barcode_config.xml");

            // Export the generator's current settings to the specified XML file.
            generator.ExportToXml(xmlFilePath);
        }

        // Inform the user that the export completed successfully.
        Console.WriteLine("Barcode configuration exported to XML successfully.");
    }

    /// <summary>
    /// Loads configuration from the given path or creates a default configuration file if none exists.
    /// </summary>
    /// <param name="path">Full path to the JSON configuration file.</param>
    /// <returns>A <see cref="Config"/> instance with a valid ExportDirectory.</returns>
    static Config LoadOrCreateConfig(string path)
    {
        if (File.Exists(path))
        {
            try
            {
                // Read the JSON content and deserialize it into a Config object.
                string json = File.ReadAllText(path);
                Config? cfg = JsonSerializer.Deserialize<Config>(json);
                if (cfg != null && !string.IsNullOrWhiteSpace(cfg.ExportDirectory))
                {
                    return cfg;
                }
            }
            catch (Exception ex)
            {
                // Log any errors encountered while reading or deserializing the config.
                Console.WriteLine($"Failed to read config: {ex.Message}");
            }
        }

        // Define a default configuration with an ExportDirectory under the application base folder.
        var defaultConfig = new Config
        {
            ExportDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExportXml")
        };

        try
        {
            // Serialize the default configuration to formatted JSON and write it to disk.
            string json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            // Log any errors encountered while writing the default config file.
            Console.WriteLine($"Failed to write default config: {ex.Message}");
        }

        return defaultConfig;
    }
}