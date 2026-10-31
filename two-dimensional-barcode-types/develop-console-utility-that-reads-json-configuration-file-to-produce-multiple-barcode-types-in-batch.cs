// Title: Batch Barcode Generation from JSON Configuration
// Description: Demonstrates how to read a JSON file that defines multiple barcode specifications and generate each barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing batch processing of barcodes. It uses key API classes such as EncodeTypes, BarcodeGenerator, and BarCodeImageFormat to create various symbologies. Typical use cases include automated creation of product labels, inventory tags, or any scenario where many barcodes must be produced programmatically. Developers often need to read configuration data, map symbology names to EncodeTypes, and save images in common formats.
// Prompt: Develop a console utility that reads a JSON configuration file to produce multiple barcode types in batch.
// Tags: barcode symbology, batch generation, json configuration, aspose.barcode, console utility, png output

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Console utility that generates multiple barcodes based on a JSON configuration file.
/// </summary>
class Program
{
    /// <summary>
    /// Represents a single barcode definition within the configuration.
    /// </summary>
    class ConfigItem
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public string OutputPath { get; set; }
    }

    /// <summary>
    /// Root object for deserializing the JSON configuration.
    /// </summary>
    class Config
    {
        public List<ConfigItem> Items { get; set; }
    }

    /// <summary>
    /// Entry point of the console application.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the path to the JSON configuration file.</param>
    static void Main(string[] args)
    {
        // Determine configuration file path (first argument or default to "config.json").
        string configPath = args.Length > 0 ? args[0] : "config.json";

        // Verify that the configuration file exists.
        if (!File.Exists(configPath))
        {
            Console.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        Config config;
        try
        {
            // Read the entire JSON file and deserialize it into a Config object.
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<Config>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read or parse configuration: {ex.Message}");
            return;
        }

        // Ensure that there is at least one barcode item defined.
        if (config?.Items == null || config.Items.Count == 0)
        {
            Console.WriteLine("No barcode items defined in configuration.");
            return;
        }

        // Process each barcode item sequentially.
        for (int i = 0; i < config.Items.Count; i++)
        {
            ConfigItem item = config.Items[i];

            // Validate required fields for the current item.
            if (string.IsNullOrWhiteSpace(item.Symbology) ||
                string.IsNullOrWhiteSpace(item.CodeText) ||
                string.IsNullOrWhiteSpace(item.OutputPath))
            {
                Console.WriteLine($"Item {i + 1} has missing required fields. Skipping.");
                continue;
            }

            // Map the symbology string to the corresponding EncodeTypes field.
            FieldInfo field = typeof(EncodeTypes).GetField(item.Symbology);
            if (field == null)
            {
                Console.WriteLine($"Item {i + 1}: Unknown symbology '{item.Symbology}'. Skipping.");
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Ensure the output directory exists; create it if necessary.
            string outputDir = Path.GetDirectoryName(item.OutputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                try
                {
                    Directory.CreateDirectory(outputDir);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Item {i + 1}: Failed to create output directory '{outputDir}': {ex.Message}");
                    continue;
                }
            }

            try
            {
                // Initialize the barcode generator with the specified type and text.
                using (var generator = new BarcodeGenerator(encodeType, item.CodeText))
                {
                    // Set a simple black bar color and white background.
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Save the generated barcode as a PNG file.
                    generator.Save(item.OutputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Item {i + 1}: Barcode saved to '{item.OutputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Item {i + 1}: Failed to generate barcode: {ex.Message}");
            }
        }
    }
}