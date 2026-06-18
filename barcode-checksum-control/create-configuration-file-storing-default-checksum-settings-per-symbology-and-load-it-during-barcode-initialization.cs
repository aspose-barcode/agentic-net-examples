using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Represents the configuration that maps barcode symbology names to their checksum settings.
/// </summary>
class ChecksumConfig
{
    // Dictionary where the key is the symbology name and the value indicates whether checksum is enabled.
    public Dictionary<string, EnableChecksum> Settings { get; set; } = new();
}

/// <summary>
/// Entry point for the barcode generation sample application.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that creates a default configuration (if missing), loads the configuration,
    /// and generates sample barcodes using the specified checksum settings.
    /// </summary>
    static void Main()
    {
        const string configPath = "checksumConfig.json";

        // --------------------------------------------------------------------
        // Create default configuration file if it does not already exist.
        // --------------------------------------------------------------------
        if (!File.Exists(configPath))
        {
            var defaultConfig = new ChecksumConfig();

            // Define default checksum settings per symbology.
            defaultConfig.Settings[EncodeTypes.Code39FullASCII.TypeName] = EnableChecksum.Yes;
            defaultConfig.Settings[EncodeTypes.Code128.TypeName] = EnableChecksum.Yes; // mandatory
            defaultConfig.Settings[EncodeTypes.ITF14.TypeName] = EnableChecksum.No;
            defaultConfig.Settings[EncodeTypes.EAN13.TypeName] = EnableChecksum.Default;
            defaultConfig.Settings[EncodeTypes.DatabarOmniDirectional.TypeName] = EnableChecksum.Default;

            // Serialize the configuration to formatted JSON and write to disk.
            var json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, json);
            Console.WriteLine($"Created default config at '{configPath}'.");
        }

        // --------------------------------------------------------------------
        // Load configuration from file.
        // --------------------------------------------------------------------
        ChecksumConfig config;
        try
        {
            var json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<ChecksumConfig>(json) ?? new ChecksumConfig();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load config: {ex.Message}");
            return;
        }

        // --------------------------------------------------------------------
        // Sample data for each supported symbology.
        // --------------------------------------------------------------------
        var sampleData = new Dictionary<string, string>
        {
            { EncodeTypes.Code39FullASCII.TypeName, "ABC-123" },
            { EncodeTypes.Code128.TypeName, "1234567890" },
            { EncodeTypes.ITF14.TypeName, "01234567890123" },
            { EncodeTypes.EAN13.TypeName, "1234567890128" },
            { EncodeTypes.DatabarOmniDirectional.TypeName, "(01)01234567890123" }
        };

        // --------------------------------------------------------------------
        // Generate barcodes using the loaded checksum settings.
        // --------------------------------------------------------------------
        foreach (var kvp in config.Settings)
        {
            string symbologyName = kvp.Key;
            EnableChecksum checksumSetting = kvp.Value;

            // Resolve the symbology name to a BaseEncodeType instance via reflection.
            var field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology: {symbologyName}. Skipping.");
                continue;
            }

            var encodeType = (BaseEncodeType)field.GetValue(null)!;

            // Retrieve sample code text; fall back to a generic placeholder if not defined.
            string codeText = sampleData.ContainsKey(symbologyName) ? sampleData[symbologyName] : "Sample";

            // Create a barcode generator with the specified type and code text.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply the checksum configuration from the settings.
                generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;

                // Save the generated barcode image to a PNG file.
                string fileName = $"{symbologyName}_barcode.png";
                generator.Save(fileName);
                Console.WriteLine($"Generated '{fileName}' with checksum setting '{checksumSetting}'.");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}