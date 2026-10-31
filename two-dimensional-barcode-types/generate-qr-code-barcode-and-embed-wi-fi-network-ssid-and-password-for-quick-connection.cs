using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Wi‑Fi credentials
        string ssid = "MyNetwork";
        string password = "MyPassword";
        string authType = "WPA";

        // Wi‑Fi QR code format
        string wifiCodeText = $"WIFI:T:{authType};S:{ssid};P:{password};;";

        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
        string outputPath = Path.Combine(outputDir, "wifi_qr.png");

        // Generate QR code
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, wifiCodeText))
        {
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Wi‑Fi QR code saved to: {outputPath}");
    }
}