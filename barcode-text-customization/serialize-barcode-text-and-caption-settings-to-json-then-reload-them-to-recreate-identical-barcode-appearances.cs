// Title: Serialize and Reload Barcode Text and Caption Settings via JSON
// Description: Demonstrates how to capture barcode text and caption properties, serialize them to a JSON file, and then recreate the same barcode appearance by deserializing the settings.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It shows how to use BarcodeGenerator, its Parameters (including CaptionAbove and CaptionBelow), and .NET System.Text.Json to persist barcode configuration. Typical use cases include saving barcode layouts for later reuse, sharing settings across services, or version‑controlling barcode designs. Developers often need to export and import barcode settings without re‑creating them manually.
// Prompt: Serialize barcode text and caption settings to JSON, then reload them to recreate identical barcode appearances.
// Tags: barcode, serialization, json, caption, code128, aspose.barcode, generation

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeJsonDemo
{
    /// <summary>
    /// Data transfer object that represents caption visual settings.
    /// </summary>
    public class CaptionSettings
    {
        public string Text { get; set; }
        public string FontFamily { get; set; }
        public float FontSize { get; set; }
        public string Alignment { get; set; } // TextAlignment enum name
        public int TextColorArgb { get; set; }
    }

    /// <summary>
    /// Data transfer object that aggregates barcode text and its caption settings.
    /// </summary>
    public class BarcodeSettings
    {
        public string CodeText { get; set; }
        public CaptionSettings CaptionAbove { get; set; }
        public CaptionSettings CaptionBelow { get; set; }
    }

    /// <summary>
    /// Demonstrates serialization of barcode settings to JSON and recreation of the barcode from those settings.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates a barcode, saves its configuration to JSON, then reloads the JSON to produce an identical barcode.
        /// </summary>
        static void Main()
        {
            // File paths for the generated images and the JSON settings file
            const string originalImagePath = "barcodeOriginal.png";
            const string reloadedImagePath = "barcodeReloaded.png";
            const string jsonPath = "barcodeSettings.json";

            // -----------------------------------------------------------------
            // Create original barcode with text and caption settings
            // -----------------------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Configure caption displayed above the barcode
                generator.Parameters.CaptionAbove.Text = "Above Caption";
                generator.Parameters.CaptionAbove.Font.FamilyName = "Helvetica";
                generator.Parameters.CaptionAbove.Font.Size.Point = 12f;
                generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
                generator.Parameters.CaptionAbove.TextColor = Aspose.Drawing.Color.Blue;

                // Configure caption displayed below the barcode
                generator.Parameters.CaptionBelow.Text = "Below Caption";
                generator.Parameters.CaptionBelow.Font.FamilyName = "Helvetica";
                generator.Parameters.CaptionBelow.Font.Size.Point = 10f;
                generator.Parameters.CaptionBelow.Alignment = TextAlignment.Right;
                generator.Parameters.CaptionBelow.TextColor = Aspose.Drawing.Color.Green;

                // Save the original barcode image to disk
                generator.Save(originalImagePath);

                // Capture the current barcode configuration into DTO objects
                var settings = new BarcodeSettings
                {
                    CodeText = generator.CodeText,
                    CaptionAbove = new CaptionSettings
                    {
                        Text = generator.Parameters.CaptionAbove.Text,
                        FontFamily = generator.Parameters.CaptionAbove.Font.FamilyName,
                        FontSize = generator.Parameters.CaptionAbove.Font.Size.Point,
                        Alignment = generator.Parameters.CaptionAbove.Alignment.ToString(),
                        TextColorArgb = generator.Parameters.CaptionAbove.TextColor.ToArgb()
                    },
                    CaptionBelow = new CaptionSettings
                    {
                        Text = generator.Parameters.CaptionBelow.Text,
                        FontFamily = generator.Parameters.CaptionBelow.Font.FamilyName,
                        FontSize = generator.Parameters.CaptionBelow.Font.Size.Point,
                        Alignment = generator.Parameters.CaptionBelow.Alignment.ToString(),
                        TextColorArgb = generator.Parameters.CaptionBelow.TextColor.ToArgb()
                    }
                };

                // Serialize the DTO to a formatted JSON string and write it to a file
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(settings, jsonOptions);
                File.WriteAllText(jsonPath, json);
            }

            // -----------------------------------------------------------------
            // Reload settings from JSON and recreate identical barcode
            // -----------------------------------------------------------------
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"JSON file not found: {jsonPath}");
                return;
            }

            // Read the JSON content and deserialize it back into DTO objects
            string jsonContent = File.ReadAllText(jsonPath);
            var loadedSettings = JsonSerializer.Deserialize<BarcodeSettings>(jsonContent);
            if (loadedSettings == null)
            {
                Console.WriteLine("Failed to deserialize barcode settings.");
                return;
            }

            // Use the deserialized settings to generate a new barcode with the same appearance
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, loadedSettings.CodeText))
            {
                // Apply the "above" caption if it exists
                if (loadedSettings.CaptionAbove != null)
                {
                    generator.Parameters.CaptionAbove.Text = loadedSettings.CaptionAbove.Text;
                    generator.Parameters.CaptionAbove.Font.FamilyName = loadedSettings.CaptionAbove.FontFamily;
                    generator.Parameters.CaptionAbove.Font.Size.Point = loadedSettings.CaptionAbove.FontSize;
                    if (Enum.TryParse<TextAlignment>(loadedSettings.CaptionAbove.Alignment, out var alignAbove))
                    {
                        generator.Parameters.CaptionAbove.Alignment = alignAbove;
                    }
                    generator.Parameters.CaptionAbove.TextColor = Aspose.Drawing.Color.FromArgb(loadedSettings.CaptionAbove.TextColorArgb);
                }

                // Apply the "below" caption if it exists
                if (loadedSettings.CaptionBelow != null)
                {
                    generator.Parameters.CaptionBelow.Text = loadedSettings.CaptionBelow.Text;
                    generator.Parameters.CaptionBelow.Font.FamilyName = loadedSettings.CaptionBelow.FontFamily;
                    generator.Parameters.CaptionBelow.Font.Size.Point = loadedSettings.CaptionBelow.FontSize;
                    if (Enum.TryParse<TextAlignment>(loadedSettings.CaptionBelow.Alignment, out var alignBelow))
                    {
                        generator.Parameters.CaptionBelow.Alignment = alignBelow;
                    }
                    generator.Parameters.CaptionBelow.TextColor = Aspose.Drawing.Color.FromArgb(loadedSettings.CaptionBelow.TextColorArgb);
                }

                // Save the regenerated barcode image to disk
                generator.Save(reloadedImagePath);
            }

            // Inform the user where the output files are located
            Console.WriteLine($"Original barcode saved to: {originalImagePath}");
            Console.WriteLine($"Reloaded barcode saved to: {reloadedImagePath}");
            Console.WriteLine($"Settings JSON saved to: {jsonPath}");
        }
    }
}