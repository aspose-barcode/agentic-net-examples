// Title: Load and Apply Aspose.BarCode ProcessorSettings from JSON Configuration
// Description: Demonstrates reading processor configuration from a JSON file at startup and applying it to Aspose.BarCode's BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to use BarCodeReader.ProcessorSettings for performance tuning. It covers JSON deserialization, default configuration generation, and applying settings via the BarCodeReader static ProcessorSettings class—common tasks for developers optimizing barcode recognition workloads.
// Prompt: Write a configuration loader that reads ProcessorSettings values from a JSON file at application startup.
// Tags: configuration, json, processor settings, aspose.barcode, performance, csharp

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Represents the processor configuration that can be loaded from a JSON file.
/// </summary>
class ProcessorConfig
{
    public bool UseAllCores { get; set; }
    public int UseOnlyThisCoresCount { get; set; }
    public int MaxAdditionalAllowedThreads { get; set; }
}

/// <summary>
/// Entry point of the application that loads processor settings from a JSON file
/// and applies them to the Aspose.BarCode BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Main method executed at application startup.
    /// </summary>
    static void Main()
    {
        // Path to the JSON configuration file.
        const string configPath = "processorSettings.json";

        // Load configuration (creates default file if missing) and apply it.
        ProcessorConfig config = LoadConfig(configPath);
        ApplyProcessorSettings(config);

        // Output the applied settings for verification.
        Console.WriteLine("ProcessorSettings applied:");
        Console.WriteLine($"UseAllCores = {BarCodeReader.ProcessorSettings.UseAllCores}");
        Console.WriteLine($"UseOnlyThisCoresCount = {BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}");
        Console.WriteLine($"MaxAdditionalAllowedThreads = {BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads}");
    }

    /// <summary>
    /// Loads the processor configuration from the specified JSON file.
    /// If the file does not exist, a default configuration is created, saved, and returned.
    /// </summary>
    /// <param name="path">The file path of the JSON configuration.</param>
    /// <returns>An instance of <see cref="ProcessorConfig"/> populated with settings.</returns>
    static ProcessorConfig LoadConfig(string path)
    {
        // If the configuration file is missing, generate a default one.
        if (!File.Exists(path))
        {
            var defaultConfig = new ProcessorConfig
            {
                UseAllCores = true,
                UseOnlyThisCoresCount = Environment.ProcessorCount,
                MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2
            };

            // Serialize the default configuration with indentation for readability.
            string json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON to the specified path.
            File.WriteAllText(path, json);
            return defaultConfig;
        }

        // Open the existing configuration file for reading.
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            // Deserialize the JSON content into a ProcessorConfig object.
            return JsonSerializer.Deserialize<ProcessorConfig>(fs) ?? new ProcessorConfig();
        }
    }

    /// <summary>
    /// Applies the provided processor configuration to the Aspose.BarCode BarCodeReader.
    /// </summary>
    /// <param name="config">The configuration values to apply.</param>
    static void ApplyProcessorSettings(ProcessorConfig config)
    {
        BarCodeReader.ProcessorSettings.UseAllCores = config.UseAllCores;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = config.UseOnlyThisCoresCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = config.MaxAdditionalAllowedThreads;
    }
}