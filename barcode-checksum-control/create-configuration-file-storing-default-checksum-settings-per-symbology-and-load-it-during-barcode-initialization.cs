// Title: Load checksum settings from JSON configuration and generate barcodes
// Description: Demonstrates how to store default checksum options per symbology in a JSON file, load them at runtime, and apply them when creating barcodes with Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode configuration category, illustrating the use of EncodeTypes, BarcodeGenerator, and EnableChecksum to control checksum behavior. Typical scenarios include batch barcode generation where checksum rules vary by symbology and need to be centrally managed via a config file. Developers often need to read settings, map symbology names to BaseEncodeType, and apply them consistently across generated images.
// Prompt: Create a configuration file storing default checksum settings per symbology and load it during barcode initialization.
// Tags: barcode symbology, checksum, configuration, aspose.barcode, generation, json

using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates sample barcodes using checksum settings loaded from a JSON configuration file.
/// </summary>
class Program
{
    // Path to the configuration file that stores checksum settings per symbology
    private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "checksumConfig.json");

    /// <summary>
    /// Entry point. Ensures the configuration exists, loads checksum settings, and creates sample barcode images.
    /// </summary>
    static void Main()
    {
        // Ensure configuration file exists with default settings
        EnsureConfigFile();

        // Load checksum settings from the configuration file
        Dictionary<BaseEncodeType, EnableChecksum> checksumSettings = LoadChecksumSettings();

        // Define sample barcodes to generate
        var samples = new List<(string SymbologyName, string CodeText, string OutputFile)>
        {
            ("Code128", "ABC123", "code128.png"),
            ("Code39", "HELLO", "code39.png")
        };

        // Iterate over each sample, resolve its symbology, apply checksum, and generate the image
        foreach (var (symbologyName, codeText, outputFile) in samples)
        {
            // Resolve symbology name to BaseEncodeType using reflection
            BaseEncodeType encodeType = ResolveEncodeType(symbologyName);
            if (encodeType == null)
            {
                Console.WriteLine($"Unknown symbology: {symbologyName}. Skipping.");
                continue;
            }

            // Determine checksum setting for this symbology (default to Yes if not configured)
            EnableChecksum checksumSetting = EnableChecksum.Yes;
            if (checksumSettings.TryGetValue(encodeType, out EnableChecksum setting))
            {
                checksumSetting = setting;
            }

            // Generate the barcode with the loaded checksum setting
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;
                generator.Save(outputFile, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated {outputFile} with checksum {checksumSetting} for {symbologyName}.");
            }
        }
    }

    // Creates a default configuration file if it does not exist
    private static void EnsureConfigFile()
    {
        if (File.Exists(ConfigPath))
            return;

        var defaultConfig = new Dictionary<string, string>
        {
            // Symbology name -> checksum setting ("Yes" or "No")
            { "Code128", "Yes" },   // Code128 always requires checksum; setting kept for completeness
            { "Code39", "No" }      // Example: disable checksum for Code39
        };

        string json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(ConfigPath, json);
        Console.WriteLine($"Created default checksum configuration at {ConfigPath}");
    }

    // Loads the configuration file and converts it to a dictionary of BaseEncodeType -> EnableChecksum
    private static Dictionary<BaseEncodeType, EnableChecksum> LoadChecksumSettings()
    {
        var result = new Dictionary<BaseEncodeType, EnableChecksum>();

        if (!File.Exists(ConfigPath))
        {
            Console.WriteLine("Configuration file not found. No checksum settings will be applied.");
            return result;
        }

        try
        {
            string json = File.ReadAllText(ConfigPath);
            var rawDict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            if (rawDict == null)
                return result;

            foreach (var kvp in rawDict)
            {
                // Resolve symbology name from config to BaseEncodeType
                BaseEncodeType encodeType = ResolveEncodeType(kvp.Key);
                if (encodeType == null)
                {
                    Console.WriteLine($"Unrecognized symbology in config: {kvp.Key}. Skipping.");
                    continue;
                }

                // Convert string value to EnableChecksum enum
                EnableChecksum checksumValue = kvp.Value.Equals("Yes", StringComparison.OrdinalIgnoreCase)
                    ? EnableChecksum.Yes
                    : EnableChecksum.No;

                result[encodeType] = checksumValue;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load checksum configuration: {ex.Message}");
        }

        return result;
    }

    // Resolves a symbology name (e.g., "Code128") to the corresponding BaseEncodeType using reflection
    private static BaseEncodeType ResolveEncodeType(string symbologyName)
    {
        if (string.IsNullOrWhiteSpace(symbologyName))
            return null;

        // EncodeTypes members are static fields returning BaseEncodeType
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
        if (field == null)
            return null;

        return field.GetValue(null) as BaseEncodeType;
    }
}