// Title: Barcode generation from JSON payload in a console demo
// Description: Demonstrates how to parse a JSON request, map the symbology to Aspose.BarCode EncodeTypes, generate a barcode image, and output it as Base64.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include creating barcodes from client‑provided data in web APIs or services. Developers often need to convert JSON input into barcode images for printing, labeling, or embedding in responses.
// Prompt: Integrate barcode generation into a web API endpoint that receives JSON payload and returns the barcode image.
// Tags: barcode generation, json, code128, png, base64, aspose.barcode, encode types

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation from a JSON payload.
/// </summary>
class Program
{
    // Simple model matching the expected JSON payload
    private class BarcodeRequest
    {
        public string symbology { get; set; }
        public string codeText { get; set; }
    }

    /// <summary>
    /// Entry point that parses a sample JSON request, generates a barcode, saves it, and prints a Base64 representation.
    /// </summary>
    static void Main()
    {
        // NOTE: The original task describes a web API endpoint.
        // The snippet runner cannot host an HTTP server, so we demonstrate the core logic
        // by using a hard‑coded JSON payload, generating the barcode, and saving it to a file.

        // Sample JSON payload
        string jsonPayload = "{\"symbology\":\"Code128\",\"codeText\":\"123ABC\"}";

        // Parse JSON into the request model
        BarcodeRequest request;
        try
        {
            request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);
            if (request == null ||
                string.IsNullOrWhiteSpace(request.symbology) ||
                string.IsNullOrWhiteSpace(request.codeText))
            {
                throw new ArgumentException("Invalid JSON payload.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse request: {ex.Message}");
            return;
        }

        // Resolve symbology name to EncodeTypes field via reflection
        var field = typeof(EncodeTypes).GetField(request.symbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {request.symbology}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Generate barcode and save as PNG
        string outputPath = "barcode.png";
        using (var generator = new BarcodeGenerator(encodeType, request.codeText))
        {
            // Example of setting a parameter (optional)
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save directly to file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Optionally, output the image as a Base64 string (simulating an API response)
        if (File.Exists(outputPath))
        {
            byte[] imageBytes = File.ReadAllBytes(outputPath);
            string base64 = Convert.ToBase64String(imageBytes);
            Console.WriteLine($"Barcode image (Base64): {base64}");
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
        }
    }
}