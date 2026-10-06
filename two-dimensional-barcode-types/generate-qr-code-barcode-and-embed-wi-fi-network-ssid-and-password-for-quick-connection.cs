// Title: Generate QR Code for Wi‑Fi network connection
// Description: Demonstrates how to create a QR Code that encodes Wi‑Fi SSID, password, and authentication type, enabling quick device connection.
// Category-Description: This example belongs to the Aspose.BarCode QR Code generation category. It shows how to use BarcodeGenerator with EncodeTypes.QR, configure QR error correction, and set encoding options. Developers often need to embed configuration data such as Wi‑Fi credentials, URLs, or contact info into QR codes for mobile scanning scenarios.
// Prompt: Generate QR Code barcode and embed Wi‑Fi network SSID and password for quick connection.
// Tags: qr code, wifi, barcode generation, aspose.barcode, png output

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program that generates a QR Code containing Wi‑Fi credentials using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the Wi‑Fi QR text, configures the generator, and saves the PNG image.
    /// </summary>
    static void Main()
    {
        // Define Wi‑Fi network details
        string ssid = "MyNetwork";
        string password = "SecretPass";
        string authentication = "WPA"; // Options: WPA, WEP, nopass

        // Construct QR payload according to the standard Wi‑Fi format
        // Format: WIFI:T:<auth>;S:<ssid>;P:<password>;;
        string qrText = $"WIFI:T:{authentication};S:{ssid};P:{password};;";

        // Create a unique temporary folder for the output file
        string outputFolder = Path.Combine(Path.GetTempPath(), "WifiQr_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "wifi_qr.png");

        // Initialize the barcode generator for QR Code
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, ""))
        {
            // Set the QR text with UTF‑8 encoding
            generator.SetCodeText(qrText, Encoding.UTF8);

            // Adjust module size (pixel dimension) for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Use high error correction level to improve scan reliability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR Code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Wi‑Fi QR code saved to: {outputPath}");
    }
}