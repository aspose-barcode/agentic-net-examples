using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Represents the configuration settings for barcode generation.
/// </summary>
public class BarcodeConfig
{
    /// <summary>
    /// Gets or sets the auto-size mode for the barcode.
    /// </summary>
    public AutoSizeMode AutoSizeMode { get; set; }

    /// <summary>
    /// Gets or sets the X-dimension (module width) of the barcode.
    /// </summary>
    public float XDimension { get; set; }

    /// <summary>
    /// Gets or sets the left padding of the barcode.
    /// </summary>
    public float PaddingLeft { get; set; }

    /// <summary>
    /// Gets or sets the top padding of the barcode.
    /// </summary>
    public float PaddingTop { get; set; }

    /// <summary>
    /// Gets or sets the right padding of the barcode.
    /// </summary>
    public float PaddingRight { get; set; }

    /// <summary>
    /// Gets or sets the bottom padding of the barcode.
    /// </summary>
    public float PaddingBottom { get; set; }
}

class Program
{
    /// <summary>
    /// Entry point of the application. Loads barcode configuration, generates a sample barcode, and saves it to a file.
    /// </summary>
    static void Main()
    {
        const string configPath = "barcodeConfig.json"; // Path to the JSON configuration file.
        const string outputPath = "barcode.png";        // Path where the generated barcode image will be saved.

        // Ensure a configuration file exists; create a default one if missing.
        if (!File.Exists(configPath))
        {
            var defaultConfig = new BarcodeConfig
            {
                AutoSizeMode = AutoSizeMode.None,
                XDimension = 2f,
                PaddingLeft = 5f,
                PaddingTop = 5f,
                PaddingRight = 5f,
                PaddingBottom = 5f
            };

            // Serialize the default configuration with indentation for readability.
            var json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, json);
            Console.WriteLine($"Default configuration created at '{configPath}'.");
        }

        // Load configuration from the JSON file.
        BarcodeConfig config;
        try
        {
            var json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<BarcodeConfig>(json);
            if (config == null)
                throw new InvalidOperationException("Configuration deserialization returned null.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load configuration: {ex.Message}");
            return;
        }

        // Generate a sample barcode using the loaded settings.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Apply appearance settings from the configuration.
            generator.Parameters.AutoSizeMode = config.AutoSizeMode;
            generator.Parameters.Barcode.XDimension.Point = config.XDimension;
            generator.Parameters.Barcode.Padding.Left.Point = config.PaddingLeft;
            generator.Parameters.Barcode.Padding.Top.Point = config.PaddingTop;
            generator.Parameters.Barcode.Padding.Right.Point = config.PaddingRight;
            generator.Parameters.Barcode.Padding.Bottom.Point = config.PaddingBottom;

            // Save the barcode image to the specified output path.
            generator.Save(outputPath);
        }

        Console.WriteLine($"Barcode generated with settings from '{configPath}' and saved to '{outputPath}'.");
    }
}