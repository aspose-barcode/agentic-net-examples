// Title: Generate MaxiCode Mode 3 barcode and return PNG data
// Description: Demonstrates building a MaxiCode Mode 3 codetext from JSON input and producing a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the ComplexBarcodeGenerator with MaxiCodeCodetextMode3. It illustrates typical use cases such as creating shipping labels or logistics tags where MaxiCode is required. Developers often need to construct codetext from structured data, customize visual parameters, and output common image formats like PNG.
// Prompt: Develop a Web API endpoint that accepts JSON, builds a MaxiCode Mode 3 codetext, and returns PNG data.
// Tags: maxicode, barcode generation, png, json, aspose.barcode, complexbarcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Contains the entry point demonstrating MaxiCode barcode generation from JSON data.
/// </summary>
class Program
{
    /// <summary>
    /// Represents the JSON payload required to build a MaxiCode Mode 3 codetext.
    /// </summary>
    public class MaxiCodeRequest
    {
        public string PostalCode { get; set; }
        public int CountryCode { get; set; }
        public int ServiceCategory { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// Entry point that simulates receiving a JSON payload, creates a MaxiCode Mode 3 codetext,
    /// generates a PNG barcode, and outputs the image as a Base64 string.
    /// </summary>
    static void Main()
    {
        // Simulated JSON payload that a Web API might receive in the request body
        string jsonPayload = @"{
            ""PostalCode"": ""B1050"",
            ""CountryCode"": 56,
            ""ServiceCategory"": 999,
            ""Message"": ""Second message""
        }";

        // Deserialize the JSON into a strongly‑typed request object
        MaxiCodeRequest request = JsonSerializer.Deserialize<MaxiCodeRequest>(jsonPayload);
        if (request == null)
        {
            Console.WriteLine("Invalid request payload.");
            return;
        }

        // Build the MaxiCode Mode 3 codetext using the deserialized values
        var codetext = new MaxiCodeCodetextMode3
        {
            PostalCode = request.PostalCode,
            CountryCode = request.CountryCode,
            ServiceCategory = request.ServiceCategory
        };

        // Attach the optional second message part
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = request.Message
        };
        codetext.SecondMessage = secondMessage;

        // Generate the barcode and capture the PNG image bytes
        byte[] pngBytes;
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            // Optional visual settings: increase module size and set bar color
            generator.Parameters.Barcode.XDimension.Pixels = 15f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                pngBytes = ms.ToArray();
            }
        }

        // Simulate an HTTP response by outputting the PNG as a Base64‑encoded string
        string base64Png = Convert.ToBase64String(pngBytes);
        Console.WriteLine(base64Png);
    }
}