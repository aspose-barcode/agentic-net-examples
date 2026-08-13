// Title: Streaming a generated barcode directly to an HTTP response
// Description: Demonstrates generating a Code128 barcode and writing the PNG image to a memory stream that can be sent as an HTTP response without creating a temporary file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to create barcodes and stream them directly to a response. Typical use cases include web APIs or MVC controllers that need to return barcode images on‑the‑fly. Developers often need to avoid filesystem I/O for performance and security, and this pattern shows the recommended approach using MemoryStream and the Save method with BarCodeImageFormat.
// Prompt: Provide sample code that streams generated barcode directly to HTTP response without intermediate file.
// Tags: barcode symbology, generation, streaming, http response, png, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a barcode and stream it directly to an HTTP response.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, writes it to a memory stream, and shows how the stream could be used in an HTTP response.
    /// </summary>
    static void Main()
    {
        // Create a BarcodeGenerator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: adjust the X dimension (module width) of the barcode.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Use a MemoryStream to simulate the HTTP response body.
            using (var responseStream = new MemoryStream())
            {
                // Save the barcode image directly to the stream in PNG format.
                generator.Save(responseStream, BarCodeImageFormat.Png);

                // Reset the stream position before reading.
                responseStream.Position = 0;

                // In a real HTTP scenario you would set the response Content-Type to "image/png"
                // and write the stream bytes to the response output.
                byte[] imageBytes = responseStream.ToArray();

                // For demonstration, convert the image to Base64 and output to console.
                string base64 = Convert.ToBase64String(imageBytes);
                Console.WriteLine("Generated barcode PNG size (bytes): " + imageBytes.Length);
                Console.WriteLine("Base64 PNG data:");
                Console.WriteLine(base64);
            }
        }
    }
}