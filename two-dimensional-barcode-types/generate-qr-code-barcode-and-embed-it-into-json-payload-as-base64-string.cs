// Title: Generate QR Code and embed as Base64 in JSON
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to PNG, encoding it to Base64, and placing it in a JSON payload.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use BarcodeGenerator, QR encoding options, and Aspose.Drawing to produce image output. Typical use cases include embedding barcodes in data interchange formats such as JSON or XML for mobile apps, web services, or IoT devices. Developers often need to convert barcode images to Base64 strings for transport or storage, and this snippet illustrates that workflow.
// Prompt: Generate QR Code barcode and embed it into a JSON payload as base64 string.
// Tags: qr code, barcode generation, base64, json, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code barcode, converts it to a PNG image,
/// encodes the image as a Base64 string, and embeds the string in a JSON payload.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Text to be encoded in the QR Code
        const string codeText = "Hello, Aspose!";

        // Initialize the QR Code generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: set the QR error correction level to Medium (LevelM)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Generate the barcode image as a Bitmap object
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Prepare a memory stream to hold the PNG-encoded image data
                using (var memoryStream = new MemoryStream())
                {
                    // Save the bitmap to the stream in PNG format
                    bitmap.Save(memoryStream, ImageFormat.Png);

                    // Retrieve the raw image bytes from the stream
                    byte[] imageBytes = memoryStream.ToArray();

                    // Convert the image bytes to a Base64-encoded string
                    string base64Image = Convert.ToBase64String(imageBytes);

                    // Build an anonymous object containing the Base64 string
                    var payload = new { barcodeImage = base64Image };

                    // Serialize the payload to a JSON string
                    string json = JsonSerializer.Serialize(payload);

                    // Write the JSON payload to the console
                    Console.WriteLine(json);
                }
            }
        }
    }
}