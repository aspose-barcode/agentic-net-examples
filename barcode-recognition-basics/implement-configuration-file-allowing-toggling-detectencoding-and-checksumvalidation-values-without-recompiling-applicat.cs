using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeConfigDemo
{
    // Configuration model matching the JSON file
    public class Config
    {
        public bool DetectEncoding { get; set; } = true;
        public string ChecksumValidation { get; set; } = "Default";
    }

    class Program
    {
        static void Main()
        {
            // Load configuration from appsettings.json (if present)
            Config config = LoadConfig("appsettings.json");

            // Generate a QR code with Unicode text and save to a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
                {
                    generator.SetCodeText("Слово", Encoding.UTF8);
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Prepare the stream for reading
                ms.Position = 0;

                // Read the barcode using settings from the configuration
                using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.QR))
                {
                    // Apply DetectEncoding flag
                    reader.BarcodeSettings.DetectEncoding = config.DetectEncoding;

                    // Apply ChecksumValidation setting
                    if (Enum.TryParse<ChecksumValidation>(config.ChecksumValidation, true, out var checksumVal))
                    {
                        reader.BarcodeSettings.ChecksumValidation = checksumVal;
                    }
                    else
                    {
                        // Fallback to default if parsing fails
                        reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;
                    }

                    // Perform recognition and output results
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                        Console.WriteLine("BarCode CodeText: " + result.CodeText);
                        Console.WriteLine("DetectEncoding: " + reader.BarcodeSettings.DetectEncoding);
                        Console.WriteLine("ChecksumValidation: " + reader.BarcodeSettings.ChecksumValidation);
                    }
                }
            }
        }

        // Helper method to load configuration with graceful fallback
        private static Config LoadConfig(string path)
        {
            if (!File.Exists(path))
            {
                // Return default configuration if file is missing
                return new Config();
            }

            try
            {
                string json = File.ReadAllText(path);
                Config? cfg = JsonSerializer.Deserialize<Config>(json);
                return cfg ?? new Config();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load configuration: " + ex.Message);
                // Return defaults on error
                return new Config();
            }
        }
    }
}