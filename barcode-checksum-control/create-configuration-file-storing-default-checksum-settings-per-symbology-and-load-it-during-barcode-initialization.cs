// Title: Load checksum settings from a configuration file and generate barcodes
// Description: Demonstrates reading per‑symbology checksum preferences from a text file and applying them when creating barcodes with Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode configuration and generation category. It shows how to use EncodeTypes, BarcodeGenerator, and the Parameters.Barcode properties (IsChecksumEnabled, ChecksumAlwaysShow) to customize barcode output. Typical use cases include applying default checksum rules across multiple symbologies, reading settings from external files, and producing PNG images for downstream processing. Developers often need such patterns when building batch barcode creation tools or integrating barcode generation into automated pipelines.
// Prompt: Create a configuration file storing default checksum settings per symbology and load it during barcode initialization.
// Tags: barcode symbology, checksum, configuration, generation, aspose.barcode, png output

using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates loading checksum settings from a configuration file and generating barcodes for multiple symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary configuration file if missing, reads checksum preferences, and generates PNG barcodes accordingly.
    /// </summary>
    static void Main()
    {
        // Define temporary paths for the configuration file and output directory
        string configPath = Path.Combine(Path.GetTempPath(), "ChecksumConfig.txt");
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeOutputs_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Ensure the configuration file exists; create a default one if it does not
        if (!File.Exists(configPath))
        {
            using (var writer = new StreamWriter(configPath))
            {
                writer.WriteLine("Code39=Yes");
                writer.WriteLine("Code128=No");
                writer.WriteLine("Codabar=Default");
            }
        }

        // Load checksum settings from the configuration file
        var settings = LoadChecksumSettings(configPath);

        // Iterate over each symbology and generate the corresponding barcode
        foreach (var kvp in settings)
        {
            string symName = kvp.Key;
            EnableChecksum checksumMode = kvp.Value;

            // Resolve the symbology name to a BaseEncodeType value using reflection
            FieldInfo field = typeof(EncodeTypes).GetField(symName, BindingFlags.Public | BindingFlags.Static);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology: {symName}");
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
            string codeText = GetSampleCodeText(symName);

            try
            {
                // Create a barcode generator with the resolved type and sample text
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Apply the checksum setting from the configuration
                    generator.Parameters.Barcode.IsChecksumEnabled = checksumMode;

                    // Demonstrate always showing the checksum for Code128
                    if (symName.Equals("Code128", StringComparison.OrdinalIgnoreCase))
                    {
                        generator.Parameters.Barcode.ChecksumAlwaysShow = true;
                    }

                    // Save the generated barcode as a PNG file
                    string outPath = Path.Combine(outputDir, $"{symName}.png");
                    generator.Save(outPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated {symName} barcode at {outPath} with checksum {checksumMode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate {symName}: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Reads a simple key‑value configuration file where each line maps a symbology name to an EnableChecksum value.
    /// </summary>
    /// <param name="path">Full path to the configuration file.</param>
    /// <returns>Dictionary of symbology names and their corresponding checksum settings.</returns>
    static Dictionary<string, EnableChecksum> LoadChecksumSettings(string path)
    {
        var dict = new Dictionary<string, EnableChecksum>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadAllLines(path))
        {
            // Skip empty lines and comments
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            var parts = line.Split(new[] { '=' }, 2);
            if (parts.Length != 2)
                continue;

            string sym = parts[0].Trim();
            string modeStr = parts[1].Trim();

            // Parse the checksum mode; if invalid, report and skip
            if (Enum.TryParse<EnableChecksum>(modeStr, true, out var mode))
            {
                dict[sym] = mode;
            }
            else
            {
                Console.WriteLine($"Invalid checksum mode '{modeStr}' for symbology '{sym}'. Skipping.");
            }
        }
        return dict;
    }

    /// <summary>
    /// Provides a simple, valid sample code text for a given symbology.
    /// </summary>
    /// <param name="symName">The name of the symbology.</param>
    /// <returns>Sample text suitable for the specified barcode type.</returns>
    static string GetSampleCodeText(string symName)
    {
        // Return example data for a few common symbologies
        switch (symName.ToUpperInvariant())
        {
            case "CODE39":
            case "CODE39EXTENDED":
                return "ABC123";
            case "CODE128":
                return "Sample128";
            case "CODABAR":
                return "-12345-";
            default:
                return "Sample";
        }
    }
}