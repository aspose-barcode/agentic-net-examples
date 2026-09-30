// Title: Barcode Appearance Settings via JSON Configuration
// Description: Demonstrates loading barcode appearance parameters from a JSON file and applying them to an Aspose.BarCode generator.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure visual aspects such as AutoSizeMode, XDimension, and padding using external configuration. It highlights key API classes like BarcodeGenerator, BarcodeParameters, and AutoSizeMode, which developers commonly use to customize barcode rendering for different output formats and layout requirements.
// Prompt: Design a configuration file format to store barcode appearance settings such as AutoSizeMode, XDimension, and padding values.
// Tags: barcode symbology, configuration, json, appearance, autosizemode, xdimension, padding, aspose.barcode, generation

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeConfigDemo
{
    /// <summary>
    /// Simple POCO that represents barcode appearance settings.
    /// These values are deserialized from a JSON configuration file.
    /// </summary>
    public class BarcodeAppearanceSettings
    {
        public string AutoSizeMode { get; set; } = "None";
        public float XDimension { get; set; } = 2f; // points
        public float PaddingLeft { get; set; } = 5f;
        public float PaddingTop { get; set; } = 5f;
        public float PaddingRight { get; set; } = 5f;
        public float PaddingBottom { get; set; } = 5f;
    }

    /// <summary>
    /// Demonstrates reading barcode appearance settings from a JSON file,
    /// applying them to a BarcodeGenerator, and saving the resulting image.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the demo application.
        /// </summary>
        static void Main()
        {
            // Determine temporary paths for the configuration file and the output image.
            string configPath = Path.Combine(Path.GetTempPath(), "barcodeAppearanceSettings.json");
            string outputPath = Path.Combine(Path.GetTempPath(), "sampleBarcode.png");

            // Ensure a configuration file exists; create a default one if missing.
            if (!File.Exists(configPath))
            {
                var defaultSettings = new BarcodeAppearanceSettings();
                string json = JsonSerializer.Serialize(defaultSettings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configPath, json);
                Console.WriteLine($"Created default configuration at: {configPath}");
            }

            // Load settings from the JSON file.
            BarcodeAppearanceSettings settings;
            try
            {
                string json = File.ReadAllText(configPath);
                settings = JsonSerializer.Deserialize<BarcodeAppearanceSettings>(json);
                if (settings == null)
                    throw new InvalidOperationException("Deserialized settings are null.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load configuration: {ex.Message}");
                return;
            }

            // Create a barcode generator with sample data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Apply AutoSizeMode if the value can be parsed; otherwise keep the default.
                if (Enum.TryParse<AutoSizeMode>(settings.AutoSizeMode, out var autoSize))
                {
                    generator.Parameters.AutoSizeMode = autoSize;
                }
                else
                {
                    Console.WriteLine($"Invalid AutoSizeMode '{settings.AutoSizeMode}'. Using default.");
                }

                // Apply XDimension (module size) in points.
                generator.Parameters.Barcode.XDimension.Point = settings.XDimension;

                // Apply individual padding values in points.
                generator.Parameters.Barcode.Padding.Left.Point = settings.PaddingLeft;
                generator.Parameters.Barcode.Padding.Top.Point = settings.PaddingTop;
                generator.Parameters.Barcode.Padding.Right.Point = settings.PaddingRight;
                generator.Parameters.Barcode.Padding.Bottom.Point = settings.PaddingBottom;

                // Optional: set a foreground color to demonstrate appearance change.
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Save the generated barcode image to the specified path.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode image saved to: {outputPath}");
            }
        }
    }
}