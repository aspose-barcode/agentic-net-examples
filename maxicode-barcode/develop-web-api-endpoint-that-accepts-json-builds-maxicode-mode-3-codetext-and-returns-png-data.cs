// Title: Generate MaxiCode Mode 3 barcode and output PNG as Base64
// Description: Demonstrates building a MaxiCode Mode 3 codetext from JSON input and returning the barcode image in PNG format.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and related classes to encode postal and service data. Developers creating shipping, logistics, or tracking solutions often need to generate MaxiCode barcodes for UPS and other carriers, and this snippet illustrates the typical workflow of parsing input, constructing codetext, and producing a PNG image.
// Prompt: Develop a Web API endpoint that accepts JSON, builds a MaxiCode Mode 3 codetext, and returns PNG data.
// Tags: maxicode, barcode, generation, png, json, aspnet, aspose.barcode, complexbarcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

namespace MaxiCodeConsoleApp
{
    /// <summary>
    /// Simple DTO matching the expected JSON structure for MaxiCode input data.
    /// </summary>
    public class MaxiCodeInput
    {
        public string PostalCode { get; set; }
        public int CountryCode { get; set; }
        public int ServiceCategory { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// Console application that demonstrates generating a MaxiCode Mode 3 barcode from JSON input and outputting the PNG image as a Base64 string.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Parses JSON input, creates MaxiCode codetext, generates a PNG barcode, and writes the image bytes as Base64 to the console.
        /// </summary>
        /// <param name="args">Command‑line arguments; the first argument may contain a JSON payload.</param>
        static void Main(string[] args)
        {
            // NOTE:
            // The original request was for a Web API endpoint.
            // The snippet runner environment does not support hosting an HTTP server,
            // so this console application demonstrates the core logic:
            //   - Parse JSON input (from command‑line argument or default)
            //   - Build a MaxiCode Mode 3 codetext
            //   - Generate a PNG image
            //   - Output the PNG bytes as a Base64 string to the console

            // Use the first command‑line argument as JSON if provided; otherwise fall back to a default payload.
            string json = args.Length > 0
                ? args[0]
                : "{\"PostalCode\":\"B1050\",\"CountryCode\":56,\"ServiceCategory\":999,\"Message\":\"Test message\"}";

            MaxiCodeInput input;
            try
            {
                // Deserialize the JSON payload into the DTO, ignoring case differences in property names.
                input = JsonSerializer.Deserialize<MaxiCodeInput>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Validate required fields.
                if (input == null ||
                    string.IsNullOrWhiteSpace(input.PostalCode) ||
                    string.IsNullOrWhiteSpace(input.Message))
                {
                    throw new ArgumentException("Invalid JSON payload.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing input JSON: {ex.Message}");
                return;
            }

            // Build the MaxiCode Mode 3 codetext using the input data.
            var codetext = new MaxiCodeCodetextMode3
            {
                PostalCode = input.PostalCode,
                CountryCode = input.CountryCode,
                ServiceCategory = input.ServiceCategory
            };

            // Attach the secondary message (free‑form text) to the codetext.
            var secondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = input.Message
            };
            codetext.SecondMessage = secondMessage;

            // Generate the barcode and write PNG data to a memory stream.
            using (var generator = new ComplexBarcodeGenerator(codetext))
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] pngBytes = ms.ToArray();

                // Convert the PNG bytes to a Base64 string for easy console output or API response.
                string base64 = Convert.ToBase64String(pngBytes);
                Console.WriteLine(base64);
            }
        }
    }
}