// Title: Generate MaxiCode Mode 3 Barcode and Return PNG via Web API Simulation
// Description: Demonstrates how to deserialize a JSON request, build a MaxiCode Mode 3 codetext, and produce a PNG barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and related classes to create MaxiCode symbols, a common requirement for shipping and logistics applications. Developers often need to accept JSON payloads, construct codetext, and return barcode images in web services.
// Prompt: Develop a Web API endpoint that accepts JSON, builds a MaxiCode Mode 3 codetext, and returns PNG data.
// Tags: maxicode, mode3, barcode generation, png, aspnet, aspose.barcode, json, web api

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

namespace MaxiCodeMode3Demo
{
    /// <summary>
    /// Represents the JSON payload that a client would POST to the API.
    /// </summary>
    public class MaxiCodeRequest
    {
        public string PostalCode { get; set; }          // 6‑character alphanumeric postal code
        public int CountryCode { get; set; }            // 3‑digit numeric country code
        public int ServiceCategory { get; set; }        // 3‑digit service category
        public string Message { get; set; }             // Standard second message text
    }

    /// <summary>
    /// Simulates a Web API endpoint that creates a MaxiCode Mode 3 barcode from JSON input and returns PNG data.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that mimics handling a single HTTP request.
        /// </summary>
        static void Main()
        {
            // -----------------------------------------------------------------
            // NOTE: The snippet runner is a plain .NET console application.
            // A real Web API host is not started; instead we simulate a single
            // HTTP request/response flow in‑process.
            // -----------------------------------------------------------------

            // Example JSON payload that a client would POST to the API
            string jsonPayload = @"
            {
                ""PostalCode"": ""B1050"",
                ""CountryCode"": 56,
                ""ServiceCategory"": 999,
                ""Message"": ""Test message""
            }";

            // Deserialize the JSON into a request object
            MaxiCodeRequest request = JsonSerializer.Deserialize<MaxiCodeRequest>(jsonPayload);

            // Build the MaxiCode Mode 3 codetext using the deserialized values
            var maxiCodeData = new MaxiCodeCodetextMode3
            {
                PostalCode = request.PostalCode,
                CountryCode = request.CountryCode,
                ServiceCategory = request.ServiceCategory
            };

            // Attach a standard second message (optional but commonly used)
            var secondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = request.Message
            };
            maxiCodeData.SecondMessage = secondMessage;

            // Generate the barcode image and obtain PNG bytes
            byte[] pngBytes;
            using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
            {
                // Enable validation of the constructed codetext; throws if invalid
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Generate the image (optional – Save will invoke it if needed)
                generator.GenerateBarCodeImage();

                // Save the generated image to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    pngBytes = ms.ToArray();
                }
            }

            // Output the PNG data as a Base64 string (simulating HTTP response body)
            string base64Png = Convert.ToBase64String(pngBytes);
            Console.WriteLine(base64Png);
        }
    }
}