// Title: Generate QR Code, serialize settings to JSON, and recreate barcode from JSON
// Description: Demonstrates creating a QR Code with custom appearance, saving its configuration to a JSON file, and rebuilding an identical barcode by deserializing those settings.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It showcases the BarcodeGenerator class for QR Code creation, the use of Barcode parameters to customize appearance, and JSON serialization via System.Text.Json to persist settings. Developers often need to store barcode configurations for later reuse, batch processing, or dynamic generation scenarios.
// Prompt: Generate QR Code barcode and deserialize settings from JSON to recreate identical barcode.
// Tags: qr code, barcode generation, json serialization, aspose.barcode, settings deserialization, png output

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR Code, saves its generation settings to a JSON file,
/// and then recreates the same QR Code by deserializing those settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the original QR Code, serializes its settings,
    /// deserializes them, and creates a recreated QR Code with identical appearance.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory and file paths
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string originalPath = Path.Combine(outputDir, "qr_original.png");
        string recreatedPath = Path.Combine(outputDir, "qr_recreated.png");
        string settingsPath = Path.Combine(outputDir, "qrsettings.json");

        // --------------------------------------------------------------------
        // Generate the original QR Code with custom visual settings
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Parameters.Barcode.QR.Version = QRVersion.Version05;
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.LightGray;
            generator.Parameters.RotationAngle = 0f;
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Save the original QR Code image
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Capture the generation settings into a data transfer object (DTO)
        // --------------------------------------------------------------------
        var settingsDto = new QrSettingsDto
        {
            EncodeType = nameof(EncodeTypes.QR),
            CodeText = "https://example.com",
            XDimensionPixels = 4f,
            QRVersion = QRVersion.Version05.ToString(),
            QRErrorLevel = QRErrorLevel.LevelH.ToString(),
            BarColorArgb = Color.Black.ToArgb(),
            BackColorArgb = Color.LightGray.ToArgb(),
            RotationAngle = 0f,
            PaddingLeft = 5f,
            PaddingTop = 5f,
            PaddingRight = 5f,
            PaddingBottom = 5f
        };

        // --------------------------------------------------------------------
        // Serialize the DTO to a formatted JSON file
        // --------------------------------------------------------------------
        string json = JsonSerializer.Serialize(settingsDto, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(settingsPath, json);

        // --------------------------------------------------------------------
        // Read the JSON file and deserialize back to a DTO
        // --------------------------------------------------------------------
        string readJson = File.ReadAllText(settingsPath);
        var deserialized = JsonSerializer.Deserialize<QrSettingsDto>(readJson);
        if (deserialized == null)
        {
            Console.WriteLine("Failed to deserialize settings.");
            return;
        }

        // --------------------------------------------------------------------
        // Resolve the EncodeTypes enum value via reflection (stored as string)
        // --------------------------------------------------------------------
        var encodeField = typeof(EncodeTypes).GetField(deserialized.EncodeType);
        if (encodeField == null)
        {
            Console.WriteLine($"Unknown encode type: {deserialized.EncodeType}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)encodeField.GetValue(null);

        // --------------------------------------------------------------------
        // Recreate the QR Code using the deserialized settings
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(encodeType, deserialized.CodeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = deserialized.XDimensionPixels;

            if (Enum.TryParse<QRVersion>(deserialized.QRVersion, out var qrVersion))
                generator.Parameters.Barcode.QR.Version = qrVersion;

            if (Enum.TryParse<QRErrorLevel>(deserialized.QRErrorLevel, out var qrError))
                generator.Parameters.Barcode.QR.ErrorLevel = qrError;

            generator.Parameters.Barcode.BarColor = Color.FromArgb(deserialized.BarColorArgb);
            generator.Parameters.BackColor = Color.FromArgb(deserialized.BackColorArgb);
            generator.Parameters.RotationAngle = deserialized.RotationAngle;
            generator.Parameters.Barcode.Padding.Left.Point = deserialized.PaddingLeft;
            generator.Parameters.Barcode.Padding.Top.Point = deserialized.PaddingTop;
            generator.Parameters.Barcode.Padding.Right.Point = deserialized.PaddingRight;
            generator.Parameters.Barcode.Padding.Bottom.Point = deserialized.PaddingBottom;

            // Save the recreated QR Code image
            generator.Save(recreatedPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Output the locations of generated files
        // --------------------------------------------------------------------
        Console.WriteLine($"Original QR saved to: {originalPath}");
        Console.WriteLine($"Recreated QR saved to: {recreatedPath}");
        Console.WriteLine($"Settings JSON saved to: {settingsPath}");
    }

    /// <summary>
    /// Data transfer object used to serialize and deserialize QR Code generation settings.
    /// </summary>
    private class QrSettingsDto
    {
        public string EncodeType { get; set; }
        public string CodeText { get; set; }
        public float XDimensionPixels { get; set; }
        public string QRVersion { get; set; }
        public string QRErrorLevel { get; set; }
        public int BarColorArgb { get; set; }
        public int BackColorArgb { get; set; }
        public float RotationAngle { get; set; }
        public float PaddingLeft { get; set; }
        public float PaddingTop { get; set; }
        public float PaddingRight { get; set; }
        public float PaddingBottom { get; set; }
    }
}