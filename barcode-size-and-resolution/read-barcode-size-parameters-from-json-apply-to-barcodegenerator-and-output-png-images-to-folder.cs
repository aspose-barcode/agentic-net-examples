// Title: Batch Barcode Generation from JSON Configuration
// Description: Demonstrates reading barcode size parameters from a JSON file, applying them to Aspose.BarCode's BarcodeGenerator, and saving PNG images to a folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to programmatically create barcodes using the BarcodeGenerator class. It covers typical use cases such as configuring image dimensions, X‑dimension, and bar height based on external data sources like JSON. Developers often need to batch‑process barcode creation with varying parameters, and this snippet illustrates a reusable pattern for such scenarios.
// Prompt: Read barcode size parameters from JSON, apply to BarcodeGenerator, and output PNG images to a folder.
// Tags: barcode, symbology, generation, json, png, aspose.barcode, size-parameters

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeBatchGenerator
{
    /// <summary>
    /// Represents a single barcode configuration read from JSON.
    /// </summary>
    public class BarcodeConfig
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public float? ImageWidth { get; set; }
        public float? ImageHeight { get; set; }
        public float? XDimension { get; set; }
        public float? BarHeight { get; set; }
        public string OutputFileName { get; set; }
    }

    /// <summary>
    /// Entry point for the batch barcode generation example.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Reads barcode configurations from a JSON file, generates each barcode with the specified size parameters,
        /// and saves the resulting PNG images to the output folder.
        /// </summary>
        static void Main()
        {
            // Path to the JSON file containing barcode size parameters.
            const string jsonPath = "barcodeConfig.json";

            // Verify that the configuration file exists before proceeding.
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"Configuration file not found: {jsonPath}");
                return;
            }

            // Read and deserialize the JSON configuration into a list of BarcodeConfig objects.
            List<BarcodeConfig> configs;
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                configs = JsonSerializer.Deserialize<List<BarcodeConfig>>(jsonStream);
            }

            // Ensure that at least one configuration was loaded.
            if (configs == null || configs.Count == 0)
            {
                Console.WriteLine("No barcode configurations found in the JSON file.");
                return;
            }

            // Ensure the output directory exists; create it if necessary.
            const string outputFolder = "GeneratedBarcodes";
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Process each barcode configuration.
            foreach (var cfg in configs)
            {
                // Resolve the symbology name to the corresponding EncodeTypes field via reflection.
                var field = typeof(EncodeTypes).GetField(cfg.Symbology);
                if (field == null)
                {
                    Console.WriteLine($"Unknown symbology: {cfg.Symbology}. Skipping entry.");
                    continue;
                }

                var encodeType = (BaseEncodeType)field.GetValue(null);

                // Create the barcode generator with the resolved symbology and provided code text.
                using (var generator = new BarcodeGenerator(encodeType, cfg.CodeText))
                {
                    // Apply optional size parameters if they are specified in the configuration.
                    if (cfg.ImageWidth.HasValue)
                        generator.Parameters.ImageWidth.Point = cfg.ImageWidth.Value;
                    if (cfg.ImageHeight.HasValue)
                        generator.Parameters.ImageHeight.Point = cfg.ImageHeight.Value;
                    if (cfg.XDimension.HasValue)
                        generator.Parameters.Barcode.XDimension.Point = cfg.XDimension.Value;
                    if (cfg.BarHeight.HasValue)
                        generator.Parameters.Barcode.BarHeight.Point = cfg.BarHeight.Value;

                    // Enable interpolation mode when explicit image dimensions are set.
                    if (cfg.ImageWidth.HasValue || cfg.ImageHeight.HasValue)
                        generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                    // Determine the output file name; generate a GUID if none is provided.
                    string fileName = string.IsNullOrWhiteSpace(cfg.OutputFileName)
                        ? $"{Guid.NewGuid()}.png"
                        : cfg.OutputFileName;

                    // Combine the output folder path with the file name.
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the generated barcode as a PNG image.
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Saved barcode to: {outputPath}");
                }
            }

            Console.WriteLine("Barcode generation completed.");
        }
    }
}