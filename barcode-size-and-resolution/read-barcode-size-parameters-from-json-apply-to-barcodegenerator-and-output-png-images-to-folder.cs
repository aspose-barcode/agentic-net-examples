using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeBatchGenerator
{
    // Model representing size parameters for a single barcode
    public class BarcodeConfig
    {
        public string Symbology { get; set; }          // e.g., "Code128", "QR"
        public string CodeText { get; set; }           // text to encode
        public float? ImageWidth { get; set; }         // in points
        public float? ImageHeight { get; set; }        // in points
        public float? XDimension { get; set; }         // in points
        public float? BarHeight { get; set; }          // in points (for 1D)
        public PaddingConfig Padding { get; set; }    // optional paddings
    }

    // Model for padding values
    public class PaddingConfig
    {
        public float? Left { get; set; }
        public float? Top { get; set; }
        public float? Right { get; set; }
        public float? Bottom { get; set; }
    }

    class Program
    {
        static void Main()
        {
            // Paths (adjust as needed)
            string jsonPath = "barcodeConfig.json";
            string outputFolder = "GeneratedBarcodes";

            // Validate JSON file existence
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"Configuration file not found: {jsonPath}");
                return;
            }

            // Ensure output directory exists
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Read and deserialize JSON
            List<BarcodeConfig> configs;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                configs = JsonSerializer.Deserialize<List<BarcodeConfig>>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (configs == null)
                {
                    Console.WriteLine("No barcode configurations found in JSON.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse JSON: {ex.Message}");
                return;
            }

            int index = 0;
            foreach (var cfg in configs)
            {
                index++;

                // Resolve symbology name to EncodeTypes member via reflection
                BaseEncodeType encodeType = ResolveEncodeType(cfg.Symbology);
                if (encodeType == null)
                {
                    Console.WriteLine($"[{index}] Unknown symbology '{cfg.Symbology}'. Skipping.");
                    continue;
                }

                // Create generator with symbology and codetext
                using (var generator = new BarcodeGenerator(encodeType, cfg.CodeText ?? string.Empty))
                {
                    // Apply image size if provided
                    if (cfg.ImageWidth.HasValue)
                        generator.Parameters.ImageWidth.Point = cfg.ImageWidth.Value;
                    if (cfg.ImageHeight.HasValue)
                        generator.Parameters.ImageHeight.Point = cfg.ImageHeight.Value;

                    // Apply XDimension if provided
                    if (cfg.XDimension.HasValue)
                        generator.Parameters.Barcode.XDimension.Point = cfg.XDimension.Value;

                    // Apply BarHeight for 1D barcodes when AutoSizeMode is None
                    if (cfg.BarHeight.HasValue)
                    {
                        generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                        generator.Parameters.Barcode.BarHeight.Point = cfg.BarHeight.Value;
                    }

                    // Apply padding if provided
                    if (cfg.Padding != null)
                    {
                        if (cfg.Padding.Left.HasValue)
                            generator.Parameters.Barcode.Padding.Left.Point = cfg.Padding.Left.Value;
                        if (cfg.Padding.Top.HasValue)
                            generator.Parameters.Barcode.Padding.Top.Point = cfg.Padding.Top.Value;
                        if (cfg.Padding.Right.HasValue)
                            generator.Parameters.Barcode.Padding.Right.Point = cfg.Padding.Right.Value;
                        if (cfg.Padding.Bottom.HasValue)
                            generator.Parameters.Barcode.Padding.Bottom.Point = cfg.Padding.Bottom.Value;
                    }

                    // Build output file name
                    string safeSymbology = cfg.Symbology.Replace("/", "_");
                    string fileName = Path.Combine(outputFolder, $"barcode_{index}_{safeSymbology}.png");

                    // Save as PNG
                    try
                    {
                        generator.Save(fileName);
                        Console.WriteLine($"[{index}] Saved barcode to {fileName}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[{index}] Failed to save barcode: {ex.Message}");
                    }
                }
            }
        }

        // Helper: resolve string name to EncodeTypes static field
        private static BaseEncodeType ResolveEncodeType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            var field = typeof(EncodeTypes).GetField(name);
            if (field == null)
                return null;

            var value = field.GetValue(null);
            return value as BaseEncodeType;
        }
    }
}