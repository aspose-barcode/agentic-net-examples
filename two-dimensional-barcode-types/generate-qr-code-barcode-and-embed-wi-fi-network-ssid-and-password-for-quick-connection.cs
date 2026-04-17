using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace WifiQrCodeGenerator
{
    class Program
    {
        static void Main()
        {
            // Wi‑Fi connection details
            string ssid = "MyNetwork";
            string password = "MyPassword";
            string authType = "WPA"; // WPA, WEP or nopass

            // Wi‑Fi QR format: WIFI:T:<auth>;S:<ssid>;P:<password>;;
            string wifiCode = $"WIFI:T:{authType};S:{ssid};P:{password};;";

            // Create QR code generator with the Wi‑Fi data
            using (Aspose.BarCode.Generation.BarcodeGenerator generator = new Aspose.BarCode.Generation.BarcodeGenerator(Aspose.BarCode.Generation.EncodeTypes.QR, wifiCode))
            {
                // High error correction for better scanning reliability
                generator.Parameters.Barcode.QR.ErrorLevel = Aspose.BarCode.Generation.QRErrorLevel.LevelH;

                // Set image size (optional)
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 300f;

                // Save the QR code image
                generator.Save("wifi_qr.png");
            }

            Console.WriteLine("Wi‑Fi QR code generated: wifi_qr.png");
        }
    }
}