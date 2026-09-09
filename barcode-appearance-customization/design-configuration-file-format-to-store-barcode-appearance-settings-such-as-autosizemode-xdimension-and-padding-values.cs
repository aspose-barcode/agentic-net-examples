// Title: Generate barcode using configuration file settings
// Description: Demonstrates reading barcode appearance settings from a JSON config file and applying them to a BarcodeGenerator.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure AutoSizeMode, XDimension, and padding via the BarcodeGenerator.Parameters API. Developers often need to customize barcode size and margins for printing or UI display; this snippet shows typical usage of EncodeTypes, AutoSizeMode enum, and BarCodeImageFormat classes.
// Prompt: Design a configuration file format to store barcode appearance settings such as AutoSizeMode, XDimension, and padding values.
// Tags: barcode, configuration, autosizemode, xdimension, padding, generation, json, aspose.barcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Represents barcode appearance settings loaded from a JSON configuration file.
/// </summary>
class Config
{
    public string AutoSizeMode { get; set; }
    public float XDimension { get; set; }
    public float PaddingLeft { get; set; }
    public float PaddingTop { get; set; }
    public float PaddingRight { get; set; }
    public float PaddingBottom { get; set; }
}

class Program
{
    /// <summary>
    /// Entry point that reads configuration, generates a barcode, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Determine the path for the JSON configuration file in the temporary folder.
        string configPath = Path.Combine(Path.GetTempPath(), "barcodeConfig.json");

        // If the config file does not exist, create one with default values.
        if (!File.Exists(configPath))
        {
            var defaultConfig = new Config
            {
                AutoSizeMode = "None",
                XDimension = 3.0f,
                PaddingLeft = 5.0f,
                PaddingTop = 5.0f,
                PaddingRight = 5.0f,
                PaddingBottom = 5.0f
            };

            // Serialize the default configuration to formatted JSON.
            string json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, json);
            Console.WriteLine($"Created default config at: {configPath}");
        }

        Config cfg;
        try
        {
            // Read and deserialize the JSON configuration file.
            string jsonContent = File.ReadAllText(configPath);
            cfg = JsonSerializer.Deserialize<Config>(jsonContent);
            if (cfg == null)
                throw new InvalidOperationException("Deserialized config is null.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read config: {ex.Message}");
            return;
        }

        // Parse the AutoSizeMode string into the corresponding enum value.
        AutoSizeMode mode;
        try
        {
            mode = (AutoSizeMode)Enum.Parse(typeof(AutoSizeMode), cfg.AutoSizeMode, ignoreCase: true);
        }
        catch (Exception)
        {
            Console.WriteLine($"Invalid AutoSizeMode value '{cfg.AutoSizeMode}'. Using AutoSizeMode.None.");
            mode = AutoSizeMode.None;
        }

        // Define the output path for the generated barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "generatedBarcode.png");

        // Create and configure the barcode generator.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Parameters.AutoSizeMode = mode;
            generator.Parameters.Barcode.XDimension.Point = cfg.XDimension;
            generator.Parameters.Barcode.Padding.Left.Point = cfg.PaddingLeft;
            generator.Parameters.Barcode.Padding.Top.Point = cfg.PaddingTop;
            generator.Parameters.Barcode.Padding.Right.Point = cfg.PaddingRight;
            generator.Parameters.Barcode.Padding.Bottom.Point = cfg.PaddingBottom;

            // Save the barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode generated and saved to: {outputPath}");
    }
}