// Title: Generate QR Code and Export Settings to JSON
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, saving it as a PNG image, and serializing the generation parameters to a JSON file for reproducibility.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce a QR Code, then captures key generation settings (code text, symbology, dimensions, error correction level) and writes them to JSON using System.Text.Json. Developers working with barcode creation and needing to persist or share configuration settings will find this pattern useful for automated testing, configuration management, and repeatable barcode generation.
// Prompt: Generate QR Code barcode and serialize generation settings to JSON for reproducibility.
// Tags: qr code, barcode generation, json serialization, aspose.barcode, csharp

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR Code generation with Aspose.BarCode and serialization of its settings to JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Simple DTO for persisting barcode generation settings.
    /// </summary>
    class BarcodeSettings
    {
        public string CodeText { get; set; }
        public string EncodeType { get; set; }
        public float XDimensionPixels { get; set; }
        public string QRErrorLevel { get; set; }
    }

    /// <summary>
    /// Entry point. Generates a QR Code, saves the image, and writes generation parameters to a JSON file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the PNG image and JSON settings
        string imagePath = Path.Combine(outputDir, "qr.png");
        string jsonPath = Path.Combine(outputDir, "settings.json");

        // Initialize the barcode generator for QR Code with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, Aspose!"))
        {
            // Configure QR-specific parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;               // Set module size
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH; // High error correction

            // Save the generated QR Code as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);

            // Capture the relevant settings for later reproducibility
            var settings = new BarcodeSettings
            {
                CodeText = generator.CodeText,
                EncodeType = nameof(EncodeTypes.QR),
                XDimensionPixels = generator.Parameters.Barcode.XDimension.Pixels,
                QRErrorLevel = generator.Parameters.Barcode.QR.ErrorLevel.ToString()
            };

            // Serialize settings to formatted JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(settings, jsonOptions);
            File.WriteAllText(jsonPath, json);
        }

        // Inform the user where the files have been saved
        Console.WriteLine($"QR code image saved to: {imagePath}");
        Console.WriteLine($"Generation settings saved to: {jsonPath}");
    }
}