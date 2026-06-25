using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeDataBarGenerator
{
    // Represents a single barcode specification from the JSON file.
    public class BarcodeSpec
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
    }

    /// <summary>
    /// Main program class that reads barcode specifications from a JSON file
    /// and generates DataBar barcode images using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            const string jsonFileName = "barcodes.json";
            const string outputFolder = "output";

            // Verify that the JSON input file exists before proceeding.
            if (!File.Exists(jsonFileName))
            {
                Console.WriteLine($"Input file '{jsonFileName}' not found. Exiting.");
                return;
            }

            // Read the JSON file and deserialize it into a list of BarcodeSpec objects.
            List<BarcodeSpec> specs;
            using (FileStream jsonStream = new FileStream(jsonFileName, FileMode.Open, FileAccess.Read))
            {
                specs = JsonSerializer.Deserialize<List<BarcodeSpec>>(jsonStream);
            }

            // Ensure that we actually loaded some specifications.
            if (specs == null || specs.Count == 0)
            {
                Console.WriteLine("No barcode specifications found in the JSON file.");
                return;
            }

            // Create the output directory if it does not already exist.
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Define the set of allowed DataBar symbologies.
            var allowedDataBarTypes = new HashSet<string>
            {
                nameof(EncodeTypes.DatabarOmniDirectional),
                nameof(EncodeTypes.DatabarStacked),
                nameof(EncodeTypes.DatabarStackedOmniDirectional),
                nameof(EncodeTypes.DatabarLimited),
                nameof(EncodeTypes.DatabarExpanded),
                nameof(EncodeTypes.DatabarExpandedStacked)
            };

            int index = 0;
            // Process each barcode specification.
            foreach (var spec in specs)
            {
                index++;

                // Skip specifications missing required fields.
                if (string.IsNullOrWhiteSpace(spec.Symbology) || string.IsNullOrWhiteSpace(spec.CodeText))
                {
                    Console.WriteLine($"Specification #{index} is missing required fields. Skipping.");
                    continue;
                }

                // Ensure the requested symbology is one of the supported DataBar types.
                if (!allowedDataBarTypes.Contains(spec.Symbology))
                {
                    Console.WriteLine($"Specification #{index}: Symbology '{spec.Symbology}' is not a supported DataBar type. Skipping.");
                    continue;
                }

                // Resolve the symbology name to a BaseEncodeType enum value using reflection.
                FieldInfo field = typeof(EncodeTypes).GetField(spec.Symbology);
                if (field == null)
                {
                    Console.WriteLine($"Specification #{index}: Unknown symbology '{spec.Symbology}'. Skipping.");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Build the output file path for the generated barcode image.
                string outputPath = Path.Combine(outputFolder, $"{spec.Symbology}_{index}.png");

                // Generate the barcode and save it as a PNG image.
                using (var generator = new BarcodeGenerator(encodeType, spec.CodeText))
                {
                    // Disable auto‑sizing so that BarHeight can be set manually if needed.
                    generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                    // Optional: set a custom bar height.
                    // generator.Parameters.Barcode.BarHeight.Point = 30f;

                    // Save the generated barcode image.
                    generator.Save(outputPath);
                }

                Console.WriteLine($"Generated barcode #{index}: {outputPath}");
            }

            Console.WriteLine("Processing completed.");
        }
    }
}