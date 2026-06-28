using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code, persisting its settings to JSON,
/// and recreating the QR code from those settings using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Simple DTO for serializing QR barcode settings.
    /// </summary>
    private class QrSettings
    {
        public string CodeText { get; set; }
        public QRErrorLevel? ErrorLevel { get; set; }
        public float? XDimension { get; set; }
    }

    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, saves its configuration, and recreates the QR code from the saved configuration.
    /// </summary>
    static void Main()
    {
        const string qrImagePath = "qr.png";
        const string jsonPath = "qr_settings.json";
        const string recreatedImagePath = "qr_from_json.png";

        // ------------------------------------------------------------
        // Step 1: Generate a QR code with custom settings and save it.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to encode.
            generator.CodeText = "https://example.com";

            // Configure high error correction level.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set the module size (XDimension) to 3 points.
            generator.Parameters.Barcode.XDimension.Point = 3f;

            // Save the generated QR code image.
            generator.Save(qrImagePath);
        }

        // ------------------------------------------------------------
        // Step 2: Serialize the used settings to a JSON file.
        // ------------------------------------------------------------
        var settingsToSave = new QrSettings
        {
            CodeText = "https://example.com",
            ErrorLevel = QRErrorLevel.LevelH,
            XDimension = 3f
        };

        // Convert the settings object to a formatted JSON string.
        string json = JsonSerializer.Serialize(
            settingsToSave,
            new JsonSerializerOptions { WriteIndented = true });

        // Write the JSON string to disk.
        File.WriteAllText(jsonPath, json);

        // ------------------------------------------------------------
        // Step 3: Load settings from JSON and recreate the QR code.
        // ------------------------------------------------------------
        if (!File.Exists(jsonPath))
        {
            Console.WriteLine($"Settings file not found: {jsonPath}");
            return;
        }

        // Read the JSON content from the file.
        string loadedJson = File.ReadAllText(jsonPath);
        QrSettings loadedSettings;

        try
        {
            // Deserialize JSON back into a QrSettings object.
            loadedSettings = JsonSerializer.Deserialize<QrSettings>(loadedJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to deserialize JSON settings: {ex.Message}");
            return;
        }

        // Validate that essential data is present.
        if (loadedSettings == null || string.IsNullOrEmpty(loadedSettings.CodeText))
        {
            Console.WriteLine("Invalid settings data.");
            return;
        }

        // Recreate the QR code using the deserialized settings.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = loadedSettings.CodeText;

            if (loadedSettings.ErrorLevel.HasValue)
                generator.Parameters.Barcode.QR.ErrorLevel = loadedSettings.ErrorLevel.Value;

            if (loadedSettings.XDimension.HasValue)
                generator.Parameters.Barcode.XDimension.Point = loadedSettings.XDimension.Value;

            // Save the recreated QR code image.
            generator.Save(recreatedImagePath);
        }

        // ------------------------------------------------------------
        // Output summary of operations.
        // ------------------------------------------------------------
        Console.WriteLine($"Original QR saved to: {qrImagePath}");
        Console.WriteLine($"Settings saved to JSON: {jsonPath}");
        Console.WriteLine($"Recreated QR saved to: {recreatedImagePath}");
    }
}