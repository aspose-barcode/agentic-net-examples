// Title: Export Barcode Generator Settings to XML Using Configurable Directory
// Description: Demonstrates loading a JSON configuration to determine the export folder and then exporting barcode generator settings to an XML file.
// Prompt: Design a configuration file that specifies the default XML export directory and integrates it with ExportToXml calls.
// Tags: barcode symbology, export, xml, configuration, aspnet, aspose.barcodes

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExportConfigExample
{
    /// <summary>
    /// Simple configuration class matching the JSON structure.
    /// Contains the default directory where exported XML files are saved.
    /// </summary>
    public class AppConfig
    {
        public string ExportDirectory { get; set; } = "Export";
    }

    class Program
    {
        /// <summary>
        /// Entry point of the example.
        /// Loads configuration, ensures the export directory exists, generates a barcode,
        /// and exports its settings to an XML file in the configured location.
        /// </summary>
        static void Main()
        {
            // Load configuration from "config.json" if it exists; otherwise use defaults.
            var config = LoadConfiguration("config.json");

            // Ensure the export directory exists; create it if necessary.
            if (!Directory.Exists(config.ExportDirectory))
            {
                Directory.CreateDirectory(config.ExportDirectory);
            }

            // Create a barcode generator for Code128 with sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Optional: customize barcode appearance here if needed.
                // e.g., generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

                // Build the full path for the exported XML file.
                string xmlPath = Path.Combine(config.ExportDirectory, "barcode_properties.xml");

                // Export generator settings to XML file.
                bool exported = generator.ExportToXml(xmlPath);
                Console.WriteLine(exported
                    ? $"Barcode configuration exported successfully to: {xmlPath}"
                    : $"Failed to export barcode configuration to: {xmlPath}");
            }
        }

        /// <summary>
        /// Reads configuration from a JSON file; falls back to defaults on any error.
        /// </summary>
        /// <param name="filePath">Path to the JSON configuration file.</param>
        /// <returns>An <see cref="AppConfig"/> instance with loaded or default values.</returns>
        private static AppConfig LoadConfiguration(string filePath)
        {
            // If the config file does not exist, return a new instance with default values.
            if (!File.Exists(filePath))
            {
                return new AppConfig();
            }

            try
            {
                // Read the entire JSON content.
                string json = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                // Deserialize JSON into AppConfig; if null, use defaults.
                var config = JsonSerializer.Deserialize<AppConfig>(json, options);
                return config ?? new AppConfig();
            }
            catch (Exception ex)
            {
                // Log any errors and revert to default configuration.
                Console.WriteLine($"Error loading configuration: {ex.Message}");
                return new AppConfig();
            }
        }
    }
}