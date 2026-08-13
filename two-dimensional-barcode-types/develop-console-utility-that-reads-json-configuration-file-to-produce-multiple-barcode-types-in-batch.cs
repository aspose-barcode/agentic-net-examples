// Title: Batch Barcode Generation from JSON Configuration
// Description: Demonstrates a console utility that reads a JSON file describing multiple barcodes and generates each as a PNG using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and related parameter classes to create various 1D and 2D symbologies in bulk. Typical use cases include automated label creation, inventory systems, and batch processing of barcode assets. Developers often need to parse configuration data, map symbology names to EncodeTypes, and customize visual properties such as dimensions, colors, and padding.
// Prompt: Develop a console utility that reads a JSON configuration file to produce multiple barcode types in batch.
// Tags: barcode, batch, json, console, aspose.barcode, generation, png, symbology, configuration

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeBatchGenerator
{
    /// <summary>
    /// Represents the overall configuration file containing a collection of barcode generation requests.
    /// </summary>
    public class Config
    {
        public List<BarcodeItem> Items { get; set; }
    }

    /// <summary>
    /// Represents a single barcode generation request with optional visual parameters.
    /// </summary>
    public class BarcodeItem
    {
        public string Symbology { get; set; }          // e.g., "Code128", "QR", "DataMatrix"
        public string CodeText { get; set; }           // text to encode
        public string OutputFile { get; set; }         // optional file name (without path)
        public float? XDimension { get; set; }         // optional module size (points)
        public float? BarHeight { get; set; }          // optional bar height (points) for 1D barcodes
        public float? PaddingLeft { get; set; }        // optional left padding (points)
        public float? PaddingTop { get; set; }         // optional top padding (points)
        public float? PaddingRight { get; set; }       // optional right padding (points)
        public float? PaddingBottom { get; set; }      // optional bottom padding (points)
        public string BarColor { get; set; }           // optional bar color name (e.g., "Blue")
        public string BackColor { get; set; }          // optional background color name
    }

    /// <summary>
    /// Console utility that reads a JSON configuration and generates a batch of barcodes using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Parses arguments, loads configuration, and creates barcode images.
        /// </summary>
        /// <param name="args">Command‑line arguments; first argument may be a path to a JSON config file.</param>
        static void Main(string[] args)
        {
            // ------------------------------------------------------------
            // Resolve configuration file path (argument or built‑in sample)
            // ------------------------------------------------------------
            string configPath = args.Length > 0 ? args[0] : null;
            if (string.IsNullOrWhiteSpace(configPath) || !File.Exists(configPath))
            {
                Console.WriteLine("Configuration file not found. Using built‑in sample configuration.");

                // Create a temporary sample JSON configuration with three barcode definitions.
                configPath = Path.Combine(Path.GetTempPath(), "sample_barcode_config.json");
                string sampleJson = @"
{
    ""Items"": [
        {
            ""Symbology"": ""Code128"",
            ""CodeText"": ""ABC123456"",
            ""OutputFile"": ""code128.png"",
            ""XDimension"": 2.0,
            ""BarHeight"": 50.0,
            ""BarColor"": ""Blue"",
            ""BackColor"": ""White""
        },
        {
            ""Symbology"": ""QR"",
            ""CodeText"": ""https://example.com"",
            ""OutputFile"": ""qr.png"",
            ""XDimension"": 3.0,
            ""PaddingLeft"": 5.0,
            ""PaddingTop"": 5.0,
            ""PaddingRight"": 5.0,
            ""PaddingBottom"": 5.0,
            ""BarColor"": ""Black"",
            ""BackColor"": ""White""
        },
        {
            ""Symbology"": ""DataMatrix"",
            ""CodeText"": ""DM12345"",
            ""OutputFile"": ""datamatrix.png"",
            ""XDimension"": 1.5,
            ""BarColor"": ""Green"",
            ""BackColor"": ""White""
        }
    ]
}";
                File.WriteAllText(configPath, sampleJson);
            }

            // ------------------------------------------------------------
            // Load and deserialize the JSON configuration
            // ------------------------------------------------------------
            Config config;
            try
            {
                string jsonContent = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<Config>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (config?.Items == null || config.Items.Count == 0)
                {
                    Console.WriteLine("No barcode items found in the configuration.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read or parse configuration file: {ex.Message}");
                return;
            }

            // ------------------------------------------------------------
            // Prepare output folder for generated barcode images
            // ------------------------------------------------------------
            string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodesBatch_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(outputFolder);
            Console.WriteLine($"Barcodes will be saved to: {outputFolder}");

            // ------------------------------------------------------------
            // Iterate over each barcode item and generate the image
            // ------------------------------------------------------------
            int index = 0;
            foreach (var item in config.Items)
            {
                index++;

                // Resolve symbology name to EncodeTypes enum via reflection (case‑insensitive).
                FieldInfo field = typeof(EncodeTypes).GetField(item.Symbology, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
                if (field == null)
                {
                    Console.WriteLine($"[{index}] Unknown symbology \"{item.Symbology}\". Skipping.");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
                string codeText = item.CodeText ?? string.Empty;

                // Determine output file name (use provided name or generate a default).
                string fileName = !string.IsNullOrWhiteSpace(item.OutputFile)
                    ? item.OutputFile
                    : $"{encodeType.TypeName}_{index}.png";
                string outputPath = Path.Combine(outputFolder, fileName);

                try
                {
                    using (var generator = new BarcodeGenerator(encodeType, codeText))
                    {
                        // Apply optional visual parameters if they are specified.
                        if (item.XDimension.HasValue)
                            generator.Parameters.Barcode.XDimension.Point = item.XDimension.Value;

                        if (item.BarHeight.HasValue)
                            generator.Parameters.Barcode.BarHeight.Point = item.BarHeight.Value;

                        if (item.PaddingLeft.HasValue)
                            generator.Parameters.Barcode.Padding.Left.Point = item.PaddingLeft.Value;
                        if (item.PaddingTop.HasValue)
                            generator.Parameters.Barcode.Padding.Top.Point = item.PaddingTop.Value;
                        if (item.PaddingRight.HasValue)
                            generator.Parameters.Barcode.Padding.Right.Point = item.PaddingRight.Value;
                        if (item.PaddingBottom.HasValue)
                            generator.Parameters.Barcode.Padding.Bottom.Point = item.PaddingBottom.Value;

                        if (!string.IsNullOrWhiteSpace(item.BarColor))
                        {
                            // Attempt to parse known color names; fallback to Black on failure.
                            Color barColor = Color.Black;
                            try { barColor = Color.FromName(item.BarColor); } catch { }
                            generator.Parameters.Barcode.BarColor = barColor;
                        }

                        if (!string.IsNullOrWhiteSpace(item.BackColor))
                        {
                            // Attempt to parse known color names; fallback to White on failure.
                            Color backColor = Color.White;
                            try { backColor = Color.FromName(item.BackColor); } catch { }
                            generator.Parameters.BackColor = backColor;
                        }

                        // Save the generated barcode as a PNG file.
                        generator.Save(outputPath, BarCodeImageFormat.Png);
                        Console.WriteLine($"[{index}] Generated \"{fileName}\" successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{index}] Failed to generate barcode for symbology \"{item.Symbology}\": {ex.Message}");
                }
            }

            Console.WriteLine("Batch barcode generation completed.");
        }
    }
}