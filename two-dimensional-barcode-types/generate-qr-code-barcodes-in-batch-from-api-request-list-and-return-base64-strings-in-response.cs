using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Simulated API request payload: list of texts to encode as QR codes
        List<string> requestPayload = new List<string>
        {
            "https://example.com/1",
            "https://example.com/2",
            "https://example.com/3",
            "https://example.com/4",
            "https://example.com/5"
        };

        // Process each request and collect Base64 strings
        List<string> base64Responses = new List<string>();

        foreach (string text in requestPayload)
        {
            // Create QR code generator
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the data to encode
                generator.CodeText = text;

                // Set high error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save barcode to a memory stream in PNG format
                using (var memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    byte[] imageBytes = memoryStream.ToArray();
                    string base64 = Convert.ToBase64String(imageBytes);
                    base64Responses.Add(base64);
                }
            }
        }

        // Output the Base64 strings (simulating API response)
        Console.WriteLine("Generated QR Code Base64 strings:");
        foreach (string base64 in base64Responses)
        {
            Console.WriteLine(base64);
        }
    }
}