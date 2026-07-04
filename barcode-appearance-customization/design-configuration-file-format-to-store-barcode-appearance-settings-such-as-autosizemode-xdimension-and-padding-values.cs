// Title: Barcode Generation with Configurable Appearance Settings
// Description: Demonstrates reading barcode appearance options from a JSON configuration file and applying them to an Aspose.BarCode generator.
// Prompt: Design a configuration file format to store barcode appearance settings such as AutoSizeMode, XDimension, and padding values.
// Tags: barcode, configuration, json, autosizemode, xdimension, padding, aspose.barcode, c#

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeConfigDemo
{
    /// <summary>
    /// Represents the structure of the configuration file.
    /// The file is a simple JSON document, e.g.:
    /// {
    ///   "AutoSizeMode": "Interpolation",
    ///   "XDimension": 2.5,
    ///   "Padding": { "Left": 5, "Top": 5, "Right": 5, "Bottom": 5 }
    /// }
    /// </summary>
    public class BarcodeConfig
    {
        // AutoSizeMode as a string; will be parsed to the corresponding enum.
        public string AutoSizeMode { get; set; } = "None";

        // Module size of the barcode (in points).
        public float XDimension { get; set; } = 1.0f;

        // Padding values around the barcode.
        public PaddingConfig Padding { get; set; } = new PaddingConfig();
    }

    /// <summary>
    /// Holds padding values for each side of the barcode.
    /// </summary>
    public class PaddingConfig
    {
        public float Left { get; set; } = 0f;
        public float Top { get; set; } = 0f;
        public float Right { get; set; } = 0f;
        public float Bottom { get; set; } = 0f;
    }

    class Program
    {
        /// <summary>
        /// Entry point of the demo. Reads configuration, creates a barcode, and saves it as an image.
        /// </summary>
        static void Main()
        {
            const string configFile = "barcodeConfig.json";

            // Ensure a configuration file exists; create a default one if missing.
            if (!File.Exists(configFile))
            {
                var defaultConfig = new BarcodeConfig
                {
                    AutoSizeMode = "Interpolation",
                    XDimension = 2.5f,
                    Padding = new PaddingConfig { Left = 5f, Top = 5f, Right = 5f, Bottom = 5f }
                };
                var json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configFile, json);
                Console.WriteLine($"Created default configuration file: {configFile}");
            }

            // Load configuration from the JSON file.
            BarcodeConfig config;
            try
            {
                var json = File.ReadAllText(configFile);
                config = JsonSerializer.Deserialize<BarcodeConfig>(json);
                if (config == null)
                    throw new InvalidOperationException("Configuration deserialization resulted in null.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            // Convert the AutoSizeMode string to the corresponding enum value.
            if (!Enum.TryParse<AutoSizeMode>(config.AutoSizeMode, ignoreCase: true, out var autoSizeMode))
            {
                Console.WriteLine($"Invalid AutoSizeMode value: {config.AutoSizeMode}");
                return;
            }

            // Create a barcode generator and apply settings from the configuration.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Apply AutoSizeMode.
                generator.Parameters.AutoSizeMode = autoSizeMode;

                // Apply XDimension (module size) using the Point unit.
                generator.Parameters.Barcode.XDimension.Point = config.XDimension;

                // Apply padding for each side.
                generator.Parameters.Barcode.Padding.Left.Point = config.Padding.Left;
                generator.Parameters.Barcode.Padding.Top.Point = config.Padding.Top;
                generator.Parameters.Barcode.Padding.Right.Point = config.Padding.Right;
                generator.Parameters.Barcode.Padding.Bottom.Point = config.Padding.Bottom;

                // Save the generated barcode image.
                const string outputFile = "barcode.png";
                generator.Save(outputFile);
                Console.WriteLine($"Barcode generated and saved to {outputFile}");
            }
        }
    }
}