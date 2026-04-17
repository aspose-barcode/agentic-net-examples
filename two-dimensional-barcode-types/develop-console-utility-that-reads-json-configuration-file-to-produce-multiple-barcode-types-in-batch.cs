using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    // Represents a single barcode generation instruction.
    private class BarcodeItem
    {
        public string Type { get; set; }          // Symbology name, e.g., "Code128"
        public string CodeText { get; set; }      // Text to encode
        public string OutputPath { get; set; }    // File name for the generated image
    }

    // Root configuration object.
    private class Config
    {
        public List<BarcodeItem> Items { get; set; }
    }

    static void Main(string[] args)
    {
        string configPath = args.Length > 0 ? args[0] : "config.json";

        // Ensure a configuration file exists; create a sample if missing.
        if (!File.Exists(configPath))
        {
            var sample = new Config
            {
                Items = new List<BarcodeItem>
                {
                    new BarcodeItem { Type = "Code128", CodeText = "ABC123", OutputPath = "code128.png" },
                    new BarcodeItem { Type = "QR", CodeText = "https://example.com", OutputPath = "qr.png" },
                    new BarcodeItem { Type = "EAN13", CodeText = "1234567890128", OutputPath = "ean13.png" }
                }
            };
            string json = JsonSerializer.Serialize(sample, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, json);
            Console.WriteLine($"Sample configuration created at '{configPath}'.");
        }

        // Load configuration.
        Config config;
        try
        {
            string jsonContent = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<Config>(jsonContent);
            if (config?.Items == null)
                throw new InvalidOperationException("Configuration file does not contain any items.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Process each barcode item.
        foreach (var item in config.Items)
        {
            try
            {
                // Resolve symbology name to BaseEncodeType using reflection.
                var prop = typeof(EncodeTypes).GetProperty(item.Type, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
                if (prop == null)
                {
                    Console.WriteLine($"Unsupported barcode type '{item.Type}'. Skipping.");
                    continue;
                }

                var encodeType = (BaseEncodeType)prop.GetValue(null);

                // Create generator with specified type and code text.
                using (var generator = new BarcodeGenerator(encodeType, item.CodeText))
                {
                    // Optional: set image size (example values).
                    generator.Parameters.ImageWidth.Pixels = 300f;
                    generator.Parameters.ImageHeight.Pixels = 150f;

                    // Ensure output directory exists.
                    string directory = Path.GetDirectoryName(item.OutputPath);
                    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Save the barcode image.
                    generator.Save(item.OutputPath);
                    Console.WriteLine($"Generated '{item.OutputPath}' for type '{item.Type}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing item (Type={item.Type}, CodeText={item.CodeText}): {ex.Message}");
            }
        }

        Console.WriteLine("Batch barcode generation completed.");
    }
}