// Title: Generate DataBar barcodes from JSON specifications and save as PNG images
// Description: This example reads a JSON file containing barcode symbology and code text, creates DataBar barcode images using Aspose.BarCode, and saves them to a designated folder.
// Category-Description: Demonstrates Aspose.BarCode generation workflow for DataBar symbologies. It covers reading input data with System.Text.Json, mapping symbology names to EncodeTypes via reflection, configuring BarcodeGenerator, and exporting PNG files. Ideal for developers needing batch barcode creation, automated report generation, or inventory labeling solutions.
// Prompt: Develop script reading barcode specs from JSON, creating DataBar images, saving to folder.
// Tags: barcode, databar, generation, json, png, aspose.barcode, encode types, batch processing

using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeGeneratorApp
{
    /// <summary>
    /// Model that matches the structure of each barcode specification in the input JSON file.
    /// </summary>
    public class BarcodeSpec
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
    }

    /// <summary>
    /// Entry point for the barcode generation console application.
    /// Reads specifications from a JSON file, generates DataBar barcodes, and saves them as PNG images.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method that orchestrates reading, processing, and saving barcode images.
        /// </summary>
        static void Main()
        {
            // Path to the JSON file that contains an array of barcode specifications.
            const string jsonPath = "barcodeSpecs.json";

            // Verify that the JSON file exists before attempting to read it.
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"JSON file not found: {jsonPath}");
                return;
            }

            // Read the entire JSON content and deserialize it into an array of BarcodeSpec objects.
            string jsonContent = File.ReadAllText(jsonPath);
            BarcodeSpec[] specs;
            try
            {
                specs = JsonSerializer.Deserialize<BarcodeSpec[]>(jsonContent);
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

            // Ensure the output directory exists; create it if it does not.
            const string outputDir = "Barcodes";
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Iterate over each barcode specification and generate the corresponding image.
            for (int i = 0; i < specs.Length; i++)
            {
                var spec = specs[i];

                // Validate that a symbology name is provided.
                if (string.IsNullOrWhiteSpace(spec?.Symbology))
                {
                    Console.WriteLine($"Specification #{i + 1} missing symbology.");
                    continue;
                }

                // Resolve the symbology name to a BaseEncodeType using reflection (case‑insensitive).
                var field = typeof(EncodeTypes).GetField(spec.Symbology,
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
                if (field == null)
                {
                    Console.WriteLine($"Unknown symbology '{spec.Symbology}' in specification #{i + 1}.");
                    continue;
                }

                if (!(field.GetValue(null) is BaseEncodeType encodeType))
                {
                    Console.WriteLine($"Failed to obtain encode type for '{spec.Symbology}'.");
                    continue;
                }

                // Determine the code text to encode; provide a default if none is supplied.
                string codeText = spec.CodeText;
                if (string.IsNullOrWhiteSpace(codeText))
                {
                    // Use a generic GTIN‑like value for DataBarLimited; otherwise a simple numeric string.
                    codeText = encodeType == EncodeTypes.DatabarLimited
                        ? "(01)08888888888888"
                        : "(01)12345678901231";
                }

                // Create the barcode generator, assign the code text, and save the image as PNG.
                using (var generator = new BarcodeGenerator(encodeType))
                {
                    generator.CodeText = codeText;

                    // Construct a unique file name based on the symbology type and index.
                    string fileName = $"{encodeType.TypeName}_{i + 1}.png";
                    string filePath = Path.Combine(outputDir, fileName);

                    // Save the generated barcode image in PNG format.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated barcode #{i + 1}: {spec.Symbology} -> {codeText}");
            }

            // Program completes without waiting for user input.
        }
    }
}