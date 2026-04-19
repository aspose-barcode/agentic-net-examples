using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace AsposeBarcodeQrJsonDemo
{
    // DTO for storing selected barcode settings
    public class QrSettingsDto
    {
        public string CodeText { get; set; }
        public string QrErrorLevel { get; set; } // enum name
        public float XDimensionPoints { get; set; }
        public float ImageWidthPoints { get; set; }
        public float ImageHeightPoints { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string originalImagePath = "qr_original.png";
            const string restoredImagePath = "qr_restored.png";
            const string jsonPath = "qr_settings.json";

            // 1. Create QR code and save image
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Set QR error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Example unit settings
                generator.Parameters.Barcode.XDimension.Point = 2f;          // module size
                generator.Parameters.ImageWidth.Point = 300f;               // image width
                generator.Parameters.ImageHeight.Point = 300f;              // image height

                // Save original image
                generator.Save(originalImagePath);
                
                // 2. Capture settings into DTO
                var settings = new QrSettingsDto
                {
                    CodeText = generator.CodeText,
                    QrErrorLevel = generator.Parameters.Barcode.QR.ErrorLevel.ToString(),
                    XDimensionPoints = generator.Parameters.Barcode.XDimension.Point,
                    ImageWidthPoints = generator.Parameters.ImageWidth.Point,
                    ImageHeightPoints = generator.Parameters.ImageHeight.Point
                };

                // 3. Serialize settings to JSON
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(settings, jsonOptions);
                File.WriteAllText(jsonPath, json);
            }

            // 4. Read JSON and deserialize settings
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"Settings file not found: {jsonPath}");
                return;
            }

            string jsonContent = File.ReadAllText(jsonPath);
            QrSettingsDto restoredSettings = JsonSerializer.Deserialize<QrSettingsDto>(jsonContent);
            if (restoredSettings == null)
            {
                Console.WriteLine("Failed to deserialize settings.");
                return;
            }

            // 5. Recreate barcode using deserialized settings
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, restoredSettings.CodeText))
            {
                // Apply QR error correction level
                if (Enum.TryParse<QRErrorLevel>(restoredSettings.QrErrorLevel, out var errorLevel))
                {
                    generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;
                }

                // Apply unit settings
                generator.Parameters.Barcode.XDimension.Point = restoredSettings.XDimensionPoints;
                generator.Parameters.ImageWidth.Point = restoredSettings.ImageWidthPoints;
                generator.Parameters.ImageHeight.Point = restoredSettings.ImageHeightPoints;

                // Save restored image
                generator.Save(restoredImagePath);
            }

            Console.WriteLine("QR code generation and JSON round‑trip completed.");
        }
    }
}