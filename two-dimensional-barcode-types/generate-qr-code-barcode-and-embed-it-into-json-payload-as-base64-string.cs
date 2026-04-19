using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Data to encode in the QR code
        string data = "https://example.com";

        // Generate QR code image and convert it to a Base64 string
        string base64Image;
        using (Aspose.BarCode.Generation.BarcodeGenerator generator = new Aspose.BarCode.Generation.BarcodeGenerator(EncodeTypes.QR, data))
        {
            // Set high error correction level (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            using (MemoryStream ms = new MemoryStream())
            {
                // Save barcode image to the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                base64Image = Convert.ToBase64String(imageBytes);
            }
        }

        // Create a simple JSON payload containing the Base64 QR code
        var payload = new { qrCode = base64Image };
        string json = JsonSerializer.Serialize(payload);

        // Output the JSON payload
        Console.WriteLine(json);
    }
}