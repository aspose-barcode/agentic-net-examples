// Title: Stream Barcode Image Directly to HTTP Response
// Description: Demonstrates generating a Code128 barcode and streaming it as PNG data to an HTTP response without writing to the file system.
// Category-Description: Shows how to use Aspose.BarCode's BarcodeGenerator to create barcodes and output them as image streams. This example belongs to the barcode generation and image output category, where developers commonly need to embed generated barcodes in web responses, emails, or APIs. Key classes include BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and standard .NET streams.
// Prompt: Stream the generated barcode directly to an HTTP response without writing to disk.
// Tags: barcode, code128, generation, png, http, streaming, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode and streams the PNG image data to an HTTP response.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, writes HTTP headers, and outputs the image as a Base64 string.
    /// </summary>
    static void Main()
    {
        // The text to encode in the barcode.
        const string codeText = "12345678";

        // Initialize the barcode generator with the desired symbology and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Use a memory stream to hold the generated image in memory.
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode as a PNG image into the memory stream.
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning before reading.
                memoryStream.Position = 0;

                // Output HTTP response headers (simulated for console demonstration).
                Console.WriteLine("Content-Type: image/png");
                Console.WriteLine($"Content-Length: {memoryStream.Length}");
                Console.WriteLine("Image-Base64:");

                // Convert the image bytes to a Base64 string and write it to the response.
                Console.WriteLine(Convert.ToBase64String(memoryStream.ToArray()));
            }
        }
    }
}