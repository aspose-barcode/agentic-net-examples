// Title: Barcode Reader Configuration Demo
// Description: Demonstrates loading a JSON configuration to toggle DetectEncoding and ChecksumValidation for Aspose.BarCode barcode reading without recompiling.
// Category-Description: This example belongs to the Aspose.BarCode configuration and decoding category, showing how to use BarCodeReader, BarcodeGenerator, and related settings. Developers often need to adjust reader behavior such as encoding detection and checksum validation via external config files for flexible deployments.
// Prompt: Implement a configuration file allowing toggling DetectEncoding and ChecksumValidation values without recompiling the application.
// Tags: barcode, configuration, json, detectencoding, checksumvalidation, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeConfigDemo
{
    /// <summary>
    /// Represents reader configuration options that can be loaded from a JSON file.
    /// </summary>
    public class ReaderConfig
    {
        // Determines whether the reader should attempt to detect the encoding of the barcode.
        public bool DetectEncoding { get; set; } = true;

        // Controls whether checksum validation is performed during barcode reading.
        public bool ChecksumValidation { get; set; } = true;
    }

    class Program
    {
        /// <summary>
        /// Entry point of the demo. Loads configuration, generates a sample barcode, reads it using the configured settings, and cleans up.
        /// </summary>
        static void Main()
        {
            // Determine the full path to the configuration file located alongside the executable.
            string configPath = Path.Combine(AppContext.BaseDirectory, "config.json");

            // If the configuration file does not exist, create one with default values.
            if (!File.Exists(configPath))
            {
                var defaultConfig = new ReaderConfig();
                string json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configPath, json);
                Console.WriteLine($"Created default config at: {configPath}");
            }

            // Load configuration from the JSON file, falling back to defaults on error.
            ReaderConfig config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<ReaderConfig>(json) ?? new ReaderConfig();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read config. Using defaults. Error: {ex.Message}");
                config = new ReaderConfig();
            }

            // Generate a temporary barcode image that will be used for reading.
            string barcodePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Save the generated barcode as a PNG file.
                generator.Save(barcodePath, BarCodeImageFormat.Png);
            }

            // Specify the expected barcode symbology for the reader.
            BaseDecodeType decodeType = DecodeType.Code128;

            // Initialize the barcode reader with the generated image and decode type.
            using (var reader = new BarCodeReader(barcodePath, decodeType))
            {
                // Apply configuration settings to the reader.
                reader.BarcodeSettings.DetectEncoding = config.DetectEncoding;
                reader.BarcodeSettings.ChecksumValidation = config.ChecksumValidation ? ChecksumValidation.On : ChecksumValidation.Off;

                // Perform the barcode reading operation.
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output results or indicate that no barcode was detected/validated.
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected or checksum validation failed.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Code Text: {result.CodeText}");
                        Console.WriteLine($"Symbology: {result.CodeTypeName}");
                        Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                    }
                }
            }

            // Attempt to delete the temporary barcode image; ignore any errors.
            try
            {
                if (File.Exists(barcodePath))
                {
                    File.Delete(barcodePath);
                }
            }
            catch
            {
                // Ignored - cleanup failure should not affect program exit.
            }

            Console.WriteLine("Demo completed.");
        }
    }
}