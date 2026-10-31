using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    class BarcodeConfig
    {
        public string CodeText { get; set; }
        public string ErrorLevel { get; set; }
        public float XDimension { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public string OutputPath { get; set; }
    }

    static void Main()
    {
        // Prepare a temporary folder and appsettings file
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string configPath = Path.Combine(tempFolder, "appsettings.json");

        string jsonContent = @"{
  ""CodeText"": ""https://example.com"",
  ""ErrorLevel"": ""LevelH"",
  ""XDimension"": 2.5,
  ""ForegroundColor"": ""Black"",
  ""BackgroundColor"": ""White"",
  ""OutputPath"": ""qr_code.png""
}";
        File.WriteAllText(configPath, jsonContent, Encoding.UTF8);

        // Read configuration
        if (!File.Exists(configPath))
        {
            Console.WriteLine("Configuration file not found.");
            return;
        }

        BarcodeConfig config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<BarcodeConfig>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse configuration: {ex.Message}");
            return;
        }

        if (config == null ||
            string.IsNullOrWhiteSpace(config.CodeText) ||
            string.IsNullOrWhiteSpace(config.ErrorLevel) ||
            string.IsNullOrWhiteSpace(config.ForegroundColor) ||
            string.IsNullOrWhiteSpace(config.BackgroundColor) ||
            string.IsNullOrWhiteSpace(config.OutputPath))
        {
            Console.WriteLine("Invalid configuration values.");
            return;
        }

        // Resolve full output path
        string outputPath = Path.Combine(tempFolder, config.OutputPath);

        // Create and configure the QR Code generator
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, config.CodeText))
        {
            // Set error correction level
            if (Enum.TryParse<QRErrorLevel>(config.ErrorLevel, out QRErrorLevel level))
            {
                generator.Parameters.Barcode.QR.ErrorLevel = level;
            }
            else
            {
                Console.WriteLine($"Unknown error level: {config.ErrorLevel}");
                return;
            }

            // Set module size (XDimension)
            generator.Parameters.Barcode.XDimension.Point = config.XDimension;

            // Set colors
            try
            {
                generator.Parameters.Barcode.BarColor = Color.FromName(config.ForegroundColor);
                generator.Parameters.BackColor = Color.FromName(config.BackgroundColor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Color parsing error: {ex.Message}");
                return;
            }

            // Save the barcode image
            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"QR Code generated at: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save barcode: {ex.Message}");
            }
        }

        // Clean up temporary config file (optional)
        try
        {
            File.Delete(configPath);
        }
        catch { }
    }
}