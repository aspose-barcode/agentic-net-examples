// Title: Serialize and Restore Barcode Captions via JSON
// Description: Demonstrates how to serialize barcode text and caption settings to a JSON file and then reload them to recreate identical barcode images.
// Category-Description: This example belongs to the Aspose.BarCode serialization and configuration category, illustrating the use of BarcodeGenerator, BarcodeParameters, and caption properties. Developers often need to persist barcode appearance settings for later reuse or for dynamic generation scenarios. The snippet shows typical steps: configure captions, serialize settings, deserialize, and regenerate the barcode.
// Prompt: Serialize barcode text and caption settings to JSON, then reload them to recreate identical barcode appearances.
// Tags: barcode, serialization, json, caption, code128, aspnet, aspose.barcode, generation

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeJsonDemo
{
    /// <summary>
    /// Holds barcode and caption settings for JSON serialization.
    /// </summary>
    public class BarcodeSettings
    {
        public string CodeText { get; set; }
        public string CaptionAboveText { get; set; }
        public string CaptionBelowText { get; set; }
        public string CaptionAboveFontFamily { get; set; }
        public float CaptionAboveFontSize { get; set; }
        public string CaptionBelowFontFamily { get; set; }
        public float CaptionBelowFontSize { get; set; }
        public string CaptionAboveAlignment { get; set; }
        public string CaptionBelowAlignment { get; set; }
        public bool CaptionAboveVisible { get; set; }
        public bool CaptionBelowVisible { get; set; }
    }

    class Program
    {
        /// <summary>
        /// Entry point. Creates a barcode with captions, serializes its settings to JSON,
        /// then deserializes the settings to recreate an identical barcode image.
        /// </summary>
        static void Main()
        {
            const string originalImagePath = "barcode_original.png";
            const string restoredImagePath = "barcode_restored.png";
            const string settingsPath = "barcode_settings.json";

            // -------------------------------------------------
            // 1. Create a barcode, configure captions, and save image
            // -------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Configure caption above the barcode
                generator.Parameters.CaptionAbove.Text = "Above Caption";
                generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
                generator.Parameters.CaptionAbove.Font.Size.Point = 12f;
                generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
                generator.Parameters.CaptionAbove.Visible = true;

                // Configure caption below the barcode
                generator.Parameters.CaptionBelow.Text = "Below Caption";
                generator.Parameters.CaptionBelow.Font.FamilyName = "Times New Roman";
                generator.Parameters.CaptionBelow.Font.Size.Point = 10f;
                generator.Parameters.CaptionBelow.Alignment = TextAlignment.Right;
                generator.Parameters.CaptionBelow.Visible = true;

                // Save the original barcode image
                generator.Save(originalImagePath);

                // -------------------------------------------------
                // 2. Extract settings into a serializable object
                // -------------------------------------------------
                var settings = new BarcodeSettings
                {
                    CodeText = generator.CodeText,
                    CaptionAboveText = generator.Parameters.CaptionAbove.Text,
                    CaptionBelowText = generator.Parameters.CaptionBelow.Text,
                    CaptionAboveFontFamily = generator.Parameters.CaptionAbove.Font.FamilyName,
                    CaptionAboveFontSize = generator.Parameters.CaptionAbove.Font.Size.Point,
                    CaptionBelowFontFamily = generator.Parameters.CaptionBelow.Font.FamilyName,
                    CaptionBelowFontSize = generator.Parameters.CaptionBelow.Font.Size.Point,
                    CaptionAboveAlignment = generator.Parameters.CaptionAbove.Alignment.ToString(),
                    CaptionBelowAlignment = generator.Parameters.CaptionBelow.Alignment.ToString(),
                    CaptionAboveVisible = generator.Parameters.CaptionAbove.Visible,
                    CaptionBelowVisible = generator.Parameters.CaptionBelow.Visible
                };

                // -------------------------------------------------
                // 3. Serialize settings to JSON file
                // -------------------------------------------------
                using (var writeStream = new FileStream(settingsPath, FileMode.Create, FileAccess.Write))
                {
                    JsonSerializer.Serialize(writeStream, settings, new JsonSerializerOptions { WriteIndented = true });
                }
            }

            // -------------------------------------------------
            // 4. Load settings from JSON and recreate barcode
            // -------------------------------------------------
            BarcodeSettings loadedSettings;
            using (var readStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Read))
            {
                loadedSettings = JsonSerializer.Deserialize<BarcodeSettings>(readStream);
            }

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, loadedSettings.CodeText))
            {
                // Apply caption above settings
                generator.Parameters.CaptionAbove.Text = loadedSettings.CaptionAboveText;
                generator.Parameters.CaptionAbove.Font.FamilyName = loadedSettings.CaptionAboveFontFamily;
                generator.Parameters.CaptionAbove.Font.Size.Point = loadedSettings.CaptionAboveFontSize;
                generator.Parameters.CaptionAbove.Alignment = (TextAlignment)Enum.Parse(typeof(TextAlignment), loadedSettings.CaptionAboveAlignment);
                generator.Parameters.CaptionAbove.Visible = loadedSettings.CaptionAboveVisible;

                // Apply caption below settings
                generator.Parameters.CaptionBelow.Text = loadedSettings.CaptionBelowText;
                generator.Parameters.CaptionBelow.Font.FamilyName = loadedSettings.CaptionBelowFontFamily;
                generator.Parameters.CaptionBelow.Font.Size.Point = loadedSettings.CaptionBelowFontSize;
                generator.Parameters.CaptionBelow.Alignment = (TextAlignment)Enum.Parse(typeof(TextAlignment), loadedSettings.CaptionBelowAlignment);
                generator.Parameters.CaptionBelow.Visible = loadedSettings.CaptionBelowVisible;

                // Save the restored barcode image
                generator.Save(restoredImagePath);
            }

            // Output file locations for verification
            Console.WriteLine($"Original barcode saved to: {originalImagePath}");
            Console.WriteLine($"Settings serialized to: {settingsPath}");
            Console.WriteLine($"Restored barcode saved to: {restoredImagePath}");
        }
    }
}