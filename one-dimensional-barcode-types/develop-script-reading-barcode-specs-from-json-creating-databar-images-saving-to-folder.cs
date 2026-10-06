// Title: Generate DataBar barcodes from JSON specifications and save as PNG images
// Description: This example reads a JSON file containing barcode type and text specifications, creates DataBar barcodes using Aspose.BarCode, and saves the images to an output folder.
// Category-Description: Demonstrates Aspose.BarCode generation workflow for DataBar symbologies. It covers reading configuration data (JSON), mapping symbology names to EncodeTypes via reflection, configuring barcode parameters, and exporting images. Ideal for developers needing batch barcode creation, automated report generation, or integration with data pipelines.
// Prompt: Develop script reading barcode specs from JSON, creating DataBar images, saving to folder.
// Tags: databar, barcode generation, json, png, aspose.barcode, encode types, reflection

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeGeneratorApp
{
    /// <summary>
    /// Represents a single barcode specification read from the JSON input.
    /// </summary>
    public class BarcodeSpec
    {
        /// <summary>
        /// The name of the DataBar symbology (e.g., "DatabarOmniDirectional").
        /// </summary>
        public string Type { get; set; } = "";

        /// <summary>
        /// The text to encode in the barcode.
        /// </summary>
        public string CodeText { get; set; } = "";
    }

    /// <summary>
    /// Main program class that reads barcode specifications, generates barcodes, and saves them as PNG files.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Executes the barcode generation workflow.
        /// </summary>
        static void Main()
        {
            // -----------------------------------------------------------------
            // Step 1: Ensure a JSON file with barcode specifications exists.
            // -----------------------------------------------------------------
            string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "specs.json");
            if (!File.Exists(jsonPath))
            {
                // Create sample specifications if the file is missing.
                var sampleSpecs = new List<BarcodeSpec>
                {
                    new BarcodeSpec { Type = "DatabarOmniDirectional", CodeText = "(01)12345678901231" },
                    new BarcodeSpec { Type = "DatabarExpanded", CodeText = "(01)12345678901231(21)ABC" },
                    new BarcodeSpec { Type = "DatabarStackedOmniDirectional", CodeText = "(01)12345678901231" }
                };
                string sampleJson = JsonSerializer.Serialize(sampleSpecs, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonPath, sampleJson);
                Console.WriteLine($"Sample JSON created at: {jsonPath}");
            }

            // -----------------------------------------------------------------
            // Step 2: Read and deserialize the JSON content into a list of specs.
            // -----------------------------------------------------------------
            string jsonContent = File.ReadAllText(jsonPath);
            List<BarcodeSpec>? specs = JsonSerializer.Deserialize<List<BarcodeSpec>>(jsonContent);
            if (specs == null || specs.Count == 0)
            {
                Console.WriteLine("No barcode specifications found.");
                return;
            }

            // -----------------------------------------------------------------
            // Step 3: Prepare the output directory for generated barcode images.
            // -----------------------------------------------------------------
            string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "BarcodesOutput");
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // -----------------------------------------------------------------
            // Step 4: Iterate over each specification, generate the barcode, and save it.
            // -----------------------------------------------------------------
            int index = 0;
            foreach (var spec in specs)
            {
                index++;

                // Resolve the symbology name to the corresponding BaseEncodeType using reflection.
                FieldInfo? field = typeof(EncodeTypes).GetField(spec.Type);
                if (field == null)
                {
                    Console.WriteLine($"[{index}] Unknown symbology: {spec.Type}");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null)!;

                // Create a BarcodeGenerator instance for the resolved type and provided text.
                using (var generator = new BarcodeGenerator(encodeType, spec.CodeText))
                {
                    // Optional: increase X-dimension for better visual clarity.
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;

                    // Build the output file name and path.
                    string fileName = $"{spec.Type}_{index}.png";
                    string filePath = Path.Combine(outputDir, fileName);

                    // Attempt to save the barcode image as PNG.
                    try
                    {
                        generator.Save(filePath, BarCodeImageFormat.Png);
                        Console.WriteLine($"[{index}] Saved: {filePath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[{index}] Failed to generate {spec.Type}: {ex.Message}");
                    }
                }
            }

            Console.WriteLine("Barcode generation completed.");
        }
    }
}