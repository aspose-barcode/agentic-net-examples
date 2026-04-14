using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeConfigDemo
{
    // Represents padding values for each side.
    public class PaddingConfig
    {
        public float Left { get; set; }
        public float Top { get; set; }
        public float Right { get; set; }
        public float Bottom { get; set; }
    }

    // Stores appearance settings that can be persisted.
    public class BarcodeConfig
    {
        public string AutoSizeMode { get; set; }   // e.g., "Interpolation"
        public float XDimension { get; set; }      // in points
        public PaddingConfig Padding { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string configPath = "barcodeConfig.json";
            const string outputImage = "code128.png";

            // Step 1: Create a sample configuration and save it.
            var sampleConfig = new BarcodeConfig
            {
                AutoSizeMode = AutoSizeMode.Interpolation.ToString(),
                XDimension = 2.5f,
                Padding = new PaddingConfig
                {
                    Left = 10f,
                    Top = 15f,
                    Right = 10f,
                    Bottom = 15f
                }
            };
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(sampleConfig, jsonOptions);
            File.WriteAllText(configPath, json);
            Console.WriteLine($"Sample configuration written to '{configPath}'.");

            // Step 2: Load the configuration from the file.
            if (!File.Exists(configPath))
            {
                Console.WriteLine($"Configuration file '{configPath}' not found.");
                return;
            }
            string loadedJson = File.ReadAllText(configPath);
            BarcodeConfig config = JsonSerializer.Deserialize<BarcodeConfig>(loadedJson);
            if (config == null)
            {
                Console.WriteLine("Failed to deserialize configuration.");
                return;
            }

            // Step 3: Apply the settings to a barcode generator.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "1234567890";

                // Apply AutoSizeMode if the enum value is valid.
                if (Enum.TryParse(typeof(AutoSizeMode), config.AutoSizeMode, out var autoSize))
                {
                    generator.Parameters.AutoSizeMode = (AutoSizeMode)autoSize;
                }

                // Set XDimension using the .Point member.
                generator.Parameters.Barcode.XDimension.Point = config.XDimension;

                // Set individual padding sides.
                generator.Parameters.Barcode.Padding.Left.Point = config.Padding.Left;
                generator.Parameters.Barcode.Padding.Top.Point = config.Padding.Top;
                generator.Parameters.Barcode.Padding.Right.Point = config.Padding.Right;
                generator.Parameters.Barcode.Padding.Bottom.Point = config.Padding.Bottom;

                // Optional: set colors to demonstrate additional appearance settings.
                generator.Parameters.Barcode.BarColor = Color.Blue;
                generator.Parameters.BackColor = Color.LightYellow;

                // Save the generated barcode image.
                generator.Save(outputImage, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode image saved to '{outputImage}'.");
            }
        }
    }
}