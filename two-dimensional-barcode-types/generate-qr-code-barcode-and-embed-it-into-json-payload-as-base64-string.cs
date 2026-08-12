// Title: Generate QR Code and embed as Base64 in JSON
// Description: Demonstrates creating a QR Code with Aspose.BarCode, converting it to PNG, encoding the image to a Base64 string, and embedding that string into a JSON payload.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to produce QR Code images, customize parameters such as error correction level, and serialize the resulting image for transport in JSON. Developers often need to embed barcodes in web APIs, mobile apps, or data interchange formats, making this pattern a common requirement for QR Code creation, image handling, and Base64 encoding.
// Prompt: Generate QR Code barcode and embed it into a JSON payload as base64 string.
// Tags: qr code, barcode generation, base64, json, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code, converts it to a Base64 string,
/// and wraps it inside a JSON payload.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR Code for a sample URL, encodes the image as Base64,
    /// and prints a JSON string containing the encoded barcode.
    /// </summary>
    static void Main()
    {
        // Sample data to encode in QR Code
        string data = "https://example.com";

        // Create QR Code generator with the desired symbology and data
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, data))
        {
            // Optional: set error correction level to improve readability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Generate QR Code image into a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert image bytes to a Base64 string for embedding
                string base64Image = Convert.ToBase64String(imageBytes);

                // Build JSON payload containing the Base64 QR Code
                string jsonPayload = $"{{\"qrCode\":\"{base64Image}\"}}";

                // Output the JSON payload to the console
                Console.WriteLine(jsonPayload);
            }
        }
    }
}