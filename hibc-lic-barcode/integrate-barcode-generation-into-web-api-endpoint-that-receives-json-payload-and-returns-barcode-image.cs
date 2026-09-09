// Title: Generate Barcode from JSON Payload Demo
// Description: Demonstrates deserializing a JSON request containing barcode data, resolving the symbology, and generating a PNG barcode image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create barcodes programmatically. Typical use cases include generating barcodes for invoices, shipping labels, or embedding them in web API responses. Developers often need to convert user‑provided data into visual barcode formats for downstream processing or display.
// Prompt: Integrate barcode generation into a web API endpoint that receives JSON payload and returns the barcode image.
// Tags: barcode, symbology, generation, png, aspose.barcode, json, csharp

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation from a JSON payload using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that simulates receiving a JSON request, generates a barcode, and outputs the image as a Base64 string.
    /// </summary>
    static void Main()
    {
        // Simulated JSON payload representing a web API request
        string json = "{\"codeText\":\"1234567890\",\"symbology\":\"Code128\"}";
        Console.WriteLine("Input JSON: " + json);

        // Deserialize JSON into a strongly‑typed request object
        BarcodeRequest? request = JsonSerializer.Deserialize<BarcodeRequest>(json);
        if (request == null || string.IsNullOrEmpty(request.CodeText) || string.IsNullOrEmpty(request.Symbology))
        {
            Console.WriteLine("Invalid request payload.");
            return;
        }

        // Resolve symbology name to BaseEncodeType using reflection
        var field = typeof(EncodeTypes).GetField(request.Symbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {request.Symbology}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Generate barcode image with the specified symbology and text
        using (var generator = new BarcodeGenerator(encodeType, request.CodeText))
        {
            generator.Parameters.Resolution = 300f; // optional high resolution

            // Save the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the PNG bytes to a Base64 string for easy transport
                string base64 = Convert.ToBase64String(imageBytes);
                Console.WriteLine("Generated barcode image (Base64 PNG):");
                Console.WriteLine(base64);
            }
        }
    }

    /// <summary>
    /// Represents the expected JSON payload for barcode generation.
    /// </summary>
    class BarcodeRequest
    {
        public string CodeText { get; set; } = "";
        public string Symbology { get; set; } = "";
    }
}