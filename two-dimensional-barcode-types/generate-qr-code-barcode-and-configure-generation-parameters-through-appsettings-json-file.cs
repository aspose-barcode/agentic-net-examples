using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace QRCodeGeneratorApp
{
    // Represents configuration settings loaded from appsettings.json
    public class AppSettings
    {
        public string CodeText { get; set; } = "https://example.com";
        public string ErrorLevel { get; set; } = "LevelM"; // QRErrorLevel enum name
        public float ImageWidth { get; set; } = 300f;      // points
        public float ImageHeight { get; set; } = 300f;     // points
        public int Resolution { get; set; } = 96;         // dpi
        public string ForegroundColor { get; set; } = "Black"; // Aspose.Drawing.Color name
        public string BackgroundColor { get; set; } = "White";
    }

    class Program
    {
        static void Main()
        {
            // Load configuration from appsettings.json if it exists; otherwise use defaults.
            AppSettings settings = LoadSettings("appsettings.json");

            // Create a QR Code generator with the specified code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, settings.CodeText))
            {
                // Set QR error correction level.
                if (Enum.TryParse<QRErrorLevel>(settings.ErrorLevel, out var errorLevel))
                {
                    generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;
                }

                // Set image dimensions (using Point units as required).
                generator.Parameters.ImageWidth.Point = settings.ImageWidth;
                generator.Parameters.ImageHeight.Point = settings.ImageHeight;

                // Set resolution.
                generator.Parameters.Resolution = settings.Resolution;

                // Set foreground (bar) color.
                generator.Parameters.Barcode.BarColor = GetColor(settings.ForegroundColor);

                // Set background color.
                generator.Parameters.BackColor = GetColor(settings.BackgroundColor);

                // Save the generated QR Code image.
                generator.Save("qr.png");
            }
        }

        // Loads settings from a JSON file; returns defaults if file is missing or invalid.
        private static AppSettings LoadSettings(string path)
        {
            if (!File.Exists(path))
            {
                return new AppSettings();
            }

            try
            {
                string json = File.ReadAllText(path);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var loaded = JsonSerializer.Deserialize<AppSettings>(json, options);
                return loaded ?? new AppSettings();
            }
            catch
            {
                // In case of any deserialization error, fall back to defaults.
                return new AppSettings();
            }
        }

        // Converts a color name to an Aspose.Drawing.Color; defaults to Black if unknown.
        private static Color GetColor(string name)
        {
            try
            {
                return Color.FromName(name);
            }
            catch
            {
                return Color.Black;
            }
        }
    }
}