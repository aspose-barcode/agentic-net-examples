using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample barcode text and symbology
        string codeText = "1234567890";

        // Create the barcode generator
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Optional: set barcode bar color
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Stream the barcode directly to a memory stream (no intermediate file)
            using (MemoryStream memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // Reset stream position for reading

                // Simulate an HTTP response that streams the image
                using (HttpResponseMessage response = new HttpResponseMessage())
                {
                    response.Content = new StreamContent(memoryStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                    // For demonstration, read the streamed bytes and output information
                    byte[] imageBytes = response.Content.ReadAsByteArrayAsync().Result;
                    Console.WriteLine($"Generated barcode stream length: {imageBytes.Length} bytes");

                    // Optionally display the image as a Base64 string (useful for quick verification)
                    string base64 = Convert.ToBase64String(imageBytes);
                    Console.WriteLine($"Base64 PNG: {base64}");
                }
            }
        }
    }
}