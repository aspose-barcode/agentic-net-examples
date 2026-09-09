// Title: Barcode generation with configurable checksum settings per symbology
// Description: Demonstrates loading checksum preferences from a simple config file and applying them while generating barcodes.
// Category-Description: This example belongs to the Aspose.BarCode configuration category, illustrating how to use Aspose.BarCode.Generation.BarcodeGenerator together with EncodeTypes and EnableChecksum to customize barcode creation. Developers often need to store default settings such as checksum enablement per symbology in external files for consistent generation across applications. The snippet shows reading a text file, parsing enum values, and applying them during barcode initialization.
// Prompt: Create a configuration file storing default checksum settings per symbology and load it during barcode initialization.
// Tags: barcode, checksum, symbology, configuration, aspnet, aspose.barcode, generation, png

using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates loading checksum configuration and generating sample barcodes for selected symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary config, loads settings, generates barcodes, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Define a temporary path for the configuration file
        string configPath = Path.Combine(Path.GetTempPath(), "checksumConfig.txt");
        CreateDefaultConfig(configPath);

        // Load checksum settings from the configuration file
        var checksumSettings = LoadConfig(configPath);

        // Create a unique output directory for generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // List of symbology names to demonstrate
        var symbologies = new List<string> { "Code39Extended", "Code128", "Codabar" };

        // Iterate over each symbology, generate a barcode, and apply checksum settings if defined
        foreach (var symName in symbologies)
        {
            // Resolve the EncodeType enum value from the symbology name
            BaseEncodeType encodeType = ResolveEncodeType(symName);
            if (encodeType == null)
            {
                Console.WriteLine($"Symbology '{symName}' not found.");
                continue;
            }

            // Get sample code text appropriate for the current symbology
            string codeText = GetSampleCodeText(symName);

            // Initialize the barcode generator with the resolved type and sample text
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply checksum setting from the configuration if present
                if (checksumSettings.TryGetValue(symName, out EnableChecksum checksumMode))
                {
                    generator.Parameters.Barcode.IsChecksumEnabled = checksumMode;
                }

                // Save the generated barcode as a PNG file
                string outPath = Path.Combine(outputDir, $"{symName}.png");
                generator.Save(outPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated {symName} barcode at: {outPath}");
            }
        }

        Console.WriteLine("Processing completed.");
    }

    // Creates a simple text configuration file with default checksum settings
    static void CreateDefaultConfig(string path)
    {
        var lines = new List<string>
        {
            "Code39Extended=No",
            "Code128=Yes",
            "Codabar=Default"
        };
        File.WriteAllLines(path, lines);
    }

    // Loads checksum settings from the configuration file into a dictionary
    static Dictionary<string, EnableChecksum> LoadConfig(string path)
    {
        var dict = new Dictionary<string, EnableChecksum>(StringComparer.OrdinalIgnoreCase);
        if (!File.Exists(path))
        {
            Console.WriteLine($"Config file not found: {path}");
            return dict;
        }

        foreach (var line in File.ReadAllLines(path))
        {
            if (string.IsNullOrWhiteSpace(line) || !line.Contains("="))
                continue;

            var parts = line.Split(new[] { '=' }, 2);
            string sym = parts[0].Trim();
            string val = parts[1].Trim();

            try
            {
                var mode = (EnableChecksum)Enum.Parse(typeof(EnableChecksum), val, true);
                dict[sym] = mode;
            }
            catch
            {
                Console.WriteLine($"Invalid checksum value '{val}' for symbology '{sym}'.");
            }
        }
        return dict;
    }

    // Resolves a symbology name to its corresponding BaseEncodeType using reflection
    static BaseEncodeType ResolveEncodeType(string symbologyName)
    {
        var field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
            return null;
        return (BaseEncodeType)field.GetValue(null);
    }

    // Provides sample code text for each supported symbology
    static string GetSampleCodeText(string symbologyName)
    {
        return symbologyName switch
        {
            "Code39Extended" => "CODE39",
            "Code128" => "CODE128",
            "Codabar" => "-12345-",
            _ => "Sample"
        };
    }
}