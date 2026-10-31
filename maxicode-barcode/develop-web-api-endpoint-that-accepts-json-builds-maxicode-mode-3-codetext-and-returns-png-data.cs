// Title: Generate MaxiCode Mode 3 Barcode and Return PNG as Base64
// Description: This example demonstrates how to deserialize a JSON request, build a MaxiCode Mode 3 codetext, generate a barcode image in PNG format, and output it as a Base64 string.
// Category-Description: Aspose.BarCode examples for complex barcode generation, focusing on MaxiCode Mode 3. The sample uses ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and related classes to create shipping‑label barcodes. Developers commonly need to convert JSON payloads into barcode images for logistics, tracking, and packaging applications.
// Prompt: Develop a Web API endpoint that accepts JSON, builds a MaxiCode Mode 3 codetext, and returns PNG data.
// Tags: maxicode, barcode generation, png, json, aspose.barcode, complexbarcode, web api, codetext

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a MaxiCode Mode 3 barcode from JSON input and returning the image as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Model representing the expected JSON request payload.
    /// </summary>
    class RequestModel
    {
        public string PostalCode { get; set; }
        public int CountryCode { get; set; }
        public int ServiceCategory { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// Entry point of the console application. Simulates a Web API endpoint by processing a JSON payload,
    /// generating a MaxiCode Mode 3 barcode, and outputting the PNG image as a Base64 string.
    /// </summary>
    static void Main()
    {
        // Simulated JSON request payload (in a real API this would come from the HTTP request body)
        string jsonPayload = @"{
            ""PostalCode"": ""B1050"",
            ""CountryCode"": 56,
            ""ServiceCategory"": 999,
            ""Message"": ""Second message""
        }";

        // Deserialize JSON into a strongly‑typed request object
        RequestModel request = JsonSerializer.Deserialize<RequestModel>(jsonPayload);
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

        // Attach the optional second message to the codetext
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = request.Message
        };
        codetext.SecondMessage = secondMessage;

        // Generate the barcode image and store it in a memory stream
        using (var memoryStream = new MemoryStream())
        {
            using (var generator = new ComplexBarcodeGenerator(codetext))
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Convert the PNG byte array to a Base64 string to simulate an API response payload
            string base64Png = Convert.ToBase64String(memoryStream.ToArray());
            Console.WriteLine(base64Png);
        }
    }
}