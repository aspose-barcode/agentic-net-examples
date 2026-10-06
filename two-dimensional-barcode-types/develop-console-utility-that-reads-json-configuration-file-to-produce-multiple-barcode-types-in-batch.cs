// Title: Batch Barcode Generation from JSON Configuration
// Description: Demonstrates a console utility that reads a JSON file describing multiple barcodes and generates corresponding images in batch.
// Category-Description: Shows how to use Aspose.BarCode to generate various symbologies programmatically. The example covers reading configuration, mapping symbology names to EncodeTypes, creating BarcodeGenerator instances, and saving PNG files. Useful for developers automating barcode creation for inventory, shipping, or marketing materials.
// Prompt: Develop a console utility that reads a JSON configuration file to produce multiple barcode types in batch.
// Tags: barcode generation, batch processing, json configuration, aspose.barcode, encode types, png output

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeBatchGenerator
{
    /// <summary>
    /// Represents a single barcode configuration item read from the JSON file.
    /// </summary>
    public class ConfigItem
    {
        public string Type { get; set; }
        public string CodeText { get; set; }
    }

    /// <summary>
    /// Console application that reads a JSON configuration and generates barcode images in batch.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the utility. Accepts an optional path to a JSON configuration file.
        /// </summary>
        /// <param name="args">Command‑line arguments; the first argument may specify the config file path.</param>
        static void Main(string[] args)
        {
            // Determine configuration file path (use default if not supplied)
            string configPath = args.Length > 0 ? args[0] : "barcode_config.json";

            // If the configuration file does not exist, create a sample file and exit
            if (!File.Exists(configPath))
            {
                Console.WriteLine($"Configuration file not found at '{configPath}'. Creating a sample configuration.");
                var sample = new List<ConfigItem>
                {
                    new ConfigItem { Type = "Code128", CodeText = "Sample123" },
                    new ConfigItem { Type = "QR", CodeText = "https://example.com" },
                    new ConfigItem { Type = "DataMatrix", CodeText = "DM12345" }
                };
                string sampleJson = JsonSerializer.Serialize(sample, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configPath, sampleJson);
                Console.WriteLine($"Sample configuration written to '{configPath}'. Please edit it as needed and rerun the program.");
                return;
            }

            // Load and deserialize the JSON configuration
            List<ConfigItem> items;
            try
            {
                string json = File.ReadAllText(configPath);
                items = JsonSerializer.Deserialize<List<ConfigItem>>(json);
                if (items == null)
                {
                    Console.WriteLine("Configuration file is empty or malformed.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read or parse configuration file: {ex.Message}");
                return;
            }

            // Create a unique temporary output directory for the generated barcodes
            string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }

            Console.WriteLine($"Generating barcodes into folder: {outputDir}");

            // Iterate over each configuration item and generate the corresponding barcode
            int index = 0;
            foreach (var item in items)
            {
                index++;

                // Validate required fields
                if (string.IsNullOrWhiteSpace(item.Type) || string.IsNullOrWhiteSpace(item.CodeText))
                {
                    Console.WriteLine($"Item {index}: Missing Type or CodeText. Skipping.");
                    continue;
                }

                // Resolve the EncodeTypes field that matches the requested symbology (case‑insensitive)
                FieldInfo field = typeof(EncodeTypes).GetField(item.Type, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
                if (field == null)
                {
                    Console.WriteLine($"Item {index}: Unknown symbology '{item.Type}'. Skipping.");
                    continue;
                }

                // Retrieve the BaseEncodeType instance from the field
                BaseEncodeType encodeType = field.GetValue(null) as BaseEncodeType;
                if (encodeType == null)
                {
                    Console.WriteLine($"Item {index}: Failed to obtain encode type for '{item.Type}'. Skipping.");
                    continue;
                }

                // Build the output file name and path
                string fileName = $"{encodeType.TypeName}_{index}.png";
                string outputPath = Path.Combine(outputDir, fileName);

                // Generate and save the barcode image
                try
                {
                    using (var generator = new BarcodeGenerator(encodeType, item.CodeText))
                    {
                        generator.Save(outputPath, BarCodeImageFormat.Png);
                    }
                    Console.WriteLine($"Item {index}: Generated {fileName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Item {index}: Error generating barcode - {ex.Message}");
                }
            }

            Console.WriteLine("Batch generation completed.");
        }
    }
}