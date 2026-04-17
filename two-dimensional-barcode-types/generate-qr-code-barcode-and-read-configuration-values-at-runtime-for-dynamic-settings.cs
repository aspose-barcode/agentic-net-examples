using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Load configuration from a simple key=value text file.
        var config = LoadConfig("config.txt");

        // Retrieve configuration values with fallbacks.
        string codeText = GetConfigValue(config, "CodeText", "https://example.com");
        float imageWidth = float.Parse(GetConfigValue(config, "ImageWidth", "300"));
        float imageHeight = float.Parse(GetConfigValue(config, "ImageHeight", "300"));
        string errorLevelStr = GetConfigValue(config, "ErrorLevel", "M");

        // Map error level string to QRErrorLevel enum.
        QRErrorLevel errorLevel = errorLevelStr.ToUpper() switch
        {
            "L" => QRErrorLevel.LevelL,
            "M" => QRErrorLevel.LevelM,
            "Q" => QRErrorLevel.LevelQ,
            "H" => QRErrorLevel.LevelH,
            _ => QRErrorLevel.LevelM
        };

        // Generate QR Code barcode.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set dynamic image size.
            generator.Parameters.ImageWidth.Point = imageWidth;
            generator.Parameters.ImageHeight.Point = imageHeight;

            // Set QR error correction level.
            generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;

            // Save the generated barcode image.
            string outputPath = "qr.png";
            generator.Save(outputPath);
            Console.WriteLine($"QR Code saved to {outputPath}");

            // Load the image for recognition.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            using (var reader = new BarCodeReader(bitmap, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Recognized Type: {result.CodeTypeName}");
                    Console.WriteLine($"Recognized Text: {result.CodeText}");
                }
            }
        }
    }

    // Reads a simple key=value configuration file.
    static Dictionary<string, string> LoadConfig(string path)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (!File.Exists(path))
        {
            Console.WriteLine($"Config file '{path}' not found. Using default settings.");
            return dict;
        }

        foreach (var line in File.ReadAllLines(path))
        {
            var trimmed = line.Trim();
            if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#"))
                continue;

            var parts = trimmed.Split(new[] { '=' }, 2);
            if (parts.Length == 2)
            {
                dict[parts[0].Trim()] = parts[1].Trim();
            }
        }
        return dict;
    }

    // Retrieves a value from the config dictionary or returns a default.
    static string GetConfigValue(Dictionary<string, string> config, string key, string defaultValue)
    {
        return config.TryGetValue(key, out var value) ? value : defaultValue;
    }
}