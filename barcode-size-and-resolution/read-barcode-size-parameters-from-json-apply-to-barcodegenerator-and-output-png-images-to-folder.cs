// Title: Generate barcodes from JSON specifications and save as PNG images
// Description: Demonstrates reading barcode size parameters (XDimension, BarHeight) from a JSON file, creating barcodes with Aspose.BarCode, and writing PNG files to a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcodes programmatically. Typical use cases include batch barcode creation from configuration files, customizing dimensions, and exporting images for inventory or labeling systems. Developers often need to parse external data sources and apply size settings before saving barcode graphics.
// Prompt: Read barcode size parameters from JSON, apply to BarcodeGenerator, and output PNG images to a folder.
// Tags: barcode, json, generation, png, aspose.barcode, dimensions, batch, csharp

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

namespace BarcodeGeneratorApp
{
    /// <summary>
    /// Represents a barcode specification read from JSON, including text, symbology, and optional size parameters.
    /// </summary>
    class BarcodeSpec
    {
        public string CodeText { get; set; }
        public string EncodeType { get; set; }
        public float? XDimension { get; set; }
        public float? BarHeight { get; set; }
    }

    /// <summary>
    /// Entry point for the barcode generation sample. Reads specifications from JSON, creates barcodes, and saves them as PNG files.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method that orchestrates reading the JSON file, validating entries, generating barcodes, and writing output images.
        /// </summary>
        /// <param name="args">Command‑line arguments; optionally the path to the JSON file.</param>
        static void Main(string[] args)
        {
            // Determine JSON file path: use first argument or create a temporary sample file.
            string jsonPath = args.Length > 0 ? args[0] : Path.Combine(Path.GetTempPath(), "barcodeParams.json");

            // If the JSON file does not exist, create a sample file with two barcode specifications.
            if (!File.Exists(jsonPath))
            {
                var sampleSpecs = new List<BarcodeSpec>
                {
                    new BarcodeSpec
                    {
                        CodeText = "ASPOSE",
                        EncodeType = "QR",
                        XDimension = 4f,
                        BarHeight = 50f
                    },
                    new BarcodeSpec
                    {
                        CodeText = "123456789012",
                        EncodeType = "Code128",
                        XDimension = 2f,
                        BarHeight = 30f
                    }
                };
                string sampleJson = JsonSerializer.Serialize(sampleSpecs, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonPath, sampleJson);
                Console.WriteLine($"Sample JSON created at: {jsonPath}");
            }

            // Read and deserialize the JSON content into a list of BarcodeSpec objects.
            string jsonContent = File.ReadAllText(jsonPath);
            List<BarcodeSpec> specs;
            try
            {
                specs = JsonSerializer.Deserialize<List<BarcodeSpec>>(jsonContent);
                if (specs == null)
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

            // Create a unique output folder for the generated PNG images.
            string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(outputFolder);
            Console.WriteLine($"Output folder: {outputFolder}");

            int index = 1;
            // Process each barcode specification.
            foreach (var spec in specs)
            {
                // Validate required fields.
                if (string.IsNullOrWhiteSpace(spec.CodeText) || string.IsNullOrWhiteSpace(spec.EncodeType))
                {
                    Console.WriteLine($"Skipping entry {index}: missing CodeText or EncodeType.");
                    index++;
                    continue;
                }

                // Resolve the EncodeType string to the corresponding enum value.
                var field = typeof(EncodeTypes).GetField(spec.EncodeType);
                if (field == null)
                {
                    Console.WriteLine($"Skipping entry {index}: unknown EncodeType '{spec.EncodeType}'.");
                    index++;
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Generate the barcode using the specified parameters.
                using (var generator = new BarcodeGenerator(encodeType, spec.CodeText))
                {
                    // Apply optional XDimension if provided.
                    if (spec.XDimension.HasValue)
                    {
                        generator.Parameters.Barcode.XDimension.Point = spec.XDimension.Value;
                    }

                    // Apply optional BarHeight if provided and valid.
                    if (spec.BarHeight.HasValue)
                    {
                        if (spec.BarHeight.Value <= 0f)
                        {
                            Console.WriteLine($"Entry {index}: BarHeight must be greater than zero. Skipping BarHeight setting.");
                        }
                        else
                        {
                            generator.Parameters.Barcode.BarHeight.Point = spec.BarHeight.Value;
                        }
                    }

                    // Save the generated barcode as a PNG file.
                    string outputPath = Path.Combine(outputFolder, $"barcode_{index}.png");
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode {index}: {outputPath}");
                }

                index++;
            }

            Console.WriteLine("Barcode generation completed.");
        }
    }
}