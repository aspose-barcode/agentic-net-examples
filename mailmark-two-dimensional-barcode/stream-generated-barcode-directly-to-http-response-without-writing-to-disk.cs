using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Simulated request payload
        string barcodeText = "1234567890";

        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            // Set image dimensions (optional)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save barcode image to a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0;

                // Simulate an HTTP response containing the image
                using (var response = new HttpResponseMessage())
                {
                    response.Content = new StreamContent(memoryStream);
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

                    // Output simulated response details
                    Console.WriteLine("Simulated HTTP Response:");
                    Console.WriteLine($"Content-Type: {response.Content.Headers.ContentType}");
                    Console.WriteLine($"Content Length: {memoryStream.Length} bytes");
                }
            }
        }
    }
}