using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, converting it to PNG,
/// and simulating an HTTP response containing the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, wraps it in an HTTP response, and prints details to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "123ABC"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Create a memory stream to hold the generated PNG image
            using (var ms = new MemoryStream())
            {
                // Save the barcode image directly into the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position to the beginning

                // Convert the stream contents to a byte array for further processing
                byte[] barcodeBytes = ms.ToArray();

                // Simulate an HTTP response that would return the barcode image
                using (var response = new HttpResponseMessage(HttpStatusCode.OK))
                {
                    // Set the response content to the barcode byte array
                    response.Content = new ByteArrayContent(barcodeBytes);
                    // Specify the MIME type as PNG image
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

                    // Output simulated response details to the console
                    Console.WriteLine($"HTTP Response Status: {response.StatusCode}");
                    Console.WriteLine($"Content-Type: {response.Content.Headers.ContentType}");
                    Console.WriteLine($"Barcode image size: {barcodeBytes.Length} bytes");

                    // Optionally display the image as a Base64-encoded string for debugging or logging
                    Console.WriteLine($"Base64 Image: {Convert.ToBase64String(barcodeBytes)}");
                }
            }
        }
    }
}