using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Load configuration from a simple key=value file.
        // Expected keys: DetectEncoding (true/false), ChecksumValidation (Default/On/Off)
        bool detectEncoding = true; // default
        ChecksumValidation checksumValidation = ChecksumValidation.Default; // default

        const string configPath = "barcodeSettings.config";
        if (File.Exists(configPath))
        {
            foreach (var line in File.ReadAllLines(configPath))
            {
                var trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#"))
                    continue; // skip empty lines and comments

                var parts = trimmed.Split('=', 2);
                if (parts.Length != 2)
                    continue;

                var key = parts[0].Trim();
                var value = parts[1].Trim();

                if (key.Equals("DetectEncoding", StringComparison.OrdinalIgnoreCase))
                {
                    if (bool.TryParse(value, out bool boolVal))
                        detectEncoding = boolVal;
                }
                else if (key.Equals("ChecksumValidation", StringComparison.OrdinalIgnoreCase))
                {
                    if (Enum.TryParse<ChecksumValidation>(value, true, out var enumVal))
                        checksumValidation = enumVal;
                }
            }
        }

        // Generate a QR code containing Unicode text.
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.SetCodeText("Слово", Encoding.UTF8);
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // First read with configuration values.
            ms.Position = 0;
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                // Apply settings from configuration.
                reader.BarcodeSettings.DetectEncoding = detectEncoding;
                reader.BarcodeSettings.ChecksumValidation = checksumValidation;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine("CodeText: " + result.CodeText);
                    Console.WriteLine("DetectEncoding: " + reader.BarcodeSettings.DetectEncoding);
                    Console.WriteLine("ChecksumValidation: " + reader.BarcodeSettings.ChecksumValidation);
                }
            }
        }
    }
}