// Title: Generate Barcodes from JSON Configuration
// Description: Demonstrates reading barcode size and padding parameters from a JSON file, creating barcodes with Aspose.BarCode, and saving them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and barcode parameter objects (XDimension, BarHeight, Padding) to produce custom barcodes. Typical use cases include batch barcode creation from configuration files, dynamic sizing, and automated image export. Developers often need to read settings, map symbology strings to EncodeTypes, and save images in various formats.
// Prompt: Read barcode size parameters from JSON, apply to BarcodeGenerator, and output PNG images to a folder.
// Tags: barcode, generation, json, png, aspose.barcode, encode-types, size, padding

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Configuration for a single barcode, including symbology, text, and optional size/padding parameters.
/// </summary>
class BarcodeConfig
{
    public string Symbology { get; set; }
    public string CodeText { get; set; }
    public float? XDimension { get; set; }
    public float? BarHeight { get; set; }
    public float? PaddingLeft { get; set; }
    public float? PaddingTop { get; set; }
    public float? PaddingRight { get; set; }
    public float? PaddingBottom { get; set; }
}

/// <summary>
/// Root object for deserializing the JSON configuration file.
/// </summary>
class ConfigRoot
{
    public List<BarcodeConfig> Barcodes { get; set; }
}

/// <summary>
/// Entry point for the barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Reads configuration, generates barcodes, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Determine the current working directory and locate the JSON config file.
        string currentDir = Directory.GetCurrentDirectory();
        string configPath = Path.Combine(currentDir, "config.json");

        // If the config file does not exist, create a default one with sample data.
        if (!File.Exists(configPath))
        {
            var defaultConfig = new ConfigRoot
            {
                Barcodes = new List<BarcodeConfig>
                {
                    new BarcodeConfig { Symbology = "QR", CodeText = "ASPOSE", XDimension = 4f },
                    new BarcodeConfig { Symbology = "Code128", CodeText = "12345678", BarHeight = 30f, PaddingLeft = 5f, PaddingTop = 5f, PaddingRight = 5f, PaddingBottom = 5f }
                }
            };
            string json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, json);
            Console.WriteLine($"Created default config at {configPath}");
        }

        // Load and deserialize the JSON configuration.
        ConfigRoot configRoot;
        try
        {
            string jsonContent = File.ReadAllText(configPath);
            configRoot = JsonSerializer.Deserialize<ConfigRoot>(jsonContent);
            if (configRoot?.Barcodes == null)
                throw new Exception("Invalid config structure.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read config: {ex.Message}");
            return;
        }

        // Prepare the output folder for generated barcode images.
        string outputFolder = Path.Combine(currentDir, "GeneratedBarcodes");
        Directory.CreateDirectory(outputFolder);

        int index = 0;
        // Iterate over each barcode configuration entry.
        foreach (var cfg in configRoot.Barcodes)
        {
            index++;

            // Validate that a symbology name is provided.
            if (string.IsNullOrWhiteSpace(cfg.Symbology))
            {
                Console.WriteLine($"Item {index}: Symbology is missing, skipping.");
                continue;
            }

            // Map the symbology string to the corresponding EncodeTypes field.
            FieldInfo field = typeof(EncodeTypes).GetField(cfg.Symbology);
            if (field == null)
            {
                Console.WriteLine($"Item {index}: Unknown symbology '{cfg.Symbology}', skipping.");
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
            string codeText = cfg.CodeText ?? string.Empty;

            try
            {
                // Create a BarcodeGenerator with the specified type and text.
                using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Apply optional size and padding parameters if they are defined.
                    if (cfg.XDimension.HasValue)
                        generator.Parameters.Barcode.XDimension.Pixels = cfg.XDimension.Value;

                    if (cfg.BarHeight.HasValue && cfg.BarHeight.Value > 0)
                        generator.Parameters.Barcode.BarHeight.Point = cfg.BarHeight.Value;

                    if (cfg.PaddingLeft.HasValue)
                        generator.Parameters.Barcode.Padding.Left.Point = cfg.PaddingLeft.Value;
                    if (cfg.PaddingTop.HasValue)
                        generator.Parameters.Barcode.Padding.Top.Point = cfg.PaddingTop.Value;
                    if (cfg.PaddingRight.HasValue)
                        generator.Parameters.Barcode.Padding.Right.Point = cfg.PaddingRight.Value;
                    if (cfg.PaddingBottom.HasValue)
                        generator.Parameters.Barcode.Padding.Bottom.Point = cfg.PaddingBottom.Value;

                    // Build the output file name and save the barcode as a PNG image.
                    string fileName = $"{cfg.Symbology}_{index}.png";
                    string outputPath = Path.Combine(outputFolder, fileName);
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode saved to {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Item {index}: Failed to generate barcode - {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}