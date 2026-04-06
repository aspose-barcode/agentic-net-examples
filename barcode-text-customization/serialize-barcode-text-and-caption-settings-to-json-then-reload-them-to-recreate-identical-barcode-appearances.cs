using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeJsonDemo
{
    // Simple DTO to hold barcode text and caption settings
    public class BarcodeSettings
    {
        public string CodeText { get; set; }
        public string CaptionAboveText { get; set; }
        public bool CaptionAboveVisible { get; set; }
        public float CaptionAboveFontSize { get; set; }
        public string CaptionBelowText { get; set; }
        public bool CaptionBelowVisible { get; set; }
        public float CaptionBelowFontSize { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string originalImage = "original.png";
            const string recreatedImage = "recreated.png";
            const string jsonFile = "settings.json";

            // 1. Create a barcode, configure captions and save the image
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                // Caption above
                generator.Parameters.CaptionAbove.Text = "Above Caption";
                generator.Parameters.CaptionAbove.Visible = true;
                generator.Parameters.CaptionAbove.Font.Size.Point = 12f;

                // Caption below
                generator.Parameters.CaptionBelow.Text = "Below Caption";
                generator.Parameters.CaptionBelow.Visible = true;
                generator.Parameters.CaptionBelow.Font.Size.Point = 12f;

                // Save the original barcode image
                generator.Save(originalImage);
                
                // 2. Capture the relevant settings into a DTO
                var settings = new BarcodeSettings
                {
                    CodeText = generator.CodeText,
                    CaptionAboveText = generator.Parameters.CaptionAbove.Text,
                    CaptionAboveVisible = generator.Parameters.CaptionAbove.Visible,
                    CaptionAboveFontSize = generator.Parameters.CaptionAbove.Font.Size.Point,
                    CaptionBelowText = generator.Parameters.CaptionBelow.Text,
                    CaptionBelowVisible = generator.Parameters.CaptionBelow.Visible,
                    CaptionBelowFontSize = generator.Parameters.CaptionBelow.Font.Size.Point
                };

                // 3. Serialize settings to JSON
                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonFile, json);
            }

            // 4. Read JSON and recreate the barcode with identical appearance
            var jsonContent = File.ReadAllText(jsonFile);
            var loadedSettings = JsonSerializer.Deserialize<BarcodeSettings>(jsonContent);

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, loadedSettings.CodeText))
            {
                // Apply caption above settings
                generator.Parameters.CaptionAbove.Text = loadedSettings.CaptionAboveText;
                generator.Parameters.CaptionAbove.Visible = loadedSettings.CaptionAboveVisible;
                generator.Parameters.CaptionAbove.Font.Size.Point = loadedSettings.CaptionAboveFontSize;

                // Apply caption below settings
                generator.Parameters.CaptionBelow.Text = loadedSettings.CaptionBelowText;
                generator.Parameters.CaptionBelow.Visible = loadedSettings.CaptionBelowVisible;
                generator.Parameters.CaptionBelow.Font.Size.Point = loadedSettings.CaptionBelowFontSize;

                // Save the recreated barcode image
                generator.Save(recreatedImage);
            }
        }
    }
}