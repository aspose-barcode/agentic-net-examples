// Title: Stream Barcode Image Directly to HTTP Response
// Description: Demonstrates generating a Code128 barcode, storing it in a memory stream, and streaming it as an HTTP response without writing to disk.
// Category-Description: This example belongs to the Aspose.BarCode generation and output category, showcasing how to use BarcodeGenerator with BarCodeImageFormat to create barcode images in memory and deliver them via HttpResponseMessage. Typical use cases include web APIs that need to return barcode images on-the-fly without persisting files. Developers often work with classes like BarcodeGenerator, BarCodeImageFormat, HttpResponseMessage, and StreamContent to achieve efficient, stateless barcode delivery.
// Prompt: Stream the generated barcode directly to an HTTP response without writing to disk.
// Tags: barcode symbology, generation, streaming, http response, memory stream, aspose.barcode, png

using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode, stores it in a memory stream,
/// and streams it directly to an HTTP response.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, writes it to a
    /// memory stream, and attaches the stream to an HttpResponseMessage.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the generated barcode image
        using (var barcodeStream = new MemoryStream())
        {
            // Initialize the barcode generator with Code128 symbology and the desired text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "HelloWorld"))
            {
                // Save the barcode image directly into the memory stream in PNG format
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning so it can be read from the start
            barcodeStream.Position = 0;

            // Simulate an HTTP response that will carry the barcode image
            using (var response = new HttpResponseMessage())
            {
                // Attach the memory stream as the response content
                response.Content = new StreamContent(barcodeStream);

                // Set the appropriate MIME type for a PNG image
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

                // Provide the content length header based on the stream size
                response.Content.Headers.ContentLength = barcodeStream.Length;

                // Output the content length to the console for demonstration purposes
                Console.WriteLine($"Barcode streamed to HTTP response. Content length: {response.Content.Headers.ContentLength} bytes");
            }
        }
    }
}