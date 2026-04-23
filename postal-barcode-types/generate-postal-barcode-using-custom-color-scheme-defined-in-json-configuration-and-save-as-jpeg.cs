using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

namespace PostalBarcodeExample
{
    // Configuration model for custom colors
    public class ColorConfig
    {
        public string BarColor { get; set; }
        public string BackColor { get; set; }
    }

    class Program
    {
        // Parses a hex color string like "#RRGGBB" into an Aspose.Drawing.Color
        private static Color ParseHexColor(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                throw new ArgumentException("Color string cannot be null or empty.");

            hex = hex.TrimStart('#');
            if (hex.Length != 6)
                throw new ArgumentException("Hex color must be in format RRGGBB.");

            int r = Convert.ToInt32(hex.Substring(0, 2), 16);
            int g = Convert.ToInt32(hex.Substring(2, 2), 16);
            int b = Convert.ToInt32(hex.Substring(4, 2), 16);
            return Color.FromArgb(255, r, g, b);
        }

        static void Main()
        {
            const string configPath = "config.json";
            const string outputPath = "postal_barcode.jpg";

            // Verify configuration file exists
            if (!File.Exists(configPath))
            {
                Console.WriteLine($"Configuration file '{configPath}' not found.");
                return;
            }

            // Load color configuration from JSON
            ColorConfig config;
            using (FileStream fs = File.OpenRead(configPath))
            {
                config = JsonSerializer.Deserialize<ColorConfig>(fs);
            }

            if (config == null)
            {
                Console.WriteLine("Failed to deserialize configuration.");
                return;
            }

            // Parse colors
            Color barColor;
            Color backColor;
            try
            {
                barColor = ParseHexColor(config.BarColor);
                backColor = ParseHexColor(config.BackColor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Color parsing error: {ex.Message}");
                return;
            }

            // Create and configure the postal barcode generator
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
            {
                // Apply custom colors
                generator.Parameters.Barcode.BarColor = barColor;
                generator.Parameters.BackColor = backColor;

                // Optional: adjust image size if needed
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save as JPEG
                generator.Save(outputPath, BarCodeImageFormat.Jpeg);
            }

            Console.WriteLine($"Barcode saved to '{outputPath}'.");
        }
    }
}