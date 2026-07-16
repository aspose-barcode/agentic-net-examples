// Title: Generate QR Code for Wi‑Fi Connection
// Description: Demonstrates creating a QR Code that encodes Wi‑Fi SSID and password, enabling quick network connection when scanned.
// Category-Description: This example belongs to the Aspose.BarCode QR Code generation category, illustrating how to use BarcodeGenerator with EncodeTypes.QR and configure QR error correction. Developers often need to embed configuration data such as Wi‑Fi credentials, URLs, or contact info into QR codes for mobile scanning scenarios.
/// Prompt: Generate QR Code barcode and embed Wi‑Fi network SSID and password for quick connection.
/// Tags: qr code, wifi, barcode generation, aspnet, aspose.barcode, png output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code that contains Wi‑Fi network credentials.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the QR Code image file "wifi_qr.png".
    /// </summary>
    static void Main()
    {
        // Define Wi‑Fi network details
        string ssid = "MyNetworkSSID";
        string password = "MySecretPassword";

        // Build the Wi‑Fi QR code payload using the standard format:
        // WIFI:T:WPA;S:<SSID>;P:<Password>;;
        string wifiCodeText = $"WIFI:T:WPA;S:{ssid};P:{password};;";

        // Initialize the QR code generator with the payload
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, wifiCodeText))
        {
            // Configure error correction level to Medium (Level M)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code as a PNG image
            generator.Save("wifi_qr.png");
        }
    }
}