// Title: Load ProcessorSettings from JSON Configuration
// Description: Demonstrates loading Aspose.BarCode processor settings from a JSON file at application startup and applying them to the BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category. It shows how to use the BarCodeReader.ProcessorSettings class to control multithreading behavior based on a JSON configuration file. Typical use cases include optimizing barcode processing performance on different hardware environments. Developers often need to read settings from external files, deserialize them, and apply them to Aspose.BarCode APIs.
// Prompt: Write a configuration loader that reads ProcessorSettings values from a JSON file at application startup.
// Tags: json, configuration, processor settings, aspose.barcode, barcodereader, multithreading, cpu cores

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Common;

namespace ProcessorSettingsLoader
{
    /// <summary>
    /// Model matching the JSON structure for ProcessorSettings.
    /// </summary>
    public class ProcessorSettingsConfig
    {
        public bool UseAllCores { get; set; } = true;
        public int UseOnlyThisCoresCount { get; set; } = 1;
        public int MaxAdditionalAllowedThreads { get; set; } = 0;
    }

    /// <summary>
    /// Entry point that loads processor settings from a JSON file and applies them to Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Application startup method. Creates a default configuration file if missing,
        /// reads the JSON, deserializes it, and applies the values to BarCodeReader.ProcessorSettings.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            const string configFileName = "processorSettings.json";

            // Ensure a configuration file exists; create a default one if missing
            if (!File.Exists(configFileName))
            {
                var defaultConfig = new ProcessorSettingsConfig
                {
                    UseAllCores = true,
                    UseOnlyThisCoresCount = Math.Max(1, Environment.ProcessorCount / 2),
                    MaxAdditionalAllowedThreads = Environment.ProcessorCount
                };

                string defaultJson = JsonSerializer.Serialize(
                    defaultConfig,
                    new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(configFileName, defaultJson);
                Console.WriteLine($"Created default configuration file '{configFileName}'.");
            }

            // Load configuration from JSON
            ProcessorSettingsConfig config;
            try
            {
                using (var reader = new StreamReader(configFileName))
                {
                    string json = reader.ReadToEnd();
                    config = JsonSerializer.Deserialize<ProcessorSettingsConfig>(json);
                }

                if (config == null)
                {
                    throw new InvalidOperationException("Deserialized configuration is null.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load configuration: {ex.Message}");
                return;
            }

            // Apply settings to Aspose.BarCode ProcessorSettings
            try
            {
                BarCodeReader.ProcessorSettings.UseAllCores = config.UseAllCores;
                BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = config.UseOnlyThisCoresCount;
                BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = config.MaxAdditionalAllowedThreads;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to apply processor settings: {ex.Message}");
                return;
            }

            // Output the applied settings for verification
            Console.WriteLine("ProcessorSettings applied:");
            Console.WriteLine($"  UseAllCores = {BarCodeReader.ProcessorSettings.UseAllCores}");
            Console.WriteLine($"  UseOnlyThisCoresCount = {BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}");
            Console.WriteLine($"  MaxAdditionalAllowedThreads = {BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads}");
        }
    }
}