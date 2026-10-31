// Title: Generate Barcode Using JSON Configuration Settings
// Description: This example reads barcode appearance settings from a JSON file (or creates a default one) and generates a Code128 barcode image.
// Category-Description: Demonstrates how to use Aspose.BarCode Generation API together with JSON configuration files. The example covers loading or creating a configuration, mapping JSON values to AutoSizeMode, XDimension, and padding properties, and saving the barcode as a PNG. Developers working with barcode rendering often need to externalize appearance settings for flexibility and reuse.
// Prompt: Design a configuration file format to store barcode appearance settings such as AutoSizeMode, XDimension, and padding values.
// Tags: barcode, generation, json, configuration, autosizemode, xdimension, padding, aspose.barcode, png

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Represents the JSON-serializable configuration for barcode appearance.
/// </summary>
class BarcodeConfig
{
    public string AutoSizeMode { get; set; }
    public float XDimension { get; set; }
    public float PaddingLeft { get; set; }
    public float PaddingTop { get; set; }
    public float PaddingRight { get; set; }
    public float PaddingBottom { get; set; }
}

/// <summary>
/// Entry point of the example that loads configuration, generates a barcode, and saves the image.
/// </summary>
class Program
{
    /// <summary>
    /// Main method – orchestrates configuration handling and barcode generation.
    /// </summary>
    static void Main()
    {
        // Determine temporary paths for config and output files.
        string configPath = Path.Combine(Path.GetTempPath(), "barcodeConfig.json");
        BarcodeConfig config = LoadOrCreateConfig(configPath);

        string outputPath = Path.Combine(Path.GetTempPath(), "barcodeOutput.png");
        GenerateBarcode(config, outputPath);

        Console.WriteLine($"Barcode generated at: {outputPath}");
    }

    /// <summary>
    /// Loads an existing JSON configuration file or creates a default one if missing or invalid.
    /// </summary>
    /// <param name="path">Full path to the configuration file.</param>
    /// <returns>Populated <see cref="BarcodeConfig"/> instance.</returns>
    static BarcodeConfig LoadOrCreateConfig(string path)
    {
        if (File.Exists(path))
        {
            try
            {
                // Read and deserialize the JSON configuration.
                string json = File.ReadAllText(path);
                BarcodeConfig cfg = JsonSerializer.Deserialize<BarcodeConfig>(json);
                if (cfg != null)
                    return cfg;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read config: {ex.Message}");
            }
        }

        // Create a default configuration when none exists or deserialization fails.
        BarcodeConfig defaultConfig = new BarcodeConfig
        {
            AutoSizeMode = "None",
            XDimension = 2f,
            PaddingLeft = 5f,
            PaddingTop = 5f,
            PaddingRight = 5f,
            PaddingBottom = 5f
        };

        try
        {
            // Serialize the default configuration to a formatted JSON file.
            string json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
            Console.WriteLine($"Default config created at: {path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write default config: {ex.Message}");
        }

        return defaultConfig;
    }

    /// <summary>
    /// Generates a barcode image using the supplied configuration and saves it to the specified file.
    /// </summary>
    /// <param name="cfg">Configuration containing appearance settings.</param>
    /// <param name="outputFile">Path where the barcode image will be saved.</param>
    static void GenerateBarcode(BarcodeConfig cfg, string outputFile)
    {
        // Convert the AutoSizeMode string to the corresponding enum value.
        if (!Enum.TryParse<AutoSizeMode>(cfg.AutoSizeMode, out AutoSizeMode mode))
        {
            Console.WriteLine($"Invalid AutoSizeMode '{cfg.AutoSizeMode}'. Using None.");
            mode = AutoSizeMode.None;
        }

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Apply configuration settings to the generator parameters.
            generator.Parameters.AutoSizeMode = mode;
            generator.Parameters.Barcode.XDimension.Point = cfg.XDimension;
            generator.Parameters.Barcode.Padding.Left.Point = cfg.PaddingLeft;
            generator.Parameters.Barcode.Padding.Top.Point = cfg.PaddingTop;
            generator.Parameters.Barcode.Padding.Right.Point = cfg.PaddingRight;
            generator.Parameters.Barcode.Padding.Bottom.Point = cfg.PaddingBottom;

            // Save the generated barcode as a PNG image.
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }
    }
}