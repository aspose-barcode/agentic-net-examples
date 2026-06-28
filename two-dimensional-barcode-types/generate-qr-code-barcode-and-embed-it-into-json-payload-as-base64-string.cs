using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code, encoding it as Base64, and outputting a JSON payload.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code image, converts it to a Base64 string, wraps it in JSON, and writes to console.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR code
        string qrText = "https://example.com";

        // Generate QR code image and store it in a memory stream
        byte[] imageBytes;
        using (var memoryStream = new MemoryStream())
        {
            // Initialize the barcode generator for QR encoding with the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
            {
                // Set error correction level (optional, LevelM provides a good balance)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code as a PNG image into the memory stream
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Retrieve the image bytes from the memory stream
            imageBytes = memoryStream.ToArray();
        }

        // Convert the image bytes to a Base64 string for easy transport in JSON
        string base64Image = Convert.ToBase64String(imageBytes);

        // Create a simple JSON payload containing the Base64-encoded image
        var payload = new { barcodeImage = base64Image };
        string json = JsonSerializer.Serialize(payload);

        // Output the JSON payload to the console
        Console.WriteLine(json);
    }
}