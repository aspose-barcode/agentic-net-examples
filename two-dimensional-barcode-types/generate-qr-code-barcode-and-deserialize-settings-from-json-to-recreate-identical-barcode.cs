// Title: Generate QR Code and Recreate It from JSON Settings
// Description: Demonstrates generating a QR Code barcode, exporting its configuration to a JSON file, and recreating an identical barcode from those settings.
// Category-Description: This example belongs to the Aspose.BarCode generation and customization category. It shows how to use BarcodeGenerator, QR-specific parameters (QRErrorLevel, QREncodeMode, ECIEncodings), and Aspose.Drawing colors, then serialize the configuration with System.Text.Json. Developers often need to persist barcode settings for later reuse, batch processing, or configuration sharing across services.
// Prompt: Generate QR Code barcode and deserialize settings from JSON to recreate identical barcode.
// Tags: qr code, barcode generation, json serialization, aspose.barcode, c#, settings persistence

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace AsposeBarcodeJsonDemo
{
    // Simple DTO to hold QR barcode settings for JSON serialization
    public class QrSettings
    {
        public string CodeText { get; set; }
        public int ErrorLevel { get; set; }          // QRErrorLevel enum value
        public int EncodeMode { get; set; }          // QREncodeMode enum value
        public int? ECIEncoding { get; set; }        // ECIEncodings enum value (nullable)
        public float XDimension { get; set; }        // in points
        public int BarColorArgb { get; set; }        // ARGB integer
        public int BackColorArgb { get; set; }       // ARGB integer
    }

    /// <summary>
    /// Demonstrates QR Code generation, JSON serialization of its settings, and recreation from those settings.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that creates an original QR barcode, saves its settings to JSON, and rebuilds the same barcode from the JSON file.
        /// </summary>
        static void Main()
        {
            // Paths for generated files
            string originalImagePath = "qr_original.png";
            string recreatedImagePath = "qr_recreated.png";
            string settingsJsonPath = "qr_settings.json";

            // -----------------------------------------------------------------
            // Step 1: Create original QR barcode with custom settings
            // -----------------------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.CodeText = "https://example.com";
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
                generator.Parameters.Barcode.XDimension.Point = 2f; // module size
                generator.Parameters.Barcode.BarColor = Color.FromArgb(0xFF, 0, 0, 0); // black
                generator.Parameters.BackColor = Color.FromArgb(0xFF, 255, 255, 255); // white

                // Save the original barcode image
                generator.Save(originalImagePath);
                Console.WriteLine($"Original QR barcode saved to '{originalImagePath}'.");

                // -----------------------------------------------------------------
                // Step 2: Capture settings into a DTO and serialize to JSON
                // -----------------------------------------------------------------
                var settings = new QrSettings
                {
                    CodeText = generator.CodeText,
                    ErrorLevel = (int)generator.Parameters.Barcode.QR.ErrorLevel,
                    EncodeMode = (int)generator.Parameters.Barcode.QR.EncodeMode,
                    ECIEncoding = (int)generator.Parameters.Barcode.QR.ECIEncoding,
                    XDimension = generator.Parameters.Barcode.XDimension.Point,
                    BarColorArgb = generator.Parameters.Barcode.BarColor.ToArgb(),
                    BackColorArgb = generator.Parameters.BackColor.ToArgb()
                };

                string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(settingsJsonPath, json);
                Console.WriteLine($"Barcode settings serialized to JSON file '{settingsJsonPath}'.");
            }

            // -----------------------------------------------------------------
            // Step 3: Read JSON, deserialize settings, and recreate identical barcode
            // -----------------------------------------------------------------
            if (!File.Exists(settingsJsonPath))
            {
                Console.WriteLine($"Settings file '{settingsJsonPath}' not found. Exiting.");
                return;
            }

            string jsonContent = File.ReadAllText(settingsJsonPath);
            QrSettings deserializedSettings = JsonSerializer.Deserialize<QrSettings>(jsonContent);

            using (var recreatedGenerator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Apply deserialized settings
                recreatedGenerator.CodeText = deserializedSettings.CodeText;
                recreatedGenerator.Parameters.Barcode.QR.ErrorLevel = (QRErrorLevel)deserializedSettings.ErrorLevel;
                recreatedGenerator.Parameters.Barcode.QR.EncodeMode = (QREncodeMode)deserializedSettings.EncodeMode;

                if (deserializedSettings.ECIEncoding.HasValue)
                {
                    recreatedGenerator.Parameters.Barcode.QR.ECIEncoding = (ECIEncodings)deserializedSettings.ECIEncoding.Value;
                }

                recreatedGenerator.Parameters.Barcode.XDimension.Point = deserializedSettings.XDimension;
                recreatedGenerator.Parameters.Barcode.BarColor = Color.FromArgb(deserializedSettings.BarColorArgb);
                recreatedGenerator.Parameters.BackColor = Color.FromArgb(deserializedSettings.BackColorArgb);

                // Save the recreated barcode image
                recreatedGenerator.Save(recreatedImagePath);
                Console.WriteLine($"Recreated QR barcode saved to '{recreatedImagePath}'.");
            }

            Console.WriteLine("Process completed successfully.");
        }
    }
}