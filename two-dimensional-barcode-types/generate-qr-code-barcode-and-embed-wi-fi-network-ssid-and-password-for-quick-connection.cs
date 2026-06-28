using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing; // Required for color definitions if needed

/// <summary>
/// Demonstrates generating a Wi‑Fi QR code using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code containing Wi‑Fi credentials and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define Wi‑Fi network details
        string ssid = "MyNetworkSSID";
        string password = "MySecretPassword";
        string authentication = "WPA"; // Options: WEP, WPA, nopass

        // Build the Wi‑Fi QR code payload string in the required format
        string wifiPayload = $"WIFI:T:{authentication};S:{ssid};P:{password};;";

        // Determine the output file path (saved in the executable's directory)
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "wifi_qr.png");

        // Initialize the QR code generator with the payload
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, wifiPayload))
        {
            // Set a high error correction level to improve readability under adverse conditions
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Increase the image resolution for a sharper PNG output
            generator.Parameters.Resolution = 300f;

            // Save the generated QR code image to the specified path
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image has been saved
        Console.WriteLine($"Wi‑Fi QR code generated and saved to: {outputPath}");
    }
}