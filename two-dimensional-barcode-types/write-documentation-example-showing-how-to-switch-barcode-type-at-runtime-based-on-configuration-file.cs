// Title: Runtime Barcode Type Switching Example
// Description: Demonstrates how to read a barcode type from a JSON configuration file and generate the corresponding barcode at runtime.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating dynamic selection of symbology using EncodeTypes and BarcodeGenerator. Developers often need to switch barcode formats based on external settings such as config files, databases, or user input; this snippet shows how to resolve a symbology name via reflection and produce an image file.
// Prompt: Write documentation example showing how to switch barcode type at runtime based on configuration file.
// Tags: barcode symbology, runtime configuration, aspose.barcode, encode types, json, generation, png

using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeRuntimeSwitchExample
{
    /// <summary>
    /// Simple POCO representing the JSON configuration for barcode generation.
    /// </summary>
    public class BarcodeConfig
    {
        public string BarcodeType { get; set; }
        public string CodeText { get; set; }
    }

    /// <summary>
    /// Demonstrates runtime selection of barcode symbology based on a JSON configuration file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Reads configuration, resolves barcode type, generates and saves the barcode image.
        /// </summary>
        static void Main()
        {
            // Path to the configuration file
            const string configPath = "barcodeConfig.json";

            // Ensure a sample configuration exists if the file is missing
            if (!File.Exists(configPath))
            {
                var sampleConfig = new BarcodeConfig
                {
                    BarcodeType = "Code128", // Name of a field in EncodeTypes
                    CodeText = "Sample123"
                };
                var json = JsonSerializer.Serialize(sampleConfig, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configPath, json);
                Console.WriteLine($"Created sample config file at '{configPath}'.");
            }

            // Read and deserialize the configuration
            BarcodeConfig config;
            try
            {
                var configJson = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<BarcodeConfig>(configJson);
                if (config == null ||
                    string.IsNullOrWhiteSpace(config.BarcodeType) ||
                    string.IsNullOrWhiteSpace(config.CodeText))
                {
                    throw new ArgumentException("Configuration file is missing required fields.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            // Resolve the symbology name to a BaseEncodeType using reflection
            var fieldInfo = typeof(EncodeTypes).GetField(config.BarcodeType);
            if (fieldInfo == null)
            {
                Console.WriteLine($"Unknown barcode type: '{config.BarcodeType}'.");
                return;
            }

            var encodeType = (BaseEncodeType)fieldInfo.GetValue(null);

            // Generate the barcode based on the runtime configuration
            using (var generator = new BarcodeGenerator(encodeType))
            {
                generator.CodeText = config.CodeText;

                // Save the barcode image; filename includes the barcode type for clarity
                string outputFile = $"barcode_{config.BarcodeType}.png";
                generator.Save(outputFile);
                Console.WriteLine($"Barcode saved to '{outputFile}'.");
            }
        }
    }
}