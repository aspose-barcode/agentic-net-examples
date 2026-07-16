// Title: Generate QR Code with Automatic Size Adjustment
// Description: Demonstrates creating a QR Code barcode where the symbol size automatically adapts to the payload length.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation. It showcases the use of BarcodeGenerator, EncodeTypes, and QR-specific parameters such as error correction level. Developers commonly use these APIs to generate dynamic QR codes for URLs, contact info, or other data where the symbol size must fit the content automatically.
// Prompt: Generate QR Code barcode and enable automatic size to adapt to payload length.
// Tags: qr code, barcode generation, automatic sizing, aspose.barcode, encode types, qrcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code image with automatic sizing based on the payload length.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a QR Code barcode, configures error correction,
    /// and saves the result as a PNG image.
    /// </summary>
    static void Main()
    {
        // Define the payload; the QR version (size) will be chosen automatically according to its length.
        string payload = "https://example.com/very/long/payload/that/needs/adjustment";

        // Initialize the QR code generator with the specified payload.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, payload))
        {
            // Set a moderate error correction level (Level M) to balance robustness and data capacity.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code image to a file named "qr.png".
            generator.Save("qr.png");
        }

        // Inform the user that the image has been saved.
        Console.WriteLine("QR code image saved as 'qr.png'.");
    }
}