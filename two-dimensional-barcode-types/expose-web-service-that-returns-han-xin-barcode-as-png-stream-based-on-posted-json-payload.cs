// Title: Han Xin Barcode Generation and PNG Output via Simulated Web Service
// Description: Demonstrates creating a Han Xin barcode from JSON input and returning the image as a Base64‑encoded PNG stream.
// Category-Description: Shows how to use Aspose.BarCode to generate 2‑D barcodes in a web‑service scenario. The example covers JSON deserialization, configuring Han Xin specific parameters, and exporting the barcode to a PNG memory stream. Developers working with barcode generation APIs such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat can reuse this pattern for REST endpoints that need to return barcode images.
// Prompt: Expose a web service that returns Han Xin barcode as PNG stream based on posted JSON payload.
// Tags: hanxin,barcode,generation,json,aspnet,aspose.barcode,png,base64,webservice

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

namespace HanXinBarcodeService
{
    /// <summary>
    /// Simulates a minimal web service that receives a JSON payload,
    /// generates a Han Xin barcode, and returns the PNG image as a Base64 string.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Represents the expected JSON request body containing the text to encode.
        /// </summary>
        class RequestPayload
        {
            public string CodeText { get; set; }
        }

        /// <summary>
        /// Entry point that processes the simulated request, creates the barcode,
        /// and writes the PNG image as Base64 to the console.
        /// </summary>
        static void Main()
        {
            // Simulate receiving a JSON payload from an HTTP POST request.
            string jsonPayload = "{\"CodeText\":\"Hello World\"}";

            // Deserialize the JSON into a strongly‑typed object.
            RequestPayload payload;
            try
            {
                payload = JsonSerializer.Deserialize<RequestPayload>(jsonPayload);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse JSON payload: {ex.Message}");
                return;
            }

            // Validate that the required CodeText property is present.
            if (payload == null || string.IsNullOrWhiteSpace(payload.CodeText))
            {
                Console.WriteLine("Invalid payload: CodeText is required.");
                return;
            }

            // Create a barcode generator for the Han Xin symbology using the supplied text.
            using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, payload.CodeText))
            {
                // Optional: configure encoding mode and error correction level.
                generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;
                generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

                // Render the barcode to a PNG image stored in a memory stream.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;

                    // Convert the PNG bytes to a Base64 string for easy transport.
                    byte[] pngBytes = ms.ToArray();
                    string base64 = Convert.ToBase64String(pngBytes);
                    Console.WriteLine("PNG Base64:");
                    Console.WriteLine(base64);
                }
            }
        }
    }
}