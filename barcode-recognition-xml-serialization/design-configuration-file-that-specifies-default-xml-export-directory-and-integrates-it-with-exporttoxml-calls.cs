// Title: Export barcode configuration to XML using Aspose.BarCode
// Description: Demonstrates loading a JSON configuration to set the default export directory and exporting a Code128 barcode generator's settings to an XML file.
// Category-Description: This example belongs to the Aspose.BarCode configuration export category, illustrating how to use the BarcodeGenerator class together with ExportToXml for persisting barcode settings. Developers often need to store generator parameters for later reuse or auditing, and this pattern shows reading configuration files, ensuring directories exist, and performing XML export—common tasks in automated barcode workflows.
// Prompt: Design a configuration file that specifies the default XML export directory and integrates it with ExportToXml calls.
// Tags: barcode symbology, export, xml, configuration, aspnet, aspose.barcode, code128, json

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExportExample
{
    /// <summary>
    /// Represents application configuration loaded from a JSON file.
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// Directory where XML exports will be saved.
        /// </summary>
        public string ExportDirectory { get; set; } = "Export";
    }

    /// <summary>
    /// Demonstrates loading configuration, ensuring export directory, generating a Code128 barcode,
    /// and exporting its settings to XML.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example.
        /// </summary>
        static void Main()
        {
            // Path to the JSON configuration file.
            const string configPath = "config.json";
            AppConfig config;

            // Load existing configuration or create a default one.
            if (File.Exists(configPath))
            {
                try
                {
                    string json = File.ReadAllText(configPath);
                    config = JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to read config file: {ex.Message}");
                    config = new AppConfig();
                }
            }
            else
            {
                // No config file – use defaults and persist them for future runs.
                config = new AppConfig();
                try
                {
                    string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(configPath, json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to write default config file: {ex.Message}");
                }
            }

            // Ensure the export directory exists.
            if (!Directory.Exists(config.ExportDirectory))
            {
                try
                {
                    Directory.CreateDirectory(config.ExportDirectory);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to create export directory '{config.ExportDirectory}': {ex.Message}");
                    return;
                }
            }

            // Create a simple Code128 barcode generator.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Optional: customize appearance.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 40f;

                // Build the full path for the XML export file.
                string xmlFilePath = Path.Combine(config.ExportDirectory, "barcode_export.xml");

                // Export generator settings to XML.
                bool exportSuccess = generator.ExportToXml(xmlFilePath);
                Console.WriteLine(exportSuccess
                    ? $"Barcode configuration exported successfully to '{xmlFilePath}'."
                    : $"Failed to export barcode configuration to '{xmlFilePath}'.");
            }
        }
    }
}