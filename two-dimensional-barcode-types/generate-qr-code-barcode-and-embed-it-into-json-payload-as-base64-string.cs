// Title: Generate QR Code and embed as Base64 in JSON
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to PNG, encoding to Base64, and placing it into a JSON payload.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to use BarcodeGenerator with QR symbology, configure parameters such as X‑Dimension and error correction level, render the barcode to an image stream, and embed the result in a data exchange format. Developers working with barcode creation for web APIs, mobile apps, or document automation often need to produce Base64‑encoded images for JSON or HTML consumption.
// Prompt: Generate QR Code barcode and embed it into a JSON payload as base64 string.
// Tags: qr code, barcode generation, base64, json, aspose.barcode, png, encode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode, converting it to a Base64 string,
/// and embedding it into a JSON payload.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code, encodes it, and outputs the JSON payload.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR Code
        string codeText = "Hello, Aspose QR!";

        // Initialize the barcode generator with QR symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set visual parameters: size of each module in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Configure QR error correction level (Level M provides a good balance)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Create a memory stream to hold the generated PNG image
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Retrieve the image bytes from the stream
                byte[] imageBytes = memoryStream.ToArray();

                // Convert the image bytes to a Base64-encoded string
                string base64 = Convert.ToBase64String(imageBytes);

                // Build a JSON payload that includes the Base64 QR code
                string jsonPayload = $"{{\"qrCode\":\"{base64}\"}}";

                // Output the JSON payload to the console
                Console.WriteLine(jsonPayload);
            }
        }
    }
}