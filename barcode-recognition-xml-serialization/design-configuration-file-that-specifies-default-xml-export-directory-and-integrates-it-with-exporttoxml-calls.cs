// Title: Export Barcode Generation State to XML Using Configurable Directory
// Description: Demonstrates generating a Code128 barcode with Aspose.BarCode, exporting its generation state to an XML file, and storing the file in a directory defined by a JSON configuration.
// Category-Description: This example belongs to the Aspose.BarCode configuration and export category. It shows how to work with the BarcodeGenerator class, configure visual properties, and use ExportToXml to persist generation settings. Typical use cases include saving barcode setup for later reuse, auditing, or integration with downstream systems. Developers often need to externalize settings via configuration files and programmatically manage export locations.
// Prompt: Design a configuration file that specifies the default XML export directory and integrates it with ExportToXml calls.
// Tags: barcode symbology, export, xml, configuration, aspose.barcode, generation

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an example of generating a barcode, exporting its configuration to XML,
/// and using a JSON configuration file to specify the export directory.
/// </summary>
class Program
{
    // Name of the JSON configuration file.
    const string ConfigFileName = "config.json";

    /// <summary>
    /// Simple POCO representing the configuration settings.
    /// </summary>
    class Config
    {
        public string ExportDirectory { get; set; }
    }

    /// <summary>
    /// Loads the configuration from <c>config.json</c> or creates a default one if the file does not exist.
    /// </summary>
    /// <returns>An instance of <see cref="Config"/> containing the export directory.</returns>
    static Config LoadOrCreateConfig()
    {
        // If the config file is missing, create a default configuration.
        if (!File.Exists(ConfigFileName))
        {
            var defaultDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeExport");
            var defaultConfig = new Config { ExportDirectory = defaultDir };
            var json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigFileName, json);
            return defaultConfig;
        }

        // Read existing configuration.
        var content = File.ReadAllText(ConfigFileName);
        return JsonSerializer.Deserialize<Config>(content);
    }

    /// <summary>
    /// Entry point of the program. Generates a barcode, ensures the export directory exists,
    /// and writes the generator state to an XML file.
    /// </summary>
    static void Main()
    {
        // Load configuration (or create a default one).
        Config config = LoadOrCreateConfig();

        // Ensure the configured export directory exists.
        if (!Directory.Exists(config.ExportDirectory))
        {
            Directory.CreateDirectory(config.ExportDirectory);
        }

        // Generate a Code128 barcode with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set visual appearance (optional).
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Build the full path for the XML export file.
            string xmlPath = Path.Combine(config.ExportDirectory, "barcodeConfig.xml");

            // Export the generator's state to XML.
            generator.ExportToXml(xmlPath);

            // Inform the user where the file was saved.
            Console.WriteLine($"Barcode generation state exported to: {xmlPath}");
        }
    }
}