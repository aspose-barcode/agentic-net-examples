using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Common;
using Aspose.BarCode.BarCodeRecognition;

namespace ProcessorSettingsLoader
{
    // Model matching the JSON configuration file
    public class ProcessorConfig
    {
        public bool UseAllCores { get; set; }
        public int UseOnlyThisCoresCount { get; set; }
        public int? MaxAdditionalAllowedThreads { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string configFile = "processorSettings.json";

            // Ensure a configuration file exists; create a default one if missing
            if (!File.Exists(configFile))
            {
                var defaultConfig = new ProcessorConfig
                {
                    UseAllCores = true,
                    UseOnlyThisCoresCount = Math.Max(1, Environment.ProcessorCount / 2),
                    MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2
                };

                string json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configFile, json);
                Console.WriteLine($"Created default configuration file at '{configFile}'.");
            }

            // Load configuration from JSON
            string configContent = File.ReadAllText(configFile);
            ProcessorConfig config;
            try
            {
                config = JsonSerializer.Deserialize<ProcessorConfig>(configContent);
                if (config == null)
                    throw new InvalidOperationException("Deserialized configuration is null.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse configuration: {ex.Message}");
                return;
            }

            // Apply settings to Aspose.BarCode ProcessorSettings
            BarCodeReader.ProcessorSettings.UseAllCores = config.UseAllCores;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = config.UseOnlyThisCoresCount;
            if (config.MaxAdditionalAllowedThreads.HasValue)
                BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = config.MaxAdditionalAllowedThreads.Value;

            // Output applied settings for verification
            Console.WriteLine("ProcessorSettings applied:");
            Console.WriteLine($"  UseAllCores = {BarCodeReader.ProcessorSettings.UseAllCores}");
            Console.WriteLine($"  UseOnlyThisCoresCount = {BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}");
            Console.WriteLine($"  MaxAdditionalAllowedThreads = {BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads}");
        }
    }
}