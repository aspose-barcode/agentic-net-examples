using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Entry point for the barcode generation console application.
/// Reads configuration from a JSON file, generates barcodes using Aspose.BarCode,
/// and saves them as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Model matching the expected JSON structure for each barcode configuration.
    /// </summary>
    private class BarcodeConfig
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public float? ImageWidth { get; set; }      // in points
        public float? ImageHeight { get; set; }     // in points
        public float? XDimension { get; set; }     // in points
        public float? BarHeight { get; set; }      // in points
        public float? PaddingLeft { get; set; }    // in points
        public float? PaddingTop { get; set; }     // in points
        public float? PaddingRight { get; set; }   // in points
        public float? PaddingBottom { get; set; }  // in points
    }

    /// <summary>
    /// Main method: orchestrates reading the JSON config, generating barcodes,
    /// and writing the output files.
    /// </summary>
    static void Main()
    {
        const string jsonPath = "barcodeConfig.json";
        const string outputFolder = "Barcodes";

        // Verify that the JSON configuration file exists
        if (!File.Exists(jsonPath))
        {
            Console.WriteLine($"JSON configuration file not found: {jsonPath}");
            return;
        }

        // Read the entire JSON file content
        string jsonContent = File.ReadAllText(jsonPath);
        List<BarcodeConfig> configs;

        // Attempt to deserialize the JSON into a list of BarcodeConfig objects
        try
        {
            configs = JsonSerializer.Deserialize<List<BarcodeConfig>>(jsonContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Ensure we have at least one configuration to process
        if (configs == null || configs.Count == 0)
        {
            Console.WriteLine("No barcode configurations found in JSON.");
            return;
        }

        // Create the output directory if it does not already exist
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process up to 5 items to keep execution time safe
        int maxItems = Math.Min(5, configs.Count);
        for (int i = 0; i < maxItems; i++)
        {
            BarcodeConfig cfg = configs[i];

            // Validate required fields
            if (string.IsNullOrWhiteSpace(cfg.Symbology) || string.IsNullOrWhiteSpace(cfg.CodeText))
            {
                Console.WriteLine($"Skipping entry {i}: missing Symbology or CodeText.");
                continue;
            }

            // Resolve the symbology name to a BaseEncodeType instance
            BaseEncodeType encodeType = ResolveEncodeType(cfg.Symbology);
            if (encodeType == null)
            {
                Console.WriteLine($"Skipping entry {i}: unknown symbology '{cfg.Symbology}'.");
                continue;
            }

            // Create a barcode generator with the resolved type and provided text
            using (var generator = new BarcodeGenerator(encodeType, cfg.CodeText))
            {
                // Apply optional image size parameters
                if (cfg.ImageWidth.HasValue && cfg.ImageWidth.Value > 0f)
                {
                    generator.Parameters.ImageWidth.Point = cfg.ImageWidth.Value;
                }
                if (cfg.ImageHeight.HasValue && cfg.ImageHeight.Value > 0f)
                {
                    generator.Parameters.ImageHeight.Point = cfg.ImageHeight.Value;
                }

                // Apply optional X dimension (module width)
                if (cfg.XDimension.HasValue && cfg.XDimension.Value > 0f)
                {
                    generator.Parameters.Barcode.XDimension.Point = cfg.XDimension.Value;
                }

                // Apply optional bar height (requires disabling auto-size)
                if (cfg.BarHeight.HasValue && cfg.BarHeight.Value > 0f)
                {
                    generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                    generator.Parameters.Barcode.BarHeight.Point = cfg.BarHeight.Value;
                }

                // Apply optional padding values
                if (cfg.PaddingLeft.HasValue && cfg.PaddingLeft.Value >= 0f)
                {
                    generator.Parameters.Barcode.Padding.Left.Point = cfg.PaddingLeft.Value;
                }
                if (cfg.PaddingTop.HasValue && cfg.PaddingTop.Value >= 0f)
                {
                    generator.Parameters.Barcode.Padding.Top.Point = cfg.PaddingTop.Value;
                }
                if (cfg.PaddingRight.HasValue && cfg.PaddingRight.Value >= 0f)
                {
                    generator.Parameters.Barcode.Padding.Right.Point = cfg.PaddingRight.Value;
                }
                if (cfg.PaddingBottom.HasValue && cfg.PaddingBottom.Value >= 0f)
                {
                    generator.Parameters.Barcode.Padding.Bottom.Point = cfg.PaddingBottom.Value;
                }

                // Determine the output file path for this barcode
                string outputPath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

                // Attempt to save the generated barcode image
                try
                {
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Saved barcode {i + 1} to '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to generate barcode {i + 1}: {ex.Message}");
                }
            }
        }
    }

    /// <summary>
    /// Resolves a symbology name to a <see cref="BaseEncodeType"/> using reflection on <see cref="EncodeTypes"/>.
    /// Returns null if the name cannot be resolved.
    /// </summary>
    /// <param name="symbologyName">The name of the symbology (e.g., "Code128").</param>
    /// <returns>The corresponding <see cref="BaseEncodeType"/>, or null if not found.</returns>
    private static BaseEncodeType ResolveEncodeType(string symbologyName)
    {
        if (string.IsNullOrWhiteSpace(symbologyName))
            return null;

        // Look for a public static field on EncodeTypes that matches the provided name
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
            return null;

        // Return the field value cast to BaseEncodeType
        return field.GetValue(null) as BaseEncodeType;
    }
}