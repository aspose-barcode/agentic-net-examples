// Title: Load ProcessorSettings from JSON configuration at startup
// Description: Demonstrates reading processor configuration values from a JSON file and applying them to Aspose.BarCode's BarCodeReader.ProcessorSettings.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showing how to use System.Text.Json to load settings for multi‑core processing. It covers key API classes such as BarCodeReader.ProcessorSettings and typical use cases like customizing thread usage for barcode recognition workloads. Developers often need to adjust these settings to optimize performance on different hardware environments.
// Prompt: Write a configuration loader that reads ProcessorSettings values from a JSON file at application startup.
// Tags: json, configuration, processor settings, multithreading, aspose.barcode, barcodereader, system.text.json

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Program that loads Aspose.BarCode processor settings from a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Creates a default configuration file if missing,
    /// reads the JSON, and applies the values to BarCodeReader.ProcessorSettings.
    /// </summary>
    static void Main()
    {
        // Path to the JSON configuration file
        const string configFile = "processorSettings.json";

        // If the config file does not exist, create one with default values
        if (!File.Exists(configFile))
        {
            var defaultConfig = new
            {
                UseAllCores = true,
                UseOnlyThisCoresCount = Environment.ProcessorCount,
                MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2
            };
            // Serialize the default configuration with indentation for readability
            string json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configFile, json);
            Console.WriteLine($"Created default config file: {configFile}");
        }

        // Read the JSON content from the configuration file
        string fileContent = File.ReadAllText(configFile);
        using (JsonDocument doc = JsonDocument.Parse(fileContent))
        {
            JsonElement root = doc.RootElement;

            // Extract individual settings from the JSON document
            bool useAllCores = root.GetProperty("UseAllCores").GetBoolean();
            int useOnlyThisCoresCount = root.GetProperty("UseOnlyThisCoresCount").GetInt32();
            int maxAdditionalAllowedThreads = root.GetProperty("MaxAdditionalAllowedThreads").GetInt32();

            // Apply the settings to Aspose.BarCode's processor configuration
            BarCodeReader.ProcessorSettings.UseAllCores = useAllCores;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = useOnlyThisCoresCount;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = maxAdditionalAllowedThreads;

            // Output the loaded settings for verification
            Console.WriteLine("ProcessorSettings loaded from JSON:");
            Console.WriteLine($"UseAllCores = {BarCodeReader.ProcessorSettings.UseAllCores}");
            Console.WriteLine($"UseOnlyThisCoresCount = {BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}");
            Console.WriteLine($"MaxAdditionalAllowedThreads = {BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads}");
        }
    }
}