// Title: Generate Code128 barcode and stream as Base64 via HTTP response
// Description: Creates a Code128 barcode, saves it to a memory stream, and writes HTTP headers and Base64 image data to the console, simulating an HTTP response.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use BarcodeGenerator to produce barcodes, save them directly to a MemoryStream, and stream the result without touching the file system. Typical use cases include web applications that need to deliver barcode images on-the-fly via HTTP responses. Developers often work with EncodeTypes, BarCodeImageFormat, and stream APIs to achieve efficient, disk‑less output.
// Prompt: Stream the generated barcode directly to an HTTP response without writing to disk.
// Tags: barcode symbology, generation, streaming, http response, memory stream, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and streaming it as a Base64‑encoded PNG image
/// directly to an HTTP‑like response (simulated via console output).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, writes HTTP headers,
    /// and outputs the Base64 image data.
    /// </summary>
    static void Main()
    {
        // Simulate an HTTP response by writing headers and Base64 image data to the console.
        using (MemoryStream ms = new MemoryStream())
        {
            // Initialize the barcode generator with Code128 symbology and the desired text.
            BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678");

            // Save the generated barcode directly into the memory stream in PNG format.
            generator.Save(ms, BarCodeImageFormat.Png);

            // Retrieve the raw image bytes from the memory stream.
            byte[] imageBytes = ms.ToArray();

            // Convert the image bytes to a Base64 string for easy transmission in text form.
            string base64 = Convert.ToBase64String(imageBytes);

            // Output HTTP-like headers indicating content type and length.
            Console.WriteLine("Content-Type: image/png");
            Console.WriteLine("Content-Length: " + imageBytes.Length);
            Console.WriteLine();

            // Output the Base64‑encoded image data.
            Console.WriteLine(base64);
        }
    }
}