using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Represents application configuration settings for barcode reading.
/// </summary>
class Config
{
    // Determines whether the reader should attempt to detect character encoding.
    public bool DetectEncoding { get; set; } = true;

    // Specifies the checksum validation mode (e.g., Default, Enabled, Disabled).
    public string ChecksumValidation { get; set; } = "Default";
}

/// <summary>
/// Main program class that demonstrates barcode generation and reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        const string configPath = "config.json";          // Path to optional JSON configuration file
        const string barcodePath = "sample_barcode.png";  // Path where the sample barcode image will be saved

        // Load configuration from file or fall back to default values
        Config config = LoadConfig(configPath);

        // Generate a sample Code128 barcode image for demonstration purposes
        GenerateSampleBarcode(barcodePath);

        // Initialize a barcode reader for the generated image, targeting Code128 symbology
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Apply the DetectEncoding setting from the configuration
            reader.BarcodeSettings.DetectEncoding = config.DetectEncoding;

            // Attempt to parse and apply the ChecksumValidation setting from the configuration
            if (Enum.TryParse<ChecksumValidation>(config.ChecksumValidation, true, out var checksumVal))
            {
                reader.BarcodeSettings.ChecksumValidation = checksumVal;
            }
            else
            {
                // If parsing fails, inform the user and use the default validation mode
                Console.WriteLine($"Invalid ChecksumValidation value '{config.ChecksumValidation}'. Using default.");
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;
            }

            // Perform barcode recognition and output details for each detected barcode
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Checksum Validation: {reader.BarcodeSettings.ChecksumValidation}");
                Console.WriteLine($"Detect Encoding: {reader.BarcodeSettings.DetectEncoding}");
            }
        }
    }

    /// <summary>
    /// Loads configuration settings from a JSON file. Returns default settings if the file is missing or invalid.
    /// </summary>
    /// <param name="path">The file path to the JSON configuration.</param>
    /// <returns>A <see cref="Config"/> instance populated with settings.</returns>
    static Config LoadConfig(string path)
    {
        // If the configuration file does not exist, use default settings
        if (!File.Exists(path))
        {
            Console.WriteLine($"Configuration file '{path}' not found. Using default settings.");
            return new Config();
        }

        try
        {
            // Read the entire JSON content
            string json = File.ReadAllText(path);

            // Deserialize JSON into a Config object
            var cfg = JsonSerializer.Deserialize<Config>(json);
            if (cfg == null)
                throw new InvalidOperationException("Deserialized config is null.");

            return cfg;
        }
        catch (Exception ex)
        {
            // On any error, log the issue and fall back to defaults
            Console.WriteLine($"Failed to load configuration: {ex.Message}");
            Console.WriteLine("Using default settings.");
            return new Config();
        }
    }

    /// <summary>
    /// Generates a sample Code128 barcode image and saves it to the specified path.
    /// </summary>
    /// <param name="outputPath">The file path where the barcode image will be saved.</param>
    static void GenerateSampleBarcode(string outputPath)
    {
        // Create a barcode generator with the desired symbology and data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890128"))
        {
            // Save the generated barcode image to the provided file path
            generator.Save(outputPath);
        }
    }
}