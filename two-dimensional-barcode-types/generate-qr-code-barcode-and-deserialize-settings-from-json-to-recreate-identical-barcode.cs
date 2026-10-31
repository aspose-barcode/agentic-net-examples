using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        string originalPath = Path.Combine(outputDir, "qr_original.png");
        string restoredPath = Path.Combine(outputDir, "qr_restored.png");
        string settingsPath = Path.Combine(outputDir, "qr_settings.json");

        // Generate original QR code with specific settings
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Text"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.Version = QRVersion.Version05;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            generator.Save(originalPath, BarCodeImageFormat.Png);

            // Capture settings
            var settings = new QrSettings
            {
                CodeText = generator.CodeText,
                XDimensionPixels = generator.Parameters.Barcode.XDimension.Pixels,
                QRVersion = generator.Parameters.Barcode.QR.Version.ToString(),
                QRErrorLevel = generator.Parameters.Barcode.QR.ErrorLevel.ToString(),
                BarColor = "Black",
                BackColor = "White"
            };

            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(settingsPath, json);
        }

        // Read settings from JSON and recreate barcode
        string jsonContent = File.ReadAllText(settingsPath);
        var restoredSettings = JsonSerializer.Deserialize<QrSettings>(jsonContent);

        using (var generator = new BarcodeGenerator(EncodeTypes.QR, restoredSettings.CodeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = restoredSettings.XDimensionPixels;
            generator.Parameters.Barcode.QR.Version = Enum.Parse<QRVersion>(restoredSettings.QRVersion);
            generator.Parameters.Barcode.QR.ErrorLevel = Enum.Parse<QRErrorLevel>(restoredSettings.QRErrorLevel);
            generator.Parameters.Barcode.BarColor = GetColor(restoredSettings.BarColor);
            generator.Parameters.BackColor = GetColor(restoredSettings.BackColor);

            generator.Save(restoredPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Original QR saved to: {originalPath}");
        Console.WriteLine($"Settings JSON saved to: {settingsPath}");
        Console.WriteLine($"Restored QR saved to: {restoredPath}");
    }

    static Color GetColor(string name)
    {
        return name switch
        {
            "Black" => Color.Black,
            "White" => Color.White,
            _ => Color.Black
        };
    }
}

class QrSettings
{
    public string CodeText { get; set; }
    public float XDimensionPixels { get; set; }
    public string QRVersion { get; set; }
    public string QRErrorLevel { get; set; }
    public string BarColor { get; set; }
    public string BackColor { get; set; }
}