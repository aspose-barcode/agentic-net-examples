// Title: Batch Barcode Generation from JSON Parameters
// Description: Reads barcode size and content settings from a JSON file, generates corresponding barcodes, and saves them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use BarcodeGenerator with dynamic parameters. It covers resolving symbology via EncodeTypes, applying image dimensions, and exporting PNG files—common tasks for developers creating bulk barcodes in automated workflows.
// Prompt: Read barcode size parameters from JSON, apply to BarcodeGenerator, and output PNG images to a folder.
// Tags: barcode, generation, json, png, aspose.barcode, batch, size parameters

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeBatchGenerator
{
    /// <summary>
    /// Represents the size and content parameters for a single barcode.
    /// </summary>
    public class BarcodeParams
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public float? ImageWidth { get; set; }
        public float? ImageHeight { get; set; }
        public float? XDimension { get; set; }
        public float? BarHeight { get; set; }
    }

    /// <summary>
    /// Demonstrates reading barcode configuration from a JSON file, generating barcodes with Aspose.BarCode,
    /// and saving them as PNG images to a specified folder.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Handles JSON deserialization, barcode generation, and file output.
        /// </summary>
        static void Main()
        {
            const string jsonFileName = "barcodeParams.json";
            const string outputFolder = "Barcodes";

            // Ensure the output directory exists.
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // If the JSON configuration file is missing, create a sample file and exit.
            if (!File.Exists(jsonFileName))
            {
                var sample = new List<BarcodeParams>
                {
                    new BarcodeParams
                    {
                        Symbology = "Code128",
                        CodeText = "Sample123",
                        ImageWidth = 300f,
                        ImageHeight = 150f,
                        XDimension = 2f,
                        BarHeight = 50f
                    },
                    new BarcodeParams
                    {
                        Symbology = "QR",
                        CodeText = "https://example.com",
                        ImageWidth = 250f,
                        ImageHeight = 250f,
                        XDimension = 3f
                    }
                };
                var sampleJson = JsonSerializer.Serialize(sample, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonFileName, sampleJson);
                Console.WriteLine($"Sample JSON created at '{jsonFileName}'. Edit it as needed and rerun the program.");
                return;
            }

            // Read and deserialize the JSON file into a list of BarcodeParams objects.
            string jsonContent = File.ReadAllText(jsonFileName);
            List<BarcodeParams> items;
            try
            {
                items = JsonSerializer.Deserialize<List<BarcodeParams>>(jsonContent);
                if (items == null)
                    throw new Exception("Deserialized list is null.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse JSON: {ex.Message}");
                return;
            }

            int index = 0;
            foreach (var item in items)
            {
                index++;

                // Validate that a symbology name is provided.
                if (string.IsNullOrWhiteSpace(item.Symbology))
                {
                    Console.WriteLine($"Item {index}: Symbology is missing. Skipping.");
                    continue;
                }

                // Resolve the symbology string to a BaseEncodeType using reflection.
                var field = typeof(EncodeTypes).GetField(item.Symbology);
                if (field == null)
                {
                    Console.WriteLine($"Item {index}: Unknown symbology '{item.Symbology}'. Skipping.");
                    continue;
                }

                var encodeType = (BaseEncodeType)field.GetValue(null);

                // Create a BarcodeGenerator with the resolved type and provided code text.
                using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, item.CodeText ?? string.Empty))
                {
                    // Apply optional size parameters if they are specified.
                    if (item.ImageWidth.HasValue)
                        generator.Parameters.ImageWidth.Point = item.ImageWidth.Value;
                    if (item.ImageHeight.HasValue)
                        generator.Parameters.ImageHeight.Point = item.ImageHeight.Value;
                    if (item.XDimension.HasValue)
                        generator.Parameters.Barcode.XDimension.Point = item.XDimension.Value;
                    if (item.BarHeight.HasValue)
                        generator.Parameters.Barcode.BarHeight.Point = item.BarHeight.Value;

                    // Set a default foreground color (optional).
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                    // Build a safe output file name.
                    string safeSymbology = item.Symbology.Replace("/", "_");
                    string outputPath = Path.Combine(outputFolder, $"{safeSymbology}_{index}.png");

                    // Save the generated barcode as a PNG file.
                    try
                    {
                        generator.Save(outputPath);
                        Console.WriteLine($"Item {index}: Barcode saved to '{outputPath}'.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Item {index}: Failed to save barcode - {ex.Message}");
                    }
                }
            }

            Console.WriteLine("Processing completed.");
        }
    }
}