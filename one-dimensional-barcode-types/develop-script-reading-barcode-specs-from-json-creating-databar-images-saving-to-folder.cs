using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeBatchGenerator
{
    // Represents a single barcode specification read from JSON.
    public class BarcodeSpec
    {
        public string Type { get; set; }          // Symbology name, e.g., "DataBarOmniDirectional"
        public string CodeText { get; set; }      // Text to encode
        public int Columns { get; set; }          // Optional DataBar columns (0 = default)
        public int Rows { get; set; }             // Optional DataBar rows (0 = default)
    }

    class Program
    {
        static void Main()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string inputFolder = Path.Combine(baseDir, "Input");
            string outputFolder = Path.Combine(baseDir, "Output");
            string jsonPath = Path.Combine(inputFolder, "specs.json");

            // Ensure input folder exists; create a sample JSON if missing.
            if (!Directory.Exists(inputFolder))
            {
                Directory.CreateDirectory(inputFolder);
                var sampleSpecs = new List<BarcodeSpec>
                {
                    new BarcodeSpec
                    {
                        Type = "DataBarOmniDirectional",
                        CodeText = "123456789012",
                        Columns = 0,
                        Rows = 0
                    },
                    new BarcodeSpec
                    {
                        Type = "DataBarStacked",
                        CodeText = "987654321098",
                        Columns = 0,
                        Rows = 0
                    }
                };
                string sampleJson = JsonSerializer.Serialize(sampleSpecs, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonPath, sampleJson);
                Console.WriteLine($"Sample JSON created at: {jsonPath}");
            }

            // Ensure output folder exists.
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Validate JSON file existence.
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"JSON file not found: {jsonPath}");
                return;
            }

            // Read and deserialize specifications.
            List<BarcodeSpec> specs;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                specs = JsonSerializer.Deserialize<List<BarcodeSpec>>(jsonContent);
                if (specs == null || specs.Count == 0)
                {
                    Console.WriteLine("No barcode specifications found in JSON.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse JSON: {ex.Message}");
                return;
            }

            // Process each specification (limit to a safe number).
            int maxItems = Math.Min(specs.Count, 10);
            for (int i = 0; i < maxItems; i++)
            {
                BarcodeSpec spec = specs[i];
                // Resolve EncodeTypes member via reflection (allowed by rule 14).
                var encodeField = typeof(EncodeTypes).GetField(spec.Type);
                if (encodeField == null)
                {
                    Console.WriteLine($"Unsupported symbology: {spec.Type}");
                    continue;
                }

                var encodeType = (BaseEncodeType)encodeField.GetValue(null);

                using (var generator = new BarcodeGenerator(encodeType, spec.CodeText))
                {
                    // Apply DataBar specific parameters if applicable.
                    generator.Parameters.Barcode.DataBar.Columns = spec.Columns;
                    generator.Parameters.Barcode.DataBar.Rows = spec.Rows;

                    // Optional: set image size to ensure visibility.
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;

                    string fileName = $"{spec.Type}_{i + 1}.png";
                    string outputPath = Path.Combine(outputFolder, fileName);

                    try
                    {
                        generator.Save(outputPath);
                        Console.WriteLine($"Saved barcode: {outputPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to save barcode '{fileName}': {ex.Message}");
                    }
                }
            }
        }
    }
}