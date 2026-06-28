using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

namespace MaxiCodeApiDemo
{
    /// <summary>
    /// Model representing the expected JSON payload for a MaxiCode request.
    /// </summary>
    public class MaxiCodeRequest
    {
        public string PostalCode { get; set; }          // 6 alphanumeric characters
        public int CountryCode { get; set; }            // 3‑digit numeric code
        public int ServiceCategory { get; set; }        // 3‑digit numeric code
        public string Message { get; set; }             // Standard second message
    }

    /// <summary>
    /// Demonstrates generating a MaxiCode (Mode 3) barcode from JSON input.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// Accepts a JSON string as a command‑line argument or uses a default sample.
        /// Generates a MaxiCode barcode and outputs the PNG image as a Base64 string.
        /// </summary>
        /// <param name="args">Command‑line arguments; first argument may contain JSON input.</param>
        static void Main(string[] args)
        {
            // Determine JSON input: use first argument if provided, otherwise fall back to a sample payload.
            string jsonInput = args.Length > 0 ? args[0] :
                @"{
                    ""PostalCode"": ""B1050"",
                    ""CountryCode"": 56,
                    ""ServiceCategory"": 999,
                    ""Message"": ""Test message""
                }";

            // Attempt to deserialize the JSON payload into a MaxiCodeRequest object.
            MaxiCodeRequest request;
            try
            {
                request = JsonSerializer.Deserialize<MaxiCodeRequest>(jsonInput, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                // Output an error message if JSON parsing fails and terminate the program.
                Console.WriteLine($"Invalid JSON input: {ex.Message}");
                return;
            }

            // Perform basic validation of required fields.
            if (request == null ||
                string.IsNullOrWhiteSpace(request.PostalCode) ||
                request.PostalCode.Length != 6 ||
                string.IsNullOrWhiteSpace(request.Message))
            {
                Console.WriteLine("Invalid request data. Ensure PostalCode is 6 characters and Message is provided.");
                return;
            }

            // Construct the MaxiCode codetext for Mode 3 using the request data.
            var maxiCodeCodetext = new MaxiCodeCodetextMode3
            {
                PostalCode = request.PostalCode,
                CountryCode = request.CountryCode,
                ServiceCategory = request.ServiceCategory
            };

            // Attach the standard second message to the codetext.
            var secondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = request.Message
            };
            maxiCodeCodetext.SecondMessage = secondMessage;

            // Generate the barcode image and write it to a memory stream.
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                using (var ms = new MemoryStream())
                {
                    // Save the generated barcode as a PNG image.
                    generator.Save(ms, BarCodeImageFormat.Png);
                    byte[] pngBytes = ms.ToArray();

                    // Convert the PNG byte array to a Base64 string (simulating an HTTP response body).
                    string base64 = Convert.ToBase64String(pngBytes);
                    Console.WriteLine(base64);
                }
            }
        }
    }
}