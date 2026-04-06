using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;

namespace BarcodeExportConfigExample
{
    // Simple configuration class matching the JSON structure
    public class Config
    {
        public string ExportDirectory { get; set; }
    }

    class Program
    {
        // Path to the configuration file
        private const string ConfigFileName = "appsettings.json";

        static void Main()
        {
            // Load or create configuration
            Config config = LoadOrCreateConfig();

            // Ensure the export directory exists
            if (!Directory.Exists(config.ExportDirectory))
            {
                Directory.CreateDirectory(config.ExportDirectory);
            }

            // Define the barcode to generate
            const string barcodeText = "123ABC";
            const string xmlFileName = "barcode_properties.xml";
            string xmlFullPath = Path.Combine(config.ExportDirectory, xmlFileName);

            // Generate barcode and export its properties to XML
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = barcodeText;

                // Export properties to the XML file defined in the configuration
                bool exportSuccess = generator.ExportToXml(xmlFullPath);
                Console.WriteLine(exportSuccess
                    ? $"Export succeeded: {xmlFullPath}"
                    : "Export failed.");
            }
        }

        // Loads the configuration from JSON; creates a default one if missing
        private static Config LoadOrCreateConfig()
        {
            if (File.Exists(ConfigFileName))
            {
                try
                {
                    string json = File.ReadAllText(ConfigFileName);
                    Config cfg = JsonSerializer.Deserialize<Config>(json);
                    if (cfg != null && !string.IsNullOrWhiteSpace(cfg.ExportDirectory))
                    {
                        return cfg;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading config: {ex.Message}");
                }
            }

            // Default configuration
            var defaultConfig = new Config
            {
                ExportDirectory = Path.Combine(Directory.GetCurrentDirectory(), "ExportXml")
            };

            // Save default configuration for future runs
            try
            {
                string defaultJson = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ConfigFileName, defaultJson);
                Console.WriteLine($"Created default config file: {ConfigFileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing default config: {ex.Message}");
            }

            return defaultConfig;
        }
    }
}