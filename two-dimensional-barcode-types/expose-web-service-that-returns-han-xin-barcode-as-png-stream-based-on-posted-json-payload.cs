using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

namespace HanXinBarcodeService
{
    /// <summary>
    /// Represents the JSON payload expected by the service.
    /// </summary>
    public class BarcodeRequest
    {
        public string Symbology { get; set; }   // e.g., "HanXin"
        public string CodeText { get; set; }   // data to encode
        public string ErrorLevel { get; set; } // optional: "L1", "L2", "L3", "L4"
    }

    /// <summary>
    /// Entry point for the Han Xin barcode generation console application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method that demonstrates barcode generation from a JSON payload.
        /// </summary>
        static void Main()
        {
            // Sample JSON payload (in a real service this would come from the HTTP request body).
            string jsonPayload = @"{
                ""Symbology"": ""HanXin"",
                ""CodeText"": ""Sample Han Xin Data"",
                ""ErrorLevel"": ""L2""
            }";

            // Deserialize the payload into a BarcodeRequest object.
            BarcodeRequest request;
            try
            {
                request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);
                // Validate required fields.
                if (request == null ||
                    string.IsNullOrWhiteSpace(request.Symbology) ||
                    string.IsNullOrWhiteSpace(request.CodeText))
                {
                    Console.WriteLine("Invalid request payload.");
                    return;
                }
            }
            catch (Exception ex)
            {
                // Handle JSON parsing errors.
                Console.WriteLine($"Failed to parse JSON payload: {ex.Message}");
                return;
            }

            // Resolve the symbology name to a BaseEncodeType using reflection.
            var field = typeof(EncodeTypes).GetField(request.Symbology);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology: {request.Symbology}");
                return;
            }

            // Cast the reflected field value to BaseEncodeType.
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Generate the Han Xin barcode using the resolved encode type and provided text.
            using (var generator = new BarcodeGenerator(encodeType, request.CodeText))
            {
                // Configure error correction level if it was supplied in the request.
                if (!string.IsNullOrWhiteSpace(request.ErrorLevel))
                {
                    // Attempt to parse the string into the HanXinErrorLevel enum.
                    if (Enum.TryParse<HanXinErrorLevel>(request.ErrorLevel, out var level))
                    {
                        generator.Parameters.Barcode.HanXin.ErrorLevel = level;
                    }
                    else
                    {
                        // If parsing fails, inform the user and continue with the default level.
                        Console.WriteLine($"Invalid ErrorLevel value: {request.ErrorLevel}. Using default.");
                    }
                }

                // Save the generated barcode to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    byte[] pngBytes = ms.ToArray();

                    // Convert the PNG byte array to a Base64 string to simulate an HTTP response body.
                    string base64 = Convert.ToBase64String(pngBytes);
                    Console.WriteLine(base64);
                }
            }
        }
    }
}