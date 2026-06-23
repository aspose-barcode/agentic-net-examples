using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Represents application configuration settings.
/// </summary>
class Config
{
    /// <summary>
    /// Gets or sets the directory where exported files will be saved.
    /// </summary>
    public string ExportDirectory { get; set; } = "Export";
}

/// <summary>
/// Main program class.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Handles configuration loading/creation,
    /// ensures the export directory exists, generates a barcode, and
    /// exports its configuration to an XML file.
    /// </summary>
    static void Main()
    {
        const string configPath = "config.json";

        Config config;

        // Load existing configuration or create a default one.
        if (File.Exists(configPath))
        {
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<Config>(json) ?? new Config();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read config: {ex.Message}");
                config = new Config();
            }
        }
        else
        {
            config = new Config();
            try
            {
                string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configPath, json);
                Console.WriteLine($"Created default config at '{configPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write default config: {ex.Message}");
            }
        }

        // Ensure the export directory exists.
        if (!Directory.Exists(config.ExportDirectory))
        {
            try
            {
                Directory.CreateDirectory(config.ExportDirectory);
                Console.WriteLine($"Created export directory '{config.ExportDirectory}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create export directory: {ex.Message}");
                return;
            }
        }

        // Define barcode parameters.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "1234567890";

        // Generate the barcode and export its configuration.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Optional barcode visual settings.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Build the full path for the XML export file.
            string xmlFilePath = Path.Combine(config.ExportDirectory, "barcode.xml");

            // Export the barcode configuration to XML.
            bool exported = generator.ExportToXml(xmlFilePath);
            Console.WriteLine(exported
                ? $"Barcode configuration exported to '{xmlFilePath}'."
                : $"Failed to export barcode configuration to '{xmlFilePath}'.");
        }
    }
}