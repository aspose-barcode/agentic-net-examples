using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeApiSimulation
{
    class Program
    {
        // Model representing the expected JSON payload
        private class BarcodeRequest
        {
            public string Symbology { get; set; }
            public string CodeText { get; set; }
        }

        static void Main()
        {
            // Simulated JSON request payload
            string jsonPayload = @"{ ""symbology"": ""Code128"", ""codeText"": ""123ABC"" }";

            // Deserialize the payload
            BarcodeRequest request;
            try
            {
                request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);
                if (request == null || string.IsNullOrWhiteSpace(request.Symbology) || string.IsNullOrWhiteSpace(request.CodeText))
                {
                    throw new ArgumentException("Invalid request payload.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse request: {ex.Message}");
                return;
            }

            // Resolve the symbology string to an EncodeTypes member using reflection
            BaseEncodeType encodeType;
            try
            {
                var fieldInfo = typeof(EncodeTypes).GetField(request.Symbology);
                if (fieldInfo == null)
                {
                    throw new ArgumentException($"Symbology '{request.Symbology}' is not supported.");
                }
                encodeType = (BaseEncodeType)fieldInfo.GetValue(null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to resolve symbology: {ex.Message}");
                return;
            }

            // Generate the barcode and obtain PNG bytes
            byte[] pngBytes;
            try
            {
                using (var generator = new BarcodeGenerator(encodeType, request.CodeText))
                {
                    // Example: set image dimensions (optional)
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;

                    // Save to a memory stream in PNG format
                    using (var memoryStream = new MemoryStream())
                    {
                        generator.Save(memoryStream, BarCodeImageFormat.Png);
                        pngBytes = memoryStream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Barcode generation failed: {ex.Message}");
                return;
            }

            // Simulate API response: Base64-encoded PNG image
            string base64Image = Convert.ToBase64String(pngBytes);
            Console.WriteLine("Base64 PNG Image:");
            Console.WriteLine(base64Image);

            // Additionally, write the image to a file for verification
            try
            {
                File.WriteAllBytes("output.png", pngBytes);
                Console.WriteLine("Barcode image saved to 'output.png'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write image file: {ex.Message}");
            }
        }
    }
}