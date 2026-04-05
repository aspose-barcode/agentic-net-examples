using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;

namespace BarcodeConfigDemo
{
    // Simple POCO to hold barcode appearance settings
    public class BarcodeAppearanceConfig
    {
        public AutoSizeMode AutoSizeMode { get; set; } = AutoSizeMode.None;
        public float XDimension { get; set; } = 2f; // points
        public float PaddingLeft { get; set; } = 5f;   // points
        public float PaddingTop { get; set; } = 5f;    // points
        public float PaddingRight { get; set; } = 5f;  // points
        public float PaddingBottom { get; set; } = 5f; // points
    }

    class Program
    {
        static void Main()
        {
            const string configPath = "barcodeConfig.json";
            const string outputImage = "barcode.png";

            // Create a default configuration and persist it
            var defaultConfig = new BarcodeAppearanceConfig
            {
                AutoSizeMode = AutoSizeMode.Nearest,
                XDimension = 3f,
                PaddingLeft = 10f,
                PaddingTop = 8f,
                PaddingRight = 10f,
                PaddingBottom = 8f
            };

            // Serialize configuration to JSON file
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(defaultConfig, jsonOptions);
            File.WriteAllText(configPath, json);
            Console.WriteLine($"Configuration saved to '{configPath}'.");

            // Load configuration from file
            if (!File.Exists(configPath))
                throw new FileNotFoundException($"Configuration file '{configPath}' not found.");

            string loadedJson = File.ReadAllText(configPath);
            var config = JsonSerializer.Deserialize<BarcodeAppearanceConfig>(loadedJson);
            if (config == null)
                throw new InvalidOperationException("Failed to deserialize barcode configuration.");

            // Create a barcode generator and apply the loaded settings
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "1234567890";

                // Apply appearance settings
                generator.Parameters.AutoSizeMode = config.AutoSizeMode;
                generator.Parameters.Barcode.XDimension.Point = config.XDimension;
                generator.Parameters.Barcode.Padding.Left.Point = config.PaddingLeft;
                generator.Parameters.Barcode.Padding.Top.Point = config.PaddingTop;
                generator.Parameters.Barcode.Padding.Right.Point = config.PaddingRight;
                generator.Parameters.Barcode.Padding.Bottom.Point = config.PaddingBottom;

                // Save the barcode image
                generator.Save(outputImage);
                Console.WriteLine($"Barcode image generated and saved to '{outputImage}'.");
            }
        }
    }
}