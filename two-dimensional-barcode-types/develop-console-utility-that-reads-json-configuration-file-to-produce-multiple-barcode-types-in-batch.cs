// Title: Batch Barcode Generator from JSON Configuration
// Description: Reads a JSON file describing multiple barcode tasks and generates each barcode image in the specified format.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class together with EncodeTypes to create various symbologies in bulk. Typical use cases include automated report creation, inventory labeling, and mass production of QR codes. Developers often need to parse configuration data, resolve symbology names, and output images in common formats such as PNG.
// Prompt: Develop a console utility that reads a JSON configuration file to produce multiple barcode types in batch.
// Tags: barcode symbology, batch generation, json configuration, console utility, aspose.barcode, png output

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeBatchGenerator
{
    /// <summary>
    /// Represents a single barcode generation instruction parsed from the JSON configuration.
    /// </summary>
    public class BarcodeTask
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public string OutputPath { get; set; }
    }

    /// <summary>
    /// Root object for the JSON configuration file. Contains a collection of <see cref="BarcodeTask"/> items.
    /// </summary>
    public class ConfigRoot
    {
        public List<BarcodeTask> Tasks { get; set; }
    }

    /// <summary>
    /// Console application entry point that processes a JSON configuration and generates barcodes in batch.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method parses command‑line arguments, loads the configuration, and iterates over each task to create barcode images.
        /// </summary>
        /// <param name="args">Optional first argument specifying the path to the JSON configuration file.</param>
        static void Main(string[] args)
        {
            // Determine configuration file path: use first argument if supplied, otherwise default to "config.json".
            string configPath = args.Length > 0 ? args[0] : "config.json";

            // Verify that the configuration file exists before proceeding.
            if (!File.Exists(configPath))
            {
                Console.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            // Read the entire JSON content from the file.
            string jsonContent = File.ReadAllText(configPath);
            ConfigRoot config;

            // Attempt to deserialize the JSON into the ConfigRoot object.
            try
            {
                config = JsonSerializer.Deserialize<ConfigRoot>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse configuration file: {ex.Message}");
                return;
            }

            // Ensure that there is at least one task defined.
            if (config?.Tasks == null || config.Tasks.Count == 0)
            {
                Console.WriteLine("No barcode tasks defined in the configuration.");
                return;
            }

            // Process each barcode task sequentially.
            foreach (var task in config.Tasks)
            {
                // Validate required fields for the current task.
                if (string.IsNullOrWhiteSpace(task.Symbology) ||
                    string.IsNullOrWhiteSpace(task.CodeText) ||
                    string.IsNullOrWhiteSpace(task.OutputPath))
                {
                    Console.WriteLine("Skipping task due to missing required fields.");
                    continue;
                }

                // Resolve the symbology name (e.g., \"Code128\") to the corresponding EncodeTypes field.
                FieldInfo field = typeof(EncodeTypes).GetField(task.Symbology);
                if (field == null)
                {
                    Console.WriteLine($"Unknown symbology: {task.Symbology}. Skipping this task.");
                    continue;
                }

                // Cast the field value to BaseEncodeType, which BarcodeGenerator expects.
                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Ensure the output directory exists; create it if necessary.
                string directory = Path.GetDirectoryName(task.OutputPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    try
                    {
                        Directory.CreateDirectory(directory);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to create directory '{directory}': {ex.Message}");
                        continue;
                    }
                }

                // Generate the barcode and save it as a PNG file.
                try
                {
                    using (var generator = new BarcodeGenerator(encodeType, task.CodeText))
                    {
                        // Optional: set a simple parameter to improve image quality.
                        generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                        // Save the generated barcode image to the specified path.
                        generator.Save(task.OutputPath, BarCodeImageFormat.Png);
                    }

                    Console.WriteLine($"Generated barcode: {task.OutputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error generating barcode for symbology '{task.Symbology}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }
}