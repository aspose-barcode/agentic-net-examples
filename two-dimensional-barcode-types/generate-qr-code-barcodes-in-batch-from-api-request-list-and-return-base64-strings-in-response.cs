// Title: Generate QR Code barcodes in batch and return Base64 strings
// Description: Demonstrates creating QR Code barcodes for multiple inputs using Aspose.BarCode, encoding them as PNG, and returning the images as Base64 strings. Useful for APIs that need to deliver barcode images without file I/O.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to use the BarcodeGenerator class with QR symbology, configure error correction, and export images to memory streams. Typical use cases include batch barcode creation for web services, mobile apps, or reporting where images are transmitted as Base64. Developers often need to generate barcodes on the fly, adjust parameters, and serialize the output for JSON responses.
// Prompt: Generate QR Code barcodes in batch from API request list and return base64 strings in response.
// Tags: qr code, batch generation, base64, aspose.barcode, aspose.barcode.generation, png, api

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch generation of QR Code barcodes and conversion to Base64 strings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcode requests, generates PNG images, and outputs Base64 strings.
    /// </summary>
    static void Main()
    {
        // Prepare a list of barcode generation requests.
        var requests = new List<BarcodeRequest>
        {
            new BarcodeRequest { Id = 1, Text = "Hello World" },
            new BarcodeRequest { Id = 2, Text = "https://example.com" },
            new BarcodeRequest { Id = 3, Text = "1234567890" }
        };

        // Container for successful barcode generation responses.
        var responses = new List<BarcodeResponse>();

        // Iterate over each request and generate a QR Code.
        foreach (var req in requests)
        {
            try
            {
                // Initialize the generator with QR symbology and the request text.
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, req.Text))
                {
                    // Set QR error correction level to Medium.
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                    // Save the generated barcode to a memory stream as PNG.
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);

                        // Convert the PNG bytes to a Base64 string.
                        string base64 = Convert.ToBase64String(ms.ToArray());

                        // Add the response entry with the original request Id.
                        responses.Add(new BarcodeResponse { Id = req.Id, Base64 = base64 });
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during generation.
                Console.WriteLine($"Failed to generate barcode for Id {req.Id}: {ex.Message}");
            }
        }

        // Output the generated Base64 strings to the console.
        foreach (var resp in responses)
        {
            Console.WriteLine($"Id: {resp.Id}");
            Console.WriteLine(resp.Base64);
        }
    }

    // Simple DTO representing a barcode generation request.
    class BarcodeRequest
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    // Simple DTO representing a barcode generation response.
    class BarcodeResponse
    {
        public int Id { get; set; }
        public string Base64 { get; set; }
    }
}