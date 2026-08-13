// Title: Generate Wi‑Fi QR Code using Aspose.BarCode
// Description: Creates a QR code that encodes Wi‑Fi network SSID, password, and authentication type, then saves it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with QR symbology to embed custom data. Typical use cases include encoding contact information, URLs, or configuration strings (e.g., Wi‑Fi credentials) into scannable QR codes. Developers often need to set error correction levels, character encodings, and output formats when creating QR codes for mobile or web applications.
// Prompt: Generate QR Code barcode and embed Wi‑Fi network SSID and password for quick connection.
// Tags: qr code, wifi, barcode generation, aspose.barcode, png, encoding

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code that contains Wi‑Fi network credentials using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds the Wi‑Fi QR code string, configures the generator, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define sample Wi‑Fi credentials
        string ssid = "MyNetwork";
        string password = "SecretPass";
        string authType = "WPA"; // Options: WPA, WEP, nopass

        // Construct the Wi‑Fi QR code payload in the standard format
        // Format: WIFI:S:<SSID>;T:<AuthType>;P:<Password>;;
        string wifiCode = $"WIFI:S:{ssid};T:{authType};P:{password};;";

        // Initialize a QR code generator with the Wi‑Fi data
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, wifiCode))
        {
            // Configure a high error correction level for improved readability on imperfect scans
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set UTF‑8 encoding to correctly represent any non‑ASCII characters in the payload
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Define the output file path and save the QR code as a PNG image
            string outputPath = "wifi_qr.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the QR code image was saved
            Console.WriteLine($"Wi‑Fi QR code saved to {outputPath}");
        }
    }
}