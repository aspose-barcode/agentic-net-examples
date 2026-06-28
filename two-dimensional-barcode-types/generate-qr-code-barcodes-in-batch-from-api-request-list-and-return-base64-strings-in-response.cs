using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of QR codes and returns their Base64 representations.
/// </summary>
class Program
{
    /// <summary>
    /// Simple request model representing an API payload for QR code generation.
    /// </summary>
    class QrRequest
    {
        public string CodeText { get; set; }
    }

    /// <summary>
    /// Entry point of the application. Generates QR codes for a set of requests and prints Base64 strings.
    /// </summary>
    static void Main()
    {
        // Prepare a sample batch of QR code generation requests.
        var requests = new List<QrRequest>
        {
            new QrRequest { CodeText = "https://example.com/1" },
            new QrRequest { CodeText = "Hello World!" },
            new QrRequest { CodeText = "1234567890" },
            new QrRequest { CodeText = "Aspose.BarCode QR Demo" },
            new QrRequest { CodeText = "Base64 Test" }
        };

        // List to hold the Base64-encoded QR code images.
        var base64Results = new List<string>();

        // Iterate over each request and generate the corresponding QR code.
        foreach (var request in requests)
        {
            // Create a barcode generator for QR type with the provided text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, request.CodeText))
            {
                // Set error correction level (optional, here using Level M).
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Convert the image bytes to a Base64 string.
                    byte[] imageBytes = ms.ToArray();
                    string base64 = Convert.ToBase64String(imageBytes);
                    // Store the result for later output.
                    base64Results.Add(base64);
                }
            }
        }

        // Output the Base64 strings (simulating an API response).
        foreach (var base64 in base64Results)
        {
            Console.WriteLine(base64);
        }
    }
}