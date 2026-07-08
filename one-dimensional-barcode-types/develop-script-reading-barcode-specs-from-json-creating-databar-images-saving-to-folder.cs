// Title: Generate DataBar barcodes from JSON specifications
// Description: Reads a JSON file describing DataBar barcode parameters, creates corresponding barcode images, and saves them to a folder.
// Category-Description: This example demonstrates Aspose.BarCode generation of DataBar symbologies (e.g., DatabarLimited, DatabarOmniDirectional). It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce PNG images. Developers working with bulk barcode creation, automated reporting, or inventory labeling can adapt this pattern for batch processing of barcode data.
// Prompt: Develop script reading barcode specs from JSON, creating DataBar images, saving to folder.
// Tags: databar, barcode generation, png, aspose.barcode, aspose.drawing, json

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeDataBarGenerator
{
    /// <summary>
    /// Represents a single barcode specification read from JSON.
    /// </summary>
    public class BarcodeSpec
    {
        public string Symbology { get; set; }   // e.g., "DatabarLimited", "DatabarOmniDirectional"
        public string CodeText { get; set; }    // Text to encode
        public string FileName { get; set; }    // Output image file name
    }

    /// <summary>
    /// Entry point for the DataBar barcode generation example.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Reads barcode specifications from a JSON file, generates DataBar barcode images, and saves them to the output folder.
        /// </summary>
        static void Main()
        {
            // Path to the input JSON file containing an array of BarcodeSpec objects.
            const string jsonPath = "barcodes.json";

            // Directory where generated barcode images will be stored.
            const string outputFolder = "output";

            // Verify that the JSON input file exists.
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"Input file not found: {jsonPath}");
                return;
            }

            // Ensure the output directory exists; create it if necessary.
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Deserialize the JSON specifications into a list of BarcodeSpec objects.
            List<BarcodeSpec> specs;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                specs = JsonSerializer.Deserialize<List<BarcodeSpec>>(jsonContent);
                if (specs == null)
                {
                    Console.WriteLine("No barcode specifications found in the JSON file.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read or parse JSON: {ex.Message}");
                return;
            }

            // Process each specification, limiting the number of items for safety.
            int processedCount = 0;
            const int maxItems = 10; // safety cap
            foreach (var spec in specs)
            {
                if (processedCount >= maxItems)
                    break;

                // Validate that all required fields are present.
                if (string.IsNullOrWhiteSpace(spec.Symbology) ||
                    string.IsNullOrWhiteSpace(spec.CodeText) ||
                    string.IsNullOrWhiteSpace(spec.FileName))
                {
                    Console.WriteLine("Skipping incomplete specification.");
                    continue;
                }

                // Resolve the symbology name to an EncodeTypes field via reflection.
                var fieldInfo = typeof(EncodeTypes).GetField(spec.Symbology);
                if (fieldInfo == null)
                {
                    Console.WriteLine($"Unknown symbology: {spec.Symbology}");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)fieldInfo.GetValue(null);

                // Create the barcode generator with the resolved type and provided text.
                using (var generator = new BarcodeGenerator(encodeType, spec.CodeText))
                {
                    // Configure common settings for DataBar images.
                    generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Determine the full output path for the image file.
                    string outputPath = Path.Combine(outputFolder, spec.FileName);
                    try
                    {
                        // Save the generated barcode as a PNG image.
                        generator.Save(outputPath, BarCodeImageFormat.Png);
                        Console.WriteLine($"Generated: {outputPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to generate barcode for {spec.FileName}: {ex.Message}");
                    }
                }

                processedCount++;
            }
        }
    }
}