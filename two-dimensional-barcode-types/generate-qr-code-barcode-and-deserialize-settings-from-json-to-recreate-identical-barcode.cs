// Title: Generate QR Code and Recreate from JSON Settings
// Description: Demonstrates creating a QR Code barcode, saving its image, serializing its configuration to JSON, then deserializing to regenerate an identical barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It showcases the use of BarcodeGenerator, EncodeTypes, and QR‑specific parameters (QRErrorLevel, QREncodeMode). Typical scenarios include persisting barcode configurations, sharing settings across services, or recreating barcodes after a round‑trip. Developers often need to serialize settings to JSON or XML and later restore them without manual re‑configuration.
// Prompt: Generate QR Code barcode and deserialize settings from JSON to recreate identical barcode.
// Tags: qr code, barcode generation, json serialization, aspose.barcode, c#

using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace AsposeBarcodeQrJsonDemo
{
    /// <summary>
    /// Data transfer object used to serialize and deserialize QR Code generation settings.
    /// </summary>
    public class BarcodeSettings
    {
        public string EncodeType { get; set; }
        public string CodeText { get; set; }
        public string ErrorLevel { get; set; }
        public string EncodeMode { get; set; }
    }

    /// <summary>
    /// Demonstrates QR Code generation, JSON serialization of its settings, and recreation of the same barcode from the JSON data.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the demo. Generates a QR Code, saves its settings to JSON, then restores the barcode from the JSON file.
        /// </summary>
        static void Main()
        {
            // File names used in the demo
            const string originalImage = "qr_original.png";
            const string restoredImage = "qr_restored.png";
            const string jsonFile = "qr_settings.json";

            // -----------------------------------------------------------------
            // 1. Create a QR Code barcode, configure QR‑specific settings,
            //    save the image and serialize the settings to JSON.
            // -----------------------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the data to encode
                generator.CodeText = "Hello Aspose!";

                // Example QR‑specific settings
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

                // Save the barcode image
                generator.Save(originalImage, BarCodeImageFormat.Png);

                // Capture the relevant settings into a DTO for serialization
                var settings = new BarcodeSettings
                {
                    EncodeType = nameof(EncodeTypes.QR),
                    CodeText = generator.CodeText,
                    ErrorLevel = generator.Parameters.Barcode.QR.ErrorLevel.ToString(),
                    EncodeMode = generator.Parameters.Barcode.QR.EncodeMode.ToString()
                };

                // Serialize to JSON (pretty printed) and write to file
                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonFile, json);
            }

            // -----------------------------------------------------------------
            // 2. Read the JSON, deserialize it, recreate the barcode with the
            //    same settings, and save a second image.
            // -----------------------------------------------------------------
            if (!File.Exists(jsonFile))
            {
                Console.WriteLine($"Settings file not found: {jsonFile}");
                return;
            }

            // Load JSON content from file
            var jsonContent = File.ReadAllText(jsonFile);
            var deserializedSettings = JsonSerializer.Deserialize<BarcodeSettings>(jsonContent);
            if (deserializedSettings == null)
            {
                Console.WriteLine("Failed to deserialize barcode settings.");
                return;
            }

            // Resolve the symbology name to a BaseEncodeType using reflection
            var fieldInfo = typeof(EncodeTypes).GetField(deserializedSettings.EncodeType);
            if (fieldInfo == null)
            {
                Console.WriteLine($"Unknown symbology: {deserializedSettings.EncodeType}");
                return;
            }

            var encodeType = (BaseEncodeType)fieldInfo.GetValue(null);

            // Recreate the barcode generator with the deserialized settings
            using (var generator = new BarcodeGenerator(encodeType, deserializedSettings.CodeText))
            {
                // Apply QR‑specific settings if they can be parsed from the JSON values
                if (Enum.TryParse<QRErrorLevel>(deserializedSettings.ErrorLevel, out var errLevel))
                {
                    generator.Parameters.Barcode.QR.ErrorLevel = errLevel;
                }

                if (Enum.TryParse<QREncodeMode>(deserializedSettings.EncodeMode, out var encMode))
                {
                    generator.Parameters.Barcode.QR.EncodeMode = encMode;
                }

                // Save the restored barcode image
                generator.Save(restoredImage, BarCodeImageFormat.Png);
            }

            // Indicate successful completion (no interactive wait)
            Console.WriteLine("Barcode generation and JSON round‑trip completed.");
        }
    }
}