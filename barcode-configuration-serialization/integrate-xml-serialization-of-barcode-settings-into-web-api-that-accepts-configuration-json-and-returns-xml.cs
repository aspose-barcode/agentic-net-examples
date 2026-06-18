using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Represents the configuration for generating a barcode.
/// </summary>
class BarcodeConfig
{
    /// <summary>
    /// Gets or sets the symbology name (e.g., "Code128").
    /// </summary>
    public string Symbology { get; set; }

    /// <summary>
    /// Gets or sets the text to encode in the barcode.
    /// </summary>
    public string CodeText { get; set; }

    /// <summary>
    /// Gets or sets the optional bar height (in points).
    /// </summary>
    public float? BarHeight { get; set; }

    /// <summary>
    /// Gets or sets the optional X-dimension (module width) (in points).
    /// </summary>
    public float? XDimension { get; set; }
}

/// <summary>
/// Demonstrates creating a barcode from a JSON configuration and exporting its settings to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    static void Main()
    {
        // Sample JSON configuration (in a real scenario this would come from an HTTP request)
        string jsonConfig = @"{
            ""Symbology"": ""Code128"",
            ""CodeText"": ""123ABC"",
            ""BarHeight"": 40.0,
            ""XDimension"": 2.5
        }";

        // Deserialize JSON to a BarcodeConfig instance
        BarcodeConfig config;
        try
        {
            config = JsonSerializer.Deserialize<BarcodeConfig>(jsonConfig);
            if (config == null)
                throw new ArgumentException("Configuration deserialization resulted in null.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing JSON configuration: {ex.Message}");
            return;
        }

        // Resolve the symbology name to the corresponding BaseEncodeType using reflection
        var field = typeof(EncodeTypes).GetField(config.Symbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {config.Symbology}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create a barcode generator with the resolved encode type and provided code text
        using (var generator = new BarcodeGenerator(encodeType, config.CodeText ?? string.Empty))
        {
            // Apply optional numeric settings if they are present and valid
            if (config.BarHeight.HasValue && config.BarHeight.Value > 0f)
                generator.Parameters.Barcode.BarHeight.Point = config.BarHeight.Value;

            if (config.XDimension.HasValue && config.XDimension.Value > 0f)
                generator.Parameters.Barcode.XDimension.Point = config.XDimension.Value;

            // Export the generator's settings to XML and display the result
            using (var ms = new MemoryStream())
            {
                generator.ExportToXml(ms);
                ms.Position = 0;
                using (var reader = new StreamReader(ms))
                {
                    string xmlOutput = reader.ReadToEnd();
                    Console.WriteLine("=== Barcode Settings XML ===");
                    Console.WriteLine(xmlOutput);
                }
            }
        }
    }
}