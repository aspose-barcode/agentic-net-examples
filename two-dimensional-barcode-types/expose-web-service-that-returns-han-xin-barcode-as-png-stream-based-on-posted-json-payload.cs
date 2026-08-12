// Title: Generate Han Xin Barcode and Return PNG as Base64
// Description: Creates a Han Xin 2D barcode from JSON input and outputs the PNG image as a Base64 string, illustrating how to use Aspose.BarCode for barcode generation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It demonstrates the use of EncodeTypes.HanXin, BarcodeGenerator, and related parameter classes to produce a Han Xin barcode with configurable error correction. Typical use cases include generating QR‑like barcodes for inventory, tracking, or mobile scanning applications where developers need to return the image as a binary stream or Base64 payload.
// Prompt: Expose a web service that returns Han Xin barcode as PNG stream based on posted JSON payload.
// Tags: hanxin, barcode, generation, png, base64, aspose.barcode, json, webservice

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

namespace HanXinBarcodeService
{
    /// <summary>
    /// Model representing the JSON payload for barcode generation.
    /// </summary>
    public class BarcodeRequest
    {
        public string CodeText { get; set; }
        public string ErrorLevel { get; set; } // Expected values: L1, L2, L3, L4
    }

    /// <summary>
    /// Demonstrates generating a Han Xin barcode from a JSON request and outputting the PNG image as a Base64 string.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Simulates receiving a JSON payload, creates the barcode, and writes the PNG data as Base64.
        /// </summary>
        static void Main()
        {
            // Simulated incoming JSON payload (in a real web service this would come from the request body)
            string jsonPayload = @"{ ""CodeText"": ""Hello HanXin"", ""ErrorLevel"": ""L2"" }";

            // Deserialize the JSON payload into a strongly‑typed request object
            BarcodeRequest request;
            try
            {
                request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);
                if (request == null || string.IsNullOrWhiteSpace(request.CodeText))
                {
                    Console.WriteLine("Invalid request payload.");
                    return;
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON deserialization error: {ex.Message}");
                return;
            }

            // Resolve the error level string to the corresponding enum; default to L1 on failure
            HanXinErrorLevel errorLevel = HanXinErrorLevel.L1;
            if (!string.IsNullOrWhiteSpace(request.ErrorLevel))
            {
                if (!Enum.TryParse<HanXinErrorLevel>(request.ErrorLevel, true, out errorLevel))
                {
                    Console.WriteLine($"Unknown error level '{request.ErrorLevel}'. Using default L1.");
                    errorLevel = HanXinErrorLevel.L1;
                }
            }

            // Generate the Han Xin barcode and capture the PNG bytes in memory
            byte[] pngBytes;
            using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, request.CodeText))
            {
                // Apply the requested error correction level
                generator.Parameters.Barcode.HanXin.ErrorLevel = errorLevel;

                // Enable automatic version selection for a square barcode
                generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

                // Save the barcode directly to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    pngBytes = ms.ToArray();
                }
            }

            // Convert the PNG byte array to a Base64 string to simulate a binary HTTP response payload
            string base64Png = Convert.ToBase64String(pngBytes);
            Console.WriteLine("Generated Han Xin barcode (Base64 PNG):");
            Console.WriteLine(base64Png);
        }
    }
}