// Title: Load Aspose.BarCode ProcessorSettings from JSON configuration
// Description: Demonstrates reading a JSON file at startup and applying its values to Aspose.BarCode's ProcessorSettings for optimal threading control.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to use System.Text.Json to deserialize settings and assign them to the static BarCodeReader.ProcessorSettings class. Typical use cases include customizing CPU core usage and thread limits for barcode recognition workloads. Developers often need to load such settings from external files to make their applications adaptable to different environments.
// Prompt: Write a configuration loader that reads ProcessorSettings values from a JSON file at application startup.
// Tags: processor-settings, configuration, json, aspose.barcode, barcodereader

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Represents processor configuration options for Aspose.BarCode.
/// </summary>
class ProcessorConfig
{
    public bool UseAllCores { get; set; }
    public int UseOnlyThisCoresCount { get; set; }
    public int MaxAdditionalAllowedThreads { get; set; }
}

/// <summary>
/// Entry point of the application that loads processor settings from a JSON file and applies them to Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that performs the configuration loading and application.
    /// </summary>
    static void Main()
    {
        // Build the full path to the JSON configuration file located in the application base directory.
        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "processorSettings.json");

        // Verify that the configuration file exists before attempting to read it.
        if (!File.Exists(configPath))
        {
            Console.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        string jsonContent;
        try
        {
            // Read the entire JSON file content into a string.
            jsonContent = File.ReadAllText(configPath);
        }
        catch (Exception ex)
        {
            // Handle any I/O errors that may occur while reading the file.
            Console.WriteLine($"Error reading configuration file: {ex.Message}");
            return;
        }

        ProcessorConfig config;
        try
        {
            // Deserialize the JSON into a ProcessorConfig instance.
            config = JsonSerializer.Deserialize<ProcessorConfig>(jsonContent);
            if (config == null)
                throw new InvalidOperationException("Deserialized config is null.");
        }
        catch (Exception ex)
        {
            // Handle JSON parsing errors.
            Console.WriteLine($"Error parsing configuration JSON: {ex.Message}");
            return;
        }

        // Apply the deserialized settings to Aspose.BarCode's static ProcessorSettings.
        BarCodeReader.ProcessorSettings.UseAllCores = config.UseAllCores;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = config.UseOnlyThisCoresCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = config.MaxAdditionalAllowedThreads;

        // Output the applied settings for verification.
        Console.WriteLine("ProcessorSettings applied:");
        Console.WriteLine($"UseAllCores = {BarCodeReader.ProcessorSettings.UseAllCores}");
        Console.WriteLine($"UseOnlyThisCoresCount = {BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}");
        Console.WriteLine($"MaxAdditionalAllowedThreads = {BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads}");
    }
}