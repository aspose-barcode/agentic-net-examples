// Title: Generate QR Code for Wi‑Fi Connection
// Description: Demonstrates creating a QR Code that encodes Wi‑Fi network SSID, authentication type, and password, then saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code barcodes. Typical use cases include embedding connection information for Wi‑Fi networks, URLs, or contact data. Developers often need to configure barcode parameters such as X‑dimension and error correction level before saving the image in a desired format.
// Prompt: Generate QR Code barcode and embed Wi‑Fi network SSID and password for quick connection.
// Tags: qr code, wifi, barcode generation, aspose.barcode, png, encode types, qrcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code that contains Wi‑Fi credentials and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the Wi‑Fi payload, configures the QR Code generator, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define Wi‑Fi network details
        string ssid = "MyNetwork";
        string password = "SecretPass";
        string authType = "WPA";

        // Build the Wi‑Fi payload string in the required format
        string wifiPayload = $"WIFI:S:{ssid};T:{authType};P:{password};;";

        // Determine output file path
        string outputPath = Path.Combine(Environment.CurrentDirectory, "wifi_qr.png");

        // Initialize the barcode generator with QR encoding and payload
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, wifiPayload))
        {
            // Set barcode visual parameters (pixel size of each module)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Set QR Code error correction level to high for better readability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform user of saved file location
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}