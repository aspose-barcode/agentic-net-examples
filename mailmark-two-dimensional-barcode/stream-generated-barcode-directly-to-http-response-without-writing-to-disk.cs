// Title: Stream Barcode Directly to HTTP Response Using MemoryStream
// Description: Demonstrates generating a barcode image in memory and preparing it for direct HTTP response streaming without writing to disk.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator and BarCodeImageFormat to create barcode images on the fly. Developers often need to embed barcodes in web pages or APIs, requiring in‑memory image handling and response streaming. The snippet shows typical usage of the generator, memory streams, and Base64 encoding for web delivery.
// Prompt: Stream the generated barcode directly to an HTTP response without writing to disk.
// Tags: barcode generation, code128, png, memorystream, base64, aspnet, aspnetcore, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode, encodes it as Base64,
/// and writes the result to the console (simulating an HTTP response body).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode in memory and outputs a Base64 data URI.
    /// </summary>
    static void Main()
    {
        // NOTE: The original request is to stream the barcode directly to an HTTP response.
        // The snippet runner is a plain console application and cannot host an HTTP server.
        // Therefore, we generate the barcode into a memory stream and output the image
        // as a Base64 string to the console, which can be used as the response body in a real web scenario.

        // Create a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Optional: customize barcode appearance here if needed.
            // e.g., generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Generate the barcode image into a memory stream.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the image bytes to a Base64 string.
                string base64Image = Convert.ToBase64String(imageBytes);

                // Output the Base64 string to the console.
                Console.WriteLine("data:image/png;base64," + base64Image);
            }
        }
    }
}