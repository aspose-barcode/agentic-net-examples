// Title: Batch Barcode Generation from JSON Configuration
// Description: Demonstrates a console utility that reads a JSON file describing multiple barcodes and generates each barcode image in a temporary output folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use EncodeTypes, BarcodeGenerator, and related parameter classes to create various barcode symbologies in batch. Typical use cases include automated report generation, inventory labeling, and bulk QR code creation. Developers often need to read configuration data, map symbology names to EncodeTypes, and save images in common formats such as PNG.
/// Prompt: Develop a console utility that reads a JSON configuration file to produce multiple barcode types in batch.
/// Tags: barcode symbology, batch generation, json configuration, console utility, aspose.barcode, generation, png output

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Console application that generates multiple barcodes based on a JSON configuration file.
/// </summary>
class Program
{
    /// <summary>
    /// Represents a single barcode definition in the configuration.
    /// </summary>
    class BarcodeItem
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public string OutputFile { get; set; }
    }

    /// <summary>
    /// Root object for the JSON configuration containing a collection of barcode items.
    /// </summary>
    class Config
    {
        public List<BarcodeItem> Items { get; set; }
    }

    /// <summary>
    /// Entry point of the utility. Reads configuration, creates output directory, and generates barcodes.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the path to the JSON configuration file.</param>
    static void Main(string[] args)
    {
        // Determine configuration file path: use first argument or fall back to a temp location.
        string configPath = args.Length > 0
            ? args[0]
            : Path.Combine(Path.GetTempPath(), "barcode_config.json");

        // If the configuration file does not exist, create a sample file for the user.
        if (!File.Exists(configPath))
        {
            CreateSampleConfig(configPath);
            Console.WriteLine($"Sample configuration created at: {configPath}");
        }

        Config config;
        try
        {
            // Read and deserialize the JSON configuration.
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<Config>(json);

            // Validate that the configuration contains at least one item.
            if (config?.Items == null || config.Items.Count == 0)
            {
                Console.WriteLine("Configuration contains no items.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read or parse configuration: {ex.Message}");
            return;
        }

        // Create a unique temporary directory for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        Console.WriteLine($"Generating barcodes into: {outputDir}");

        // Process each barcode item defined in the configuration.
        foreach (var item in config.Items)
        {
            // Skip items missing required fields.
            if (string.IsNullOrWhiteSpace(item.Symbology) || string.IsNullOrWhiteSpace(item.CodeText))
            {
                Console.WriteLine("Skipping item with missing symbology or codetext.");
                continue;
            }

            // Resolve the EncodeTypes field that matches the requested symbology name (case‑insensitive).
            FieldInfo field = typeof(EncodeTypes).GetField(item.Symbology,
                BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);

            if (field == null)
            {
                Console.WriteLine($"Unknown symbology: {item.Symbology}. Skipping.");
                continue;
            }

            // Cast the field value to BaseEncodeType for use with BarcodeGenerator.
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Determine output file name: use provided name or generate a unique one.
            string fileName = !string.IsNullOrWhiteSpace(item.OutputFile)
                ? item.OutputFile
                : $"{encodeType.TypeName}_{Guid.NewGuid().ToString("N")}.png";

            string outputPath = Path.Combine(outputDir, fileName);

            try
            {
                // Generate the barcode image with default colors and save as PNG.
                using (var generator = new BarcodeGenerator(encodeType, item.CodeText))
                {
                    generator.Parameters.Barcode.BarColor = Color.Black;
                    generator.Parameters.BackColor = Color.White;
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode for {item.Symbology}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch generation completed.");
    }

    /// <summary>
    /// Creates a sample JSON configuration file with a few common barcode definitions.
    /// </summary>
    /// <param name="path">File path where the sample configuration will be written.</param>
    static void CreateSampleConfig(string path)
    {
        var sample = new Config
        {
            Items = new List<BarcodeItem>
            {
                new BarcodeItem { Symbology = "Code128", CodeText = "Sample123" },
                new BarcodeItem { Symbology = "QR", CodeText = "https://example.com" },
                new BarcodeItem { Symbology = "DataMatrix", CodeText = "DM12345" }
            }
        };

        // Serialize with indentation for readability.
        string json = JsonSerializer.Serialize(sample, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
    }
}