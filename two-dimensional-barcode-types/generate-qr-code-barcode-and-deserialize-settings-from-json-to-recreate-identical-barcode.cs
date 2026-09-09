// Title: Generate QR Code and Recreate It from JSON Settings
// Description: Demonstrates creating a QR Code barcode, persisting its generation parameters to a JSON file, and rebuilding an identical QR Code by deserializing those settings.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It showcases the BarcodeGenerator class, QR-specific parameters (QRVersion, QRErrorLevel), and the use of System.Text.Json for persisting settings. Developers often need to store barcode configurations for later reuse, batch processing, or audit purposes; this snippet provides a clear pattern for saving and restoring barcode settings.
// Prompt: Generate QR Code barcode and deserialize settings from JSON to recreate identical barcode.
// Tags: qr code, barcode generation, json serialization, aspose.barcode, csharp

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code, saving its settings to JSON, and recreating the same QR Code from those settings.
/// </summary>
class Program
{
    /// <summary>
    /// Simple DTO for persisting QR Code generation parameters.
    /// </summary>
    class QrSettings
    {
        public string CodeText { get; set; }
        public float XDimensionPixels { get; set; }
        public string QRVersion { get; set; }
        public string ErrorLevel { get; set; }
        public int BarColorArgb { get; set; }
        public int BackColorArgb { get; set; }
    }

    /// <summary>
    /// Entry point. Generates a QR Code, serializes its settings, deserializes them, and generates an identical QR Code.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all output files.
        string workFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the original QR, the recreated QR, and the JSON settings.
        string qrPath1 = Path.Combine(workFolder, "qr1.png");
        string qrPath2 = Path.Combine(workFolder, "qr2.png");
        string jsonPath = Path.Combine(workFolder, "settings.json");

        // ---------- Generate first QR code ----------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure QR-specific parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.Version = QRVersion.Version05;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated QR code image.
            generator.Save(qrPath1, BarCodeImageFormat.Png);

            // Capture the current settings into a DTO.
            var settings = new QrSettings
            {
                CodeText = generator.CodeText,
                XDimensionPixels = generator.Parameters.Barcode.XDimension.Pixels,
                QRVersion = generator.Parameters.Barcode.QR.Version.ToString(),
                ErrorLevel = generator.Parameters.Barcode.QR.ErrorLevel.ToString(),
                BarColorArgb = generator.Parameters.Barcode.BarColor.ToArgb(),
                BackColorArgb = generator.Parameters.BackColor.ToArgb()
            };

            // Serialize the settings to a formatted JSON file.
            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonPath, json);
        }

        // ---------- Deserialize settings and generate identical QR code ----------
        if (!File.Exists(jsonPath))
        {
            Console.WriteLine("Settings JSON not found.");
            return;
        }

        // Read and deserialize the JSON settings.
        string jsonContent = File.ReadAllText(jsonPath);
        QrSettings deserialized = JsonSerializer.Deserialize<QrSettings>(jsonContent);
        if (deserialized == null)
        {
            Console.WriteLine("Failed to deserialize settings.");
            return;
        }

        // Recreate the QR code using the deserialized parameters.
        using (var generator2 = new BarcodeGenerator(EncodeTypes.QR, deserialized.CodeText))
        {
            generator2.Parameters.Barcode.XDimension.Pixels = deserialized.XDimensionPixels;

            // Convert string representations back to enum values.
            generator2.Parameters.Barcode.QR.Version = (QRVersion)Enum.Parse(typeof(QRVersion), deserialized.QRVersion);
            generator2.Parameters.Barcode.QR.ErrorLevel = (QRErrorLevel)Enum.Parse(typeof(QRErrorLevel), deserialized.ErrorLevel);

            // Restore colors from ARGB values.
            generator2.Parameters.Barcode.BarColor = Color.FromArgb(deserialized.BarColorArgb);
            generator2.Parameters.BackColor = Color.FromArgb(deserialized.BackColorArgb);

            // Save the recreated QR code image.
            generator2.Save(qrPath2, BarCodeImageFormat.Png);
        }

        // Output the locations of generated files.
        Console.WriteLine($"First QR code saved to: {qrPath1}");
        Console.WriteLine($"Settings JSON saved to: {jsonPath}");
        Console.WriteLine($"Recreated QR code saved to: {qrPath2}");
    }
}