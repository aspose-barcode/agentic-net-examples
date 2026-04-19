using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;
using Aspose.Drawing;

namespace HanXinBarcodeService
{
    // Represents the JSON payload posted to the service
    public class BarcodeRequest
    {
        public string CodeText { get; set; }
    }

    class Program
    {
        static void Main()
        {
            // Simulated JSON payload
            string jsonPayload = "{\"CodeText\":\"1234567890ABCDEFGabcdefg,Han Xin Code\"}";

            // Deserialize the payload
            BarcodeRequest request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);
            if (request == null || string.IsNullOrWhiteSpace(request.CodeText))
            {
                throw new ArgumentException("Invalid request payload: CodeText is required.");
            }

            // Generate Han Xin barcode and obtain PNG stream
            using (var memoryStream = new MemoryStream())
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, request.CodeText))
                {
                    // Optional: set encoding mode if needed
                    // generator.Parameters.Barcode.HanXin.EncodeMode = EncodeMode.Auto;

                    // Save barcode image to the memory stream in PNG format
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                }

                // Reset stream position for further use
                memoryStream.Position = 0;

                // For demonstration, write the PNG to a file
                const string outputFile = "HanXinBarcode.png";
                using (var fileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.CopyTo(fileStream);
                }

                Console.WriteLine($"Barcode PNG stream generated successfully. Size: {memoryStream.Length} bytes. Saved to '{outputFile}'.");
            }
        }
    }
}