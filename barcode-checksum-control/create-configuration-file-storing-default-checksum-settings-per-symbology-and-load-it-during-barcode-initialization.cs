// Title: Load checksum settings from JSON and generate barcodes per symbology
// Description: Demonstrates reading a JSON configuration file that defines default checksum settings for various barcode symbologies, then creates sample barcodes using Aspose.BarCode with those settings.
// Category-Description: This example belongs to the Aspose.BarCode configuration and generation category, illustrating how to use EncodeTypes, BarcodeGenerator, and the IsChecksumEnabled property. Developers often need to apply consistent checksum rules across multiple symbologies, and loading such rules from external files (e.g., JSON) simplifies maintenance and deployment. The pattern shown is common for batch barcode creation, automated reporting, and integration pipelines.
// Prompt: Create a configuration file storing default checksum settings per symbology and load it during barcode initialization.
// Tags: barcode, symbology, checksum, json, configuration, aspose.barcode, generation, encode types

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates loading checksum configuration from a JSON file and generating sample barcodes for each symbology.
/// </summary>
class Program
{
    private const string ConfigFileName = "checksumConfig.json";

    /// <summary>
    /// Entry point. Ensures configuration exists, loads it, and generates barcodes with the specified checksum settings.
    /// </summary>
    static void Main()
    {
        // Build the full path to the configuration file in the temporary folder.
        string configPath = Path.Combine(Path.GetTempPath(), ConfigFileName);

        // Create a default configuration file if one does not already exist.
        EnsureConfigExists(configPath);

        // Load the checksum settings from the JSON configuration.
        var config = LoadConfig(configPath);

        // Iterate over each symbology defined in the configuration.
        foreach (var kvp in config)
        {
            string symName = kvp.Key;
            EnableChecksum checksumSetting = kvp.Value;

            // Resolve the EncodeTypes field name to a BaseEncodeType instance.
            BaseEncodeType encodeType = ResolveEncodeType(symName);
            if (encodeType == null)
            {
                Console.WriteLine($"Symbology '{symName}' not recognized.");
                continue;
            }

            // Get a sample code text appropriate for the current symbology.
            string codeText = GetSampleCodeText(symName);

            // Define the output image path for the generated barcode.
            string outputPath = Path.Combine(Path.GetTempPath(), $"{symName}.png");

            // Generate the barcode with the configured checksum setting.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated {symName} barcode with checksum setting {checksumSetting} at {outputPath}");
        }
    }

    /// <summary>
    /// Creates a default JSON configuration file containing checksum settings if it does not already exist.
    /// </summary>
    /// <param name="path">Full path to the configuration file.</param>
    static void EnsureConfigExists(string path)
    {
        if (File.Exists(path))
            return;

        var defaultConfig = new Dictionary<string, EnableChecksum>
        {
            { "Code39Extended", EnableChecksum.No },
            { "Code128", EnableChecksum.Yes },
            { "Codabar", EnableChecksum.Default }
        };

        string json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// Loads the checksum configuration from a JSON file.
    /// </summary>
    /// <param name="path">Full path to the configuration file.</param>
    /// <returns>Dictionary mapping symbology names to their checksum settings.</returns>
    static Dictionary<string, EnableChecksum> LoadConfig(string path)
    {
        try
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Dictionary<string, EnableChecksum>>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load config: {ex.Message}");
            return new Dictionary<string, EnableChecksum>();
        }
    }

    /// <summary>
    /// Resolves a symbology name to its corresponding <see cref="BaseEncodeType"/> using reflection.
    /// </summary>
    /// <param name="symName">The name of the symbology (matches a field in <see cref="EncodeTypes"/>).</param>
    /// <returns>The resolved <see cref="BaseEncodeType"/>, or null if not found.</returns>
    static BaseEncodeType ResolveEncodeType(string symName)
    {
        FieldInfo field = typeof(EncodeTypes).GetField(symName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
            return null;
        return (BaseEncodeType)field.GetValue(null);
    }

    /// <summary>
    /// Provides sample code text for a given symbology.
    /// </summary>
    /// <param name="symName">The symbology name.</param>
    /// <returns>Sample text suitable for the specified symbology.</returns>
    static string GetSampleCodeText(string symName)
    {
        return symName switch
        {
            "Code39Extended" => "CODE39",
            "Code128" => "CODE128",
            "Codabar" => "-12345-",
            _ => "SAMPLE"
        };
    }
}