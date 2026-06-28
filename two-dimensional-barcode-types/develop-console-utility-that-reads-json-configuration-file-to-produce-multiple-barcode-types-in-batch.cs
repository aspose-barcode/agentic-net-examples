using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeBatchGenerator
{
    /// <summary>
    /// Represents a single barcode generation request from the JSON configuration.
    /// </summary>
    public class BarcodeConfigItem
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public string OutputPath { get; set; }
    }

    /// <summary>
    /// Entry point for the barcode batch generator application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Reads a JSON configuration file and generates barcode images accordingly.
        /// </summary>
        /// <param name="args">Command‑line arguments; the first argument may specify the config file path.</param>
        static void Main(string[] args)
        {
            // Determine configuration file path (first argument or default "config.json").
            string configPath = args.Length > 0 ? args[0] : "config.json";

            // Verify that the configuration file exists before proceeding.
            if (!File.Exists(configPath))
            {
                Console.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            // Read the entire JSON file content.
            string jsonContent = File.ReadAllText(configPath);
            List<BarcodeConfigItem> items;

            // Attempt to deserialize the JSON into a list of configuration items.
            try
            {
                items = JsonSerializer.Deserialize<List<BarcodeConfigItem>>(jsonContent);
                if (items == null)
                {
                    Console.WriteLine("Configuration file is empty or has invalid format.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse configuration file: {ex.Message}");
                return;
            }

            // Iterate over each configuration item to generate the corresponding barcode.
            for (int i = 0; i < items.Count; i++)
            {
                BarcodeConfigItem item = items[i];

                // Validate required fields; skip the item if they are missing.
                if (string.IsNullOrWhiteSpace(item.Symbology) || string.IsNullOrWhiteSpace(item.CodeText))
                {
                    Console.WriteLine($"Item #{i + 1} is missing required fields. Skipping.");
                    continue;
                }

                // Resolve the symbology name to a BaseEncodeType enum value using reflection.
                FieldInfo field = typeof(EncodeTypes).GetField(item.Symbology);
                if (field == null)
                {
                    Console.WriteLine($"Unknown symbology '{item.Symbology}' in item #{i + 1}. Skipping.");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Determine the output file path; use a default naming scheme if none is provided.
                string outputPath = string.IsNullOrWhiteSpace(item.OutputPath)
                    ? $"{item.Symbology}_{i + 1}.png"
                    : item.OutputPath;

                try
                {
                    // Create a barcode generator with the resolved type and code text.
                    using (var generator = new BarcodeGenerator(encodeType, item.CodeText))
                    {
                        // Example of setting a common parameter (optional).
                        // generator.Parameters.Resolution = 300f;

                        // Save the generated barcode image to the specified path.
                        generator.Save(outputPath);
                    }

                    Console.WriteLine($"Generated barcode #{i + 1}: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to generate barcode for item #{i + 1}: {ex.Message}");
                }
            }

            // Indicate that all items have been processed.
            Console.WriteLine("Batch processing completed.");
        }
    }
}