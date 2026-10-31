// Title: Generate GS1 DataMatrix barcode and return as PNG via HTTP response
// Description: Demonstrates creating a GS1 DataMatrix barcode using Aspose.BarCode, converting it to a PNG byte array, and embedding it in an HTTP response.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.GS1DataMatrix. Typical use cases include producing machine-readable GS1 DataMatrix images for product labeling and transmitting them over web APIs. Developers often need to customize dimensions, output formats, and integrate the resulting image bytes into HTTP responses or other streams.
// Prompt: Generate a GS1 DataMatrix barcode, retrieve its byte array, and send it via HTTP response.
// Tags: gs1 datamatrix, barcode generation, png, http response, aspose.barcode, byte array

using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode,
/// converts it to a PNG byte array, and simulates sending it in an HTTP response.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, obtains its PNG bytes,
    /// and writes simulated HTTP response details to the console.
    /// </summary>
    static void Main()
    {
        // Define the GS1 DataMatrix code text according to GS1 Application Identifiers.
        string gs1CodeText = "(01)12345678901231(21)ASPOSE(30)9876";

        // Generate the barcode and capture the PNG image as a byte array.
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, gs1CodeText))
        {
            // Set the X-dimension (module size) to 8 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                barcodeBytes = ms.ToArray(); // Extract the byte array from the stream.
            }
        }

        // Simulate constructing an HTTP response that carries the PNG image.
        using (var response = new HttpResponseMessage())
        {
            // Attach the barcode byte array as the response content.
            response.Content = new ByteArrayContent(barcodeBytes);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            response.StatusCode = System.Net.HttpStatusCode.OK;

            // Output simulated response details for verification.
            Console.WriteLine($"HTTP {((int)response.StatusCode)} {response.StatusCode}");
            Console.WriteLine($"Content-Type: {response.Content.Headers.ContentType}");
            Console.WriteLine($"Content Length: {barcodeBytes.Length} bytes");
        }
    }
}