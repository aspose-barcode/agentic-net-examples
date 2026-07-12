// Title: Load ProcessorSettings from JSON Configuration
// Description: Demonstrates loading Aspose.BarCode processor settings from a JSON file at application startup and applying them to the BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category. It shows how to use the BarCodeReader.ProcessorSettings class to control multithreading behavior. Typical use cases include optimizing performance on multi‑core machines by toggling UseAllCores or limiting the number of cores. Developers often need to read such settings from external configuration files (e.g., JSON) to make the application adaptable without recompilation.
// Prompt: Write a configuration loader that reads ProcessorSettings values from a JSON file at application startup.
// Tags: processor settings, configuration, json, aspose.barcode, barcodereader

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Represents the processor configuration that can be loaded from a JSON file.
/// </summary>
class ProcessorConfig
{
    // Indicates whether the BarCodeReader should use all available CPU cores.
    public bool UseAllCores { get; set; } = true;

    // Specifies the exact number of cores to use when UseAllCores is false.
    public int UseOnlyThisCoresCount { get; set; } = Environment.ProcessorCount;
}

/// <summary>
/// Entry point of the application that loads processor settings and applies them to Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Application startup method. Loads configuration, applies settings, and reports the applied values.
    /// </summary>
    static void Main()
    {
        const string configPath = "processorSettings.json";

        // Load configuration from JSON file (or use defaults if missing/invalid).
        ProcessorConfig config = LoadConfig(configPath);

        // Apply the loaded settings to the Aspose.BarCode processor.
        ApplyProcessorSettings(config);

        // Output the effective processor settings.
        Console.WriteLine(
            $"ProcessorSettings applied: UseAllCores={BarCodeReader.ProcessorSettings.UseAllCores}, " +
            $"UseOnlyThisCoresCount={BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}");
    }

    /// <summary>
    /// Reads the processor configuration from the specified JSON file.
    /// </summary>
    /// <param name="path">Path to the JSON configuration file.</param>
    /// <returns>A <see cref="ProcessorConfig"/> instance populated with values from the file or defaults.</returns>
    static ProcessorConfig LoadConfig(string path)
    {
        if (!File.Exists(path))
        {
            // Config file missing; fall back to default settings.
            return new ProcessorConfig();
        }

        // Open the file for reading within a using block to ensure proper disposal.
        using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            try
            {
                // Deserialize JSON into ProcessorConfig; handle null result gracefully.
                ProcessorConfig? cfg = JsonSerializer.Deserialize<ProcessorConfig>(stream);
                return cfg ?? new ProcessorConfig();
            }
            catch (JsonException ex)
            {
                // Invalid JSON format; log the error and use default settings.
                Console.WriteLine($"Error parsing config: {ex.Message}");
                return new ProcessorConfig();
            }
        }
    }

    /// <summary>
    /// Applies the provided processor configuration to the Aspose.BarCode BarCodeReader.
    /// </summary>
    /// <param name="config">The configuration to apply.</param>
    static void ApplyProcessorSettings(ProcessorConfig config)
    {
        // Transfer settings to the static ProcessorSettings used by BarCodeReader.
        BarCodeReader.ProcessorSettings.UseAllCores = config.UseAllCores;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = config.UseOnlyThisCoresCount;
    }
}