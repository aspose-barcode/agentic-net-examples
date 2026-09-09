// Title: Generate Han Xin Barcode and Return PNG Stream
// Description: Demonstrates creating a Han Xin barcode using Aspose.BarCode, encoding it as PNG, and outputting the image as a Base64 string (simulating a web service response).
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce a Han Xin symbology image. Typical scenarios include generating barcodes for inventory, tracking, or mobile scanning applications where developers need to customize encoding mode, error correction level, and version. The code illustrates how to deserialize request data, configure generator parameters, and return the barcode as a PNG byte stream.
// Prompt: Expose a web service that returns Han Xin barcode as PNG stream based on posted JSON payload.
// Tags: hanxin, barcode, generation, png, base64, aspose.barcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

namespace HanXinBarcodeService
{
    /// <summary>
    /// Simulates a web service that generates a Han Xin barcode PNG image from a JSON payload.
    /// </summary>
    class Program
    {
        // Sample JSON payload representing a request to the web service
        private const string SampleJson = @"{
            ""CodeText"": ""1234567890"",
            ""EncodeMode"": ""Auto"",
            ""ErrorLevel"": ""L2"",
            ""Version"": 24
        }";

        /// <summary>
        /// Entry point that processes the sample request, generates the barcode, and outputs the PNG as Base64.
        /// </summary>
        static void Main()
        {
            // NOTE: The original task describes a web service. The snippet runner cannot host an HTTP server,
            // so we simulate a single request/response flow in-process.

            // Deserialize the JSON payload into a strongly‑typed request object
            RequestPayload request = JsonSerializer.Deserialize<RequestPayload>(SampleJson);
            if (request == null || string.IsNullOrEmpty(request.CodeText))
            {
                Console.WriteLine("Invalid request payload.");
                return;
            }

            // Create a barcode generator for Han Xin symbology with the supplied code text
            using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, request.CodeText))
            {
                // Apply optional encoding mode if provided
                if (!string.IsNullOrEmpty(request.EncodeMode) &&
                    Enum.TryParse<HanXinEncodeMode>(request.EncodeMode, out var encodeMode))
                {
                    generator.Parameters.Barcode.HanXin.EncodeMode = encodeMode;
                }

                // Apply optional error correction level if provided
                if (!string.IsNullOrEmpty(request.ErrorLevel) &&
                    Enum.TryParse<HanXinErrorLevel>(request.ErrorLevel, out var errorLevel))
                {
                    generator.Parameters.Barcode.HanXin.ErrorLevel = errorLevel;
                }

                // Apply optional version if provided (maps integer to enum name, e.g., 24 -> Version24)
                if (request.Version.HasValue)
                {
                    string enumName = $"Version{request.Version.Value:D2}";
                    if (Enum.TryParse<HanXinVersion>(enumName, out var version))
                    {
                        generator.Parameters.Barcode.HanXin.Version = version;
                    }
                }

                // Render the barcode to a memory stream in PNG format
                using (var memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    byte[] pngBytes = memoryStream.ToArray();

                    // Write PNG to a file for local verification (optional)
                    File.WriteAllBytes("hanxin.png", pngBytes);

                    // Convert PNG bytes to Base64 string to simulate a stream response
                    string base64 = Convert.ToBase64String(pngBytes);
                    Console.WriteLine("Generated Han Xin barcode PNG (Base64):");
                    Console.WriteLine(base64);
                }
            }
        }

        // DTO representing the expected JSON request payload
        private class RequestPayload
        {
            public string CodeText { get; set; }
            public string EncodeMode { get; set; }
            public string ErrorLevel { get; set; }
            public int? Version { get; set; }
        }
    }
}