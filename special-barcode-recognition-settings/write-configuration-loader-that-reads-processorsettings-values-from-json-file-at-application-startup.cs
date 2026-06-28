using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Represents the configuration settings for processor cores.
/// </summary>
class ProcessorSettingsConfig
{
    /// <summary>
    /// Gets or sets the number of cores to be used by the processor.
    /// </summary>
    public int UseOnlyThisCoresCount { get; set; }
}

/// <summary>
/// Entry point of the application that loads processor settings from a JSON file
/// and applies them to the Aspose.BarCode BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that orchestrates reading the configuration and applying it.
    /// </summary>
    static void Main()
    {
        // Path to the JSON configuration file
        const string configPath = "processorSettings.json";

        // Verify that the configuration file exists before attempting to read it
        if (!File.Exists(configPath))
        {
            Console.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Read the entire JSON content from the file
        string json = File.ReadAllText(configPath);
        ProcessorSettingsConfig? config;

        // Attempt to deserialize the JSON into a ProcessorSettingsConfig instance
        try
        {
            config = JsonSerializer.Deserialize<ProcessorSettingsConfig>(json);
        }
        catch (Exception ex)
        {
            // Output any parsing errors and abort execution
            Console.WriteLine($"Failed to parse configuration: {ex.Message}");
            return;
        }

        // Ensure that deserialization produced a non‑null object
        if (config == null)
        {
            Console.WriteLine("Configuration is empty or invalid.");
            return;
        }

        // Apply the deserialized settings to the Aspose.BarCode BarCodeReader processor
        // ProcessorSettings is a static property of BarCodeReader that controls core usage
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = config.UseOnlyThisCoresCount;

        // Confirm successful loading and display the applied setting
        Console.WriteLine("Processor settings loaded successfully.");
        Console.WriteLine($"UseOnlyThisCoresCount = {BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}");
    }
}