using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeJsonDemo
{
    // DTO for serializing barcode settings
    public class BarcodeSettingsDto
    {
        public string CodeText { get; set; }
        public string CaptionAboveText { get; set; }
        public bool CaptionAboveVisible { get; set; }
        public TextAlignment CaptionAboveAlignment { get; set; }
    }

    class Program
    {
        static void Main()
        {
            // Paths for output files
            string originalImagePath = "original.png";
            string restoredImagePath = "restored.png";
            string jsonPath = "barcodeSettings.json";

            // Create a barcode generator and configure text and caption
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "123ABC";
                generator.Parameters.CaptionAbove.Text = "Sample Caption";
                generator.Parameters.CaptionAbove.Visible = true;
                generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

                // Save the original barcode image
                generator.Save(originalImagePath);
                
                // Prepare DTO with the settings we want to serialize
                var dto = new BarcodeSettingsDto
                {
                    CodeText = generator.CodeText,
                    CaptionAboveText = generator.Parameters.CaptionAbove.Text,
                    CaptionAboveVisible = generator.Parameters.CaptionAbove.Visible,
                    CaptionAboveAlignment = generator.Parameters.CaptionAbove.Alignment
                };

                // Serialize settings to JSON
                string json = JsonSerializer.Serialize(dto, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonPath, json);
            }

            // Load settings from JSON and recreate the barcode
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine("JSON file not found.");
                return;
            }

            string jsonContent = File.ReadAllText(jsonPath);
            var restoredDto = JsonSerializer.Deserialize<BarcodeSettingsDto>(jsonContent);
            if (restoredDto == null)
            {
                Console.WriteLine("Failed to deserialize JSON.");
                return;
            }

            // Create a new generator using the restored settings
            using (var restoredGenerator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                restoredGenerator.CodeText = restoredDto.CodeText;
                restoredGenerator.Parameters.CaptionAbove.Text = restoredDto.CaptionAboveText;
                restoredGenerator.Parameters.CaptionAbove.Visible = restoredDto.CaptionAboveVisible;
                restoredGenerator.Parameters.CaptionAbove.Alignment = restoredDto.CaptionAboveAlignment;

                // Save the restored barcode image
                restoredGenerator.Save(restoredImagePath);
            }

            Console.WriteLine("Original and restored barcodes have been generated.");
        }
    }
}