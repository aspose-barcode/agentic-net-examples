using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode using Aspose.BarCode,
/// converting it to PNG, and simulating an HTTP response containing the image.
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, writes it to a memory stream, and creates an HttpResponseMessage.
    /// </summary>
    public static void Main()
    {
        // Define the GS1 DataMatrix payload (GTIN example)
        const string codeText = "(01)12345678901231";

        // Initialize the barcode generator with GS1 DataMatrix encoding and the payload
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the image resolution to 300 DPI (optional)
            generator.Parameters.Resolution = 300f;

            // Create a memory stream to hold the generated PNG image
            using (var ms = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
                // Retrieve the raw byte array of the PNG image
                byte[] barcodeBytes = ms.ToArray();

                // Simulate sending the image as an HTTP response
                using (var response = new HttpResponseMessage(HttpStatusCode.OK))
                {
                    // Attach the image bytes as the response content
                    response.Content = new ByteArrayContent(barcodeBytes);
                    // Set the appropriate MIME type for PNG images
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

                    // Output response details for demonstration purposes
                    Console.WriteLine($"HTTP Status: {response.StatusCode}");
                    Console.WriteLine($"Content Length: {barcodeBytes.Length} bytes");

                    // Show a snippet of the Base64 representation of the image
                    string base64 = Convert.ToBase64String(barcodeBytes);
                    Console.WriteLine($"Base64 (first 100 chars): {base64.Substring(0, Math.Min(100, base64.Length))}...");
                }
            }
        }
    }
}