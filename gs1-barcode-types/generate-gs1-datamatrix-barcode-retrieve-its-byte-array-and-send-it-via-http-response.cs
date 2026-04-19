using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeGs1DataMatrixDemo
{
    class Program
    {
        static void Main()
        {
            // Sample GS1 DataMatrix code text (AI 01 - GTIN)
            const string codeText = "(01)12345678901231";

            // Generate the barcode and obtain its PNG byte array
            byte[] barcodeBytes;
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
            {
                // Optional: adjust image resolution if needed
                generator.Parameters.Resolution = 300;

                using (var memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    barcodeBytes = memoryStream.ToArray();
                }
            }

            // Simulate an HTTP response containing the barcode image
            using (var response = new HttpResponseMessage())
            {
                response.Content = new ByteArrayContent(barcodeBytes);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                response.StatusCode = System.Net.HttpStatusCode.OK;

                // For demonstration, output response details to console
                Console.WriteLine("HTTP Response prepared:");
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Content-Type: {response.Content.Headers.ContentType}");
                Console.WriteLine($"Content Length: {barcodeBytes.Length} bytes");
            }
        }
    }
}