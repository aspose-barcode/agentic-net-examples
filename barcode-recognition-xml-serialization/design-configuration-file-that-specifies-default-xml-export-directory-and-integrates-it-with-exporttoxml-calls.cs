using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Config
{
    public string ExportDirectory { get; set; } = "ExportXml";
}

class Program
{
    static void Main()
    {
        const string configPath = "appsettings.json";

        // Create a default config file if it does not exist
        if (!File.Exists(configPath))
        {
            var defaultConfig = new Config();
            var json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, json);
        }

        // Load configuration from JSON
        Config config;
        try
        {
            var jsonText = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<Config>(jsonText) ?? new Config();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read config: {ex.Message}");
            config = new Config();
        }

        // Ensure the export directory exists
        if (!Directory.Exists(config.ExportDirectory))
        {
            Directory.CreateDirectory(config.ExportDirectory);
        }

        // Generate a barcode and export its properties to XML using the configured directory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "Sample123";
            string xmlFile = Path.Combine(config.ExportDirectory, "barcode.xml");
            bool success = generator.ExportToXml(xmlFile);
            Console.WriteLine(success ? $"Exported to {xmlFile}" : "Export failed");
        }
    }
}