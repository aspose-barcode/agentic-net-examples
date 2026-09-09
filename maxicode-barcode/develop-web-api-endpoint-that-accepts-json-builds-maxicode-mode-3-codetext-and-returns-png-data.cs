// Title: Generate MaxiCode Mode 3 barcode and return PNG data
// Description: This example creates a MaxiCode Mode 3 barcode from JSON input, encodes it as PNG, and outputs the image data. It demonstrates how to build codetext for shipping and logistics applications.
// Category-Description: Shows how to use Aspose.BarCode's ComplexBarcodeGenerator with MaxiCodeCodetextMode3 to produce high‑density 2‑D barcodes. Typical for logistics, parcel tracking, and postal services where MaxiCode is required. Developers often need to construct codetext objects, generate images, and return them via web APIs.
// Prompt: Develop a Web API endpoint that accepts JSON, builds a MaxiCode Mode 3 codetext, and returns PNG data.
// Tags: maxicode, barcode generation, png, aspose.barcodes, complexbarcode

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 3 barcode from JSON data and outputting PNG image data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that simulates receiving a JSON payload, builds the codetext, generates the barcode, and outputs PNG data.
    /// </summary>
    static void Main()
    {
        // Simulated JSON request payload (as would be received by a Web API)
        string jsonRequest = @"{
            ""PostalCode"": ""B1050"",
            ""CountryCode"": 56,
            ""ServiceCategory"": 999,
            ""Message"": ""Second message""
        }";

        // Deserialize JSON into a strongly‑typed request object
        MaxiCodeRequest request = JsonSerializer.Deserialize<MaxiCodeRequest>(jsonRequest);
        if (request == null)
        {
            Console.WriteLine("Invalid request payload.");
            return;
        }

        // Build the MaxiCode Mode 3 codetext using the request data
        var codetext = new MaxiCodeCodetextMode3
        {
            PostalCode = request.PostalCode,
            CountryCode = request.CountryCode,
            ServiceCategory = request.ServiceCategory
        };

        // Add the optional second message to the codetext
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = request.Message
        };
        codetext.SecondMessage = secondMessage;

        // Generate the barcode image and capture it as PNG bytes
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] pngData = ms.ToArray();

                // Save the PNG to a file for local verification
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode3.png");
                File.WriteAllBytes(outputPath, pngData);
                Console.WriteLine($"Barcode saved to: {outputPath}");

                // Convert PNG bytes to Base64 string to simulate an API response payload
                string base64 = Convert.ToBase64String(pngData);
                Console.WriteLine("Base64 PNG Data:");
                Console.WriteLine(base64);
            }
        }
    }

    // Simple DTO representing the expected JSON payload structure
    private class MaxiCodeRequest
    {
        public string PostalCode { get; set; }
        public int CountryCode { get; set; }
        public int ServiceCategory { get; set; }
        public string Message { get; set; }
    }
}