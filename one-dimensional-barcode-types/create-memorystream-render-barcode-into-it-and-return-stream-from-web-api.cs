// Title: Generate Barcode Image as MemoryStream for Web API
// Description: Demonstrates creating a barcode, saving it to a MemoryStream, and preparing the stream for return from a web API.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator and BarCodeImageFormat to produce barcode images in memory. Typical use cases include returning barcode images from ASP.NET Web API endpoints or embedding them in responses without writing to disk. Developers often need to render barcodes to streams, reset positions, and convert them to formats like Base64 for client consumption.
// Prompt: Create a MemoryStream, render the barcode into it, and return the stream from a web API.
// Tags: barcode symbology, generation, memorystream, png, aspnet, aspose.barcode, webapi

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of generating a barcode image into a <see cref="MemoryStream"/>,
/// suitable for returning from a web API endpoint.
/// </summary>
class Program
{
    /// <summary>
    /// Simulates a web API call that generates a barcode and returns it as a <see cref="MemoryStream"/>.
    /// </summary>
    static void Main()
    {
        // Generate a barcode stream for the given text and symbology
        using (MemoryStream barcodeStream = GenerateBarcodeStream("12345678", EncodeTypes.Code128))
        {
            // Output stream length for demonstration purposes
            Console.WriteLine($"Generated barcode stream length: {barcodeStream.Length} bytes");

            // Example of converting the stream to Base64 (e.g., for JSON response)
            string base64 = Convert.ToBase64String(barcodeStream.ToArray());
            Console.WriteLine($"Base64 preview (first 100 chars): {base64.Substring(0, Math.Min(100, base64.Length))}");
        }
    }

    /// <summary>
    /// Creates a <see cref="MemoryStream"/> containing a barcode image rendered in PNG format.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <returns>A <see cref="MemoryStream"/> positioned at the beginning of the image data.</returns>
    static MemoryStream GenerateBarcodeStream(string codeText, BaseEncodeType encodeType)
    {
        // Initialize an empty memory stream to hold the image
        var stream = new MemoryStream();

        // Use Aspose.BarCode's generator to create the barcode
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Save the generated barcode as PNG directly into the memory stream
            generator.Save(stream, BarCodeImageFormat.Png);
        }

        // Reset the stream position so callers can read from the start
        stream.Position = 0;
        return stream;
    }
}