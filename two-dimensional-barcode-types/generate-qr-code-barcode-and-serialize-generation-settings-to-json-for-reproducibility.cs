// Title: Generate QR Code and Export Settings to JSON
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, saving it as a PNG image, and serializing the generation parameters to a JSON file for reproducibility.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration category. It showcases the use of BarcodeGenerator, EncodeTypes, QRErrorLevel, and BarCodeImageFormat to produce a QR Code, then captures the active settings into a plain‑old‑CLR object and serializes it with System.Text.Json. Developers working with barcode creation, custom styling, or needing to persist generation settings for later reuse will find this pattern useful.
// Prompt: Generate QR Code barcode and serialize generation settings to JSON for reproducibility.
// Tags: qr code, barcode generation, json serialization, aspose.barcode, csharp

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR Code barcode, saves it as an image,
/// and writes the generation settings to a JSON file for later reuse.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare the output directory in the system temporary folder.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Define file paths for the generated image and the JSON settings file.
        string imagePath = Path.Combine(outputDir, "qr.png");
        string jsonPath = Path.Combine(outputDir, "qr_settings.json");

        // --------------------------------------------------------------------
        // Generate a QR Code barcode using Aspose.BarCode.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Configure barcode appearance and quality.
            generator.Parameters.Barcode.XDimension.Point = 2f;                     // Size of the smallest module.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;      // Error correction level.
            generator.Parameters.Resolution = 300f;                                 // Image resolution (dpi).
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;    // Foreground color.
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;           // Background color.

            // Save the generated QR Code as a PNG image.
            generator.Save(imagePath, BarCodeImageFormat.Png);

            // ----------------------------------------------------------------
            // Capture the current generation settings for serialization.
            // ----------------------------------------------------------------
            var settings = new BarcodeSettings
            {
                EncodeType = EncodeTypes.QR.ToString(),
                CodeText = generator.CodeText,
                XDimension = generator.Parameters.Barcode.XDimension.Point,
                ErrorLevel = generator.Parameters.Barcode.QR.ErrorLevel.ToString(),
                Resolution = generator.Parameters.Resolution,
                BarColor = generator.Parameters.Barcode.BarColor.ToString(),
                BackColor = generator.Parameters.BackColor.ToString()
            };

            // Serialize the settings object to a formatted JSON string.
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(settings, jsonOptions);
            File.WriteAllText(jsonPath, json);
        }

        // Inform the user where the files have been saved.
        Console.WriteLine($"QR Code image saved to: {imagePath}");
        Console.WriteLine($"Generation settings saved to: {jsonPath}");
    }

    /// <summary>
    /// Simple POCO class that holds the barcode generation parameters
    /// needed to recreate the same QR Code later.
    /// </summary>
    class BarcodeSettings
    {
        public string EncodeType { get; set; }
        public string CodeText { get; set; }
        public float XDimension { get; set; }
        public string ErrorLevel { get; set; }
        public float Resolution { get; set; }
        public string BarColor { get; set; }
        public string BackColor { get; set; }
    }
}