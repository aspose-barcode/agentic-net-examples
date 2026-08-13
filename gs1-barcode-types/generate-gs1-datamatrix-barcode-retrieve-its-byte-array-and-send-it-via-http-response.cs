// Title: Generate GS1 DataMatrix barcode and return as HTTP response
// Description: Demonstrates how to create a GS1 DataMatrix barcode, extract its PNG byte array, and embed it in an HTTP response.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include creating machine-readable GS1 symbols for product identification and delivering them via web APIs. Developers often need to generate barcode images on the fly and send them directly to clients without persisting to disk.
// Prompt: Generate a GS1 DataMatrix barcode, retrieve its byte array, and send it via HTTP response.
// Tags: gs1, datamatrix, barcode, generation, http, response, bytearray, aspose.barcode

using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a GS1 DataMatrix barcode, converts it to a PNG byte array,
/// and packages it into an HTTP response message.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the GS1 DataMatrix payload (AI (01) with a 14‑digit GTIN)
        string gs1CodeText = "(01)00123456789012";

        // Generate the barcode and capture the PNG image as a byte array
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, gs1CodeText))
        {
            // Optional: increase image resolution for higher quality output
            generator.Parameters.Resolution = 300;

            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
                barcodeBytes = ms.ToArray(); // Extract the byte array from the stream
            }
        }

        // Build an HTTP response that carries the barcode image
        using (var response = new HttpResponseMessage())
        {
            // Attach the PNG byte array as the response content
            response.Content = new ByteArrayContent(barcodeBytes);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            response.StatusCode = System.Net.HttpStatusCode.OK;

            // Output response metadata to the console for demonstration purposes
            Console.WriteLine("HTTP Response prepared:");
            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Content-Type: {response.Content.Headers.ContentType}");
            Console.WriteLine($"Content Length: {barcodeBytes.Length} bytes");
        }

        // End of program
    }
}